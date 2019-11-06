using Autofac;
using Autofac.Integration.Mvc;
using DTRBlog.IManager;
using DTRBlog.IService;
using DTRBlog.Manager;
using DTRBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DTRBlog.ContainerFactory
{
    public class DependencyRegister
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetCallingAssembly());
            builder.RegisterType<BlogUserService>().As<IBlogUserService>().InstancePerRequest();
            builder.RegisterType<CollectionService>().As<ICollectionService>().InstancePerRequest();
            builder.RegisterType<FollowService>().As<IFollowService>().InstancePerRequest();
            builder.RegisterType<NewsService>().As<INewsService>().InstancePerRequest();
            builder.RegisterType<BlogCommentService>().As<IBlogCommentService>().InstancePerRequest();
            builder.RegisterType<ArticleService>().As<IArticleService>().InstancePerRequest();
            builder.RegisterType<SupportService>().As<ISupportService>().InstancePerRequest();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerRequest();

            builder.RegisterType<BlogUserManager>().As<IBlogUserManager>().InstancePerRequest();
            builder.RegisterType<ArticleManager>().As<IArticleManager>().InstancePerRequest();
            builder.RegisterType<NewsManager>().As<INewsManager>().InstancePerRequest();
            builder.RegisterType<CategoryManager>().As<ICategoryManager>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
