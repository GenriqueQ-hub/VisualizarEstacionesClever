using Microsoft.AspNetCore.Mvc;
using VisualizadorEstacionesClever.Api.Services;

namespace VisualizadorEstacionesClever.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PruebaLdapsController : ControllerBase
{
    private readonly PruebaLdapsService _servicio;

    public PruebaLdapsController(PruebaLdapsService servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public async Task<IActionResult> Probar()
    {
        try
        {
            var resultado = await _servicio.ProbarConexionAsync();
            return Ok(new { mensaje = resultado });
        }
        catch (System.Security.Authentication.AuthenticationException)
        {
            return StatusCode(503, new
            {
                mensaje = "No se pudo validar el certificado LDAPS."
            });
        }
        catch (Exception)
        {
            return StatusCode(503, new
            {
                mensaje = "No fue posible establecer la conexión LDAPS."
            });
        }
    }
}