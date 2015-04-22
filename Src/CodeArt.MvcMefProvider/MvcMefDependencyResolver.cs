using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CodeArt.MvcMefProvider
{
    /// <summary>
    /// Dependency resolver used for MVC (note that this is a different interface from the one used for webapis)
    /// </summary>
    internal class MvcMefDependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// object that provides a CompositionContext scoped to current http request
        /// </summary>
        private readonly CompositionContextProvider _compositionProvider;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="compositionProvider"></param>
        public MvcMefDependencyResolver(CompositionContextProvider compositionProvider)
        {
            if (compositionProvider == null)
                throw new ArgumentNullException("compositionProvider");
            _compositionProvider = compositionProvider;
        }

        /// <summary>
        /// Resolves a dependency
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            object export;
            _compositionProvider.GetCompositionContext().TryGetExport(serviceType, out export);
            return export;
        }

        /// <summary>
        /// Resolves multiple depdencies
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _compositionProvider.GetCompositionContext().GetExports(serviceType);
        }
    }
}
