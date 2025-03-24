USE MacroBalance;
GO

ALTER TABLE Usuario
ADD Genero NVARCHAR(50) NULL;
GO

SELECT * FROM Alimento_Dieta;


---Ganar masa muscular
SELECT * FROM Dieta WHERE Nombre LIKE '%Dieta alta en proteínas%';

INSERT INTO Objetivo (UsuarioId, NombreObjetivo, PesoObjetivo)
VALUES (5, 'Ganar masa muscular', 70.00);

INSERT INTO Dieta (IdUsuario, Nombre, Descripcion)
VALUES (5, 'Dieta alta en proteínas', 'Enfocada en el desarrollo muscular y recuperación.');

INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas)
VALUES ('Pechuga de pollo', 'Pechuga de pollo cocida sin piel', 165, 31.0, 0.0, 3.6);

INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas)
VALUES ('Huevo', 'Huevo entero cocido', 155, 13.0, 1.1, 11.0);

INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas)
VALUES ('Arroz integral', 'Arroz integral cocido', 130, 2.5, 28.0, 1.0);

INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas)
VALUES ('Brócoli', 'Brócoli cocido al vapor', 55, 3.7, 11.0, 0.6);

INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas)
VALUES ('Yogur griego', 'Yogur griego natural sin azúcar', 59, 10.0, 3.6, 0.4);


INSERT INTO Alimento_Dieta (DietaId, AlimentoId, CantidadDiaria)
VALUES (4, 4, 200); 

INSERT INTO Alimento_Dieta (DietaId, AlimentoId, CantidadDiaria)
VALUES (4, 5, 100); 

INSERT INTO Alimento_Dieta (DietaId, AlimentoId, CantidadDiaria)
VALUES (4, 6, 150); 

INSERT INTO Alimento_Dieta (DietaId, AlimentoId, CantidadDiaria)
VALUES (4, 7, 100); 

INSERT INTO Alimento_Dieta (DietaId, AlimentoId, CantidadDiaria)
VALUES (4, 8, 200); 





----Mantener peso saludable

INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas)
VALUES ('Manzana', 'Manzana roja fresca', 52, 0.3, 14.0, 0.2);

INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas)
VALUES ('Pan integral', 'Pan 100% integral', 69, 3.5, 12.0, 1.0);

INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas)
VALUES ('Leche descremada', 'Leche baja en grasa', 35, 3.4, 5.0, 0.1);

INSERT INTO Objetivo (UsuarioId, NombreObjetivo, PesoObjetivo)
VALUES (5, 'Mantener peso saludable', 65.00);

INSERT INTO Dieta (IdUsuario, Nombre, Descripcion)
VALUES (5, 'Dieta equilibrada', 'Ideal para mantener un peso saludable con alimentos balanceados y naturales.');

INSERT INTO Alimento_Dieta (DietaId, AlimentoId, CantidadDiaria)
VALUES 
(7, 12, 150),  
(7, 13, 100),  
(7, 14, 250);  



----Aumentar resistencia
INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas)
VALUES ('Salmón', 'Salmón a la plancha', 208, 20.0, 0.0, 13.0);

INSERT INTO Objetivo (UsuarioId, NombreObjetivo, PesoObjetivo)
VALUES (5, 'Aumentar resistencia', 68.00);

INSERT INTO Dieta (IdUsuario, Nombre, Descripcion)
VALUES (5, 'Dieta rica en omega 3', 'Enfocada en mejorar salud cardiovascular y resistencia con alimentos ricos en grasas saludables.');

INSERT INTO Alimento_Dieta (DietaId, AlimentoId, CantidadDiaria)
VALUES (9, 15, 180);

SELECT * FROM Objetivo WHERE NombreObjetivo = 'Aumentar resistencia';

INSERT INTO Dieta (IdUsuario, Nombre, Descripcion)
VALUES (5, 'Dieta rica en carbohidratos', 'Ideal para aumentar la resistencia con alimentos energéticos.');

INSERT INTO Alimento_Dieta (DietaId, AlimentoId, CantidadDiaria)
VALUES
(9, 15, 250); 

SELECT * FROM Dieta WHERE Nombre LIKE '%rica en carbohidratos%';

SELECT * FROM Alimento;

INSERT INTO Alimento_Dieta (DietaId, AlimentoId, CantidadDiaria)
VALUES
(11, 6, 200),
(11, 13, 200);  


----dietas bajas en azucar
INSERT INTO Alimento (Nombre, Descripcion, Calorias, Proteina, Carbohidratos, Grasas)
VALUES ('Frijoles negros', 'Frijoles negros cocidos', 132, 8.9, 23.7, 0.5);

INSERT INTO Objetivo (UsuarioId, NombreObjetivo, PesoObjetivo)
VALUES (5, 'Controlar azúcar en sangre', 65.00);

INSERT INTO Dieta (IdUsuario, Nombre, Descripcion)
VALUES (5, 'Dieta baja en azúcares simples', 'Recomendada para controlar los niveles de azúcar en sangre con alimentos de bajo índice glucémico.');

INSERT INTO Alimento_Dieta (DietaId, AlimentoId, CantidadDiaria)
VALUES (12, 16, 150);










