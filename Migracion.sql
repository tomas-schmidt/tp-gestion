/****************************************************************/
--						CREAR ESQUEMA
/****************************************************************/

CREATE SCHEMA C_HASHTAG AUTHORIZATION gd
GO

/***********************************************************************
 *
 *						STORED PROCEDURES y FUNCIONES
 *
 ***********************************************************************/


/****************************************************************
 *						crearRol
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.crearRol
	@Nombre varchar(255),
	@Habilitado bit
AS
	BEGIN TRY
		INSERT INTO C_HASHTAG.Rol (Nombre_Rol, Habilitado) VALUES (@Nombre, @Habilitado)
		-- Devuelvo el id del rol
		SELECT Id_Rol FROM C_HASHTAG.Rol WHERE Nombre_Rol = @Nombre
	END TRY
	BEGIN CATCH
		DECLARE @MensajeError varchar(255)
		SET @MensajeError = 'El nombre "' + @Nombre + '" ya esta en uso'
		RAISERROR(@MensajeError, 16, 1)
	END CATCH
GO


/****************************************************************
 *							ObtenerRoles
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.obtenerRoles
AS
	SELECT Id_Rol, Nombre_Rol, Habilitado
		FROM Rol
GO

/****************************************************************
 *					obtenerFuncionalides
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.obtenerFuncionalidades
AS
	SELECT * FROM C_HASHTAG.Funcionalidad
GO

/****************************************************************
 *							bajaRol
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.bajaRol
	@Id_Rol int
AS
	UPDATE C_HASHTAG.Rol
		SET Habilitado = 0
		WHERE Id_Rol = @Id_Rol
GO

/****************************************************************
 *						agregarFuncionalidadARol
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.agregarFuncionalidadARol
	@Id_Rol int,
	@Id_Funcionalidad int
AS
	INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad)
		VALUES (@Id_Rol, @Id_Funcionalidad)
GO

/****************************************************************
 *				actualizarYBorrarFuncionalidadesRol
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.actualizarYBorrarFuncionalidadesRol
	@Id_Rol int,
    @Nombre varchar(255),
    @Habilitado bit
AS
	UPDATE C_HASHTAG.Rol
		SET Nombre_Rol = @Nombre, Habilitado = @Habilitado
		WHERE Id_Rol = @Id_Rol
		
	DELETE C_HASHTAG.Funcionalidad_Rol
		WHERE Id_Rol = @Id_Rol
GO

/****************************************************************
 *							LOGIN
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.Login
	@Username varchar(255),
	@Contrase�a varchar(255)
AS
	-- Verifico si las credenciales son correctas
	IF (SELECT COUNT(*) FROM Usuario WHERE Username = @Username AND Contrase�a = @Contrase�a) = 1
	BEGIN
		-- Verifico si el usuario esta lockeado
		IF (SELECT Intentos_Fallidos FROM Usuario WHERE Username = @Username) >= 3
		BEGIN
			RAISERROR('El usuario se encuentra bloqueado por acumulacion de intentos fallidos', 16, 1)
			RETURN
		END
		-- login satisfactorio
		-- Borro intentos fallidos
			UPDATE Usuario SET Intentos_Fallidos = 0
				FROM Usuario u
				WHERE u.Username = @Username

		-- Devuelvo Id_User
		SELECT Id_User
			FROM Usuario
			WHERE Username = @Username AND Contrase�a = @Contrase�a
	END
	ELSE
	BEGIN
		-- Verifico si existe el usuario
		IF (SELECT COUNT(*) FROM Usuario WHERE Username = @Username) = 1
		BEGIN
			-- Si existe incremento intentos fallidos
			UPDATE Usuario 
				SET Intentos_Fallidos=
					(SELECT Intentos_Fallidos + 1
						FROM Usuario u
						WHERE u.Username = @Username)
				WHERE Username = @Username
		END
		RAISERROR('Username y/o password incorrectos', 16, 1)
	END
GO

/****************************************************************
 *							obtenerRolesDeUsuario
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.obtenerRolesDeUsuario
	@Id_User int
AS
	SELECT R.Id_Rol, Nombre_Rol, Habilitado
		FROM Rol R, Rol_Usuario UR
		WHERE UR.Id_User = @Id_User
			AND UR.Id_Rol = R.Id_Rol
GO

/****************************************************************
 *					obtenerFuncionalidadesDeRol
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.obtenerFuncionalidadesDeRol
	@Id_Rol int
AS
	SELECT f.Id_Funcionalidad as Id_Funcionalidad, Nombre_Funcionalidad
		FROM Funcionalidad f, Funcionalidad_Rol fr, Rol r
		WHERE f.Id_Funcionalidad = fr.Id_Funcionalidad
			AND fr.Id_Rol = r.Id_Rol
			AND r.Id_Rol = @Id_Rol
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
GO

INSERT INTO C_HASHTAG.Rol (Nombre_Rol, Habilitado) VALUES ('Administrador', 1)
INSERT INTO C_HASHTAG.Rol (Nombre_Rol, Habilitado) VALUES ('Cliente', 1)
INSERT INTO C_HASHTAG.Rol (Nombre_Rol, Habilitado) VALUES ('Empresa', 1)

/*TRIGGER

CREATE TRIGGER C_HASHTAG.Rol_Inhabilitado
ON  C_HASHTAG.Rol
AFTER UPDATE
AS 
BEGIN TRAN
SET NOCOUNT ON;
      If Update(Habilitado)
      Begin
           DELETE ru
		   FROM C_HASHTAG.Rol_Usuario ru
			INNER JOIN C_HASHTAG.Rol r
			ON r.Id_Rol = ru.Id_Rol
		   WHERE  r.Habilitado=0
      End
SET NOCOUNT OFF
COMMIT TRAN
GO
*/

/****************************************************************/
--							Funcionalidad
/****************************************************************/
CREATE TABLE C_HASHTAG.Funcionalidad
(
	Id_Funcionalidad numeric(18,0)IDENTITY(1,1) PRIMARY KEY,
	Nombre_Funcionalidad nvarchar(255) unique
)

INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('ABMRol')
INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('ABMUsuario')
INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('ABMVisibilidad')
INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('GenerarPublicacion')
INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('Comprar/Ofertar')
INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('ObtenerHistorial')
INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('Calificar')
INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('ObtenerListadoEstadistico')

/****************************************************************/
--							Funcionalidad_Rol
/****************************************************************/
CREATE TABLE C_HASHTAG.Funcionalidad_Rol
(
	Id_Rol numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Rol(Id_Rol),
	Id_Funcionalidad numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Funcionalidad(Id_Funcionalidad) 
	CONSTRAINT PK_Funcionalidad_Rol PRIMARY KEY CLUSTERED (Id_Rol,Id_Funcionalidad)
)

--Inserto funcionalidades del administrador
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (1,1)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (1,2)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (1,3)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (1,8)

--Inserto funcionalidades del cliente
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (2,5)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (2,6)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (2,7)

--Inserto funcionalidades de la empresa
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (3,4)

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
	Contrase�a varchar(255),
	Intentos_Fallidos numeric(18,0),
	Habilitado bit
)

--Creo administrador con Id_User:1, username:admin1 y password:administrador (hasheada)
INSERT INTO C_HASHTAG.Usuario (Username, Contrase�a, Intentos_Fallidos, Habilitado) VALUES 
	('admin1', 'b20b0f63ce2ed361e8845d6bf2e59811aaa06ec96bcdb92f9bc0c5a25e83c9a6',0,1)


--Creo administrador con Id_User:2, username:admin2 y password:administrador (hasheada)
INSERT INTO C_HASHTAG.Usuario (Username, Contrase�a, Intentos_Fallidos, Habilitado) VALUES 
	('admin2', 'b20b0f63ce2ed361e8845d6bf2e59811aaa06ec96bcdb92f9bc0c5a25e83c9a6',0,1)

INSERT INTO C_HASHTAG.Usuario
(
	Username,
	Contrase�a,
	Intentos_Fallidos,
	Habilitado
)
	SELECT DISTINCT
		'usuario.cliente.' + RIGHT(CONVERT(varchar(18),Cli_Dni),18),
		'5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', --SHA256 de "password"
		0,
		1
		FROM gd_esquema.Maestra
		where Cli_Dni IS NOT NULL
	UNION
		SELECT DISTINCT
		'usuario.cliente.' + RIGHT(CONVERT(varchar(18),Publ_Cli_Dni),18),
		'5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', --SHA256 de "password"
		0,
		1
		FROM gd_esquema.Maestra
		where Publ_Cli_Dni IS NOT NULL

	UNION
	SELECT DISTINCT
		'usuario.empresa.' + RIGHT(CONVERT(varchar(18),Publ_Empresa_Cuit),18),
		'5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', --SHA256 de "password"
		0,
		1
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
	Localidad varchar (255),
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
	Localidad,
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
		NULL, -- no hay localidad en la maestra
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
		NULL, -- no hay localidad en la maestra
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
	Creacion DateTime,
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
	Creacion,
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
		(SELECT Id_Rubro
		FROM C_HASHTAG.Rubro r
		WHERE r.Desc_Corta = (  SELECT TOP 1 Publicacion_Rubro_Descripcion
								FROM gd_esquema.Maestra g2
								where g2.Publ_Empresa_Cuit = g1.Publ_Empresa_Cuit
								GROUP BY Publicacion_Rubro_Descripcion
								ORDER BY COUNT (*) DESC)), -- ELIJO EL RUBRO QUE APARECE EN MAS PUBLICACIONES
		Publ_Empresa_Nro_Calle,
		Publ_Empresa_Piso,
		Publ_Empresa_Depto,
		NULL, -- falta fecha de creacion
		(SELECT Id_User
			FROM C_HASHTAG.Usuario
			WHERE Username = 'usuario.empresa.' + RIGHT(CONVERT(varchar(18),Publ_Empresa_Cuit),18))		
		FROM gd_esquema.Maestra g1
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

/*Gratis no provee servicio de env�o.
Para las restantes publicaciones independientemente de cual sea su visibilidad, se
debe elegir si dicha publicaci�n brindar� o no el servicio de env�o sobre el producto
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
VALUES ('Gratis', 0.00, 0.00, 0) 

/*
HAY QUE IMPLEMENTAR:
todo usuario nuevo en la plataforma (aquellos agregados post-migraci�n)
tendr�n el beneficio de tener por �nica vez una publicaci�n sin
costo de comisi�n de publicaci�n (aplicable para aquellas que no sean
gratuitas), la cual corresponder� a la primera publicaci�n que registren en
la plataforma. Esta caracter�stica no es aplicable a los datos migrados.
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
--							Calificacion
/****************************************************************/
CREATE TABLE C_HASHTAG.Calificacion
(
	Id_Calificacion numeric(18,0) PRIMARY KEY,
	Cant_Estrellas numeric(18,0),
	Descripcion nvarchar(255)
)

INSERT INTO C_HASHTAG.Calificacion
(
	Id_Calificacion,
	Cant_Estrellas,
	Descripcion
)
	SELECT DISTINCT
		g2.Calificacion_Codigo,
		g2.Calificacion_Cant_Estrellas,
		g2.Calificacion_Descripcion
	FROM gd_esquema.Maestra g2	
	WHERE Calificacion_Codigo IS NOT NULL


/****************************************************************/
--							Compra
/****************************************************************/
CREATE TABLE C_HASHTAG.Compra
(
	Id_Compra numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Id_User numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Usuario(Id_User),
	ID_Publicacion numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Publicacion(Id_Publicacion),
	Monto numeric(18,2),
	Cantidad numeric(18,0),
	Fecha datetime,
	Id_Calif numeric(18,0)FOREIGN KEY REFERENCES C_HASHTAG.Calificacion(Id_Calificacion)
)

/*permitir la repeticion de valores no null, pero si null*/
CREATE UNIQUE NONCLUSTERED INDEX Id_Calif_NotNull 
ON C_HASHTAG.Compra(Id_Calif)
WHERE Id_Calif IS NOT NULL;

INSERT INTO C_HASHTAG.Compra
(
	Id_User,
	Id_Publicacion,
	Monto,
	Cantidad,
	Fecha,
	Id_Calif
)
	SELECT DISTINCT
		(SELECT Id_User
			FROM C_HASHTAG.Usuario
			WHERE Username = 'usuario.cliente.' + RIGHT(CONVERT(varchar(18),Cli_Dni),18)),
		Publicacion_Cod,
		Publicacion_Precio,
		Compra_Cantidad,
		Compra_Fecha,
		Calificacion_Codigo
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
	SELECT DISTINCT
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
		(SELECT Id_Factura
			FROM C_HASHTAG.Factura
			WHERE Factura_Nro = Numero),
		NULL, -- falta descripcion
		Item_Factura_Monto,
		Item_Factura_Cantidad
		FROM gd_esquema.Maestra
		where Item_Factura_Cantidad IS NOT NULL -- no se si poner la restriccion de que el monto tampoco sea null


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

-- Cargo rol de administrador a usuarios administradores (Id_Rol = 1)
INSERT INTO C_HASHTAG.Rol_Usuario(Id_User, Id_Rol) VALUES
	(1,1)
INSERT INTO C_HASHTAG.Rol_Usuario(Id_User, Id_Rol) VALUES
	(2,1)

--Cargo Rol de cliente a usuarios clientes (Id_Rol = 2)
INSERT INTO C_HASHTAG.Rol_Usuario (Id_User, Id_Rol)
	SELECT u.Id_User, 2
		FROM C_HASHTAG.Usuario u JOIN C_HASHTAG.Cliente c
		ON (u.Id_User = c.Id_User)

--Cargo Rol de empresa a usuarios empresas (Id_Rol = 3)
INSERT INTO C_HASHTAG.Rol_Usuario (Id_User, Id_Rol)
	SELECT u.Id_User, 3
		FROM C_HASHTAG.Usuario u JOIN C_HASHTAG.Empresa e
		ON (u.Id_User = e.Id_User)

	