using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;

namespace VisualizadorEstacionesClever.Api.Services;

public class PruebaLdapsService
{
    private readonly IConfiguration _configuration;

    public PruebaLdapsService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> ProbarConexionAsync()
    {
        var servidor = _configuration["ActiveDirectory:Server"];
        var puerto = _configuration.GetValue<int>(
            "ActiveDirectory:Port", 636);

        using var tcp = new TcpClient();

        await tcp.ConnectAsync(servidor!, puerto);

        using var ssl = new SslStream(tcp.GetStream(), false);

        await ssl.AuthenticateAsClientAsync(servidor!);

        return $"Conexión LDAPS exitosa con {servidor}. " +
               $"Protocolo TLS: {ssl.SslProtocol}";
    }
}