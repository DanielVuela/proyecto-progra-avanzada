CREATE DATABASE MacroBalance;
GO

USE MacroBalance;
GO

-- Tabla: Usuario
CREATE TABLE Usuario (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    Apellidos NVARCHAR(100),
    FotoDePerfilUrl NVARCHAR(255),
    FechaDeNacimiento DATE,
    Peso DECIMAL(5,2),
    Altura DECIMAL(5,2),
    CorreoElectronico NVARCHAR(100) UNIQUE,
    Contrasena NVARCHAR(255),
    Rol NVARCHAR(50),
    NivelActividadFisica NVARCHAR(50),
    Genero NVARCHAR(50)
);

-- Tabla: EstadoEmocional
CREATE TABLE EstadoEmocional (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT FOREIGN KEY REFERENCES Usuario(Id),
    Descripcion NVARCHAR(255),
    Estado NVARCHAR(100),
    Fecha DATE
);

-- Tabla: Sugerencia
CREATE TABLE Sugerencia (
    Id INT PRIMARY KEY IDENTITY(1,1),
    EstadoId INT FOREIGN KEY REFERENCES EstadoEmocional(Id),
    Descripcion NVARCHAR(255)
);

-- Tabla: Recordatorios
CREATE TABLE Recordatorios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT FOREIGN KEY REFERENCES Usuario(Id),
    Frecuencia NVARCHAR(50),
    Hora TIME,
    Nombre NVARCHAR(100)
);

-- Tabla: Progreso
CREATE TABLE Progreso (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT FOREIGN KEY REFERENCES Usuario(Id),
    Fecha DATE,
    Peso DECIMAL(5,2),
    Cintura DECIMAL(5,2),
    Cadera DECIMAL(5,2),
    Pecho DECIMAL(5,2)
);

-- Tabla: Ejercicio
CREATE TABLE Ejercicio (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    CaloriasQuemadas DECIMAL(6,2),
    Descripcion NVARCHAR(255)
);

-- Tabla: EjercicioRealizado
CREATE TABLE EjercicioRealizado (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT FOREIGN KEY REFERENCES Usuario(Id),
    EjercicioId INT FOREIGN KEY REFERENCES Ejercicio(Id),
    Fecha DATE
);

-- Tabla: PreferenciaAlimenticia
CREATE TABLE PreferenciaAlimenticia (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT FOREIGN KEY REFERENCES Usuario(Id),
    Preferencia NVARCHAR(100)
);

-- Tabla: Objetivo
CREATE TABLE Objetivo (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT FOREIGN KEY REFERENCES Usuario(Id),
    NombreObjetivo NVARCHAR(100),
    PesoObjetivo DECIMAL(5,2)
);

-- Tabla: Dieta
CREATE TABLE Dieta (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT FOREIGN KEY REFERENCES Usuario(Id),
    Nombre NVARCHAR(100),
    Descripcion NVARCHAR(255)
);

-- Tabla: Alimento
CREATE TABLE Alimento (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    Descripcion NVARCHAR(255),
    Calorias DECIMAL(6,2),
    Proteina DECIMAL(5,2),
    Carbohidratos DECIMAL(5,2),
    Grasas DECIMAL(5,2)
);

-- Tabla: Alimento_Dieta
CREATE TABLE Alimento_Dieta (
    DietaId INT,
    AlimentoId INT,
    CantidadDiaria DECIMAL(5,2),
    PRIMARY KEY (DietaId, AlimentoId),
    FOREIGN KEY (DietaId) REFERENCES Dieta(Id),
    FOREIGN KEY (AlimentoId) REFERENCES Alimento(Id)
);

-- Tabla: RegistroAlimento
CREATE TABLE RegistroAlimento (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DietaId INT FOREIGN KEY REFERENCES Dieta(Id),
    AlimentoId INT FOREIGN KEY REFERENCES Alimento(Id),
    Fecha DATE,
    Cantidad DECIMAL(5,2)
);

CREATE TABLE LogErrores (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FechaHora DATETIME DEFAULT GETDATE(),
    UsuarioId INT NULL, -- Puede ser NULL si el error ocurre antes del login
    Modulo NVARCHAR(100) NOT NULL, -- Ej: 'Login', 'RegistroAlimento', 'API/Dieta'
    MensajeError NVARCHAR(1000) NOT NULL,
    Stacktrace NVARCHAR(MAX) NULL,
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id)
);


-- Tabla: LogErrores
CREATE TABLE LogErrores (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FechaHora DATETIME DEFAULT GETDATE(),
    UsuarioId INT NULL, -- Puede ser NULL si el error ocurre antes del login
    Modulo NVARCHAR(100) NOT NULL, -- Ej: 'Login', 'RegistroAlimento', 'API/Dieta'
    MensajeError NVARCHAR(1000) NOT NULL,
    Stacktrace NVARCHAR(MAX) NULL
);


---------------
-- cambio para perfil dieta
---------------

CREATE TABLE PerfilDieta (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    CaloriasObjetivo DECIMAL(6,2),
    Proteinas DECIMAL(6,2),
    Carbohidratos DECIMAL(6,2),
    Grasas DECIMAL(6,2),
    Agua DECIMAL(6,2),
    Ejercicio DECIMAL(6,2),
    FechaAsignacion DATETIME DEFAULT GETDATE()
);

ALTER TABLE PerfilDieta
ADD CONSTRAINT FK_PerfilDieta_Usuario
FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id);