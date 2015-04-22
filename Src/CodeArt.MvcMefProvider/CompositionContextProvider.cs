using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CodeArt.MvcMefProvider
{
    /// <summary>
    /// Managed the lifetime of sub containers and makes sure that each http request gets a unique container
    /// </summary>
    internal class CompositionContextProvider
    {
        /// <summary>
        /// Factory to create a sub container scoped to the http request
        /// </summary>
        private readonly ExportFactory<CompositionContext> _compositionContextFactory;

        /// <summary>
        /// Key used to lookup composition context in the httpcontext items dictionary
        /// </summary>
        private static readonly object _compositionContextKey = new object();

        public CompositionContextProvider(CompositionContext rootCompositionContext)
        {
            if (rootCompositionContext == null)
                throw new ArgumentNullException("rootCompositionContext");
            // Create factory
            var contract = new CompositionContract(typeof(ExportFactory<CompositionContext>), null, new Dictionary<string, object>() { { Constants.SharingBoundaryNameKey, new string[] { Constants.InstancePerRequestBoundaryName } } });
            _compositionContextFactory = (ExportFactory<CompositionContext>)rootCompositionContext.GetExport(contract);
        }

        /// <summary>
        /// Get the composition context (if any) from the http context dictionary
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private static Export<CompositionContext> GetCurrentCompositionContext(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            return (Export<CompositionContext>)httpContext.Items[_compositionContextKey];
        }

        /// <summary>
        /// Disposes of current http context container (if any)
        /// </summary>
        /// <param name="httpContext"></param>
        internal static void DisposeCurrentCompositionContext(HttpContext httpContext)
        {
            if (httpContext == null)
                return;
            var context = (Export<CompositionContext>)httpContext.Items[_compositionContextKey];
            if (context != null)
            {
                context.Dispose();
                httpContext.Items.Remove(_compositionContextKey);
            }
        }

        /// <summary>
        /// Adds a composition context to the httpcontext items dictionary
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="compositionContext"></param>
        private static void AddCompositionContext(HttpContext httpContext, Export<CompositionContext> compositionContext)
        {
            httpContext.Items.Add(_compositionContextKey, compositionContext);
        }

        /// <summary>
        /// Get a composition sub container scoped to the http request
        /// </summary>
        /// <returns></returns>
        public CompositionContext GetCompositionContext()
        {
            var currentHttpContext = HttpContext.Current;
            var export = CompositionContextProvider.GetCurrentCompositionContext(currentHttpContext);
            if (export == null)
            {
                export = _compositionContextFactory.CreateExport();
                AddCompositionContext(currentHttpContext, export);
            }
            return export.Value;
        }

    }
}
