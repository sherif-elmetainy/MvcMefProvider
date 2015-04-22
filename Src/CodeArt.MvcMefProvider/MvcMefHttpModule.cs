using CodeArt.MvcMefProvider;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


[assembly: PreApplicationStartMethod(typeof(MvcMefHttpModule), "Register")]

namespace CodeArt.MvcMefProvider
{
    /// <summary>
    /// An http module to perform cleanup at the end of an httprequest
    /// </summary>
    
    public class MvcMefHttpModule : IHttpModule
    {
        /// <summary>
        /// Dynamically registers the http module
        /// </summary>
        public static void Register()
        {
            DynamicModuleUtility.RegisterModule(typeof(MvcMefHttpModule));
        }

        /// <summary>
        /// Nothing to dispose
        /// </summary>
        public void Dispose()
        {
            // Nothing to dispose
        }

        /// <summary>
        /// Register an end request event handler to perform cleanup (dispose of any composition contexts which may be allocated in the http request)
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.EndRequest += OnEndRequest;
        }

        


        /// <summary>
        /// Disposes of composition contexts (if any) which were allocated in the http request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEndRequest(object sender, EventArgs e)
        {
            CompositionContextProvider.DisposeCurrentCompositionContext(HttpContext.Current);
        }
    }
}
