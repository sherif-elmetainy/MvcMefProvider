using CodeArt.MvcMefProvider;
using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System.Composition.Hosting
{
    /// <summary>
    /// Extensions for composition context
    /// </summary>
    public static class CompositionContextExtensions
    {
        /// <summary>
        /// Use the container as dependency resolver for ASP.NET MVC
        /// </summary>
        /// <param name="compositionContext">container to use</param>
        public static void UseWithMvc(this CompositionContext compositionContext)
        {
            var compositionProvider = new CompositionContextProvider(compositionContext);

            var dependencyResolver = new MvcMefDependencyResolver(compositionProvider);
            DependencyResolver.SetResolver(dependencyResolver);

            var controllerFactory = new MvcMefControllerFactory(compositionProvider);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}
