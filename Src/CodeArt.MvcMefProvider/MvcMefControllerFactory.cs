using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeArt.MvcMefProvider
{
    internal class MvcMefControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// object that provides a CompositionContext scoped to current http request
        /// </summary>
        private readonly CompositionContextProvider _compositionProvider;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="compositionProvider"></param>
        public MvcMefControllerFactory(CompositionContextProvider compositionProvider)
        {
             if (compositionProvider == null)
                throw new ArgumentNullException("compositionProvider");
            _compositionProvider = compositionProvider;
        }
        
        /// <summary>
        /// Get a controller instance either by getting it as export or using default MVC implementation to create controller instance and using the composition provider to satisfy imports if any
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            object controller;
            var context = _compositionProvider.GetCompositionContext();
            context.TryGetExport(controllerType, out controller);
            if (controller == null)
            {
                controller = base.GetControllerInstance(requestContext, controllerType);
                context.SatisfyImports(controller);
            }
            return (IController)controller;
        }
    }
}
