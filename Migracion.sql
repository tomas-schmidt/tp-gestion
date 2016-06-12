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
	Nombre_Funcionalidad nvarchar(255) unique 
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
--					TIPO DOCUMENTO	
/****************************************************************/
CREATE TABLE C_HASHTAG.Tipo_Doc(
	Doc_Codigo numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Doc_Desc varchar(255) UNIQUE NOT NULL
)
		
INSERT INTO C_HASHTAG.Tipo_Doc(Doc_Desc) VALUES ('DNI')
INSERT INTO C_HASHTAG.Tipo_Doc(Doc_Desc) VALUES ('Cedula')


/****************************************************************/
--							Usuario
/****************************************************************/
CREATE TABLE C_HASHTAG.Usuario
(
	Id_User numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Username nvarchar(50) UNIQUE,
	Contraseña varchar(255),
	Intentos_Fallidos numeric(18,0),
	Habilitado char (1)
)

INSERT INTO C_HASHTAG.Usuario
(
	Username,
	Contraseña,
	Intentos_Fallidos,
	Habilitado
)
	SELECT DISTINCT
		'usuario.cliente.' + RIGHT(CONVERT(varchar(18),Cli_Dni),18),
		'5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', --SHA256 de "password"
		0,
		's'
		FROM gd_esquema.Maestra
		where Cli_Dni IS NOT NULL
	UNION
		SELECT DISTINCT
		'usuario.cliente.' + RIGHT(CONVERT(varchar(18),Publ_Cli_Dni),18),
		'5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', --SHA256 de "password"
		0,
		's'
		FROM gd_esquema.Maestra
		where Publ_Cli_Dni IS NOT NULL

	UNION
	SELECT DISTINCT
		'usuario.empresa.' + RIGHT(CONVERT(varchar(18),Publ_Empresa_Cuit),18),
		'5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', --SHA256 de "password"
		0,
		's'
		FROM gd_esquema.Maestra
		where Publ_Empresa_Cuit IS NOT NULL


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
	Creacion datetime,
	Id_User numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Usuario(Id_User),
	CONSTRAINT Doc UNIQUE
    (
        Tipo_Doc, Nro_Doc
    )
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
	Creacion ,
	Id_User
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
		NULL, -- falta obtener la fecha de creacion
		(SELECT Id_User
			FROM C_HASHTAG.Usuario
			WHERE Username = 'usuario.cliente.' + RIGHT(CONVERT(varchar(18),Cli_DNI),18))
		FROM gd_esquema.Maestra
		where Cli_Nombre IS NOT NULL
	UNION
	SELECT DISTINCT
		Publ_Cli_Nombre,
		Publ_Cli_Apeliido,
		Publ_Cli_Dni,
		1, -- son todos DNI
		Publ_Cli_Mail,
		NULL, -- no existe el campo telefono
		Publ_Cli_Dom_Calle,
		Publ_Cli_Nro_Calle,
		Publ_Cli_Piso,
		Publ_Cli_Depto,
		Publ_Cli_Cod_Postal,
		Publ_Cli_Fecha_Nac,
		NULL, -- falta obtener la fecha de creacion
		(SELECT Id_User
			FROM C_HASHTAG.Usuario
			WHERE Username = 'usuario.cliente.' + RIGHT(CONVERT(varchar(18),Publ_Cli_Dni),18))
		FROM gd_esquema.Maestra
		where Publ_Cli_Nombre IS NOT NULL


/****************************************************************/
--							Rubro
/****************************************************************/
CREATE TABLE C_HASHTAG.Rubro
(
	Id_Rubro numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Desc_Corta nvarchar(50),
	Desc_Larga nvarchar(255)
)

INSERT INTO C_HASHTAG.Rubro
(
	Desc_Corta,
	Desc_Larga
)
	SELECT DISTINCT
		Publicacion_Rubro_Descripcion,
		NULL -- falta descripcion larga
		FROM gd_esquema.Maestra


/****************************************************************/
--							Empresa
/****************************************************************/
CREATE TABLE C_HASHTAG.Empresa
(
	Id_Empresa numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Razon_Social nvarchar(255) UNIQUE,
	Mail nvarchar(50),
	Telefono numeric(18,0),
	Calle nvarchar(100),
	Cod_Postal nvarchar(50),
	Ciudad nvarchar(255),
	Cuit nvarchar(50) UNIQUE,
	Nombre_Contacto nvarchar(255),
	Rubro_Principal numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Rubro(Id_Rubro),
	Nro_Calle numeric(18,0),
	Piso numeric(18,0),
	Departamento nvarchar(50),
	Id_User numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Usuario(Id_User)
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
	Departamento,
	Id_User
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
		Publ_Cli_Depto,
		(SELECT Id_User
			FROM C_HASHTAG.Usuario
			WHERE Username = 'usuario.empresa.' + RIGHT(CONVERT(varchar(18),Publ_Empresa_Cuit),18))		
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
/*Gratis no provee servicio de envío.
Para las restantes publicaciones independientemente de cual sea su visibilidad, se
debe elegir si dicha publicación brindará o no el servicio de envío sobre el producto
comprado.*/


INSERT INTO C_HASHTAG.Visibilidad (Visibilidad_Desc, Comision_Prod_Vend, Comision_Envio_Prod, Comision_Tipo_Public)
VALUES ('Platino', 180.00, 10.00, 0.10)

INSERT INTO C_HASHTAG.Visibilidad (Visibilidad_Desc, Comision_Prod_Vend, Comision_Envio_Prod, Comision_Tipo_Public)
VALUES ('Oro', 140.00, 10.00, 0.15)

INSERT INTO C_HASHTAG.Visibilidad (Visibilidad_Desc, Comision_Prod_Vend, Comision_Envio_Prod, Comision_Tipo_Public)
VALUES ('Plata', 100.00, 10.00, 0.20)

INSERT INTO C_HASHTAG.Visibilidad (Visibilidad_Desc, Comision_Prod_Vend, Comision_Envio_Prod, Comision_Tipo_Public)
VALUES ('Bronce', 60.00, 10.00, 0.30)

INSERT INTO C_HASHTAG.Visibilidad (Visibilidad_Desc, Comision_Prod_Vend, Comision_Envio_Prod, Comision_Tipo_Public)
VALUES ('Gratis', 0.00, 0.00, 0) --No se si en Comision_Envio_Prod va NULL o 0.00, porque dice que no provee servicio de envio

/*
HAY QUE IMPLEMENTAR:
todo usuario nuevo en la plataforma (aquellos agregados post-migración)
tendrán el beneficio de tener por única vez una publicación sin
costo de comisión de publicación (aplicable para aquellas que no sean
gratuitas), la cual corresponderá a la primera publicación que registren en
la plataforma. Esta característica no es aplicable a los datos migrados.
*/

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
	Id_Publicacion numeric(18,0) PRIMARY KEY,--auto numerico y consecutivo entre publicaciones
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
		(SELECT Id_User
			FROM C_HASHTAG.Usuario
			WHERE Username = 'usuario.empresa.' + RIGHT(CONVERT(varchar(18),Publ_Empresa_Cuit),18)),
		2, -- estan todas activas
		(SELECT Id_Tipo_Public  
			FROM C_HASHTAG.Tipo_Public
			where Descripcion = Publicacion_Estado),  -- ARREGLADO Era Publicacion_Estado, no Publicacion_Tipo
		Publicacion_Fecha,
		Publicacion_Fecha_Venc,
		'Si', -- supongo que todas aceptan preguntas
		Publicacion_Stock,
		Publicacion_Descripcion
		FROM gd_esquema.Maestra
		WHERE Publ_Empresa_Cuit IS NOT NULL

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
		(SELECT Id_User
			FROM C_HASHTAG.Usuario
			WHERE Username = 'usuario.cliente.' + RIGHT(CONVERT(varchar(18),Publ_Cli_Dni),18) ),
		2, -- estan todas activas
		(SELECT Id_Tipo_Public  
			FROM C_HASHTAG.Tipo_Public
			where Descripcion = Publicacion_Estado),  -- ARREGLADO Era Publicacion_Estado, no Publicacion_Tipo
		Publicacion_Fecha,
		Publicacion_Fecha_Venc,
		'Si', -- supongo que todas aceptan preguntas
		Publicacion_Stock,
		Publicacion_Descripcion
		FROM gd_esquema.Maestra
		WHERE Publ_Cli_Dni IS NOT NULL

/****************************************************************/
--							Compra
/****************************************************************/
CREATE TABLE C_HASHTAG.Compra
(
	Id_Compra numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Id_User numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Usuario(Id_User),
	Id_Publicacion numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Publicacion(Id_Publicacion),
	Monto numeric(18,2),
	Cantidad numeric(18,0),
	Fecha datetime
)

INSERT INTO C_HASHTAG.Compra
(
	Id_User,
	Id_Publicacion,
	Monto,
	Cantidad,
	Fecha
)
	SELECT 
		(SELECT Id_User
			FROM C_HASHTAG.Usuario
			WHERE Username = 'usuario.cliente.' + RIGHT(CONVERT(varchar(18),Cli_Dni),18)),
		Publicacion_Cod,
		Publicacion_Precio,
		Compra_Cantidad,
		Compra_Fecha
		FROM gd_esquema.Maestra
		WHERE Compra_Cantidad IS NOT NULL -- ME PONIA COMPRAS CON LA CANT Y FECHA EN NULL

/****************************************************************/
--							Factura
/****************************************************************/
CREATE TABLE C_HASHTAG.Factura
(
	Id_Factura numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Id_Publicacion numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Publicacion(Id_Publicacion),
	Numero numeric(18,0),
	Fecha datetime,
	Total numeric(18,0),
)
INSERT INTO C_HASHTAG.Factura
(
	Id_Publicacion,
	Numero,
	Fecha,
	Total
)
	SELECT
		Publicacion_Cod,
		Factura_Nro,
		Factura_Fecha,
		Factura_Total
		FROM gd_esquema.Maestra

/****************************************************************/
--							Item
/****************************************************************/
CREATE TABLE C_HASHTAG.Item
(
	Id_Item numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Id_Factura numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Factura(Id_Factura),
	Descripcion nvarchar(255),
	Monto numeric(18,0),
	Cantidad numeric(18,0)
)

INSERT INTO C_HASHTAG.Item
(
	Id_Factura,
	Descripcion,
	Monto,
	Cantidad
)
	SELECT
		NULL, -- falta id_factura, agregar id_publicacion?
		NULL, -- falta descripcion
		Item_Factura_Monto,
		Item_Factura_Cantidad
		FROM gd_esquema.Maestra

/****************************************************************/
--							Calificacion
/****************************************************************/
CREATE TABLE C_HASHTAG.Calificacion
(
	Id_Calificacion numeric(18,0) PRIMARY KEY,
	Cant_Estrellas numeric(18,0),
	Descripcion nvarchar(255),
	Id_Compra numeric(18,0) UNIQUE FOREIGN KEY REFERENCES C_HASHTAG.Compra(Id_Compra)
)

INSERT INTO C_HASHTAG.Calificacion
(
	Id_Calificacion,
	Cant_Estrellas,
	Descripcion,
	Id_Compra
)
	SELECT DISTINCT
		g2.Calificacion_Codigo,
		g2.Calificacion_Cant_Estrellas,
		g2.Calificacion_Descripcion,
		NULL /* no funciona la query
			(SELECT DISTINCT Id_Compra
			FROM C_HASHTAG.Compra JOIN gd_esquema.Maestra m
			ON (g2.Id_Publicacion = m.Publicacion_Cod )
			WHERE m.Calificacion_Codigo = g2.Calificacion_Codigo)*/
		FROM gd_esquema.Maestra g2
		WHERE Calificacion_Codigo IS NOT NULL

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

INSERT INTO C_HASHTAG.Oferta
(
	Id_Publicacion,
	Id_User,
	Monto_Ofertado,
	Fecha
)
	SELECT
		Publicacion_Cod,
		(SELECT Id_User
			FROM C_HASHTAG.Usuario
			WHERE Username = 'usuario.cliente.' + RIGHT(CONVERT(varchar(18),Cli_Dni),18)),
		Oferta_Monto,
		Oferta_Fecha
		FROM gd_esquema.Maestra
		WHERE Compra_Cantidad is null AND Oferta_Monto IS NOT NULL

/****************************************************************/
--							Rubro_Publicacion
/****************************************************************/
CREATE TABLE C_HASHTAG.Rubro_Publicacion
(
	Id_Rubro numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Rubro(Id_Rubro),
	Id_Publicacion numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Publicacion(Id_Publicacion),
	CONSTRAINT PK_Rubro_Publicacion PRIMARY KEY CLUSTERED (Id_Rubro, Id_Publicacion)
)

INSERT INTO C_HASHTAG.Rubro_Publicacion
(
	Id_Rubro,
	Id_Publicacion
)

SELECT DISTINCT Id_Rubro, Id_Publicacion
	FROM C_HASHTAG.Publicacion p JOIN gd_esquema.Maestra m
	ON (p.Id_Publicacion = m.Publicacion_Cod)
	JOIN C_HASHTAG.Rubro r
	ON (r.Desc_Corta = m.Publicacion_Rubro_Descripcion)

/****************************************************************/
--							Rol_Usuario
/****************************************************************/
CREATE TABLE C_HASHTAG.Rol_Usuario
(
	Id_User numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Usuario(Id_User),
	Id_Rol numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Rol(Id_Rol),
	CONSTRAINT PK_Rol_Usuario PRIMARY KEY CLUSTERED(Id_User, Id_Rol)
)
