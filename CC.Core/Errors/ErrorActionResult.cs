using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CC.Core.Errors
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorActionResult : IActionResult
    {
        private readonly ErrorResult _result;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorInfo"></param>
        public ErrorActionResult(ErrorInfo errorInfo)
        {
            _result = new ErrorResult
            {
                StatusCode = errorInfo.Status,
                Error = new ErrorResponse
                {
                    Error = errorInfo
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ExecuteResultAsync(ActionContext context)
        {
            ObjectResult objectResult = null;

            if (_result.Error != null)
            {
                objectResult = new ObjectResult(_result.Error)
                {
                    StatusCode = _result.StatusCode
                };
            }

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
