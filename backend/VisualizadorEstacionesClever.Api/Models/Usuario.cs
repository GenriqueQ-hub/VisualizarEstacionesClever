
namespace VisualizadorEstacionesClever.Api.Models;

public class Usuario
{
    public int IdUsuario { get; set; }

    public int IdRol { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? Apellido { get; set; }

    public string Correo { get; set; } = string.Empty;

    public string NombreUsuario { get; set; } = string.Empty;

    // Campo heredado de la base de datos.
    // No se utilizará para validar contraseñas de Active Directory.
    public string? Password { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string? UsuarioAD { get; set; }

    public Rol? Rol { get; set; }
}
