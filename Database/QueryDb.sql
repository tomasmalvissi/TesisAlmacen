CREATE DATABASE Almacen
GO

USE Almacen
GO

CREATE TABLE Usuarios
(
	Id INT PRIMARY KEY IDENTITY,
	Nombre VARCHAR(100) NOT NULL,
	Usuario VARCHAR(100) UNIQUE NOT NULL,
	Email VARCHAR(100) NOT NULL,
	Contraseņa VARCHAR(100) NOT NULL,
	FechaBaja DATETIME 
)
GO

CREATE TABLE Proveedores
(
	Id INT PRIMARY KEY IDENTITY,
	Nombre VARCHAR(100) NOT NULL,
	CUIL BIGINT NOT NULL,
	Direccion VARCHAR(100) NOT NULL,
	Telefono BIGINT NOT NULL,
	FechaBaja DATETIME 
)
GO

CREATE TABLE Clientes
(
	Id INT PRIMARY KEY IDENTITY,
	Nombre VARCHAR(100) NOT NULL,
	DNI BIGINT NOT NULL,
	Direccion VARCHAR(100) NOT NULL,
	Telefono VARCHAR(100) NOT NULL,
	FechaBaja DATETIME 
)
GO

CREATE TABLE Ventas
(
	Id INT PRIMARY KEY IDENTITY,
	Fecha DATETIME NOT NULL,
	Cliente_Id INT NOT NULL,
	Empleado_Id INT NOT NULL,
	Total DECIMAL(16,2) NOT NULL,
	Saldo DECIMAL(16,2),
	FechaBaja DATETIME,
	FOREIGN KEY (Cliente_Id) REFERENCES Clientes(Id),
	FOREIGN KEY (Empleado_Id) REFERENCES Usuarios(Id)
)
GO


CREATE TABLE Articulos
(
	Id INT PRIMARY KEY IDENTITY,
	Nombre VARCHAR(100) NOT NULL,
	Codigo_Art BIGINT NOT NULL,
	Precio_Unit DECIMAL(16,2) NOT NULL,
	Precio_Mayor DECIMAL(16,2) NOT NULL,
	Stock_Act INT NOT NULL,
	Ultima_Modif DATETIME NOT NULL,
	FechaBaja DATETIME 
)
GO

CREATE TABLE DetalleVentas
(
	Id INT PRIMARY KEY IDENTITY, 
	Articulo_Id INT NOT NULL,
	Precio INT NOT NULL,
	Cantidad INT NOT NULL,
	Venta_Id INT NOT NULL,
	SubTotal DECIMAL(16,2),
	FOREIGN KEY (Venta_Id) REFERENCES Ventas(Id),
	FOREIGN KEY (Articulo_Id) REFERENCES Articulos(Id)
)
GO

CREATE TABLE Compras
(
	Id INT PRIMARY KEY IDENTITY,
	Fecha DATETIME NOT NULL,
	Proveedor_Id INT NOT NULL,
	NroRecibo BIGINT NOT NULL,
	Empleado_Id INT NOT NULL,
	Total DECIMAL(16,2) NOT NULL,
	FechaBaja DATETIME,
	FOREIGN KEY (Proveedor_Id) REFERENCES Proveedores(Id),
	FOREIGN KEY (Empleado_Id) REFERENCES Usuarios(Id)
)
GO

CREATE TABLE DetalleCompras
(
	Id INT PRIMARY KEY IDENTITY,
	Articulo_Id INT NOT NULL,
	Cantidad INT NOT NULL,
	Precio_Mayor DECIMAL(16,2) NOT NULL,
	Precio_Unit DECIMAL(16,2) NOT NULL,
	SubTotal DECIMAL(16,2),
	Compra_Id INT NOT NULL,
	FOREIGN KEY (Compra_Id) REFERENCES Compras(Id),
	FOREIGN KEY (Articulo_Id) REFERENCES Articulos(Id)
)
GO

CREATE TABLE Caja
(
	Id INT PRIMARY KEY IDENTITY,
	Fecha DATETIME NOT NULL,
	Empleado_Id INT NOT NULL,
	Apertura DECIMAL(16,2) NOT NULL,
	Cierre DECIMAL(16,2) NOT NULL,
	FechaBaja DATETIME,
	FOREIGN KEY (Empleado_Id) REFERENCES Usuarios(Id)
)
GO

CREATE TABLE MovimientosCaja
(
	Id INT PRIMARY KEY IDENTITY,
	Fecha DATETIME NOT NULL,
	Descripcion VARCHAR(100) NULL,
	Ingreso DECIMAL(16,2) NOT NULL,
	Egreso DECIMAL(16,2) NOT NULL,
	Total DECIMAL(16,2) NOT NULL,
	FechaBaja DATETIME NULL,
	Venta_Id INT NULL, 
	Compra_Id INT NULL,
	FOREIGN KEY (Venta_Id) REFERENCES Ventas(Id),
	FOREIGN KEY (Compra_Id) REFERENCES Compras(Id)
)
GO

CREATE TABLE FormasPago
(
	Id INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(100) NULL,
)
GO


CREATE TABLE FormasPagoVentas
(
	Id INT PRIMARY KEY IDENTITY,
	Venta_Id INT NOT NULL, 
	FormaPago_Id INT NOT NULL,
	Importe DECIMAL(16,2) NOT NULL,
	Fecha DATETIME NOT NULL
	FOREIGN KEY (FormaPago_Id) REFERENCES FormasPago(Id),
	FOREIGN KEY (Venta_Id) REFERENCES Ventas(Id)
)
GO

CREATE TABLE FormasPagoCompras
(
	Id INT PRIMARY KEY IDENTITY,
	Compra_Id INT NOT NULL, 
	FormaPago_Id INT NOT NULL,
	Importe DECIMAL(16,2) NOT NULL,
	Fecha DATETIME NOT NULL
	FOREIGN KEY (FormaPago_Id) REFERENCES FormasPago(Id),
	FOREIGN KEY (Compra_Id) REFERENCES Compras(Id)
)
GO

CREATE TABLE SalidasDinero
(
	Id INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(20) NULL,
	Importe DECIMAL(16,2) NOT NULL,
	Caja_Id INT NOT NULL,
	FOREIGN KEY (Caja_Id) REFERENCES Caja(Id)
)
GO


INSERT INTO FormasPago VALUES('Efectivo')
GO
INSERT INTO FormasPago VALUES('Tarjeta Debito')
GO
INSERT INTO FormasPago VALUES('Tarjeta Credito')
GO
INSERT INTO FormasPago VALUES('Transferencia Bancaria')
GO
INSERT INTO FormasPago VALUES('Cheque')
GO

INSERT INTO Clientes VALUES ('CLIENTE', 11111111, 'DIRECCION 123', '1234567890', NULL)
GO