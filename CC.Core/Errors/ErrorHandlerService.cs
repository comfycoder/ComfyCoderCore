using CC.Core.HttpClientRequests;
using CC.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace CC.Core.Errors
{
    /// <summary>
    /// Error Handler Service
    /// </summary>
    public class ErrorHandlerService : IErrorHandlerService
    {
        private readonly IConfiguration _config;
        private readonly IRequestMetadata _requestMetadata;
        private readonly ITelemetryService _telemetryService;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="requestMetadata"></param>
        /// <param name="telemetryService"></param>
        public ErrorHandlerService(IConfiguration config, IRequestMetadata requestMetadata, ITelemetryService telemetryService)
        {
            _config = config;
            _requestMetadata = requestMetadata;
            _telemetryService = telemetryService;
        }

        /// <summary>
        /// Get error information base in input status code and error code.
        /// </summary>
        /// <param name="statusCode">
        /// Http Status Code
        /// </param>
        /// <param name="errorCode">
        /// Application Error Code
        /// </param>
        /// <param name="identity">
        /// LogonIdentity object instance.
        /// </param>
        /// <returns>
        /// An ErrorInfo object instance.
        /// </returns>
        public ErrorInfo GetErrorInformation(int statusCode, int errorCode)
        {
            var messageId = _requestMetadata.MessageId;
            var transactionId = _requestMetadata.TransactionId;

            var errorInfo = new ErrorInfo();

            switch (errorCode)
            {
                case ErrorCodes.Unauthorized:
                    errorInfo = SetErrorInformation(statusCode, ErrorMessages.Unauthorized,
                        errorCode, messageId, transactionId);
                    break;

                case ErrorCodes.DataAccessException:
                    errorInfo = SetErrorInformation(statusCode, ErrorMessages.DataAccessError,
                        errorCode, messageId, transactionId);
                    break;
            }

            var message = $"Http Status Code: {errorInfo.Status}; Error Code: {errorInfo.ErrorCode}; Message: {errorInfo.Message}";

            var ex = new ApplicationException(message);

            var telemetryProperties = SetTelemetryProperties(errorInfo);

            _telemetryService.TrackException(ex, telemetryProperties);

            return errorInfo;
        }

        /// <summary>
        /// Gets error information based on input exception.
        /// </summary>
        /// <param name="ex">
        /// An Exception object instance.
        /// </param>
        /// <param name="identity">
        /// LogonIdentity object instance.
        /// </param>
        /// <returns>
        /// An ErrorInfo object instance.
        /// </returns>        
        public ErrorInfo GetErrorInformation(Exception ex)
        {
            var messageId = _requestMetadata.MessageId;
            var transactionId = _requestMetadata.TransactionId;

            var errorInfo = new ErrorInfo();

            var exceptionType = ex.GetType();

            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                errorInfo = SetErrorInformation(StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized,
                    StatusCodes.Status401Unauthorized, messageId, transactionId);
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                errorInfo = SetErrorInformation(StatusCodes.Status501NotImplemented, ErrorMessages.NotImplemented,
                    StatusCodes.Status501NotImplemented, messageId, transactionId);
            }
            else
            {
                errorInfo = SetErrorInformation(StatusCodes.Status500InternalServerError, ErrorMessages.InternalServerError,
                    StatusCodes.Status500InternalServerError, messageId, transactionId);
            }

            var telemetryProperties = SetTelemetryProperties(errorInfo);

            _telemetryService.TrackException(ex, telemetryProperties);

            return errorInfo;
        }

        /// <summary>
        /// Populates an error infor object.
        /// </summary>
        /// <param name="status">
        /// An Http Status Code.
        /// </param>
        /// <param name="developerMessage">
        /// An error message to support developer troubleshooting.
        /// </param>
        /// <param name="userMessage">
        /// An error message suitable for desplay to an end user.
        /// </param>
        /// <param name="errorCode">
        /// An application error code number.
        /// </param>
        /// <param name="messageId">
        /// Correlation message identifier.
        /// </param>
        /// <param name="transactionId">
        /// Correlation transaction identifier.
        /// </param>        
        /// <returns>
        /// An ErrorInfo object instance.
        /// </returns>
        private ErrorInfo SetErrorInformation(int status, string message, int errorCode, string messageId, string transactionId)
        {
            var errorInfo = new ErrorInfo
            {
                Status = status,
                Message = message,
                ErrorCode = errorCode,
                MessageId = messageId,
                TransactionId = transactionId,
                MoreInformation = _config["ApiDocumentationUrl"] + errorCode
            };

            return errorInfo;
        }

        /// <summary>
        /// Set additional properties for the telemetry object.
        /// </summary>
        /// <param name="errorInfo">
        /// An ErrorInfo object instance.
        /// </param>
        /// <param name="identity">
        /// And (optional) LogonIdentity object instance.
        /// </param>
        /// <returns>
        /// A string Dictionary object with the newly set additional telemetry properties.
        /// </returns>
        private Dictionary<string, string> SetTelemetryProperties(ErrorInfo errorInfo)
        {
            var properties = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(errorInfo.MessageId))
            {
                properties.Add("Correlation_MessageId", errorInfo.MessageId);
            }

            if (!string.IsNullOrWhiteSpace(errorInfo.TransactionId))
            {
                properties.Add("Correlation_TransactionId", errorInfo.TransactionId);
            }

            properties.Add("Error_Code", errorInfo.ErrorCode.ToString());

            properties.Add("Error_Message", errorInfo.Message);

            properties.Add("Identity_IsAuthorized", _requestMetadata.IsAuthorized.ToString());

            if (!string.IsNullOrWhiteSpace(_requestMetadata.UserIdentifier))
            {
                properties.Add("Identity_Username", _requestMetadata.UserIdentifier);
            }

            if (_requestMetadata.ProfileIdentifier != Guid.Empty)
            {
                properties.Add("Identity_ProfileId", _requestMetadata.ProfileIdentifier.ToString());
            }            

            return properties;
        }
    }

}
