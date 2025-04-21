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

---pasos arreglo--
UPDATE Dieta SET ObjetivoId = NULL WHERE ObjetivoId NOT IN (SELECT Id FROM Objetivo);

----------aqui se asocia cada dieta al objetivo correspondiente------


---ver si hay duplicados--

DELETE FROM Objetivo 
WHERE NombreObjetivo = 'Ganar masa muscular' AND Id <> 14;

DELETE FROM Objetivo 
WHERE NombreObjetivo = 'Mantener peso saludable' AND Id <> 16;

DELETE FROM Objetivo 
WHERE NombreObjetivo = 'Aumentar resistencia' AND Id <> 17;

DELETE FROM Objetivo 
WHERE NombreObjetivo = 'Controlar azúcar en sangre' AND Id <> 19;



DELETE FROM Alimento_Dieta WHERE DietaId = 15;
DELETE FROM Alimento_Dieta WHERE DietaId = 16;
DELETE FROM Alimento_Dieta WHERE DietaId = 17;
DELETE FROM Alimento_Dieta WHERE DietaId = 18;
DELETE FROM Alimento_Dieta WHERE DietaId = 19;

-----enlazar las dietas con los objetivos actualizados -
-- "Dieta alta en proteínas" ? "Ganar masa muscular"
UPDATE Dieta
SET ObjetivoId = 14
WHERE Nombre = 'Dieta alta en proteínas';

-- "Dieta equilibrada" ? "Mantener peso saludable"
UPDATE Dieta
SET ObjetivoId = 16
WHERE Nombre = 'Dieta equilibrada';

-- "Dieta rica en omega 3" ? "Aumentar resistencia"
UPDATE Dieta
SET ObjetivoId = 17
WHERE Nombre = 'Dieta rica en omega 3';

-- "Dieta rica en carbohidratos" ? "Aumentar resistencia"
UPDATE Dieta
SET ObjetivoId = 17
WHERE Nombre = 'Dieta rica en carbohidratos';

-- "Dieta baja en azúcares simples" ? "Controlar azúcar en sangre"
UPDATE Dieta
SET ObjetivoId = 19
WHERE Nombre = 'Dieta baja en azúcares simples';


----Alimento_dieta alta en proteinas---
INSERT INTO Alimento_Dieta (DietaId, AlimentoId, CantidadDiaria) VALUES 
(14, 4, 200),   -- Pechuga de pollo
(14, 5, 100),   -- Huevo
(14, 6, 150),   -- Arroz integral
(14, 7, 100),   -- Brócoli
(14, 8, 200);   -- Yogur griego

----Alimento alta en proteinas---
INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas) VALUES
('Pechuga de pollo', 'Pechuga de pollo cocida sin piel', 165, 31.0, 0.0, 3.6),
('Huevo', 'Huevo entero cocido', 155, 13.0, 1.1, 11.0),
('Arroz integral', 'Arroz integral cocido', 130, 2.5, 28.0, 1.0),
('Brócoli', 'Brócoli cocido al vapor', 55, 3.7, 11.0, 0.6),
('Yogur griego', 'Yogur griego natural sin azúcar', 59, 10.0, 3.6, 0.4);

DELETE FROM Alimento_Dieta WHERE DietaId = 16;

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
(17, 6, 200),   -- Arroz integral
(17, 13, 200);  -- Pan integral

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









