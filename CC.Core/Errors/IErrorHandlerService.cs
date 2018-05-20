using System;

namespace CC.Core.Errors
{
    public interface IErrorHandlerService
    {
        ErrorInfo GetErrorInformation(Exception ex);
        ErrorInfo GetErrorInformation(int statusCode, int errorCode);
    }
}