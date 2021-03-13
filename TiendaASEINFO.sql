DROP DATABASE IF EXISTS TiendaASEINFO
GO
CREATE DATABASE TiendaASEINFO
GO
USE TiendaASEINFO
GO

/* Definción de la tabla Rol */
CREATE TABLE Rol(
	IdRol INT PRIMARY KEY IDENTITY,
	Nombre NVARCHAR(50) NOT NULL,
	Habilitado BIT NOT NULL DEFAULT 1,
	FechaModifica DATETIME NOT NULL,
	IpModifica NVARCHAR(25) NOT NULL
);

/* Definción de la tabla Usuarios */
CREATE TABLE Usuarios(
	IdUsuario INT PRIMARY KEY IDENTITY,
	NombreUsuario NVARCHAR(50) NOT NULL,
	Correo NVARCHAR(50) NOT NULL,
	Contrasenia NVARCHAR(MAX) NOT NULL,
	Salt NVARCHAR(50) NOT NULL,
	Imagen NVARCHAR(250),
	IdRol INT,
	Habilitado BIT NOT NULL DEFAULT 1,
	FechaModifica DATETIME NOT NULL,
	IpModifica NVARCHAR(25) NOT NULL
);

/* Definción de la tabla Clientes */
CREATE TABLE Clientes(
	IdCliente INT PRIMARY KEY IDENTITY,
	Nombre NVARCHAR(50) NOT NULL,
	Apellidos NVARCHAR(50) NOT NULL,
	Direccion NVARCHAR(300) NOT NULL,
	Genero CHAR NOT NULL,
	IdUsuario INT NOT NULL,
	Habilitado BIT NOT NULL DEFAULT 1,
	FechaModifica DATETIME NOT NULL,
	IpModifica NVARCHAR(25) NOT NULL
);

/* Definción de la tabla RecuperaContrasenias */
CREATE TABLE RecuperaContrasenias(
	IdRecupera INT PRIMARY KEY IDENTITY,
	Token NVARCHAR(MAX) NOT NULL,
	IdUsuario INT NOT NULL,
	Habilitado BIT NOT NULL DEFAULT 1,
	FechaModifica DATETIME NOT NULL,
	IpModifica NVARCHAR(25) NOT NULL
);

/* Definción de la tabla Logins */
CREATE TABLE Logins(
	IdLogin INT PRIMARY KEY IDENTITY,
	FechaLogin DATETIME NOT NULL,
	IdUsuario INT NOT NULL,
	Habilitado BIT NOT NULL DEFAULT 1,
	FechaModifica DATETIME NOT NULL,
	IpModifica NVARCHAR(25) NOT NULL
);

/* Definción de la tabla Marcas */
CREATE TABLE Marcas(
	IdMarca INT PRIMARY KEY IDENTITY,
	Nombre NVARCHAR(50) NOT NULL,
	Habilitado BIT NOT NULL DEFAULT 1,
	FechaModifica DATETIME NOT NULL,
	IpModifica NVARCHAR(25) NOT NULL
);

/* Definción de la tabla Categorias */
CREATE TABLE Categorias(
	IdCategoria INT PRIMARY KEY IDENTITY,
	Nombre NVARCHAR(50) NOT NULL,
	Habilitado BIT NOT NULL DEFAULT 1,
	FechaModifica DATETIME NOT NULL,
	IpModifica NVARCHAR(25) NOT NULL
);

/* Definción de la tabla Tipos */
CREATE TABLE Tipos(
	IdTipo INT PRIMARY KEY IDENTITY,
	Nombre NVARCHAR(50) NOT NULL,
	Habilitado BIT NOT NULL DEFAULT 1,
	FechaModifica DATETIME NOT NULL,
	IpModifica NVARCHAR(25) NOT NULL
);

/* Definción de la tabla Productos */
CREATE TABLE Productos(
	IdProducto INT PRIMARY KEY IDENTITY,
	Nombre NVARCHAR(50) NOT NULL,
	Descripcion NVARCHAR(300) NOT NULL,
	Precio DECIMAL(8,2) NOT NULL,
	Stock INT NOT NULL,
	Imagen NVARCHAR(250) NOT NULL,
	IdMarca INT,
	IdCategoria INT,
	IdTipo INT,
	Habilitado BIT NOT NULL DEFAULT 1,
	FechaModifica DATETIME NOT NULL,
	IpModifica NVARCHAR(25) NOT NULL
);

/* Definción de la tabla Compras */
CREATE TABLE Compras(
	IdCompra INT PRIMARY KEY IDENTITY,
	IdUsuario INT,
	IdProducto INT,
	Cantidad INT NOT NULL,
	TotalPagar DECIMAL(8,2) NOT NULL,
	Habilitado BIT NOT NULL DEFAULT 1,
	FechaModifica DATETIME NOT NULL,
	IpModifica NVARCHAR(25) NOT NULL
);

/* Definición de llaves foráneas */
ALTER TABLE Usuarios ADD CONSTRAINT FKUsuarioRol FOREIGN KEY (IdRol) 
REFERENCES Rol(IdRol) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE Clientes ADD CONSTRAINT FKClienteUsuario FOREIGN KEY (IdUsuario) 
REFERENCES Usuarios(IdUsuario) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE Productos ADD CONSTRAINT FKProductoMarca FOREIGN KEY (IdMarca) 
REFERENCES Marcas(IdMarca) ON UPDATE CASCADE ON DELETE SET NULL;

ALTER TABLE Productos ADD CONSTRAINT FKProductoCategoria FOREIGN KEY (IdCategoria) 
REFERENCES Categorias(IdCategoria) ON UPDATE CASCADE ON DELETE SET NULL;

ALTER TABLE Productos ADD CONSTRAINT FKProductoTipo FOREIGN KEY (IdTipo) 
REFERENCES Tipos(IdTipo) ON UPDATE CASCADE ON DELETE SET NULL;

ALTER TABLE Logins ADD CONSTRAINT FKLoginsUsuario FOREIGN KEY (IdUsuario) 
REFERENCES Usuarios(IdUsuario) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE RecuperaContrasenias ADD CONSTRAINT FKRContraUsuario FOREIGN KEY (IdUsuario) 
REFERENCES Usuarios(IdUsuario) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE Compras ADD CONSTRAINT FKCompraProducto FOREIGN KEY (IdProducto) 
REFERENCES Productos(IdProducto) ON UPDATE CASCADE ON DELETE SET NULL;

ALTER TABLE Compras ADD CONSTRAINT FKCompraUsuario FOREIGN KEY (IdUsuario) 
REFERENCES Usuarios(IdUsuario) ON UPDATE CASCADE ON DELETE SET NULL;

INSERT INTO Rol (Nombre, FechaModifica, IpModifica) VALUES
('Administrador', '2021-03-10','192.168.0.1'),
('Cliente', '2021-03-10','192.168.0.1');