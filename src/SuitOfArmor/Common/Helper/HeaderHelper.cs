using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace BLun.SuitOfArmor.Common.Helper
{
    internal class HeaderHelper
    {
        public (bool result, StringValues? resultValue) GetHeader([NotNull] HttpContext context, string headerName)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (context.Request == null) throw new ArgumentOutOfRangeException(nameof(context.Request));
            if (context.Request.Headers == null) throw new ArgumentOutOfRangeException(nameof(context.Request.Headers));

            if (context.Request.Headers.TryGetValue(HeaderNames.IfNoneMatch, out var result))
            {
                return (true, result);
            }

            return (false, null);
        }

        public void SetHeader([NotNull] ActionExecutingContext context, [NotNull] string headerName,
            [ItemNotNull] StringValues values)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            SetHeader(context.HttpContext, headerName, values);
        }

        public void SetHeader([NotNull] HttpContext context, [NotNull] string headerName,
            [ItemNotNull] StringValues values)
        {
            if (context == null) throw new ArgumentOutOfRangeException(nameof(context));
            if (context.Response == null)
                throw new ArgumentOutOfRangeException(nameof(context.Response));
            if (context.Response.Headers == null)
                throw new ArgumentOutOfRangeException(nameof(context.Response.Headers));
            if (string.IsNullOrEmpty(headerName))
                throw new ArgumentException("Value cannot be null or empty.", nameof(headerName));
            if (values.Count == 0)
                throw new ArgumentOutOfRangeException("Value cannot be an empty collection.", nameof(values));

            context.Response.Headers.Add(headerName, values);
        }
    }
}