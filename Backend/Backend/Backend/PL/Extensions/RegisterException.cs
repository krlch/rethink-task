using Backend.BL.Enums;
using Backend.PL.Exceptions;
using Serilog;

namespace Backend.PL.Extensions
{
    public static class ExceptionHandlerExtension
    {
        
        public static void RegisterExceptionHandler(this IApplicationBuilder app, Serilog.ILogger logger)
        {
#if DEBUG
            app.UseDeveloperExceptionPage();
#endif
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (ApplicationHelperException ex)
                {

                    context.Response.StatusCode = ex.ErrorStatus switch
                    {
                        ServiceResultType.NotFound => StatusCodes.Status404NotFound,
                        ServiceResultType.Forbidden => StatusCodes.Status403Forbidden,
                        ServiceResultType.InvalidData => StatusCodes.Status400BadRequest,
                        _ => throw ex
                    };

                    logger.Error($"Error caught in ApplicationHelperException: {ex.Message}");
                    var result = new { errorMessage = ex.Message };
                    await context.Response.WriteAsJsonAsync(result);
                }
                catch (System.Exception ex)
                {
                    logger.Error($"{ex.Message}");
//#if DEBUG
//                    throw ex;
//#else
                    context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    var result = new { errorMessage = "Something went wrong..." };
                    await context.Response.WriteAsJsonAsync(result);
//#endif
                }
            });
        }
    }
}
