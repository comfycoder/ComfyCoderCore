//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Diagnostics;
//using Microsoft.AspNetCore.Http;
//using Newtonsoft.Json;
//using System;

//namespace CC.Core.Errors
//{
//    public static class UseWebApiGlobalExceptionHandlerExtensions
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="app"></param>
//        /// <param name="serviceProvider"></param>
//        /// <returns></returns>
//        public static void UseWebApiGlobalExceptionHandler(this IApplicationBuilder app, IServiceProvider serviceProvider)
//        {
//            app.UseExceptionHandler(config =>
//            {
//                config.Run(
//                    async context =>
//                    {
//                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

//                        if (exceptionHandlerFeature?.Error != null)
//                        {
//                            var ex = exceptionHandlerFeature.Error;

//                            var errorHandlerService = serviceProvider.GetService<IErrorHandlerService>();

//                            var errorInfo = errorHandlerService.GetErrorInformation(ex);

//                            context.Response.StatusCode = errorInfo.Status;

//                            context.Response.ContentType = "application/json";

//                            var errorResponse = new ErrorResponse
//                            {
//                                Error = errorInfo
//                            };

//                            var error = JsonConvert.SerializeObject(errorResponse);

//                            await context.Response.WriteAsync(error);
//                        }
//                    });
//            });
//        }
//    }
//}
