using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DTRBlog.Models
{
    public class duanxinyz
    {
        static string url = "https://api.netease.im/sms/sendcode.action";
        static string appKey = "143edd7041905dc54ef407e45bcebeae";
        static string appSecret = "20a41b02fed4";

        public static string Send(string mobile)
        {
            string nonce = new Random().Next(100000, 999999).ToString();
            string curTime = DateTime.Now.ToString("yyyyMMddhhmmss");
            string checkSum = SHA1_Hash(appSecret + nonce + curTime);

            string post = string.Format("mobile={0}", mobile);
            byte[] btBodys = Encoding.UTF8.GetBytes(post);

            System.Net.WebRequest wReq = System.Net.WebRequest.Create(url);
            wReq.Method = "POST";
            wReq.Headers.Add("AppKey", appKey);
            wReq.Headers.Add("Nonce", nonce);
            wReq.Headers.Add("CurTime", curTime);
            wReq.Headers.Add("CheckSum", checkSum);
            wReq.ContentLength = btBodys.Length;
            wReq.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            using (var wsr = wReq.GetRequestStream())
            {
                wsr.Write(btBodys, 0, btBodys.Length);
            }

            System.Net.WebResponse wResp = wReq.GetResponse();
            System.IO.Stream respStream = wResp.GetResponseStream();

            string result;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, System.Text.Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            //Json数据，obj是网易生成的验证码
            return result;
        }

        private static string SHA1_Hash(string str_sha1_in)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            str_sha1_out = str_sha1_out.Replace("-", "").ToLower();
            return str_sha1_out;
        }

        private static string getFormattedText(byte[] bytes)
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