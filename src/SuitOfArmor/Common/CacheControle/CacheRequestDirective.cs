using System;
using JetBrains.Annotations;

namespace BLun.SuitOfArmor.Common.CacheControle
{
    /// <summary>
    /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9
    /// 
    /// cache-request-directive =
    /// "no-cache"                          ; Section 14.9.1
    /// | "no-store"                          ; Section 14.9.2
    /// | "max-age" "=" delta-seconds         ; Section 14.9.3, 14.9.4
    /// | "max-stale" [ "=" delta-seconds ]   ; Section 14.9.3
    /// | "min-fresh" "=" delta-seconds       ; Section 14.9.3
    /// | "no-transform"                      ; Section 14.9.5
    /// | "only-if-cached"                    ; Section 14.9.4
    /// | cache-extension                     ; Section 14.9.6
    /// 
    /// cache-extension = token [ "=" ( token | quoted-string ) ]
    /// </summary>
    internal static class CacheRequestDirective
    {
        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.1
        /// 
        /// cache-request-directive =
        /// "no-cache"
        /// </summary>
        /// <returns>cache-request-directive name</returns>
        public static string NoCache() => "no-cache";
        
        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.2
        /// 
        /// cache-request-directive =
        /// "no-store"
        /// </summary>
        /// <returns>cache-request-directive name</returns>
        public static string NoStore() => "no-store";
        
        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.3
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.4
        /// 
        /// cache-request-directive =
        /// "max-age" "=" delta-seconds
        /// </summary>
        /// <returns>cache-request-directive name</returns>
        public static string MaxAge([NotNull] long deltaSeconds)
        {
            return $"{_maxAge}={deltaSeconds}";
        }
        private static readonly string _maxAge = "max-age";
        
        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.3
        /// 
        /// cache-request-directive =
        /// "max-stale"
        /// </summary>
        /// <returns>cache-request-directive name</returns>
        public static string MaxStale() => "max-stale";
        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.3
        /// 
        /// cache-request-directive =
        /// "max-stale" "=" delta-seconds
        /// </summary>
        /// <returns>cache-request-directive name</returns>
        public static string MaxStale([NotNull] long deltaSeconds)
        {
            return $"{MaxStale()}={deltaSeconds}";
        }
        
        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.3
        /// 
        /// cache-request-directive =
        /// "min-fresh" "=" delta-seconds
        /// </summary>
        /// <returns>cache-request-directive name</returns>
        public static string MinFresh([NotNull] long deltaSeconds)
        {
            return $"{_minFresh}={deltaSeconds}";
        }
        private static readonly string _minFresh = "min-fresh";
        
        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.5
        /// 
        /// cache-request-directive =
        /// "no-transform"
        /// </summary>
        /// <returns>cache-request-directive name</returns>
        public static string NoTransform() => "no-transform";
        
        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.4
        /// 
        /// cache-request-directive =
        /// "only-if-cached"
        /// </summary>
        /// <returns>cache-request-directive name</returns>
        public static string OnlyIfCached() => "only-if-cached";
        
        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.6
        /// 
        /// cache-request-directive =
        /// cache-extension
        /// 
        /// cache-extension = token
        /// </summary>
        /// <returns>cache-request-directive name</returns>
        public static string CacheExtension() => "token";

        /// <summary>
        /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.9.6
        /// 
        /// cache-request-directive =
        /// cache-extension
        /// 
        /// cache-extension = token "=" ( token | quoted-string )
        /// </summary>
        /// <returns>cache-request-directive name</returns>
        public static string CacheExtension(string tokenOrQuotedString)
        {
            return $"{CacheExtension()}={tokenOrQuotedString}";
        }
    }
}