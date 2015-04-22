using CodeArt.MvcMefProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Composition.Convention
{
    /// <summary>
    /// Helper methods for configuring containers
    /// </summary>
    public static class PartBuilderExtensions
    {
        /// <summary>
        /// Configures the part as instance per Http request
        /// </summary>
        /// <param name="partConventionBuilder"></param>
        /// <returns></returns>
        public static PartConventionBuilder InstancePerHttpRequest(this PartConventionBuilder partConventionBuilder)
        {
            return partConventionBuilder.Shared(Constants.InstancePerRequestBoundaryName);
        }

        /// <summary>
        /// Configure the part to use constructor with most parameters
        /// </summary>
        /// <param name="partConventionBuilder"></param>
        /// <returns></returns>
        public static PartConventionBuilder SelectConstructorWithMostParameters(this PartConventionBuilder partConventionBuilder)
        {
            return partConventionBuilder.SelectConstructor((constructors) => constructors.OrderByDescending(c => c.GetParameters().Length).FirstOrDefault());
        }
    }
}
