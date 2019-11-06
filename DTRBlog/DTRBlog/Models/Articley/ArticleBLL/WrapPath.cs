using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTR.DATAConfig;

namespace DTRBlog.Models.Articley.ArticleBLL
{
    //将资源包装成http路径
    public class WrapPath
    {
        public static string RootUrl()
        {
            return DATAPathConfigs.DTRDataConfigNetWorkelPath;
        }

        //根据参数，返回其对应的级别的URL
        public static string GetUrlByAuto(string path)
        {
            if (string.IsNullOrEmpty(path))
                return RootUrl();
            return DATAPathConfigs.DTRDataConfigNetWorkelPath + path;
        }
        //根据文章文件夹的相对路径，包装成其资源文件夹级别的Url
        public static string WorksResourceUrl(string WorksPath)
        {
            if (string.IsNullOrEmpty(WorksPath))
                return DATAPathConfigs.DTRDataConfigNetWorkelPath;
            return DATAPathConfigs.DTRDataConfigNetWorkelPath + WorksPath.Replace("\\", "/") + DATAPathConfigs.WorksResource.Replace("\\","/");
        }
        //根据文章文件夹的相对路径，包装成文章封面的Ulr
        public static string WorksFaceUrl(string WorksPath)
        {
            if (string.IsNullOrEmpty(WorksPath))
                return DATAPathConfigs.DTRDataConfigNetWorkelPath;
            return DATAPathConfigs.DTRDataConfigNetWorkelPath + WorksPath.Replace("\\","/") + DATAPathConfigs.WorksFace;
        }
        
    }
}