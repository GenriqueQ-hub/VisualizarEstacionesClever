USE [master];
GO

CREATE DATABASE [VisualizadorEstacionesClever];
GO

USE [VisualizadorEstacionesClever];
GO

/* 1. Roles de acceso */
CREATE TABLE dbo.Roles
(
    IdRol       INT IDENTITY(1,1) NOT NULL,
    Nombre      NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(200) NULL,

    CONSTRAINT PK_Roles PRIMARY KEY (IdRol),
    CONSTRAINT UQ_Roles_Nombre UNIQUE (Nombre)
);
GO

/* 2. Usuarios del portal */
CREATE TABLE dbo.Usuarios
(
    IdUsuario      INT IDENTITY(1,1) NOT NULL,
    IdRol          INT NOT NULL,
    Nombre         NVARCHAR(100) NOT NULL,
    Apellido       NVARCHAR(100) NULL,
    Correo         NVARCHAR(254) NOT NULL,
    NombreUsuario  NVARCHAR(100) NOT NULL,
    Password	   NVARCHAR(500) NULL,
    Activo         BIT NOT NULL CONSTRAINT DF_Usuarios_Activo DEFAULT (1),
    FechaCreacion  DATETIME2(0) NOT NULL CONSTRAINT DF_Usuarios_FechaCreacion DEFAULT (SYSUTCDATETIME()),

    CONSTRAINT PK_Usuarios PRIMARY KEY (IdUsuario),
    CONSTRAINT FK_Usuarios_Roles FOREIGN KEY (IdRol)
        REFERENCES dbo.Roles (IdRol),
    CONSTRAINT UQ_Usuarios_Correo UNIQUE (Correo),
    CONSTRAINT UQ_Usuarios_NombreUsuario UNIQUE (NombreUsuario)
);
GO

/* 3. Líneas de producción */
CREATE TABLE dbo.Lineas
(
    IdLinea     INT IDENTITY(1,1) NOT NULL,
    Nombre      NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(200) NULL,
    Activa      BIT NOT NULL CONSTRAINT DF_Lineas_Activa DEFAULT (1),

    CONSTRAINT PK_Lineas PRIMARY KEY (IdLinea),
    CONSTRAINT UQ_Lineas_Nombre UNIQUE (Nombre)
);
GO

/* 4. Estaciones que se mostrarán en el portal */
CREATE TABLE dbo.Estaciones
(
    IdEstacion   INT IDENTITY(1,1) NOT NULL,
    IdLinea      INT NOT NULL,
    NumeroClever CHAR(2) NOT NULL,
    Descripcion  NVARCHAR(200) NOT NULL,
    Url          NVARCHAR(500) NULL,
    IP           VARCHAR(45) NOT NULL,
    Activa       BIT NOT NULL CONSTRAINT DF_Estaciones_Activa DEFAULT (1),
    FechaCreacion DATETIME2(0) NOT NULL CONSTRAINT DF_Estaciones_FechaCreacion DEFAULT (SYSUTCDATETIME()),

    CONSTRAINT PK_Estaciones PRIMARY KEY (IdEstacion),
    CONSTRAINT FK_Estaciones_Lineas FOREIGN KEY (IdLinea)
        REFERENCES dbo.Lineas (IdLinea)
);
GO

/* Roles iniciales */
INSERT INTO dbo.Roles (Nombre, Descripcion)
VALUES
    ('Administrador','Administra usuarios, líneas y estaciones.'),
    ('Consulta', 'Consulta la información autorizada.');
GO


/* Registro de errores*/
CREATE TABLE dbo.LogsErrores
(
    IdLog INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NULL,
    DetalleError NVARCHAR(MAX) NOT NULL,
    Modulo NVARCHAR(100) NOT NULL,

    FechaHora DATETIME2(0) NOT NULL
        CONSTRAINT DF_LogsErrores_FechaHora
        DEFAULT SYSDATETIME(),

    CONSTRAINT FK_LogsErrores_Usuarios
        FOREIGN KEY (IdUsuario)
        REFERENCES dbo.Usuarios(IdUsuario)
);

CREATE INDEX IX_LogsErrores_FechaHora
ON dbo.LogsErrores(FechaHora);


/************************************/
/*modificaciones*/

USE [VisualizadorEstacionesClever];
GO

ALTER TABLE dbo.Usuarios
ADD UsuarioAD NVARCHAR(150) NULL;
GO

CREATE UNIQUE INDEX UX_Usuarios_UsuarioAD
ON dbo.Usuarios(UsuarioAD)
WHERE UsuarioAD IS NOT NULL;
GO

execute sp_helpindex 'dbo.Usuarios';
