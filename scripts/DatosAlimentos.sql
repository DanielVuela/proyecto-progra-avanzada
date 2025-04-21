USE MacroBalance;
GO

----

ALTER TABLE Objetivo
ALTER COLUMN UsuarioId INT NULL;

ALTER TABLE Dieta
ALTER COLUMN IdUsuario INT NULL;

--- insert objetivos---
INSERT INTO Objetivo (UsuarioId, NombreObjetivo, PesoObjetivo)
VALUES (NULL, 'Ganar masa muscular', 70.0);

INSERT INTO Objetivo (UsuarioId, NombreObjetivo, PesoObjetivo)
VALUES (NULL, 'Mantener peso saludable', 65.0);

INSERT INTO Objetivo (UsuarioId, NombreObjetivo, PesoObjetivo)
VALUES (NULL, 'Aumentar resistencia', 68.0);

INSERT INTO Objetivo (UsuarioId, NombreObjetivo, PesoObjetivo)
VALUES (NULL, 'Controlar azúcar en sangre', 65.0);

---insert dietas--
INSERT INTO Dieta (IdUsuario, Nombre, Descripcion)
VALUES (NULL, 'Dieta alta en proteínas', 'Enfocada en el desarrollo muscular y recuperación.');

INSERT INTO Dieta (IdUsuario, Nombre, Descripcion)
VALUES (NULL, 'Dieta equilibrada', 'Ideal para mantener un peso saludable con alimentos balanceados y naturales.');

INSERT INTO Dieta (IdUsuario, Nombre, Descripcion)
VALUES (NULL, 'Dieta rica en omega 3', 'Enfocada en mejorar salud cardiovascular y resistencia con alimentos ricos en grasas saludables.');

INSERT INTO Dieta (IdUsuario, Nombre, Descripcion)
VALUES (NULL, 'Dieta rica en carbohidratos', 'Ideal para aumentar la resistencia con alimentos energéticos.');

INSERT INTO Dieta (IdUsuario, Nombre, Descripcion)
VALUES (NULL, 'Dieta baja en azúcares simples', 'Recomendada para controlar los niveles de azúcar en sangre con alimentos de bajo índice glucémico.');

----Alimento_dieta alta en proteinas---
INSERT INTO Alimento_Dieta (DietaId, AlimentoId, CantidadDiaria) VALUES 
(15, 4, 200),   -- Pechuga de pollo
(15, 5, 100),   -- Huevo
(15, 6, 150),   -- Arroz integral
(15, 7, 100),   -- Brócoli
(15, 8, 200);   -- Yogur griego

----Alimento alta en proteinas---
INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas) VALUES
('Pechuga de pollo', 'Pechuga de pollo cocida sin piel', 165, 31.0, 0.0, 3.6),
('Huevo', 'Huevo entero cocido', 155, 13.0, 1.1, 11.0),
('Arroz integral', 'Arroz integral cocido', 130, 2.5, 28.0, 1.0),
('Brócoli', 'Brócoli cocido al vapor', 55, 3.7, 11.0, 0.6),
('Yogur griego', 'Yogur griego natural sin azúcar', 59, 10.0, 3.6, 0.4);


----Alimento_dieta equilibrada---
INSERT INTO Alimento_Dieta (DietaId, AlimentoId, CantidadDiaria) VALUES 
(16, 12, 150),  -- Manzana
(16, 13, 100),  -- Pan integral
(16, 14, 250);  -- Leche descremada

----Alimento equilibrada---
INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas) VALUES
('Manzana', 'Manzana roja fresca', 52, 0.3, 14.0, 0.2),
('Pan integral', 'Pan 100% integral', 69, 3.5, 12.0, 1.0),
('Leche descremada', 'Leche baja en grasa', 35, 3.4, 5.0, 0.1);

----Alimento_dieta rica en omega---
INSERT INTO Alimento_Dieta (DietaId, AlimentoId, CantidadDiaria) VALUES 
(17, 15, 180);  -- Salmón

----Alimento rica en omega---
INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas) VALUES
('Salmón', 'Salmón a la plancha', 208, 20.0, 0.0, 13.0);

----Alimento_dieta rica en carbohidratos---
INSERT INTO Alimento_Dieta (DietaId, AlimentoId, CantidadDiaria) VALUES 
(18, 6, 200),   -- Arroz integral
(18, 13, 200);  -- Pan integral

----Alimento rica en carbohidratos---
INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas) VALUES
('Arroz integral', 'Arroz integral cocido', 130, 2.5, 28.0, 1.0),  
('Pan integral', 'Pan 100% integral', 69, 3.5, 12.0, 1.0);          

----Alimento_dieta baja en azucares simples---
INSERT INTO Alimento_Dieta (DietaId, AlimentoId, CantidadDiaria) VALUES 
(19, 16, 150);  -- Frijoles negros

----Alimento baja en azucares---
INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas) VALUES
('Frijoles negros', 'Frijoles negros cocidos', 132, 8.9, 23.7, 0.5);







