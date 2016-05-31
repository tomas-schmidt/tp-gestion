/****************************************************************/
--						CREAR ESQUEMA
/****************************************************************/

USE GD1C2016
GO
CREATE SCHEMA C_HASHTAG AUTHORIZATION gd

CREATE TABLE Rol
(
Id_Rol numeric(18,0) PRIMARY KEY,
Nombre_Rol nvarchar(255),
Habilitado bit 
)

CREATE TABLE Funcionalidad
(
Id_Funcionalidad numeric(18,0) PRIMARY KEY,
Nombre_Funcionalidad nvarchar(255) 
)

CREATE TABLE Funcionalidad_Rol
(
Id_Rol numeric(18,0) FOREIGN KEY REFERENCES Rol(Id_Rol),
Id_Funcionalidad numeric(18,0) FOREIGN KEY REFERENCES Funcionalidad(Id_Funcionalidad) 
)

CREATE TABLE Cliente
(
Id_Cliente numeric(18,0) PRIMARY KEY,
Nombre nvarchar(255),
Apellido nvarchar(255),
DNI numeric(18,0),
Tipo_Doc nvarchar(255),
Mail nvarchar(255),
Telefono numeric(18,0),
Domicilio_Calle nvarchar(100),
Nro_Calle numeric(18,0),
Piso numeric(18,0),
Departamento nvarchar(50),
Cod_Postal nvarchar(50),
Nacimiento datetime,
Creacion datetime
)

CREATE TABLE Rubro
(
Id_Rubro numeric(18,0) PRIMARY KEY,
Desc_Corta nvarchar(50),
Desc_Larga nvarchar(255)
)

CREATE TABLE Empresa
(
Id_Empresa numeric(18,0) PRIMARY KEY,
Razon_Social nvarchar(255),
Mail nvarchar(50),
Telefono numeric(18,0),
Calle nvarchar(100),
Cod_Postal nvarchar(50),
Ciudad nvarchar(255),
Cuit nvarchar(50),
Nombre_Contacto nvarchar(255),
Rubro_Principal numeric(18,0) FOREIGN KEY REFERENCES Rubro(Id_Rubro),
Nro_Calle numeric(18,0),
Piso numeric(18,0),
Departamento nvarchar(50)
)

CREATE TABLE Estado
(
Id_Estado numeric(18,0) PRIMARY KEY,
Descripcion nvarchar(255) 
)

CREATE TABLE Visibilidad
(
Id_Visibilidad numeric(18,0) PRIMARY KEY,
Comision_Prod_Vend numeric(18,2),
Comision_Envio_Prod numeric(18,2),
Comision_Tipo_Public numeric(18,2)
--Importacia ?
)

CREATE TABLE Usuario
(
Id_User numeric(18,0) PRIMARY KEY,
Username nvarchar(50),
Password nvarchar(50),
Intentos_Fallidos numeric(18,0),
Estado bit,
Id_Empresa numeric(18,0) FOREIGN KEY REFERENCES Empresa(Id_Empresa),
Id_Cliente numeric(18,0) FOREIGN KEY REFERENCES Cliente(Id_Cliente)
)

CREATE TABLE Publicacion
(
Id_Publicacion numeric(18,0) PRIMARY KEY,
Monto numeric(18,2),
Id_Visibilidad numeric(18,0) FOREIGN KEY REFERENCES Visibilidad(Id_Visibilidad),
Id_User numeric(18,0) FOREIGN KEY REFERENCES Usuario(Id_User),
Id_Estado numeric(18,0) FOREIGN KEY REFERENCES Estado(Id_Estado),
Tipo nvarchar(255),
Fecha_Inicial datetime,
Fecha_Final datetime,
Preguntas nvarchar(255),
Stock numeric(18,0),
Descripcion nvarchar(255)
)

CREATE TABLE Tipo
(
Id_Tipo numeric(18,0) PRIMARY KEY,
Descripcion nvarchar(255) 
)

CREATE TABLE Compra
(
Id_Compra numeric(18,0) PRIMARY KEY,
Id_Cliente numeric(18,0) FOREIGN KEY REFERENCES Cliente(Id_Cliente),
Id_Publicacion numeric(18,0) FOREIGN KEY REFERENCES Publicacion(Id_Publicacion)
)

CREATE TABLE Factura
(
Id_Factura numeric(18,0) PRIMARY KEY,
Monto numeric(18,2),
Comision_Tipo numeric(18,2), 
Comision_Producto numeric(18,2), 
Comision_Envio numeric(18,2),
Cantidad numeric(18,0),
Nro_Factura numeric(18,0),
Id_Compra numeric(18,0) FOREIGN KEY REFERENCES Compra(Id_Compra)
)

CREATE TABLE Item
(
Id_Item numeric(18,0) PRIMARY KEY,
Id_Factura numeric(18,0) FOREIGN KEY REFERENCES Factura(Id_Factura),
Descripcion nvarchar(255),
Monto numeric(18,2)
)

CREATE TABLE Calificacion
(
Id_Calificacion numeric(18,0) PRIMARY KEY,
Cant_Estrellas numeric(18,0),
Descripcion nvarchar(255),
Id_Compra numeric(18,0) FOREIGN KEY REFERENCES Compra(Id_Compra)
)

CREATE TABLE Oferta
(
Id_Oferta numeric(18,0) PRIMARY KEY,
Id_Publicacion numeric(18,0) FOREIGN KEY REFERENCES Publicacion(Id_Publicacion), 
Id_User numeric(18,0) FOREIGN KEY REFERENCES Usuario(Id_User),
Monto_Ofertado numeric(18,2),
Fecha datetime 
)

CREATE TABLE Rubro_Publicacion
(
Id_Rubro numeric(18,0) FOREIGN KEY REFERENCES Rubro(Id_Rubro),
Id_Publicacion numeric(18,0) FOREIGN KEY REFERENCES Publicacion(Id_Publicacion)
)


CREATE TABLE Rol_Usuario
(
Id_User numeric(18,0) FOREIGN KEY REFERENCES Usuario(Id_User),
Id_Rol numeric(18,0) FOREIGN KEY REFERENCES Rol(Id_Rol)
)

GO


/***********************************************************************
 *
 *						MIGRACION DE DATOS
 *
 ***********************************************************************/
 
 
