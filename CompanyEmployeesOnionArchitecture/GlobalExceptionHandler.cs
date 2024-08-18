using Domain.ErrorModel;
using Domain.Exceptions.Base;
using Domain.Repositories;
using Microsoft.AspNetCore.Diagnostics;

namespace CompanyEmployeesOnionArchitecture;

public class GlobalExceptionHandler(ILoggerManager logger) : IExceptionHandler
{
    private readonly ILoggerManager _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";

        var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null) {
            httpContext.Response.StatusCode = contextFeature.Error switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            };

            _logger.LogError($"Something went wrong: {exception}");

            await httpContext.Response.WriteAsync(new ErrorDetails()
            {
                Message = contextFeature.Error.Message,
                StatusCode = httpContext.Response.StatusCode
            }.ToString(), cancellationToken: cancellationToken);
        }

        return true;
    }
}