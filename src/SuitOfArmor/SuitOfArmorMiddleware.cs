using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BLun.SuitOfArmor
{
    /// <summary>
    /// Enables Suit of Armor middleware for requests
    /// </summary>
    public class SuitOfArmorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SuitOfArmorOption _options;
        private readonly ILogger<SuitOfArmorMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BLun.SuitOfArmor.SuitOfArmorMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="options">The configuration options.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> instance used to create loggers.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SuitOfArmorMiddleware([NotNull] RequestDelegate next,
            [NotNull] IOptions<SuitOfArmorOption> options,
            [NotNull] ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));

            if (options == null) throw new ArgumentNullException(nameof(options));
            _options = options.Value;

            if (loggerFactory == null) throw new ArgumentNullException(nameof(loggerFactory));
            _logger = loggerFactory.CreateLogger<SuitOfArmorMiddleware>();
        }

        /// <summary>
        /// Processes a request to do the Suit of Armor handshake
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke([NotNull] HttpContext context)
        {
            // Call the next delegate/middleware in the pipeline
            await this._next(context);
        }
    }
}