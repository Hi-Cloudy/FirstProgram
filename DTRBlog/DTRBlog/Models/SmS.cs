using DTRBlog.App_Start;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DTRBlog.Models
{
    public class SmS
    {
        static String url = "https://api.netease.im/sms/sendcode.action";
        static String appKey = "ea388f936c2facdf6125c54a4e0f5a7f";
        static String appSecret = "34e63e38f2de";

        public string Send(string mobile)
        {
            url += "?mobile=" + mobile;
            //网易云信分配的账号，请替换你在管理后台应用下申请的Appkey
            //String appKey = appKey;
            ////网易云信分配的密钥，请替换你在管理后台应用下申请的appSecret
            //String appSecret = appSecret;
            //随机数（最大长度128个字符）
            String nonce = "12345";
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);
            Int32 ticks = System.Convert.ToInt32(ts.TotalSeconds);
            //当前UTC时间戳，从1970年1月1日0点0 分0 秒开始到现在的秒数(String)
            String curTime = ticks.ToString();
            //SHA1(AppSecret + Nonce + CurTime),三个参数拼接的字符串，进行SHA1哈希计算，转化成16进制字符(String，小写)
            String checkSum = CheckSumBuilder.getCheckSum(appSecret, nonce, curTime);

            IDictionary<object, String> headers = new Dictionary<object, String>();
            headers["AppKey"] = appKey;
            headers["Nonce"] = nonce;
            headers["CurTime"] = curTime;
            headers["CheckSum"] = checkSum;
            headers["ContentType"] = "application/x-www-form-urlencoded;charset=utf-8";
            //执行Http请求
            var result = HttpClient.HttpPost(url, null, headers);
            if (string.IsNullOrEmpty(result)) return "";
            SmSCode lo = JsonConvert.DeserializeObject<SmSCode>(result);
            string SmSNum = lo.obj;
            //Json数据，obj是网易生成的验证码
            return SmSNum;
        }

        private string SHA1_Hash(string str_sha1_in)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            str_sha1_out = str_sha1_out.Replace("-", "").ToLower();
            return str_sha1_out;
        }

        private string getFormattedText(byte[] bytes)
        {
            int len = bytes.Length;
            StringBuilder buf = new StringBuilder(len * 2);
            for (int j = 0; j < len; j++)
            {
                buf.Append(HEX_DIGITS[(bytes[j] >> 4) & 0x0f]);
                buf.Append(HEX_DIGITS[bytes[j] & 0x0f]);
            }
            return buf.ToString();
        }

        private static char[] HEX_DIGITS = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
    }
}