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

--------sugerencias de alimentos en el ciclo menstrual-----
---tabla--

CREATE TABLE SugerenciaCicloMenstrual (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fase NVARCHAR(100),
    AlimentosRecomendados NVARCHAR(MAX),
    Beneficios NVARCHAR(MAX),
    SintomasRelacioandos NVARCHAR(MAX)
);

----inserts de sugerencias ciclo mentrual---

INSERT INTO SugerenciaCicloMenstrual (Fase, AlimentosRecomendados, Beneficios, SintomasRelacioandos)
VALUES
('Fase Menstrual',
 'Verduras de hoja verde, lentejas, hígado, frutas con vitamina C',
 'Ayuda a reponer hierro, combatir la fatiga y regular el estado de ánimo',
 'Fatiga, cólicos, cambios de humor'),

('Fase Folicular',
 'Avena, plátano, salmón, semillas de chía',
 'Favorece la energía, regula hormonas, mejora el enfoque',
 'Baja energía, niebla mental'),

('Ovulación',
 'Huevos, vegetales crucíferos, bayas, nueces',
 'Optimiza la ovulación, antioxidantes, equilibrio hormonal',
 'Sensibilidad mamaria, irritabilidad'),

('Fase Lútea',
 'Chocolate negro, semillas de girasol, pavo, batata',
 'Reduce antojos, ayuda a estabilizar el estado de ánimo',
 'Síntomas premenstruales, antojos, ansiedad');


 -- Tabla: SugerenciaBase
CREATE TABLE SugerenciaBase (
    Id INT PRIMARY KEY IDENTITY(1,1),
    EstadoRelacionado NVARCHAR(100) NOT NULL, 
    Descripcion NVARCHAR(500) NOT NULL
);

select * from Usuario;

INSERT INTO SugerenciaBase (EstadoRelacionado, Descripcion)
VALUES
-- Ansiedad
('Ansiedad', 'Consumí alimentos ricos en magnesio como nueces, espinaca o chocolate negro.'),
('Ansiedad', 'Evitá bebidas con cafeína y preferí infusiones relajantes como manzanilla o lavanda.'),

-- Tristeza
('Tristeza', 'Comé alimentos ricos en triptófano como plátano, avena o huevos.'),
('Tristeza', 'Aumentá el consumo de omega-3 mediante salmón, semillas de lino o nueces.'),

-- Felicidad
('Felicidad', 'Mantené una dieta balanceada para prolongar tu bienestar.'),
('Felicidad', 'Incorporá frutas frescas y agua para sostener tu energía positiva.'),

-- Estrés
('Estrés', 'Consumí avena, frutos secos y vegetales verdes para estabilizar el sistema nervioso.'),
('Estrés', 'Tomá agua en abundancia y evitá azúcar refinada.'),

-- Irritabilidad
('Irritabilidad', 'Reducí alimentos ultraprocesados y aumentá vegetales frescos.'),
('Irritabilidad', 'Comé chocolate oscuro (mínimo 70%) y magnesio para mejorar el ánimo.'),

-- Calma
('Calma', 'Mantené una dieta rica en fibra, agua y grasas saludables.'),
('Calma', 'Elegí alimentos integrales y livianos que te mantengan en equilibrio.'),

-- Motivación
('Motivación', 'Incluí proteínas magras, cereales integrales y frutas para sostener tu energía.'),
('Motivación', 'Tomá agua frecuentemente y evitá el exceso de carbohidratos simples.');





---tabla receta-----
CREATE TABLE TipoReceta (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255)
);

-- Inserts dieta
INSERT INTO TipoReceta (Nombre, Descripcion) VALUES
('Dieta Vegana', 'Excluye todos los productos de origen animal.'),
('Vegetariana', 'Sin carnes, incluye lácteos y huevos.'),
('Alta en Proteínas', 'Enfatiza el consumo de alimentos proteicos.'),
('Sin Gluten', 'Libre de trigo, cebada y centeno.'),
('Baja en Calorías', 'Ideal para reducción de peso.');




----inserts alimentos--
--  Dieta Vegana
INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas)
VALUES 
('Tofu', 'Tofu firme bajo en grasa', 144.00, 15.00, 3.90, 8.00),
('Garbanzos', 'Garbanzos cocidos', 164.00, 8.90, 27.40, 2.60);

-- Vegetariana
INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas)
VALUES 
('Lentejas', 'Lentejas cocidas', 116.00, 9.00, 20.00, 0.40),
('Quinoa', 'Quinoa cocida', 120.00, 4.10, 21.30, 1.90);

-- Alta en Proteínas
INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas)
VALUES 
('Pechuga de pollo', 'Pechuga cocida sin piel', 165.00, 31.00, 0.00, 3.60),
('Claras de huevo', 'Claras cocidas', 52.00, 11.00, 0.70, 0.20);

--  Sin Gluten
INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas)
VALUES 
('Arroz integral', 'Arroz integral cocido', 130.00, 2.50, 28.00, 1.00),
('Papa cocida', 'Papa hervida sin cáscara', 87.00, 2.00, 20.10, 0.10);

--  Baja en Calorías
INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas)
VALUES 
('Espinaca', 'Espinaca cocida', 23.00, 2.90, 3.80, 0.30),
('Pepino', 'Pepino con cáscara', 16.00, 0.70, 3.60, 0.10);

INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas) VALUES
('Banano', 'Banano maduro', 89.00, 1.10, 23.00, 0.30),
('Fresa', 'Fresas frescas', 33.00, 0.70, 8.00, 0.30),
('Piña', 'Piña fresca en trozos', 50.00, 0.50, 13.00, 0.10),
('Zanahoria', 'Zanahoria cocida', 35.00, 0.80, 8.00, 0.20),
('Repollo', 'Repollo cocido', 23.00, 1.00, 5.00, 0.10),
('Atún', 'Atún en agua', 132.00, 28.00, 0.00, 1.00),
('Tempeh', 'Fermentado de soya', 195.00, 20.00, 9.00, 11.00),
('Carne magra', 'Res cocida sin grasa', 250.00, 31.00, 0.00, 15.00),
('Frijoles negros', 'Frijoles cocidos sin sal', 123.00, 8.80, 22.00, 0.50),
('Pan integral', 'Rebanada de pan integral', 69.00, 2.50, 12.00, 1.00);





---tabla AlimentoTipoReceta---

CREATE TABLE AlimentoTipoReceta (
    Id INT IDENTITY(1,1) PRIMARY KEY, 
    AlimentoId INT NOT NULL,
    TipoRecetaId INT NOT NULL,
    FOREIGN KEY (AlimentoId) REFERENCES Alimento(Id),
    FOREIGN KEY (TipoRecetaId) REFERENCES TipoReceta(Id)
);




-- insert alimentostiporeceta--
-- Dieta Vegana 
INSERT INTO AlimentoTipoReceta (AlimentoId, TipoRecetaId) VALUES (1, 1); -- Tofu
INSERT INTO AlimentoTipoReceta (AlimentoId, TipoRecetaId) VALUES (2, 1); -- Garbanzos

--Vegetariana 
INSERT INTO AlimentoTipoReceta (AlimentoId, TipoRecetaId) VALUES (3, 2); -- Lentejas
INSERT INTO AlimentoTipoReceta (AlimentoId, TipoRecetaId) VALUES (4, 2); -- Quinoa

-- Alta en Proteinas 
INSERT INTO AlimentoTipoReceta (AlimentoId, TipoRecetaId) VALUES (5, 3); -- Pechuga de pollo
INSERT INTO AlimentoTipoReceta (AlimentoId, TipoRecetaId) VALUES (6, 3); -- Claras de huevo

-- Sin Gluten 
INSERT INTO AlimentoTipoReceta (AlimentoId, TipoRecetaId) VALUES (7, 4); -- Arroz integral
INSERT INTO AlimentoTipoReceta (AlimentoId, TipoRecetaId) VALUES (8, 4); -- Papa cocida

-- Baja en Calorias 
INSERT INTO AlimentoTipoReceta (AlimentoId, TipoRecetaId) VALUES (9, 5); -- Espinaca
INSERT INTO AlimentoTipoReceta (AlimentoId, TipoRecetaId) VALUES (10, 5); -- Pepino

-- Frutas y vegetales (baja en calorias y vegetariana)
INSERT INTO AlimentoTipoReceta (AlimentoId, TipoRecetaId) VALUES
(11, 5), ----Banano 
(12, 5), -- Fresa
(13, 5), -- Piña
(14, 5), -- Zanahoria
(15, 2), -- Repollo

-- Proteinas (alta en proteinas, sin gluten)
(16, 3), -- Atun 
(16, 4), -- Atun 
(17, 1), -- Tempeh 
(17, 3), -- Tempeh 
(18, 3), -- Carne magra

-- Legumbres y cereales
(19, 1), -- Frijoles 
(19, 2), -- Frijoles 
(20, 2), -- Pan integral 
(20, 4); -- Pan integral 

CREATE TABLE EtiquetaNutricional (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Imagen NVARCHAR(255),
    AlimentoId INT NOT NULL,
    TamanoPorcion NVARCHAR(100),
    Calorias DECIMAL(5,2) NULL, -- Permite null
    PorcionesPorEnvase DECIMAL(4,2) NULL, -- Permite null
    Sodio DECIMAL(6,2) NULL, -- Permite null
    Azucares DECIMAL(5,2) NULL, -- Permite null
    FibraDietetica DECIMAL(5,2) NULL, -- Permite null
    VitaminaA DECIMAL(5,2) NULL, -- Permite null
    VitaminaC DECIMAL(5,2) NULL, -- Permite null
    Calcio DECIMAL(5,2) NULL, -- Permite null
    Hierro DECIMAL(5,2) NULL, -- Permite null
    FOREIGN KEY (AlimentoId) REFERENCES Alimento(Id)
);

------Inserts Etiquetas 
INSERT INTO EtiquetaNutricional (
    Nombre, Imagen, AlimentoId, TamanoPorcion, Calorias, PorcionesPorEnvase, Sodio, Azucares,
    FibraDietetica, VitaminaA, VitaminaC, Calcio, Hierro
)
VALUES
('Mantequilla de Maní',
 'https://karayfoods.com/wp-content/uploads/2021/02/tabla-nutricional-mantequilla-de-mani-001.jpg',
 1, '32g', 190.00, 1, 140.00, 3.00, 2.00, 0.00, 0.00, 0.00, 1.00),

('Cereal de Maíz',
 'https://th.bing.com/th/id/OIP.L5BfZYsdR224SqdFMJP23wAAAA?w=445&h=445&rs=1&pid=ImgDetMain',
 2, '100g', 370.00, 1, 150.00, 10.00, 5.00, 0.00, 0.00, 0.00, 20.00),

('Leche Descremada',
 'https://th.bing.com/th/id/OIP.NbPgN1L7r6iU8k6lpPmHAAHaHa?rs=1&pid=ImgDetMain',
 3, '1 taza (240ml)', 90.00, 1, 100.00, 12.00, 0.00, 10.00, 2.00, 10.00, 10.00),

('Galletas de Avena',
 'https://hebmx.vtexassets.com/arquivos/ids/787065/327048_3-1679016940.jpg?v=638243501181300000',
 4, '1 galleta (30g)', 150.00, 10, 100.00, 5.00, 2.00, 1.00, 5.00, 3.00, 3.00),

('Yogur Natural',
 'https://th.bing.com/th/id/OIP.kz2IDBzF1DjsWazE-HshJQHaHr?w=1000&h=1037&rs=1&pid=ImgDetMain',
 5, '125g', 80.00, 1, 50.00, 4.00, 0.00, 5.00, 0.00, 170.00, 4.00),

('Jugo de Naranja',
 'https://res.cloudinary.com/walmart-labs/image/upload/d_default.jpg/w_960,dpr_auto,f_auto,q_auto:best/gr/images/product-images/img_large/00750105241446L1.jpg?odnHeight=80&odnWidth=80&odnBg=FFFFFF',
 6, '1 taza (240ml)', 306.00, 1, 2.00, 23.00, 0.00, 0.00, 60.00, 0.00, 4.00),

('Pan Integral',
 'https://th.bing.com/th/id/OIP.iDHhBIDf8u7XinE3lvIWWgHaH5?rs=1&pid=ImgDetMain',
 7, '1 rebanada (40g)', 90.00, 1, 150.00, 2.00, 4.00, 1.00, 4.00, 0.00, 1.50),

('Atún en Agua',
 'https://th.bing.com/th/id/OIP.7HBZxjeHwar0yyJPsfBLowHaHa?rs=1&pid=ImgDetMain',
 8, '1 lata (140g)', 160.00, 1, 100.00, 0.00, 35.00, 0.00, 0.00, 0.00, 6.00),

('Sopa de Verduras',
 'https://th.bing.com/th/id/OIP.92xprT20ZhQe1sPdW1oJxwAAAA?rs=1&pid=ImgDetMain',
 9, '1 taza (240ml)', 100.00, 1, 800.00, 5.00, 3.00, 1.00, 2.00, 3.00, 5.00),

('Chocolate Oscuro',
 'https://th.bing.com/th/id/OIP.VA7uhWZ7CXD59mjkwGt9GgAAAA?rs=1&pid=ImgDetMain',
 10, '30g', 170.00, 1, 10.00, 15.00, 13.00, 0.00, 2.00, 5.00, 1.00);
