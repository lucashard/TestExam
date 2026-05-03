using System.Text.Json;

namespace TestExam;

public class ApiExceptionMiddleware
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly RequestDelegate _next;

    public ApiExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ErrorException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex.StatusCode;

            var payload = new
            {
                mensaje = ex.Mensaje,
                descripcion = ex.Descripcion
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(payload, JsonOptions));
        }
    }
}

