using Microsoft.AspNetCore.Http.Features;
using System.Text.Json;

namespace Presentacion.Middlewares
{
    public class ManejoDeExcepcionesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ManejoDeExcepcionesMiddleware> _logger;

        public ManejoDeExcepcionesMiddleware(RequestDelegate next, ILogger<ManejoDeExcepcionesMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsynk(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            
            catch (Exception ex)
            {
                await ManejarExcepcionAsync(context, ex);
            }

        }

        private Task ManejarExcepcionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            int statusCode;
            string mensaje;
            List<string> detalles = new();

            _logger.LogError(ex, "Ocurrio un error no controlado en el servidor");
            statusCode = StatusCodes.Status500InternalServerError;
            mensaje = "Ha ocurrido un error interno en el servidor";

            string requestId = Guid.NewGuid().ToString();

            var detallesError = new
            {
                codigo = statusCode,
                mensaje = mensaje,
                detalles = detalles,
                requestId = requestId
            };

            return context.Response.WriteAsJsonAsync(JsonSerializer.Serialize(detallesError));

        }
    }
}
