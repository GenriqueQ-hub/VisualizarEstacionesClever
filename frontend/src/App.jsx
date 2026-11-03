
import { useState } from "react";
import Login from "./Login";
import Inicio from "./Inicio";

function App() {
  const [sesion, setSesion] = useState(null);

  function cerrarSesion() {
    setSesion(null);
  }

  if (!sesion) {
    return (
      <Login
        onLogin={(datosUsuario) => setSesion(datosUsuario)}
      />
    );
  }

  return (
    <Inicio
      rol={sesion.rol}
      usuario={sesion.usuario}
      onLogout={cerrarSesion}
    />
  );
}

export default App;
