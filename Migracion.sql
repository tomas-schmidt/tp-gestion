/****************************************************************/
--						CREAR ESQUEMA
/****************************************************************/

CREATE SCHEMA C_HASHTAG AUTHORIZATION gd
GO

/***********************************************************************
 *
 *						MIGRACION DE DATOS
 *
 ***********************************************************************/

USE GD1C2016


/****************************************************************/
--							ROL
/****************************************************************/
CREATE TABLE C_HASHTAG.Rol
(
	Id_Rol numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Nombre_Rol nvarchar(255),
	Habilitado bit 
)

/****************************************************************/
--							Funcionalidad
/****************************************************************/
CREATE TABLE C_HASHTAG.Funcionalidad
(
	Id_Funcionalidad numeric(18,0)IDENTITY(1,1) PRIMARY KEY,
	Nombre_Funcionalidad nvarchar(255) 
)


/****************************************************************/
--							Funcionalidad_Rol
/****************************************************************/
CREATE TABLE C_HASHTAG.Funcionalidad_Rol
(
	Id_Rol numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Rol(Id_Rol),
	Id_Funcionalidad numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Funcionalidad(Id_Funcionalidad) 
	CONSTRAINT PK_Funcionalidad_Rol PRIMARY KEY CLUSTERED (Id_Rol,Id_Funcionalidad)
)

/****************************************************************/
--						DOCUMENTO
/****************************************************************/
CREATE TABLE C_HASHTAG.Tipo_Doc(
	Doc_Codigo numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Doc_Desc varchar(255) NOT NULL
)
		
INSERT INTO C_HASHTAG.Tipo_Doc(Doc_Desc) VALUES ('DNI')
INSERT INTO C_HASHTAG.Tipo_Doc(Doc_Desc) VALUES ('Cedula')


/****************************************************************/
--							Cliente
/****************************************************************/
CREATE TABLE C_HASHTAG.Cliente
(
	Id_Cliente numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Nombre nvarchar(255),
	Apellido nvarchar(255),
	Nro_Doc numeric(18,0),
	Tipo_Doc numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Tipo_doc(Doc_Codigo),
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

INSERT INTO C_HASHTAG.Cliente(
	Nombre,
	Apellido,
	Nro_Doc,
	Tipo_Doc,
	Mail,
	Telefono,
	Domicilio_Calle,
	Nro_Calle,
	Piso,
	Departamento,
	Cod_Postal,
	Nacimiento,
	Creacion 
)
	SELECT DISTINCT
		Cli_Nombre,
		Cli_Apeliido,
		Cli_Dni,
		1, -- son todos DNI
		Cli_Mail,
		NULL, -- no existe el campo telefono
		Cli_Dom_Calle,
		Cli_Nro_Calle,
		Cli_Piso,
		Cli_Depto,
		Cli_Cod_Postal,
		Cli_Fecha_Nac,
		NULL -- falta obtener la fecha de creacion
		FROM gd_esquema.Maestra
		where Cli_Nombre IS NOT NULL
		

/****************************************************************/
--							Rubro
/****************************************************************/
CREATE TABLE C_HASHTAG.Rubro
(
	Id_Rubro numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Desc_Corta nvarchar(50),
	Desc_Larga nvarchar(255)
)

/****************************************************************/
--							Empresa
/****************************************************************/
CREATE TABLE C_HASHTAG.Empresa
(
	Id_Empresa numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Razon_Social nvarchar(255),
	Mail nvarchar(50),
	Telefono numeric(18,0),
	Calle nvarchar(100),
	Cod_Postal nvarchar(50),
	Ciudad nvarchar(255),
	Cuit nvarchar(50),
	Nombre_Contacto nvarchar(255),
	Rubro_Principal numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Rubro(Id_Rubro),
	Nro_Calle numeric(18,0),
	Piso numeric(18,0),
	Departamento nvarchar(50)
)

INSERT INTO C_HASHTAG.Empresa
(
	Razon_Social, 
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
)
	SELECT DISTINCT
		Publ_Empresa_Razon_Social,
		Publ_Empresa_Mail,
		NULL, -- no existe el campo telefono
		Publ_Empresa_Dom_Calle,
		Publ_Empresa_Cod_Postal,
		NULL, -- no existe el campo ciudad
		Publ_Empresa_Cuit,
		NULL, -- falta nombre contacto
		NULL, -- falta rubro principal
		Publ_Cli_Nro_Calle,
		Publ_Cli_Piso,
		Publ_Cli_Depto
		FROM gd_esquema.Maestra
		where Publ_Empresa_Cuit IS NOT NULL


/****************************************************************/
--							Estado
/****************************************************************/
CREATE TABLE C_HASHTAG.Estado
(
	Id_Estado numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Descripcion nvarchar(255) 
)

INSERT INTO C_HASHTAG.Estado (Descripcion) VALUES ('Borrador')
INSERT INTO C_HASHTAG.Estado (Descripcion) VALUES ('Activa')
INSERT INTO C_HASHTAG.Estado (Descripcion) VALUES ('Pausada')
INSERT INTO C_HASHTAG.Estado (Descripcion) VALUES ('Finalizada')


/****************************************************************/
--							Visibilidad
/****************************************************************/
CREATE TABLE C_HASHTAG.Visibilidad
(
	Id_Visibilidad numeric(18,0) IDENTITY (10002,1) PRIMARY KEY,
	Visibilidad_Desc nvarchar(255),
	Comision_Prod_Vend numeric(18,2),
	Comision_Envio_Prod numeric(18,2),
	Comision_Tipo_Public numeric(18,2)
)

-- FALTA AGREGAR BIEN LOS VALORES DE COMISIONES

INSERT INTO C_HASHTAG.Visibilidad (Visibilidad_Desc, Comision_Prod_Vend, Comision_Envio_Prod, Comision_Tipo_Public)
VALUES ('Platino', 0, 0, 0.10)

INSERT INTO C_HASHTAG.Visibilidad (Visibilidad_Desc, Comision_Prod_Vend, Comision_Envio_Prod, Comision_Tipo_Public)
VALUES ('Oro', 0, 0, 0.15)

INSERT INTO C_HASHTAG.Visibilidad (Visibilidad_Desc, Comision_Prod_Vend, Comision_Envio_Prod, Comision_Tipo_Public)
VALUES ('Plata', 0, 0, 0.20)

INSERT INTO C_HASHTAG.Visibilidad (Visibilidad_Desc, Comision_Prod_Vend, Comision_Envio_Prod, Comision_Tipo_Public)
VALUES ('Bronce',0, 0, 0.30)

INSERT INTO C_HASHTAG.Visibilidad (Visibilidad_Desc, Comision_Prod_Vend, Comision_Envio_Prod, Comision_Tipo_Public)
VALUES ('Gratis', 0, 0, 0)


/****************************************************************/
--							Usuario
/****************************************************************/
CREATE TABLE C_HASHTAG.Usuario
(
	Id_User numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Username nvarchar(50),
	Password nvarchar(50),
	Intentos_Fallidos numeric(18,0),
	Estado bit,
	Id_Empresa numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Empresa(Id_Empresa),
	Id_Cliente numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Cliente(Id_Cliente)
)

/****************************************************************/
--							Tipo Publicacion
/****************************************************************/
CREATE TABLE C_HASHTAG.Tipo_Public
(
	Id_Tipo_Public numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Descripcion nvarchar(255) 
)
INSERT INTO C_HASHTAG.Tipo_Public (Descripcion) VALUES ('Compra Inmediata')
INSERT INTO C_HASHTAG.Tipo_Public (Descripcion) VALUES ('Subasta')

/****************************************************************/
--							Publicacion
/****************************************************************/
CREATE TABLE C_HASHTAG.Publicacion
(
	Id_Publicacion numeric(18,0) PRIMARY KEY,
	Monto numeric(18,2),
	Id_Visibilidad numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Visibilidad(Id_Visibilidad),
	Id_User numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Usuario(Id_User),
	Id_Estado numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Estado(Id_Estado),
	Id_Tipo_Public numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Tipo_Public(Id_Tipo_Public), 
	Fecha_Inicial datetime,
	Fecha_Final datetime,
	Preguntas nvarchar(255),
	Stock numeric(18,0),
	Descripcion nvarchar(255)
)

INSERT INTO C_HASHTAG.Publicacion
(
	Id_Publicacion,
	Monto,
	Id_Visibilidad,
	Id_User,
	Id_Estado,
	Id_Tipo_Public, 
	Fecha_Inicial,
	Fecha_Final,
	Preguntas,
	Stock,
	Descripcion
)
	SELECT DISTINCT
		Publicacion_Cod,
		Publicacion_Precio,
		Publicacion_Visibilidad_Cod,
		NULL, --Falta id user,
		2, -- estan todas activas
		(SELECT t.Id_Tipo_Public  
			FROM C_HASHTAG.Tipo_Public t JOIN gd_esquema.Maestra m
			ON (t.Descripcion = m.Publicacion_Estado)),-- no se porque no funciona y las pone en null
		Publicacion_Fecha,
		Publicacion_Fecha_Venc,
		'Si', -- supongo que todas aceptan preguntas
		Publicacion_Stock,
		Publicacion_Descripcion
		FROM gd_esquema.Maestra

/****************************************************************/
--							Compra
/****************************************************************/
CREATE TABLE C_HASHTAG.Compra
(
	Id_Compra numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Id_User numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Usuario(Id_User),
	Id_Publicacion numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Publicacion(Id_Publicacion),
	Monto numeric(18,2),
	Fecha datetime
)

/****************************************************************/
--							Factura
/****************************************************************/
CREATE TABLE C_HASHTAG.Factura
(
	Id_Factura numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Id_Publicacion numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Publicacion(Id_Publicacion)
)

/****************************************************************/
--							Item
/****************************************************************/
CREATE TABLE C_HASHTAG.Item
(
	Id_Item numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Id_Factura numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Factura(Id_Factura),
	Descripcion nvarchar(255),
	Monto numeric(18,2)
)


/****************************************************************/
--							Calificacion
/****************************************************************/
CREATE TABLE C_HASHTAG.Calificacion
(
	Id_Calificacion numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Cant_Estrellas numeric(18,0),
	Descripcion nvarchar(255),
	Id_Compra numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Compra(Id_Compra)
)

/****************************************************************/
--							Oferta
/****************************************************************/
CREATE TABLE C_HASHTAG.Oferta
(
	Id_Oferta numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Id_Publicacion numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Publicacion(Id_Publicacion), 
	Id_User numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Usuario(Id_User),
	Monto_Ofertado numeric(18,2),
	Fecha datetime 
)

/****************************************************************/
--							Rubro_Publicacion
/****************************************************************/
CREATE TABLE C_HASHTAG.Rubro_Publicacion
(
	Id_Rubro numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Rubro(Id_Rubro),
	Id_Publicacion numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Publicacion(Id_Publicacion),
	CONSTRAINT PK_Rubro_Publicacion PRIMARY KEY CLUSTERED (Id_Rubro, Id_Publicacion)
)

/****************************************************************/
--							Rol_Usuario
/****************************************************************/
CREATE TABLE C_HASHTAG.Rol_Usuario
(
	Id_User numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Usuario(Id_User),
	Id_Rol numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Rol(Id_Rol),
	CONSTRAINT PK_Rol_Usuario PRIMARY KEY CLUSTERED(Id_User, Id_Rol)
)
