namespace Estacionamento.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = ex switch
                {
                    FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
                    InvalidOperationException => StatusCodes.Status400BadRequest,
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };
                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsJsonAsync(new
                {
                    error = statusCode,
                    mensagem = statusCode == StatusCodes.Status500InternalServerError ? "Ocorreu um erro interno" : ex.Message
                });
            }
        }
    }
}
