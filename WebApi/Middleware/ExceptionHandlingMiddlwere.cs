using System.Net;
using WebApi.Dtos;

namespace WebApi.Middlewere
{
    /// <summary>
    /// The ExceptionHandlingMiddlwere class is a middleware that handles exceptions in the HTTP request pipeline.
    /// </summary>
    public class ExceptionHandlingMiddlwere
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddlwere> _logger;

        /// <summary>
        /// Initializes a new instance of the ExceptionHandlingMiddlwere class.
        /// </summary>
        /// <param name="next">The next middleware in the HTTP request pipeline.</param>
        /// <param name="logger">An instance of ILogger.</param>
        public ExceptionHandlingMiddlwere(RequestDelegate next,
            ILogger<ExceptionHandlingMiddlwere> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="httpContext">The HttpContext for the current request and response.</param>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExeptionAsync(httpContext, ex.Message,
                      HttpStatusCode.InternalServerError, "MiddlewereErrorHandler");
                throw;
            }
        }

        /// <summary>
        /// Handles exceptions by logging the error and returning an error response.
        /// </summary>
        /// <param name="context">The HttpContext for the current request and response.</param>
        /// <param name="exMassage">The exception message.</param>
        /// <param name="httpStatusCode">The HTTP status code to return in the response.</param>
        /// <param name="massage">The error message to return in the response.</param>
        private async Task HandleExeptionAsync(HttpContext context,
            string exMassage, HttpStatusCode httpStatusCode, string massage)
        {
            _logger.LogError(exMassage);

            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)httpStatusCode;

            ErrorDto errorDto = new()
            {
                Message = massage,
                StatusCode = (int)httpStatusCode,
            };

            await response.WriteAsJsonAsync(errorDto);
        }
    }

}
