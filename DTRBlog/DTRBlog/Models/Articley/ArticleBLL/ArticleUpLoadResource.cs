using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DTR.DATAConfig;

namespace DTRBlog.Models.Articley.ArticleBLL
{
    //表示用于保存文章的文件相关的数据
    public class ArticleUpLoadResource
    {
        public ArticleUpLoadResource(string compentPaht)
        {
            this.ArticleFile = new ArticleFile(compentPaht);

            if (!this.ArticleFile.AritcleDicretionExists)//是否存在
                return;

            this.ArticleImgTitlePaht = this.ArticleFile.ArticleDicretionPath + DATAPathConfigs.WorksFace;
            this.ArticleImgTitleFullPaht = this.ArticleFile.ArticleDicretionFullPath + DATAPathConfigs.WorksFace;
            this.ArticleResourcePath = this.ArticleFile.ArticleDicretionPath + DATAPathConfigs.WorksResource;
            this.ArticleResourceFullPath = this.ArticleFile.ArticleDicretionFullPath + DATAPathConfigs.WorksResource;
        }
        public ArticleFile ArticleFile { get; private set; }

        public bool AritcleDicretionExists { get { return this.ArticleFile.AritcleDicretionExists; } }//文章文件夹是否存在
        public string ArticleImgTitlePaht { get; private set; }//封面图片的相对路径
        public string ArticleImgTitleFullPaht { get; private set; }//封面图片的完全限定路径
        public string ArticleResourcePath { get; private set; }//文章的资源文件夹的相对路径
        public string ArticleResourceFullPath { get; private set; }//文章的资源文件夹的完全限定路径

        //返回资源文章的完全限定路径
        private string GetResourceFile(string FileName)
        {
            if (!this.ArticleFile.AritcleDicretionExists)
                return "";
            if (string.IsNullOrEmpty(FileName))
                return "";
            return this.ArticleResourceFullPath + FileName;
            
        }

        //上传封面图片
        public bool UpLoadWorksFace(HttpPostedFileBase img)
        {
            if(img !=null && img.ContentLength > 0)
            {
                img.SaveAs(this.ArticleImgTitleFullPaht);
                return true;
            }
            return false;
        }
        //返回所有的资源文件
        public IEnumerable<string> GetAllResource()
        {
            if (!this.ArticleFile.AritcleDicretionExists)//是否存在
                return Enumerable.Empty<string>();
            return Directory.EnumerateFiles(this.ArticleResourceFullPath).Select(f => f.Substring(this.ArticleResourceFullPath.Length));
        }
        //保存文件列表
        public bool SaveFiles(IEnumerable<HttpPostedFileBase> files)
        {
            if (files == null)
                return false;
            if (!this.ArticleFile.AritcleDicretionExists)//是否存在对应的文件夹
                return false;

            if (!Directory.Exists(this.ArticleResourceFullPath))//如果没有资源文件夹则创建
                Directory.CreateDirectory(this.ArticleResourceFullPath);

            foreach (HttpPostedFileBase file in files)
            {
                if (file == null || file.ContentLength <= 0)
                    continue;
                if (string.IsNullOrEmpty(file.FileName))
                    continue;
                //if (File.Exists(SaveFilePath))
                //    continue;

                //有的浏览器的名称只有文件名，有的却包含路径
                string fileName = null;
                int index = file.FileName.LastIndexOf('\\');
                if (index >= 0)
                    fileName = file.FileName.Substring(index + 1);

                string SaveFilePath = this.ArticleResourceFullPath + (fileName ?? file.FileName);
                try {
                    file.SaveAs(SaveFilePath);
                }
                catch 
                { return false; }
            }

            return true;
        }
        //移除文章中对应的资源
        public bool RemoveFiles(string fileName)
        {
            if (!this.ArticleFile.AritcleDicretionExists)
                return false;
            if (!Directory.Exists(this.ArticleResourceFullPath))
                return false;

            string fullPaht = this.GetResourceFile(fileName);
            if (File.Exists(fullPaht))
                File.Delete(fullPaht);
            return true;
        }

    }
}