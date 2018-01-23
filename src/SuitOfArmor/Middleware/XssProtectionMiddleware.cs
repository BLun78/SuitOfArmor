using System;
using System.Threading.Tasks;
using BLun.SuitOfArmor.Common.CacheControle;
using BLun.SuitOfArmor.Common.Helper;
using BLun.SuitOfArmor.Filter;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BLun.SuitOfArmor.Middleware
{
    /// <summary>
    /// Enables Suit of Armor middleware for requests
    /// </summary>
    public class XssProtectionMiddleware : IMiddleware
    {
        private readonly BaseMiddleware<XssProtectionAttribute> _baseMiddleware;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BLun.SuitOfArmor.Middleware.NoCacheMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> instance used to create loggers.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public XssProtectionMiddleware(
            [NotNull] ILoggerFactory loggerFactory)
        {
            _baseMiddleware = new BaseMiddleware<XssProtectionAttribute>(
                new XssProtectionAttribute(new HeaderHelper(),  XssProtection.Enables, loggerFactory),   
                loggerFactory,
                loggerFactory.CreateLogger<NoCacheMiddleware>());
        }


        /// <summary>Request handling method.</summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Http.HttpContext" /> for the current request.</param>
        /// <param name="next">The delegate representing the remaining middleware in the request pipeline.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the execution of this middleware.</returns>
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            return _baseMiddleware.InvokeAsync(context, next);
        }
    }
}