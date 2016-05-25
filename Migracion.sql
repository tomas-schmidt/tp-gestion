/****************************************************************/
--						CREAR ESQUEMA
/****************************************************************/

USE GD1C2016
GO
CREATE SCHEMA C_HASHTAG AUTHORIZATION gd


CREATE TABLE Rol
(
Id_Rol PRIMARY KEY,
Nombre_Rol,
Habilitado
);

CREATE TABLE Funcionalidad_Rol
(
Id_Rol,
Id_Funcionalidad
);

CREATE TABLE Funcionalidad
(
Id_Funcionalidad PRIMARY KEY,
Nombre_Funcionalidad
);

CREATE TABLE Cliente
(
Id_Cliente PRIMARY KEY,
Nombre,
Apellido,
DNI,
Tipo_Doc,
Mail,
Telefono,
Direccion,
Cod_Postal,
Nacimiento,
Creacion
);

CREATE TABLE Empresa
(
IdEmpresa PRIMARY KEY,
RazonSocial,
Mail,
Telefono,
Calle,
Cod_Postal,
Ciudad,
Cuit,
Nombre_Contacto,
Rubro_Principal,
Nro_Calle,
Piso,
Departamento
);

CREATE TABLE Rubro
(
Id_Rubro PRIMARY KEY,
Desc_Corta,
Desc_Larga
);

CREATE TABLE Rubro_Publicacion
(
Id_Rubro (FK),
Id_Publicacion (FK)
);

CREATE TABLE Oferta
(
Id_Oferta numeric(18,0) PRIMARY KEY,
Id_Publicacion numeric(18,0), 
Id_Cliente numeric(18,0),
Monto_Ofertado numeric(18,2)
);

CREATE TABLE Compra
(
Id_Compra PRIMARY KEY,
Id_Cliente (FK),
Id_Publicacion (FK)
);

CREATE TABLE Calificacion
(
Id_Calificacion PRIMARY KEY,
Cant_Estrellas,
Descripcion,
Id_Compra (FK)
);

CREATE TABLE Factura
(
Id_Factura PRIMARY KEY,
Monto,
Comision_Tipo,
Comision_Producto,
Comision_Envio,
Cantidad,
Nro_Factura,
Id_Compra (FK)
);

CREATE TABLE Estado
(
Id_Estado PRIMARY KEY,
Descripcion
);

CREATE TABLE Publicacion
(
Id_Publicacion PRIMARY KEY,
Monto,
Id_Visibilidad (FK),
Id_User (FK),
Id_Estado (FK),
Tipo,
Fecha_Inicial,
Fecha_Final,
Preguntas,
Stock,
Descripcion
);

CREATE TABLE Visibilidad
(
Id_Visibilidad PRIMARY KEY,
Comision_Prod_Vend,
Comision_Envio_Prod,
Comision_Tipo_Public,
Importancia
);

CREATE TABLE Usuario
(
Id_User PRIMARY KEY,
Username,
Password,
lntentos_Fallidos,
Estado,
Id_Empresa (FK),
Id_Cliente (FK)
);

CREATE TABLE Rol_Usuario
(
Id_User (FK),
Id_Rol (FK)
);

GO


/***********************************************************************
 *
 *						MIGRACION DE DATOS
 *
 ***********************************************************************/
 
 
