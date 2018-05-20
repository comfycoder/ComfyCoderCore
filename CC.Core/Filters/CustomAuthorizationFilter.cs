using CC.Core.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace CC.Core.HttpClientRequests
{
    /// <summary>
    /// Mvc Authorization Filter
    /// </summary>
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IRequestMetadata _requestMetadata;
        private readonly IErrorHandlerService _errorHandlerService;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="requestMetadata"></param>
        /// <param name="errorHandlerService"></param>
        public CustomAuthorizationFilter(IRequestMetadata requestMetadata, IErrorHandlerService errorHandlerService)
        {
            _requestMetadata = requestMetadata;
            _errorHandlerService = errorHandlerService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Allow Anonymous skips all authorization
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }

            if (!_requestMetadata.IsAuthorized)
            {
                var errorInfo = _errorHandlerService.GetErrorInformation(StatusCodes.Status401Unauthorized, ErrorCodes.Unauthorized);

                context.Result = new ErrorActionResult(errorInfo);
            }
        }
    }

}
