
import { useState } from "react";
import "./Login.css";

function Login({ onLogin }) {
  const [usuario, setUsuario] = useState("");
  const [password, setPassword] = useState("");

  function manejarEnvio(evento) {
    evento.preventDefault();

    const nombreUsuario = usuario.trim().toLowerCase();

    if (nombreUsuario === "admin" && password === "admin") {
      onLogin({ usuario: "admin", rol: "Administrador" });
    } else if (
      nombreUsuario === "operador" &&
      password === "operador"
    ) {
      onLogin({ usuario: "operador", rol: "Operador" });
    } else {
      alert("Usuario o contraseña incorrectos.");
    }
  }

  return (
    <main className="login-container">
      <form className="login-card" onSubmit={manejarEnvio}>
        <div className="login-logo-contenedor">
          <img
            src="/images/logo.png"
            alt="Motherson"
            className="login-logo"
          />
        </div>

        <h1>Visualizador de estaciones clever</h1>
        <p>Inicia sesión para continuar</p>

        <label htmlFor="usuario">Nombre de usuario</label>
        <input
          id="usuario"
          type="text"
          value={usuario}
          onChange={(e) => setUsuario(e.target.value)}
          autoComplete="username"
          required
        />

        <label htmlFor="password">Contraseña</label>
        <input
          id="password"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          autoComplete="current-password"
          required
        />

        <button type="submit">Iniciar sesión</button>
      </form>
    </main>
  );
}

export default Login;
