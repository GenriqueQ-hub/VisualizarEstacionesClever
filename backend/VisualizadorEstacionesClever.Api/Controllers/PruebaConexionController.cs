
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisualizadorEstacionesClever.Api.Data;

namespace VisualizadorEstacionesClever.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PruebaConexionController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PruebaConexionController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Obtener()
    {
        try
        {
            var usuarios = await _context.Usuarios
                .Select(u => new
                {
                    u.IdUsuario,
                    u.NombreUsuario,
                    u.Nombre,
                    u.Apellido,
                    u.Activo,
                    Rol = u.Rol!.Nombre
                })
                .ToListAsync();

            return Ok(new
            {
                mensaje = "Conexión a SQL Server exitosa",
                totalUsuarios = usuarios.Count,
                usuarios
            });
        }
        catch (Exception)
        {
            return StatusCode(500, new
            {
                mensaje = "No fue posible consultar la base de datos."
            });
        }
    }
}
