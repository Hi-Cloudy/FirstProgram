using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTRBlog.Models.Articley.ArticleBLL;
using DTR.DATAConfig;
using System.IO;

namespace DTRBlog.Models.Articley.ArticleBLL
{
    public class ArticleCreate
    {
    
        public ArticleCreate(string CompentUserPath)
        {
            this.CompentUserPath = CompentUserPath;//
            this.FullUserPath = DATAPathConfigs.DTRDataConfigFullPath + this.CompentUserPath;
            this.IsExists = Directory.Exists(this.FullUserPath);

            this.ArticlePath = this.CreateResource();
            if (string.IsNullOrEmpty(this.ArticlePath))
            {
                this.IsCreate = false;
                this.ArticleFile = null;
            }
            else {
                this.IsCreate = true;
                this.ArticleFile = new ArticleFile(this.ArticlePath);
            }

        }

        public string CompentUserPath { get; private set; }//用户的相对资源路径
        public string FullUserPath { get; private set; }//用户的资源完全限定路劲
        public  ArticleFile ArticleFile { get; set; }//表示文章文件
        public bool IsCreate { get; private set; }//表示文章是否创建
        public bool IsExists { get; private set; }//便表示用户路径是否存在
        public string ArticlePath { get; private set; }//文章文件的相对路径

        //创建用户的新文章文件夹，并返回新的文章文件夹的相对路径
        private string CreateResource()
        {
            if (!this.IsExists)//如果用户的文件夹不存在则不创建
                return null;

            string userWorksPath = this.FullUserPath + DATAPathConfigs.BlogUser;//用户的所有文章的文件夹
            if (!Directory.Exists(userWorksPath))//用户的总文章文件夹如果不存在则创建
                Directory.CreateDirectory(userWorksPath);//创建总文章文件夹

            return this.CreateResurceCore(userWorksPath);
        }

        //根据用户的总文章文件夹的全路径创建文件
        private string CreateResurceCore(string UserWorksPath)
        {
            string ActicleDirectroyNanme = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");//生成文章文件夹的名称
            string WorksPath = this.CompentUserPath + DATAPathConfigs.BlogUser;//相对总文章资源路径
            string fullWorksPath = UserWorksPath + ActicleDirectroyNanme;//要生成的文章文件夹的完全限定路径

            while (Directory.Exists(fullWorksPath))//是否已经存在了
            {
                ActicleDirectroyNanme = DateTime.Now.AddSeconds(1).ToString("yyyy-MM-dd hh-mm-ss");
                fullWorksPath = UserWorksPath + ActicleDirectroyNanme;
            }

            DirectoryInfo fullWorksInfo = Directory.CreateDirectory(fullWorksPath);//创建新的文章文件夹
            fullWorksInfo.CreateSubdirectory(DATAPathConfigs.WorksResource);//创建文章的资源文件夹

            string ActicleRelativePath = WorksPath + ActicleDirectroyNanme + "\\";//文章文件夹的相对路径

            //将默认的文章封面copy至该文章文件夹之下
            string defaultTitle = DATAPathConfigs.DTRDataConfigFullPath + DATAPathConfigs.DTRDataDefaultResourceComponentPath + DATAPathConfigs.BlogWorksTitleDefault;//默认封面路劲
            if (!File.Exists(defaultTitle))
                throw new Exception("抱歉，没有配置好默认的文章封面。");
            File.Copy(defaultTitle, fullWorksPath + "\\" + DATAPathConfigs.WorksFace);

            return ActicleRelativePath;
        }
        
    }
}