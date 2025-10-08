-- Paso 1: Creación de la nueva base de datos
CREATE DATABASE ExampleApp;
GO

-- Paso 2: Cración de tablas
USE ExampleApp;
GO

CREATE TABLE Departments (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Employees (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    DateHired DATE NOT NULL,
    DepartmentId INT NOT NULL,

    CONSTRAINT UQ_Employees_Email UNIQUE (Email),
    CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentId)
        REFERENCES Departments(Id)
        ON DELETE NO ACTION
        ON UPDATE CASCADE
);
GO

-- Paso 3: Inserción de filas en las tablas
INSERT INTO Departments (Name) VALUES (N'Tecnología');
INSERT INTO Departments (Name) VALUES (N'Recursos Humanos');
INSERT INTO Departments (Name) VALUES (N'Finanzas');
GO

INSERT INTO Employees (FirstName, LastName, Email, DateHired, DepartmentId)
VALUES (N'Ana', N'Gómez', N'ana.gomez@empresa.com', '2022-03-15', 1);
INSERT INTO Employees (FirstName, LastName, Email, DateHired, DepartmentId)
VALUES (N'Carlos', N'Fernández', N'carlos.fernandez@empresa.com', '2021-11-01', 2);
INSERT INTO Employees (FirstName, LastName, Email, DateHired, DepartmentId)
VALUES (N'Lucía', N'Martínez', N'lucia.martinez@empresa.com', '2023-06-20', 1);
INSERT INTO Employees (FirstName, LastName, Email, DateHired, DepartmentId)
VALUES (N'Marcos', N'Pérez', N'marcos.perez@empresa.com', '2020-01-10', 3);
INSERT INTO Employees (FirstName, LastName, Email, DateHired, DepartmentId)
VALUES (N'Valeria', N'Sosa', N'valeria.sosa@empresa.com', '2024-02-05', 2);
GO
