namespace Doctorly.EventManager.Api.WebApi.Middlewares
{
    public class RequestValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Property and relational pattern matching on incoming request
            bool isValid = context.Request switch
            {
                { Method: "POST" } => true,
                { Method: "GET" } => true,
                { Method: "PUT" } => true,
                { Method: "DELETE" } => true,
                { Method: "OPTIONS" } => true,
                _ => false
            };

            if (!isValid)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid request parameters or unauthorized path.");
                return; // Short-circuit pipeline
            }

            await _next(context);
        }

    }

}
