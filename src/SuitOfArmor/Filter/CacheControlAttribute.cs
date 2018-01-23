using System;
using System.Linq;
using System.Threading.Tasks;
using BLun.SuitOfArmor.Common.CacheControle;
using BLun.SuitOfArmor.Common.Helper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace BLun.SuitOfArmor.Filter
{
    /// <summary>
    /// CacheControlAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CacheControlAttribute : AddHeaderAttribute, IAsyncActionFilter
    {
        private const string HeaderName = HeaderNames.CacheControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BLun.SuitOfArmor.Filter.CacheControlAttribute"/> class.
        /// </summary>
        /// <param name="cacheResponseDirective">Name of the cache-response-dirictive</param>
        public CacheControlAttribute(string cacheResponseDirective)
            : this(new HeaderHelper(), cacheResponseDirective, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BLun.SuitOfArmor.Filter.CacheControlAttribute"/> class.
        /// </summary>
        /// <param name="cacheResponseDirective">Name of the cache-response-dirictive</param>
        /// <param name="deltaSeconds">delta in seconds</param>
        public CacheControlAttribute(string cacheResponseDirective, long deltaSeconds)
            : this(new HeaderHelper(), cacheResponseDirective, deltaSeconds, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BLun.SuitOfArmor.Filter.CacheControlAttribute"/> class.
        /// </summary>
        /// <param name="headerHelper">HeaderHelper</param>
        /// <param name="cacheResponseDirective">Name of the cache-response-dirictive</param>
        /// <param name="deltaSeconds">delta in seconds, can be null</param>
        /// <param name="loggerFactory">logger factory, can be null</param>
        /// <exception cref="ArgumentException">can throw for param:cacheResponseDirective</exception>
        /// <exception cref="ArgumentNullException">can throw for param:headerHelper</exception>
        internal CacheControlAttribute([NotNull] HeaderHelper headerHelper, [NotNull] string cacheResponseDirective,
            long? deltaSeconds, ILoggerFactory loggerFactory)
            : base(HeaderName,
                CreateHeaderDirektive(
                    loggerFactory?.CreateLogger<CacheControlAttribute>(),
                    cacheResponseDirective,
                    deltaSeconds),
                headerHelper,
                loggerFactory?.CreateLogger<CacheControlAttribute>())
        {
        }

        /// <summary>
        /// Create the HeaderDirektive
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        private static string CreateHeaderDirektive(
            [CanBeNull] ILogger logger,
            [NotNull] string cacheResponseDirective,
            [CanBeNull] long? deltaSeconds)
        {
            if (string.IsNullOrEmpty(cacheResponseDirective))
                throw new ArgumentException("Value cannot be null or empty.", nameof(cacheResponseDirective));
            var parameterTest = CacheResponseDirective.ParameterList.Any(x => x == cacheResponseDirective);
            string directive;
            if (deltaSeconds.HasValue && parameterTest)
            {
                var seconds = deltaSeconds.Value;
                directive = $"{cacheResponseDirective}={seconds.ToString()}";
            }
            else if (!parameterTest)
            {
                directive = cacheResponseDirective;
            }
            else
            {
                var message =
                    $"The CacheResponseDirective '{HeaderName}:{cacheResponseDirective}' needs a parameter 'deltaSeconds'!";
                logger?.LogWarning(message);
                throw new InvalidOperationException(message);
            }

            return directive;
        }
    }
}