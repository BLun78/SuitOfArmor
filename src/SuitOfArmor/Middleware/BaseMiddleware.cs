using System;
using System.Threading.Tasks;
using BLun.SuitOfArmor.Common;
using BLun.SuitOfArmor.Common.CacheControle;
using BLun.SuitOfArmor.Common.Helper;
using BLun.SuitOfArmor.Filter;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BLun.SuitOfArmor.Middleware
{
    /// <summary>
    /// Enables Suit of Armor middleware for requests
    /// </summary>
    internal class BaseMiddleware<TAttribute> : IMiddleware
        where TAttribute : AddHeaderAttribute, IAsyncActionFilter
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly SuitOfArmorOption _options;
        private readonly ILogger _logger;
        private readonly TAttribute _attribute;
        private readonly string _cacheResponseDirective;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BLun.SuitOfArmor.Middleware.NoCacheMiddleware"/> class.
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> instance used to create loggers.</param>
        /// <param name="logger"></param>
        /// <param name="cacheResponseDirective">cache-response-dirictive name</param>
        /// <param name="options">The configuration options.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public BaseMiddleware(
            [NotNull] TAttribute attribute,
            [NotNull] ILoggerFactory loggerFactory,
            [NotNull] ILogger logger,
            [NotNull] IOptions<SuitOfArmorOption> options
        ) : this(attribute, loggerFactory, logger)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            _options = options.Value ?? throw new ArgumentException(nameof(options.Value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BLun.SuitOfArmor.Middleware.NoCacheMiddleware"/> class.
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> instance used to create loggers.</param>
        /// <param name="logger"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public BaseMiddleware(
            [NotNull] TAttribute attribute,
            [NotNull] ILoggerFactory loggerFactory,
            [NotNull] ILogger logger)
        {
            _attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _logger.LogDebug("'cache-controle: no-cache' was initialized.");
        }

        /// <summary>Request handling method.</summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Http.HttpContext" /> for the current request.</param>
        /// <param name="next">The delegate representing the remaining middleware in the request pipeline.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the execution of this middleware.</returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Call the next delegate/middleware in the pipeline
            await next(context);
            try
            {
                _attribute.Invoker(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                throw new InvalidOperationException(
                    $"An exception is done, during set '{_attribute.ToString()}' response header.",
                    e);
            }
        }
    }
}