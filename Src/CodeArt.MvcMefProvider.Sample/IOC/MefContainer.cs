using CodeArt.MvcMefProvider.Sample.Models;
using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.Composition;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CodeArt.MvcMefProvider.Sample.IOC
{
    public static class MefContainer
    {
        public static void ConfigureContainer()
        {
            var containerConventions = new ConventionBuilder();

            containerConventions.ForType<DbProductRepository>()
                .ExportInterfaces()
                .SelectConstructorWithMostParameters()
                .InstancePerHttpRequest();

            containerConventions.ForType<DbLogger>()
                .ExportInterfaces()
                .SelectConstructorWithMostParameters()
                .InstancePerHttpRequest();

            containerConventions.ForType<ProductDbContext>()
                .Export()
                .InstancePerHttpRequest();

            containerConventions.ForTypesDerivedFrom<Controller>()
                .Export<IController>()
                .Export()
                .SelectConstructorWithMostParameters();

            var containerConfig = new ContainerConfiguration();
            containerConfig.WithAssembly(Assembly.GetExecutingAssembly(), containerConventions);

            containerConfig.CreateContainer().UseWithMvc();
        }
    }
}