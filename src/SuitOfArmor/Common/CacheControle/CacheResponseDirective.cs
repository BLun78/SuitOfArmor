using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Primitives;

namespace BLun.SuitOfArmor.Common.CacheControle
{
    /// <summary>
    /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9
    /// 
    /// cache-response-directive =
    /// "public"                               ; Section 14.9.1
    /// | "private" [ "=" <"> 1#field-name <"> ] ; Section 14.9.1
    /// | "no-cache" [ "=" <"> 1#field-name <"> ]; Section 14.9.1
    /// | "no-store"                             ; Section 14.9.2
    /// | "no-transform"                         ; Section 14.9.5
    /// | "must-revalidate"                      ; Section 14.9.4
    /// | "proxy-revalidate"                     ; Section 14.9.4
    /// | "max-age" "=" delta-seconds            ; Section 14.9.3
    /// | "s-maxage" "=" delta-seconds           ; Section 14.9.3
    /// | cache-extension                        ; Section 14.9.6
    /// 
    /// cache-extension = token [ "=" ( token | quoted-string ) ]
    /// </summary>
    public static class CacheResponseDirective
    {
        internal static readonly string[] ParameterList = new[] {"max-age", "s-maxage"};

        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.1
        /// 
        /// cache-response-directive =
        /// "public"
        /// </summary>
        /// <returns>cache-response-directive name</returns>
        public const string @Public = "public";

        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.1
        /// 
        /// cache-response-directive =
        /// "private" [ "=" <"> 1#field-name <"> ]
        /// </summary>
        /// <returns>cache-response-directive name</returns>
        public const string @Private = "private";

        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.1
        /// 
        /// cache-response-directive =
        /// "no-cache" [ "=" <"> 1#field-name <"> ]
        /// </summary>
        /// <returns>cache-response-directive name</returns>
        public const string NoCache = "no-cache";

        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.2
        /// 
        /// cache-response-directive =
        /// "no-store"
        /// </summary>
        /// <returns>cache-response-directive name</returns>
        public const string NoStore = "no-store";

        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.5
        /// 
        /// cache-response-directive =
        /// "no-transform"
        /// </summary>
        /// <returns>cache-response-directive name</returns>
        public const string NoTransform = "no-transform";

        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.4
        /// 
        /// cache-response-directive =
        /// "must-revalidate"
        /// </summary>
        /// <returns>cache-response-directive name</returns>
        public const string MustRevalidate = "must-revalidate";

        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.4
        /// 
        /// cache-response-directive =
        /// "proxy-revalidate"
        /// </summary>
        /// <returns>cache-response-directive name</returns>
        public const string ProxyRevalidate = "proxy-revalidate";

        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.3
        /// 
        /// cache-response-directive =
        /// "max-age" "=" delta-seconds
        /// </summary>
        /// <returns>cache-response-directive name</returns>
        public const string MaxAge = "max-age";

        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.3
        /// 
        /// cache-response-directive =
        /// "s-maxage" "=" delta-seconds
        /// </summary>
        /// <returns>cache-response-directive name</returns>
        public const string SMaxage = "s-maxage";
    }
}