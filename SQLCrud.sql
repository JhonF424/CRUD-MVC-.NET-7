CREATE DATABASE DBEmpleado;

USE DBEmpleado;

CREATE TABLE Departamento(
	idDepartamento INT PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(50)
);


CREATE TABLE Empleado(
	idEmpleado INT PRIMARY KEY IDENTITY(1,1),
	nombreCompleto VARCHAR(50),
	idDepartamento INT REFERENCES Departamento(idDepartamento),
	sueldo INT,
	fechaContrato DATE
);

INSERT INTO Departamento(nombre) VALUES ('Administracion'), ('Marketing'), ('Ventas'), ('Comercio');

INSERT INTO Empleado(nombreCompleto, idDepartamento, sueldo, fechaContrato) VALUES ('Franco Hernandez', 1, 1400, GETDATE());

SELECT * FROM Empleado;

GO

CREATE PROCEDURE sp_ListaDepartamentos
AS
BEGIN
	SELECT idDepartamento, nombre FROM Departamento
END

GO

CREATE PROCEDURE sp_ListaEmpleados
AS
BEGIN
	SET DATEFORMAT DMY
	SELECT 
	E.idEmpleado, 
	E.nombreCompleto, 
	D.idDepartamento, 
	D.nombre, 
	E.sueldo, 
	CONVERT(CHAR(10), E.fechaContrato, 103) AS 'FechaContrato' 
	FROM Empleado AS E INNER JOIN Departamento AS D ON E.idDepartamento = D.idDepartamento;
END

GO

CREATE PROCEDURE sp_GuardarEmpleado(@nombreCompleto VARCHAR(50), @idDepartamento INT, @sueldo INT, @fechaContrato VARCHAR(10))
AS
BEGIN
	SET DATEFORMAT DMY
	INSERT INTO Empleado(nombreCompleto, idDepartamento, sueldo, fechaContrato)
	VALUES
	(@nombreCompleto, @idDepartamento, @sueldo, CONVERT(DATE, @fechaContrato))
END

GO

CREATE PROCEDURE sp_EditarEmpleado(@nombreCompleto VARCHAR(50), @idEmpleado INT, @idDepartamento INT, @sueldo INT, @fechaContrato VARCHAR(10))
AS
BEGIN
	SET DATEFORMAT DMY
	UPDATE Empleado SET 
	nombreCompleto = @nombreCompleto, 
	idDepartamento = @idDepartamento,
	sueldo = @sueldo,
	fechaContrato = CONVERT(DATE, @fechaContrato)
	WHERE idEmpleado = @idEmpleado
END

GO

CREATE PROCEDURE sp_EliminarEmpleado(@idEmpleado INT)
AS 
BEGIN
	DELETE FROM Empleado WHERE idEmpleado = @idEmpleado
END


