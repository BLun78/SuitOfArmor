using System;
using BLun.SuitOfArmor.Common;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BLun.SuitOfArmor
{
    /// <summary>
    /// Extension methods for the SuitOfArmorMiddleware
    /// </summary>
    public static class SuitOfArmorMiddlewareExtensions
    {
        /// <summary>
        /// Enable 'cache-controle: no-cache' header
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">can throw for param:app</exception>
        public static IApplicationBuilder UseNoCache([NotNull] this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<global::BLun.SuitOfArmor.Middleware.NoCacheMiddleware>();
        }
        
        
    }
}