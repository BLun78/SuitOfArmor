using System;
using System.Threading.Tasks;
using BLun.SuitOfArmor.Common.Helper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BLun.SuitOfArmor.Filter
{
    /// <summary>
    /// CacheControlAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AddHeaderAttribute: Attribute, IAsyncActionFilter
    {
        private readonly HeaderHelper _headerHelper;
        private readonly string _headerDirective;
        protected readonly ILogger Logger;
        private readonly string _headerName;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BLun.SuitOfArmor.Filter.AddHeaderAttribute"/> class.
        /// </summary>
        /// <param name="headerName"></param>
        /// <param name="headerDirective"></param>
        public AddHeaderAttribute(
            [NotNull] string headerName,
            [NotNull] string headerDirective) 
            : this (headerName, headerDirective, new HeaderHelper(), null)
        {   
        }
            
        /// <summary>
        /// Initializes a new instance of the <see cref="T:BLun.SuitOfArmor.Filter.CacheControlAttribute"/> class.
        /// </summary>
        /// <param name="headerHelper">HeaderHelper</param>
        /// <param name="headerDirective"></param>
        /// <param name="headerName"></param>
        /// <param name="logger"></param>
        /// <exception cref="ArgumentException">can throw for param:headerName, param:headerDirective</exception>
        /// <exception cref="ArgumentNullException">can throw for param:headerHelper</exception>
        internal AddHeaderAttribute(
            [NotNull] string headerName,
            [NotNull] string headerDirective,
            [NotNull] HeaderHelper headerHelper,
            [CanBeNull] ILogger logger)
        {
            if (string.IsNullOrEmpty(headerName))
                throw new ArgumentException("Value cannot be null or empty.", nameof(headerName));
            if (string.IsNullOrEmpty(headerDirective))
                throw new ArgumentException("Value cannot be null or empty.", nameof(headerDirective));
            _headerHelper = headerHelper ?? throw new ArgumentNullException(nameof(headerHelper));
            _headerName = headerName;
            _headerDirective = headerDirective;
            Logger = logger;
        }

        /// <summary>
        /// Called asynchronously before the action, after model binding is complete.
        /// Set 
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
        /// <param name="next">
        /// The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutionDelegate" />. Invoked to execute the next action filter or the action itself.
        /// </param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that on completion indicates the filter has executed.</returns>
        /// <exception cref="ArgumentNullException">can throw for param:next, param:context, param:invoker</exception>
        /// <exception cref="ArgumentOutOfRangeException">can throw for param:context.HttpContext</exception>
        public async Task OnActionExecutionAsync([NotNull] ActionExecutingContext context,
            [NotNull] ActionExecutionDelegate next)
        {
            if (next == null) throw new ArgumentNullException(nameof(next));

            await next();

            if (context == null) throw new ArgumentNullException(nameof(context));
            
            Invoker(context.HttpContext);
        }

        public void Invoker([NotNull] HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            
            try
            {
                SetHeader(context);
            }
            catch (Exception e)
            {
                Logger?.LogError(e.Message, e);
                throw new InvalidOperationException(
                    $"An exception is done, during set '{_headerName}:{_headerDirective}' response header.", e);
            }
        }
        
        /// <summary>
        /// Set the cache-control header to the response header
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="InvalidOperationException"></exception>
        private void SetHeader([NotNull] HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            _headerHelper.SetHeader(context, _headerName, _headerDirective);
            Logger?.LogDebug($"The headerdirective '{_headerName}:{_headerDirective}' is set to the response.");
        }
    }
}