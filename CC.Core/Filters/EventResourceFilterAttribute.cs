using CC.Core.HttpClientRequests;
using CC.Core.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CC.Core
{
    /// <summary>
    /// Logs when a controller resource is accessed.
    /// </summary>
    public class EventResourceFilterAttribute : Attribute, IResourceFilter
    {
        readonly ITelemetryService _telemetry;
        readonly IRequestMetadata _requestMetadata;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="telemetry">
        /// A TelemetryService object instance.
        /// </param>
        /// <param name="requestMetadata">
        /// A RequestMetadata object instance.
        /// </param>
        public EventResourceFilterAttribute(
            ITelemetryService telemetry,
            IRequestMetadata requestMetadata
        )
        {
            _requestMetadata = requestMetadata;
            _telemetry = telemetry;
        }

        /// <summary>
        /// Executes the resource filter. Called before execution of the remainder of the pipeline.
        /// </summary>
        /// <param name="context">
        /// A context for the resource filter.
        /// </param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            var displayName = context?.ActionDescriptor?.AttributeRouteInfo?.Name;

            if (!string.IsNullOrWhiteSpace(displayName))
            {
                var properties = _requestMetadata.GetMetadataDictionary();

                _telemetry.TrackEvent(displayName, properties);
            }
        }

        /// <summary>
        /// Executes the resource filter. Called after execution of the remainder of the pipeline.
        /// </summary>
        /// <param name="context">
        /// A context for the resource filter.
        /// </param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {

        }
    }

}
