using System;
using System.Collections;
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
    public class XssProtectionAttribute : AddHeaderAttribute, IAsyncActionFilter
    {
        private const string HeaderName = "X-XSS-Protection";

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BLun.SuitOfArmor.Filter.CacheControlAttribute"/> class.
        /// </summary>
        public XssProtectionAttribute(XssProtection xssProtection)
            : this(new HeaderHelper(), xssProtection, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BLun.SuitOfArmor.Filter.CacheControlAttribute"/> class.
        /// </summary>
        /// <param name="headerHelper">HeaderHelper</param>
        /// <param name="loggerFactory">logger factory, can be null</param>
        /// <exception cref="ArgumentException">can throw for param:cacheResponseDirective</exception>
        /// <exception cref="ArgumentNullException">can throw for param:headerHelper</exception>
        internal XssProtectionAttribute([NotNull] HeaderHelper headerHelper,
            XssProtection xssProtection,
            ILoggerFactory loggerFactory)
            : base(HeaderName,
                CreateHeaderDirektive(xssProtection),
                headerHelper,
                loggerFactory?.CreateLogger<XssProtectionAttribute>())
        {
        }

        private static string CreateHeaderDirektive(XssProtection xssProtection)
        {
            switch (xssProtection)
            {
                case XssProtection.Disables:
                    return "0";
                case XssProtection.Enables:
                    return "1";
                case XssProtection.EnablesForOldIe8:
                    return "1; mode=block";
            }

            throw new InvalidOperationException("The XssProtection was not realy right set!");
        }
    }
}