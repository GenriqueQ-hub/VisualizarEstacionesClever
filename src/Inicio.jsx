
import { useState } from "react";
import "./Inicio.css";

// Opciones disponibles en el portal
const menus = [
  { id: "inicio", nombre: "Inicio", icono: "⌂" },
  { id: "lineas", nombre: "Administrar líneas", icono: "☷" },
  { id: "estaciones", nombre: "Administrar estaciones", icono: "▣" },
  { id: "asignaciones", nombre: "Asignar estaciones", icono: "⇄" },
  { id: "visualizar", nombre: "Visualizar estaciones", icono: "▤" },
  { id: "usuarios", nombre: "Administrar usuarios", icono: "♙" },
];

// Filtra las opciones según el rol del usuario
function obtenerMenusPermitidos(rol) {
  if (rol === "Operador") {
    return menus.filter((menu) => menu.id === "visualizar");
  }

  return menus;
}

function Inicio({ onLogout, rol, usuario }) {
  const menusPermitidos = obtenerMenusPermitidos(rol);

  const [menuActivo, setMenuActivo] = useState(
    rol === "Operador" ? "visualizar" : "inicio"
  );

  const [sidebarColapsada, setSidebarColapsada] = useState(false);

  const menuSeleccionado = menusPermitidos.find(
    (menu) => menu.id === menuActivo
  );

  function seleccionarMenu(id) {
    // Evita seleccionar opciones que el rol no tiene permitidas
    if (menusPermitidos.some((menu) => menu.id === id)) {
      setMenuActivo(id);
    }
  }

  function manejarCerrarSesion() {
    if (typeof onLogout === "function") {
      onLogout();
    } else {
      alert("La función de cerrar sesión aún no está configurada.");
    }
  }

  return (
    <div
      className={`portal ${
        sidebarColapsada ? "portal-colapsado" : ""
      }`}
    >
      <aside
        className={`sidebar ${
          sidebarColapsada ? "sidebar-colapsada" : ""
        }`}
      >
        <div className="sidebar-cabecera">
          <div className="sidebar-marca">
            <img
              className="sidebar-logo"
              src="/images/logo.png"
              alt="Logo de la empresa"
            />

            {!sidebarColapsada && (
              <div className="sidebar-marca-texto">
                <span className="sidebar-titulo">
                  Visualizar estaciones
                </span>
                <span className="sidebar-subtitulo">
                  clever
                </span>
              </div>
            )}
          </div>

          <button
            type="button"
            className="sidebar-boton-colapsar"
            onClick={() =>
              setSidebarColapsada((estado) => !estado)
            }
            aria-label={
              sidebarColapsada
                ? "Expandir barra lateral"
                : "Contraer barra lateral"
            }
            title={
              sidebarColapsada
                ? "Expandir menú"
                : "Contraer menú"
            }
          >
            {sidebarColapsada ? "»" : "«"}
          </button>
        </div>

        <div className="sidebar-separador" />

        <nav
          className="sidebar-navegacion"
          aria-label="Menú principal"
        >
          {!sidebarColapsada && (
            <span className="sidebar-etiqueta">
              MENÚ PRINCIPAL
            </span>
          )}

          {menusPermitidos.map((menu) => (
            <button
              key={menu.id}
              type="button"
              className={`sidebar-enlace ${
                menuActivo === menu.id ? "activo" : ""
              }`}
              onClick={() => seleccionarMenu(menu.id)}
              title={sidebarColapsada ? menu.nombre : undefined}
              aria-label={menu.nombre}
              aria-current={
                menuActivo === menu.id ? "page" : undefined
              }
            >
              <span className="sidebar-icono" aria-hidden="true">
                {menu.icono}
              </span>

              {!sidebarColapsada && (
                <span className="sidebar-nombre">
                  {menu.nombre}
                </span>
              )}

              {menuActivo === menu.id && (
                <span className="sidebar-indicador" />
              )}
            </button>
          ))}
        </nav>

        <div className="sidebar-pie">
          <div className="sidebar-separador" />

          <button
            type="button"
            className="sidebar-enlace sidebar-salir"
            onClick={manejarCerrarSesion}
            title={sidebarColapsada ? "Cerrar sesión" : undefined}
            aria-label="Cerrar sesión"
          >
            <span className="sidebar-icono" aria-hidden="true">
              ↪
            </span>

            {!sidebarColapsada && (
              <span className="sidebar-nombre">
                Cerrar sesión
              </span>
            )}
          </button>

          {!sidebarColapsada && (
            <div className="sidebar-version">
              Design by: IT Department Motherson
            </div>
          )}
        </div>
      </aside>

      <main className="contenido-principal">
        <div className="contenido-encabezado">
          <div>
            <p className="texto-superior">PANEL PRINCIPAL</p>

            <h1>
              {menuSeleccionado?.nombre ?? "Inicio"}
            </h1>

            <p className="contenido-descripcion">
              Bienvenido a Visualizar estaciones clever.
            </p>
          </div>
        </div>

        {menuActivo === "inicio" && rol !== "Operador" ? (
          <section className="tarjeta-bienvenida">
            <h2>Bienvenido al portal</h2>

            <p>
              Selecciona una opción de la barra lateral para comenzar
              a trabajar con las líneas, estaciones y usuarios.
            </p>
          </section>
        ) : (
          <section className="tarjeta-bienvenida">
            <h2>{menuSeleccionado?.nombre}</h2>

            <p>
              {menuActivo === "visualizar"
                ? "Desde aquí podrás consultar las estaciones disponibles."
                : `Esta sección está preparada para su desarrollo. Aquí incorporaremos las funciones de ${
                    menuSeleccionado?.nombre.toLowerCase() ?? "inicio"
                  }.`}
            </p>
          </section>
        )}
      </main>
    </div>
  );
}

export default Inicio;
