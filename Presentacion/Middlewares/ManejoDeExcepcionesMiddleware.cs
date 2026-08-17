using CapaNegocio.Excepciones;
using Microsoft.AspNetCore.Http.Features;
using System.Net;
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

        public async Task InvokeAsync(HttpContext context)
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

        private async Task ManejarExcepcionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            int statusCode;
            string mensaje;

            switch (ex)
            {
                case ValidacionException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    mensaje = ex.Message;
                    break;

                case NotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    mensaje = ex.Message;
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    mensaje = "Ocurrió un error inesperado en el servidor.";
                    break;
            }

            context.Response.StatusCode = statusCode;

            var respuesta = new
            {
                error = true,
                mensaje = mensaje,
                codigo = statusCode
            };

            var json = JsonSerializer.Serialize(respuesta);
            await context.Response.WriteAsync(json);
        }
    }
}
