using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTR.DATAConfig;
using System.IO;

namespace DTRBlog.Models.Articley.ArticleBLL
{
   
    public class ArticleFile
    {
        public ArticleFile(string compoentPaht)
        {
            this.ArticleDicretionPath = compoentPaht;
            this.ArticleFilePath = compoentPaht + DATAPathConfigs.WorksFile;
            this.ArticleDicretionFullPath = DATAPathConfigs.DTRDataConfigFullPath + this.ArticleDicretionPath;
            this.ArticleFileFullPath = DATAPathConfigs.DTRDataConfigFullPath + this.ArticleFilePath;
            this.ArticleFileExists = File.Exists(this.ArticleFileFullPath);
            this.AritcleDicretionExists = Directory.Exists(this.ArticleDicretionFullPath);
        }

        public bool ArticleFileExists { get; private set; }//文章文件是否存在
        public bool AritcleDicretionExists { get; private set; }//文章文件夹是否存在
        public string ArticleFilePath { get; private set; }//相对文章路径
        public string ArticleDicretionPath { get; private set; }//相对文件夹路径
        public string ArticleFileFullPath { get; private set; }//文章文件全路径
        public string ArticleDicretionFullPath { get; private set; }//文章文件夹全路径

    
        public void Delete()
        {
            if (this.AritcleDicretionExists)
                Directory.Delete(this.ArticleDicretionFullPath, true);
        }

     
        public string GetContent()
        {
            if (this.ArticleFileExists)
                return File.ReadAllText(this.ArticleFileFullPath,Encoding.UTF8);
            return "";
        }

        public void UpdateContent(string Content)
        {
            StreamWriter writer = File.CreateText(this.ArticleFileFullPath);
            writer.Write(Content);
            writer.Dispose();
        }
    }
}