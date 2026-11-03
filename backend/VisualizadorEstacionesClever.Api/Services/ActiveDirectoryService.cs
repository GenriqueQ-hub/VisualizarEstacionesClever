using System.DirectoryServices.Protocols;
using System.Net;

namespace VisualizadorEstacionesClever.Api.Services;

public class ActiveDirectoryService
{
    private readonly IConfiguration _configuration;

    public ActiveDirectoryService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public bool ValidarCredenciales(
        string nombreUsuario,
        string password)
    {
        if (string.IsNullOrWhiteSpace(nombreUsuario) ||
            string.IsNullOrWhiteSpace(password))
        {
            return false;
        }

        var servidor = _configuration["ActiveDirectory:Server"];
        var puerto = _configuration.GetValue<int>(
            "ActiveDirectory:Port", 636);
        var dominio = _configuration["ActiveDirectory:DomainNetbios"];

        if (string.IsNullOrWhiteSpace(servidor) ||
            string.IsNullOrWhiteSpace(dominio))
        {
            throw new InvalidOperationException(
                "La configuración de Active Directory está incompleta.");
        }

        var usuario = nombreUsuario.Trim();
        var prefijo = dominio + "\\";

        if (usuario.StartsWith(
            prefijo, StringComparison.OrdinalIgnoreCase))
        {
            usuario = usuario[prefijo.Length..];
        }

        var identificador =
            new LdapDirectoryIdentifier(servidor, puerto);

        using var conexion = new LdapConnection(identificador)
        {
            AuthType = AuthType.Basic,
            Credential = new NetworkCredential(
                $"{dominio}\\{usuario}",
                password),
            Timeout = TimeSpan.FromSeconds(10)
        };

        conexion.SessionOptions.ProtocolVersion = 3;
        conexion.SessionOptions.SecureSocketLayer = true;

        try
        {
            conexion.Bind();
            return true;
        }
        catch (LdapException ex) when (ex.ErrorCode == 49)
        {
            return false;
        }
    }
}