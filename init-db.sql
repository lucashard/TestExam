-- Crear base de datos
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TestExamDb')
BEGIN
    CREATE DATABASE TestExamDb;
END
GO

USE TestExamDb;
GO

-- Crear tabla Direccion
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Direccion')
BEGIN
    CREATE TABLE Direccion
    (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(255) NOT NULL,
        Altura INT NOT NULL
    );
END
GO

-- Crear tabla Cliente
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Cliente')
BEGIN
    CREATE TABLE Cliente
    (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(255) NOT NULL,
        Apellido NVARCHAR(255) NOT NULL,
        Documento NVARCHAR(50) NOT NULL,
        Email NVARCHAR(255) NOT NULL,
        Telefono NVARCHAR(100) NOT NULL,
        DireccionId INT NOT NULL,
        CONSTRAINT FK_Cliente_Direccion FOREIGN KEY (DireccionId) REFERENCES dbo.Direccion(Id)
    );
END
GO

-- Crear tabla MateriaPrima
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'MateriaPrima')
BEGIN
    CREATE TABLE MateriaPrima
    (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(255) NOT NULL,
        Descripcion NVARCHAR(MAX) NOT NULL,
        Cantidad DECIMAL(18,2) NOT NULL CONSTRAINT DF_MateriaPrima_Cantidad DEFAULT(0)
    );
END
ELSE
BEGIN
    IF COL_LENGTH('dbo.MateriaPrima', 'Cantidad') IS NULL
    BEGIN
        ALTER TABLE dbo.MateriaPrima
        ADD Cantidad DECIMAL(18,2) NOT NULL CONSTRAINT DF_MateriaPrima_Cantidad DEFAULT(0);
    END
END
GO

-- Crear tabla Operarios
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Operarios')
BEGIN
    CREATE TABLE Operarios
    (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(255) NOT NULL
    );
END
GO

-- Crear tabla Etapas
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Etapas')
BEGIN
    CREATE TABLE Etapas
    (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(255) NOT NULL
    );
END
GO

-- Crear tabla OperarioEtapa
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OperarioEtapa')
BEGIN
    CREATE TABLE OperarioEtapa
    (
        Id INT PRIMARY KEY IDENTITY(1,1),
        EtapaId INT NOT NULL,
        OperarioId INT NOT NULL,
        Duracion DECIMAL(18,2) NOT NULL,
        CONSTRAINT FK_OperarioEtapa_Etapas FOREIGN KEY (EtapaId) REFERENCES dbo.Etapas(Id),
        CONSTRAINT FK_OperarioEtapa_Operarios FOREIGN KEY (OperarioId) REFERENCES dbo.Operarios(Id)
    );
END
GO

-- Crear tabla Factura
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Factura')
BEGIN
    CREATE TABLE Factura
    (
        Id INT PRIMARY KEY IDENTITY(1,1),
        ClienteId INT NOT NULL,
        CONSTRAINT FK_Factura_Cliente FOREIGN KEY (ClienteId) REFERENCES dbo.Cliente(Id)
    );
END
GO

-- Crear tabla FacturaMateriaPrima
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FacturaMateriaPrima')
BEGIN
    CREATE TABLE FacturaMateriaPrima
    (
        FacturaId INT NOT NULL,
        MateriaPrimaId INT NOT NULL,
        Cantidad DECIMAL(18,2) NOT NULL,
        CONSTRAINT PK_FacturaMateriaPrima PRIMARY KEY (FacturaId, MateriaPrimaId),
        CONSTRAINT FK_FacturaMateriaPrima_Factura FOREIGN KEY (FacturaId) REFERENCES dbo.Factura(Id),
        CONSTRAINT FK_FacturaMateriaPrima_MateriaPrima FOREIGN KEY (MateriaPrimaId) REFERENCES dbo.MateriaPrima(Id)
    );
END
GO

-- Crear tabla FacturaEtapa
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FacturaEtapa')
BEGIN
    CREATE TABLE FacturaEtapa
    (
        FacturaId INT NOT NULL,
        EtapaId INT NOT NULL,
        CONSTRAINT PK_FacturaEtapa PRIMARY KEY (FacturaId, EtapaId),
        CONSTRAINT FK_FacturaEtapa_Factura FOREIGN KEY (FacturaId) REFERENCES dbo.Factura(Id),
        CONSTRAINT FK_FacturaEtapa_Etapas FOREIGN KEY (EtapaId) REFERENCES dbo.Etapas(Id)
    );
END
GO

-- Insertar datos iniciales en MateriaPrima
IF NOT EXISTS (SELECT 1 FROM dbo.MateriaPrima)
BEGIN
    INSERT INTO dbo.MateriaPrima (Nombre, Descripcion, Cantidad)
    VALUES ('Harina', 'Harina de trigo', 100),
           ('Azucar', 'Azucar refinada', 200);
END
GO
