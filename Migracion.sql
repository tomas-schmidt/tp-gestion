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
ld_Cliente PRIMARY KEY,
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
ld_Rubro PRIMARY KEY,
Desc_Corta,
Desc_Larga
);

CREATE TABLE Rubro_Publicacion
(
ld_Rubro (FK),
ld_Publicacion (FK)
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

);



GO


/***********************************************************************
 *
 *						MIGRACION DE DATOS
 *
 ***********************************************************************/
 
 
