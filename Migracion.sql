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
 *							FECHA
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.SetFecha
	@Fecha DateTime
AS
	TRUNCATE TABLE C_HASHTAG.FECHA_DEL_SISTEMA
	INSERT INTO C_HASHTAG.FECHA_DEL_SISTEMA ([Fecha]) VALUES (@Fecha)

	--Finalizo compras inmediatas vencidas
	update C_HASHTAG.Publicacion
	set Id_Estado = 4
	where Fecha_Final < @Fecha and Id_Tipo_Public = 1 and Id_Estado != 4

	--Finalizo subastas vencidas con ofertas y realizo la facturacion y registro de compra
	declare @Id_Publicacion int, @Id_User int
	declare subsVencidas cursor for
		select p.Id_Publicacion, o.Id_User
		from C_HASHTAG.Publicacion p
		join C_HASHTAG.Oferta  o
		on (p.Id_Publicacion = o.Id_Publicacion)
		where Monto = Monto_Ofertado and Fecha_Final < @Fecha and Id_Tipo_Public = 2 and Id_Estado != 4

	open subsVencidas
	fetch next from subsVencidas
		into @Id_Publicacion, @Id_User
	while @@FETCH_STATUS = 0
		begin
			--finalizo la subasta
			update C_HASHTAG.Publicacion
				set Id_Estado = 4 -- 'Finalizada'
				where Id_Publicacion = @Id_Publicacion

			-- registro la compra y realizo la facturacion
			EXEC C_HASHTAG.FacturarYResgistrarCompra @Id_Publicacion, 1, @Id_User

		fetch next from subsVencidas
			into @Id_Publicacion, @Id_User
		end
	close subsVencidas
	deallocate subsVencidas

	--Finalizo las subastas vencidas sin ofertas
	update C_HASHTAG.Publicacion
	set Id_Estado = 4
	where Fecha_Final < @Fecha and Id_Tipo_Public = 2 and Id_Estado != 4
GO

CREATE FUNCTION C_HASHTAG.obtenerFecha () RETURNS DateTime
AS
BEGIN
	RETURN(SELECT TOP(1) * FROM C_HASHTAG.FECHA_DEL_SISTEMA)
END
GO


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
	@Contraseña varchar(255)
AS
	-- Verifico si las credenciales son correctas
	IF (SELECT COUNT(*) FROM Usuario WHERE Username = @Username AND Contraseña = @Contraseña) = 1
	BEGIN
		-- Verifico si el usuario esta lockeado
		IF (SELECT Intentos_Fallidos FROM Usuario WHERE Username = @Username) >= 3
		BEGIN
			RAISERROR('El usuario se encuentra bloqueado por acumulacion de intentos fallidos', 16, 1)
			RETURN
		END
		
		IF (SELECT Habilitado FROM Usuario WHERE Username = @Username) = 0
		BEGIN
			RAISERROR('El usuario se encuentra deshabilitado', 16, 1)
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
			WHERE Username = @Username AND Contraseña = @Contraseña
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
	SELECT R.Id_Rol, Nombre_Rol, Habilitado, UR.Id_User
		FROM Rol R, Rol_Usuario UR
		WHERE UR.Id_User = @Id_User
			AND UR.Id_Rol = R.Id_Rol
			AND R.Habilitado = 1
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


/****************************************************************
 *							ObtenerRol
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.obtenerRol @Id_Rol int
AS
	SELECT * FROM C_HASHTAG.Rol
	where Id_Rol = @Id_Rol
GO

/****************************************************************
 *					obtenerTiposDocumento
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.obtenerTiposDocumento
AS
	SELECT * FROM C_HASHTAG.Tipo_Doc
		ORDER BY Doc_Desc
GO

/****************************************************************
 *					crearUsuarioYCliente
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.crearUsuarioYCliente
	@Username varchar(255), @Contraseña varchar(255),
	@Nombre varchar(255), @Apellido varchar(255), @NroDocumento numeric(18,0),
	@Nombre_Tipo_Doc varchar(255), @Mail varchar(255),  @Telefono numeric(18,0), @Domicilio_Calle nvarchar(255), @Nro_Calle numeric(18,0),
	@Piso numeric(18,0), @Departamento nvarchar(50), @Cod_Postal numeric(18,0), @Nacimiento datetime, @Localidad nvarchar(50)
AS
BEGIN TRANSACTION
	BEGIN TRY
		INSERT INTO C_HASHTAG.Usuario (Username, Contraseña , Intentos_Fallidos, Habilitado, Publicacion_Gratis, Id_Reputacion) VALUES 
		(@Username, @Contraseña, 0, 1, 1,1) --todos los usuario nuevos poseen su primera publicacion gratis
	END TRY
	BEGIN CATCH
		ROLLBACK
		RAISERROR('El username ya se encuentra en uso', 16, 1)
		RETURN
	END CATCH
	
	BEGIN TRY
		DECLARE @Id_User int;
		SET @Id_User = (SELECT Id_User FROM C_HASHTAG.Usuario WHERE Username = @Username)
		INSERT INTO C_HASHTAG.Cliente(Nombre, Apellido, Nro_Doc, Tipo_Doc, Mail,
				Telefono, Domicilio_Calle, Nro_Calle, Piso, Departamento, Cod_Postal, Nacimiento, Localidad, Creacion , Id_User)
			VALUES (@Nombre, @Apellido, @NroDocumento, 	(SELECT TOP 1 Doc_Codigo FROM C_HASHTAG.Tipo_Doc
															where Doc_Desc = @Nombre_Tipo_Doc),
					@Mail, @Telefono, @Domicilio_Calle, @Nro_Calle,
					@Piso, @Departamento, @Cod_Postal, @Nacimiento, @Localidad, C_HASHTAG.obtenerFecha(), @Id_User)
		INSERT INTO C_HASHTAG.Rol_Usuario (Id_User, Id_Rol)	
			Values (@Id_User, 2)
	END TRY
	BEGIN CATCH
		ROLLBACK
		RAISERROR('Ya existe un cliente con esa identificacion y/o email', 16, 1)
		RETURN
	END CATCH
	
	SELECT TOP 1 Id_Cliente, Id_User
		FROM C_HASHTAG.Cliente
		ORDER BY Id_Cliente DESC
COMMIT
GO

/****************************************************************
 *					obtenerRubros
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.obtenerRubros
AS
	SELECT * FROM C_HASHTAG.Rubro
		ORDER BY Desc_Corta
GO

/****************************************************************
 *					crearUsuarioYEmpresa
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.crearUsuarioYEmpresa
	@Username nvarchar(255),
	@Contraseña nvarchar(255),
	@Razon_Social nvarchar(255),
	@Mail nvarchar(50),
	@Telefono numeric(18,0),
	@Calle nvarchar(100),
	@Cod_Postal nvarchar(50),
	@Ciudad nvarchar(255),
	@Localidad nvarchar(255),
	@Cuit nvarchar(255),
	@Nombre_Contacto nvarchar(255),
	@Rubro_Principal nvarchar(255),
	@Nro_Calle numeric(18,0),
	@Piso numeric(18,0),
	@Departamento nvarchar(50)
AS
BEGIN TRANSACTION
	BEGIN TRY
		INSERT INTO C_HASHTAG.Usuario (Username, Contraseña , Intentos_Fallidos, Habilitado, Publicacion_Gratis, Id_Reputacion) VALUES 
		(@Username, @Contraseña, 0, 1, 1, 1) --todos los usuario nuevos poseen su primera publicacion gratis
	END TRY
	BEGIN CATCH
		ROLLBACK
		RAISERROR('El username ya se encuentra en uso', 16, 1)
		RETURN
	END CATCH
	
	BEGIN TRY
		DECLARE @Id_User int;
		SET @Id_User = (SELECT Id_User FROM C_HASHTAG.Usuario WHERE Username = @Username)
		INSERT INTO C_HASHTAG.Empresa
			(Razon_Social, Mail, Telefono, Calle, Cod_Postal,
			Ciudad, Localidad, Cuit, Nombre_Contacto, Rubro_Principal, Nro_Calle, Piso, Departamento, Creacion, Id_User)
			VALUES (@Razon_Social, @Mail, @Telefono, @Calle, @Cod_Postal, @Ciudad, @Localidad, @Cuit, @Nombre_Contacto,
					(SELECT TOP 1 Id_Rubro FROM C_HASHTAG.Rubro
					where Desc_Corta = @Rubro_Principal),
					@Nro_Calle, @Piso, @Departamento, C_HASHTAG.obtenerFecha(), @Id_User)
		INSERT INTO C_HASHTAG.Rol_Usuario (Id_User, Id_Rol)	
			Values (@Id_User, 3)
	END TRY
	BEGIN CATCH
		ROLLBACK
		RAISERROR('Ya existe una empresa con esa razon social, y/o CUIT, y/o email', 16, 1)
		RETURN
	END CATCH
	
	SELECT TOP 1 Id_Empresa, Id_User
		FROM C_HASHTAG.Empresa
		ORDER BY Id_Empresa DESC
COMMIT
GO

/****************************************************************
 *							cambiarEstadoEmpresa
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.cambiarEstadoEmpresa
	@Id_Empresa int
AS 
	declare @Id_User int
	
	set @Id_User = (select u.Id_User from C_HASHTAG.Usuario u join C_HASHTAG.Empresa e
					on (e.Id_User = u.Id_User) where e.Id_Empresa = @Id_Empresa)

		UPDATE C_HASHTAG.Usuario
		SET Habilitado = ~(Habilitado)
		where Id_User = @Id_User
		
		if ((select Habilitado from C_HASHTAG.Usuario where Id_User = @Id_User) = 0)
		begin
			update C_HASHTAG.Publicacion
			set Id_Estado =  3 --pausada
			where Id_User = @Id_User and Id_Estado = 2
		end

		else
		begin
			update C_HASHTAG.Publicacion
			set Id_Estado =  2 --activa
			where Id_User = @Id_User and Id_Estado = 3
		end
GO

/****************************************************************
 *							ObtenerEmpresaYUsername
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.ObtenerEmpresaYUsername @Id_Empresa int
AS
select top 1 u.Username, e.* from
	C_HASHTAG.Empresa e join C_HASHTAG.Usuario u
	on (e.Id_User = u.Id_User)
	where e.Id_Empresa = @Id_Empresa
GO

/****************************************************************
 *							ObtenerRubroDeEmpresa
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.obtenerRubroDeEmpresa @Id_Empresa int
AS
	SELECT * FROM C_HASHTAG.Rubro r
		join C_HASHTAG.Empresa e
		on (r.Id_Rubro = e.Rubro_Principal)
		where e.Id_Empresa = @Id_Empresa
		ORDER BY Desc_Corta
GO

/****************************************************************
 *					modificarEmpresa
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.modificarEmpresa
	@Username nvarchar(255),
	@Razon_Social nvarchar(255),
	@Mail nvarchar(50),
	@Telefono numeric(18,0),
	@Calle nvarchar(100),
	@Cod_Postal nvarchar(50),
	@Ciudad nvarchar(255),
	@Localidad nvarchar(255),
	@Cuit nvarchar(255),
	@Nombre_Contacto nvarchar(255),
	@Rubro_Principal nvarchar(255),
	@Nro_Calle numeric(18,0),
	@Piso numeric(18,0),
	@Departamento nvarchar(50)
AS
BEGIN TRANSACTION
	BEGIN TRY
		DECLARE @Id_User int;
		SET @Id_User = (SELECT Id_User FROM C_HASHTAG.Usuario WHERE Username = @Username)
		UPDATE e set
			Razon_Social = @Razon_Social, Mail =  @Mail, Telefono = @Telefono, Calle = @Calle, Cod_Postal = @Cod_Postal,
			Ciudad = @Ciudad, Localidad = @Localidad, Cuit = @Cuit, Nombre_Contacto = @Nombre_Contacto,
			Rubro_Principal = (SELECT TOP 1 Id_Rubro FROM C_HASHTAG.Rubro
								where Desc_Corta = @Rubro_Principal), Nro_Calle = @Nro_Calle, Piso = @Piso, Departamento = @Departamento
			from C_HASHTAG.Empresa e join C_HASHTAG.Usuario u
			on(e.Id_User = u.Id_User)
			where e.Id_User = @Id_User
	END TRY
	BEGIN CATCH
		ROLLBACK
		RAISERROR('Ya existe una empresa con esa razon social, y/o CUIT, y/o email', 16, 1)
		RETURN
	END CATCH
	
	SELECT TOP 1 Id_Empresa, Id_User
		FROM C_HASHTAG.Empresa
		ORDER BY Id_Empresa DESC
COMMIT
GO


/****************************************************************
 *							ObtenerClienteYUsername
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.obtenerClienteYUsername @Id_Cliente int
AS
select top 1 u.Username, c.* from
	C_HASHTAG.Cliente c join C_HASHTAG.Usuario u
	on (c.Id_User = u.Id_User)
	where c.Id_Cliente = @Id_Cliente
GO

/****************************************************************
 *							cambiarEstadoCliente
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.cambiarEstadoCliente
	@Id_Cliente int
AS 
		UPDATE u SET u.Habilitado = ~(u.Habilitado)
		from C_HASHTAG.Usuario u join C_HASHTAG.Cliente c
		on(u.Id_User = c.Id_User)
		where c.Id_Cliente = @Id_Cliente
GO

/****************************************************************
 *							ObtenerDocDeCliente
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.obtenerDocDeCliente @Id_Cliente int
AS
	SELECT * FROM C_HASHTAG.Tipo_Doc t
		join C_HASHTAG.Cliente c
		on (t.Doc_Codigo = c.Tipo_Doc)
		where c.Id_Cliente = @Id_Cliente
GO

/****************************************************************
 *					modificarCliente
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.modificarCliente
	@Username nvarchar(255),
	@Nombre nvarchar(255),
	@Mail nvarchar(50),
	@Telefono numeric(18,0),
	@Domicilio_Calle nvarchar(100),
	@Cod_Postal nvarchar(50),
	@Apellido nvarchar(255),
	@Localidad nvarchar(255),
	@Nro_Doc nvarchar(255),
	@Tipo_Doc nvarchar(255),
	@Nro_Calle numeric(18,0),
	@Piso numeric(18,0),
	@Departamento nvarchar(50),
	@Nacimiento datetime
AS
BEGIN TRANSACTION
	BEGIN TRY
		DECLARE @Id_User int;
		SET @Id_User = (SELECT Id_User FROM C_HASHTAG.Usuario WHERE Username = @Username)
		UPDATE c set
				Nombre = @Nombre, Mail = @Mail, Telefono = @Telefono, Domicilio_Calle = @Domicilio_Calle, Cod_Postal = @Cod_Postal, Apellido = @Apellido,Localidad = @Localidad,
				Nro_Doc = @Nro_Doc, Tipo_Doc = (SELECT TOP 1 Doc_Codigo FROM C_HASHTAG.Tipo_Doc
															where Doc_Desc = @Tipo_Doc),
				Nro_Calle = @Nro_Calle, Piso = @Piso,Departamento = @Departamento, Nacimiento = @Nacimiento
			from C_HASHTAG.Cliente c join C_HASHTAG.Usuario u
			on(c.Id_User = u.Id_User)
			where c.Id_User = @Id_User
	END TRY
	BEGIN CATCH
		ROLLBACK
		RAISERROR('Ya existe un cliente con ese documento y/o email', 16, 1)
		RETURN
	END CATCH
	
	SELECT TOP 1 Id_Cliente Id_User
		FROM C_HASHTAG.Cliente
		ORDER BY Id_Cliente DESC
COMMIT
GO

/****************************************************************
 *						crearVisibilidad
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.crearVisibilidad
	@Visibilidad_Desc nvarchar(255),
	@Comision_Prod_Vend numeric(18,2),
	@Comision_Envio_Prod numeric(18,2),
	@Comision_Tipo_Public numeric(18,2),
	@Habilitado bit
AS
	BEGIN TRY
		INSERT INTO C_HASHTAG.Visibilidad (Visibilidad_Desc,Comision_Prod_Vend, Comision_Envio_Prod, Comision_Tipo_Public, Habilitado)
		VALUES (@Visibilidad_Desc, @Comision_Prod_Vend, @Comision_Envio_Prod, @Comision_Tipo_Public, @Habilitado)
		-- Devuelvo el id de la visibilidad
		SELECT Id_Visibilidad FROM C_HASHTAG.Visibilidad WHERE Visibilidad_Desc = @Visibilidad_Desc
	END TRY
	BEGIN CATCH
		DECLARE @MensajeError varchar(255)
		SET @MensajeError = 'El nombre "' + @Visibilidad_Desc + '" ya esta en uso'
		RAISERROR(@MensajeError, 16, 1)
	END CATCH
GO

/****************************************************************
 *							ObtenerVisibilidades
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.obtenerVisibilidades
AS
	SELECT *
		FROM Visibilidad
		where Habilitado = 1
GO

/****************************************************************
 *							cambiarEstadoVisibilidad
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.cambiarEstadoVisibilidad
	@Id_Visibilidad int
AS 
		UPDATE C_HASHTAG.Visibilidad
		set Habilitado = ~Habilitado
		where Id_Visibilidad = @Id_Visibilidad
GO

/****************************************************************
 *							ObtenerVisibilidad
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.obtenerVisibilidad @Id_Visibilidad int
AS
	SELECT * FROM C_HASHTAG.Visibilidad
	where Id_Visibilidad = @Id_Visibilidad
GO

/****************************************************************
 *						modificarVisibilidad
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.modificarVisibilidad
	@Id_Visibilidad numeric (18,0),
	@Visibilidad_Desc nvarchar(255),
	@Comision_Prod_Vend numeric(18,2),
	@Comision_Envio_Prod numeric(18,2),
	@Comision_Tipo_Public numeric(18,2),
	@Habilitado bit
AS
	BEGIN TRY
		UPDATE C_HASHTAG.Visibilidad set 
			Visibilidad_Desc = @Visibilidad_Desc,
			Comision_Prod_Vend = @Comision_Prod_Vend,
			Comision_Envio_Prod = @Comision_Envio_Prod,
			Comision_Tipo_Public = @Comision_Tipo_Public,
			Habilitado = @Habilitado
			where Id_Visibilidad = @Id_Visibilidad
	END TRY
	BEGIN CATCH
		DECLARE @MensajeError varchar(255)
		SET @MensajeError = 'El nombre "' + @Visibilidad_Desc + '" ya esta en uso'
		RAISERROR(@MensajeError, 16, 1)
	END CATCH
GO

/****************************************************************
 *						facturarPublicacion
 ****************************************************************/
 CREATE PROCEDURE C_HASHTAG.facturarPublicacion @Id_Publicacion int
 as

	declare @Id_User int
	set @Id_User = (select Id_User from C_HASHTAG.Publicacion where Id_Publicacion = @Id_Publicacion)

	declare @Comision_Tipo_Public numeric(18,2), @Id_Factura int
	set @Comision_Tipo_Public = (select Comision_Tipo_Public 
								from C_HASHTAG.Publicacion p
								join C_HASHTAG.Visibilidad v
								on (p.Id_Visibilidad = v.Id_Visibilidad)
								where Id_Publicacion = @Id_Publicacion)

	--Si es la primera publicacion de un usuario nuevo no le facturo ninguna comision
	if((select Publicacion_Gratis from C_HASHTAG.Usuario where Id_User = @Id_User) = 1)
	begin
		set @Comision_Tipo_Public = 0
		
		update C_HASHTAG.Usuario
		set Publicacion_Gratis = 0
		where Id_User = @Id_User
		
	end

	insert into C_HASHTAG.Factura
	Values (@Id_Publicacion, C_HASHTAG.obtenerFecha(), @Comision_Tipo_Public)

	set @Id_Factura = (select top 1 Id_Factura from C_HASHTAG.Factura
						order by Id_Factura desc)

	insert into C_HASHTAG.Item 
	(Id_Factura, Descripcion, Monto, Cantidad)
	values
	(@Id_Factura,'Comision por publicacion', @Comision_Tipo_Public, 1)
go


/****************************************************************
 *						generarCompra
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.generarCompra
	@Monto numeric(18,2),
	@Visibilidad nvarchar(255),
	@Id_User numeric(18,0),
	@Preguntas bit,
	@Stock numeric(18,0),
	@Estado nvarchar(255),
	@Descripcion nvarchar(255),
	@Envio bit
as
	if (@Monto < 0 or @Stock  <= 0 or @Descripcion is null or @Visibilidad is null)
	begin
		RAISERROR('Debe ingresar bien los parametros', 16, 1)
		return
	end

	begin transaction
		declare @fecha datetime, @Id_Publicacion int
		set @fecha = C_HASHTAG.obtenerFecha()
		INSERT INTO C_HASHTAG.Publicacion
		(
			Monto,
			Id_Visibilidad,
			Id_User,
			Id_Estado,
			Id_Tipo_Public, 
			Fecha_Inicial,
			Fecha_Final,
			Preguntas,
			Stock,
			Descripcion,
			Envio
		)	
		values
			(@Monto, (select top 1 Id_Visibilidad from C_HASHTAG.Visibilidad v where v.Visibilidad_Desc = @Visibilidad),
			@Id_User, (select top 1 e.Id_Estado from C_HASHTAG.Estado e where e.Descripcion = @Estado), 1 /*compra inmediata*/ , @fecha , dateadd(month, 1, @fecha), @Preguntas, @Stock, @Descripcion, @Envio)
					
		set @Id_Publicacion = (select top 1 Id_Publicacion
								from C_HASHTAG.Publicacion
								order by Id_Publicacion desc)

		select top 1 Id_Publicacion
								from C_HASHTAG.Publicacion
								order by Id_Publicacion desc

		exec C_HASHTAG.facturarPublicacion @Id_Publicacion

	commit transaction
go

/****************************************************************
 *						generarSubasta
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.generarSubasta
	@Monto numeric(18,2),
	@Visibilidad nvarchar(255),
	@Id_User numeric(18,0),
	@Preguntas bit,
	@Estado nvarchar(255) ,
	@Descripcion nvarchar(255),
	@Envio bit
as
	if (@Monto < 0 or @Descripcion is null or @Visibilidad is null)
	begin
		RAISERROR('Ingresar bien los parametros', 16, 1)
		return
	end

	begin transaction
		declare @fecha datetime, @Id_Publicacion int
		set @fecha = C_HASHTAG.obtenerFecha()
		INSERT INTO C_HASHTAG.Publicacion
		(
			Monto,
			Id_Visibilidad,
			Id_User,
			Id_Estado,
			Id_Tipo_Public, 
			Fecha_Inicial,
			Fecha_Final,
			Preguntas,
			Stock,
			Descripcion,
			Envio
		)	
		values
			(@Monto, (select top 1 Id_Visibilidad from C_HASHTAG.Visibilidad v where v.Visibilidad_Desc = @Visibilidad),
			@Id_User, (select top 1 e.Id_Estado from C_HASHTAG.Estado e where e.Descripcion = @Estado), 2 /*subasta*/, @fecha , dateadd(month, 1, @fecha), @Preguntas, 1, @Descripcion, @Envio)
	
			set @Id_Publicacion = (select top 1 Id_Publicacion
								from C_HASHTAG.Publicacion
								order by Id_Publicacion desc)

		select top 1 Id_Publicacion
								from C_HASHTAG.Publicacion
								order by Id_Publicacion desc

		exec C_HASHTAG.facturarPublicacion @Id_Publicacion

	commit transaction
go

/****************************************************************
 *							ObtenerEstadosElegibles
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.obtenerEstadosElegibles
AS
	SELECT *
		FROM C_HASHTAG.Estado e
		where e.Descripcion like 'Activa' or e.Descripcion like 'Borrador'
GO

/****************************************************************
 *							agregarRubroAPublicacion
 ****************************************************************/

CREATE PROCEDURE C_HASHTAG.agregarRubroAPublicacion
	@Id_Publicacion int,
	@Rubro nvarchar(255)
AS
	INSERT INTO C_HASHTAG.Rubro_Publicacion(Id_Publicacion, Id_Rubro)
		VALUES (@Id_Publicacion, (select top 1 Id_Rubro from C_HASHTAG.Rubro where Desc_Corta = @Rubro))
GO


/****************************************************************
 *							ObtenerPublicaciones
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.obtenerPublicaciones @Descripcion nvarchar(255), @Rubro nvarchar(255), @Id_User int
AS
	SELECT DISTINCT p.*, tp.Descripcion as 'tipo_public' FROM C_HASHTAG.Publicacion p
	join C_HASHTAG.Tipo_Public tp on(p.Id_Tipo_Public = tp.Id_Tipo_Public)
	join C_HASHTAG.Rubro_Publicacion rp on(p.Id_Publicacion = rp.Id_Publicacion)
	where (Id_Estado = 2 or Id_Estado = 3) --solo muestro las activas y pausadas
	and rp.Id_Rubro = (select top 1 Id_Rubro from C_HASHTAG.Rubro r where r.Desc_Corta = @Rubro)
	and p.Descripcion like '%'+@Descripcion+'%'
	--order by  Id_Visibilidad -- LISTO POR IMPORTANCIA
GO

/****************************************************************
 *							ObtenerPublicacion
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.obtenerPublicacion @Id_Publicacion int
AS
	select p.*, Desc_Reputacion as 'reputacion', Username from C_HASHTAG.Publicacion p
	join C_HASHTAG.Usuario u on (p.Id_User = u.Id_User)
	join C_HASHTAG.Reputacion r on (u.Id_Reputacion = r.Id_Reputacion)
	where Id_Publicacion = @Id_Publicacion
GO

/****************************************************************
 *							FacturarYResgistrarCompra
 ****************************************************************/
 CREATE PROCEDURE C_HASHTAG.FacturarYResgistrarCompra @Id_Publicacion int, @Stock int, @Id_User int
 as
	--realizo la facturacion de la nueva compra
	declare @Total numeric(18,2), @Monto numeric(18,2), @Comision_Prod_Vend numeric(18,2), @Comision_Envio_Prod numeric(18,2), @Envio bit, @Id_Factura int, @TotalEnvio int, @SubTotal int
	
	select  @Monto = Monto, @Envio = Envio, @Comision_Prod_Vend = Comision_Prod_Vend, @Comision_Envio_Prod = Comision_Envio_Prod
	from C_HASHTAG.Publicacion p
	join C_HASHTAG.Visibilidad v
	on (v.Id_Visibilidad = p.Id_Visibilidad)
	where Id_Publicacion = @Id_Publicacion
		set @SubTotal = (@Stock * @Comision_Prod_Vend * @Monto)

	set @TotalEnvio = 0
	if (@Envio = 1)
	begin
		set @TotalEnvio = (@Comision_Envio_Prod * @Monto)
	end	

	set @Total = @SubTotal + @TotalEnvio

	insert into C_HASHTAG.Factura
	(Id_Publicacion, Fecha, Total) Values
	(@Id_Publicacion, C_HASHTAG.obtenerFecha(), @Total)

	set @Id_Factura = (select top 1 Id_Factura from C_HASHTAG.Factura
						order by Id_Factura desc)

	insert into C_HASHTAG.Item 
	(Id_Factura, Descripcion, Monto, Cantidad)
	values
	(@Id_Factura,'Comision por productos vendidos', @SubTotal, @Stock)

	if (@Envio = 1)
	begin
		insert into C_HASHTAG.Item 
		(Id_Factura, Descripcion, Monto, Cantidad)
		values
		(@Id_Factura,'Comision por envio', @TotalEnvio, 1)
	end	

	--registro la compra

	insert into C_HASHTAG.Compra
	(Id_User, Id_Publicacion, Monto, Cantidad, Fecha, Id_Calif)
	values
	(@Id_User, @Id_Publicacion, (select Monto from C_HASHTAG.Publicacion where Id_Publicacion = @Id_Publicacion), @Stock, C_HASHTAG.obtenerFecha(), NULL)
GO
/****************************************************************
 *							RealizarCompra
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.realizarCompra @Id_Publicacion int, @Stock int, @Id_User int
AS
	if ((select Id_Estado from C_HASHTAG.Publicacion where Id_Publicacion = @Id_Publicacion) != 2 )
	begin
		raiserror ('Publicacion no activa',16,1)
		return
	end

	if ((select Id_User from C_HASHTAG.Publicacion where Id_Publicacion = @Id_Publicacion) = @Id_User )
	begin
		raiserror ('No podes auto-comprarte',16,1)
		return
	end

	if ((select count(*) from C_HASHTAG.Compra where Id_User = @Id_User and Id_Calif is null) >= 3)
	begin
		raiserror ('Tenes muchas calificaciones pendientes',16,1)
		return
	end


	if ((@Stock <= (select Stock from C_HASHTAG.Publicacion where Id_Publicacion = @Id_Publicacion)))
	begin
		begin transaction
			--modifico stock de la publicacion
			Update C_HASHTAG.Publicacion
			set Stock = Stock - @Stock
			where Id_Publicacion = @Id_Publicacion

			-- registro la compra y realizo la facturacion
			EXEC C_HASHTAG.FacturarYResgistrarCompra @Id_Publicacion, @Stock, @Id_User

			-- me fijo si la publicacion se quedo sin stock, de ser cierto la finalizo
			if ((select stock from C_HASHTAG.Publicacion where Id_Publicacion = @Id_Publicacion) = 0)
			begin
				update C_HASHTAG.Publicacion
				set Id_Estado = 4 -- 'Finalizada'
				where Id_Publicacion = @Id_Publicacion
			end
		commit transaction
	end
	else
	begin
		raiserror ('No hay suficiente stock',16,1)
	end
GO

/****************************************************************
 *							RealizarOferta
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.realizarOferta @Id_Publicacion int, @MontoOfertado int, @Id_User int
AS
	if ((select Id_Estado from C_HASHTAG.Publicacion where Id_Publicacion = @Id_Publicacion) != 2 )
	begin
		raiserror ('Publicacion no activa',16,1)
		return
	end

	if ((select Id_User from C_HASHTAG.Publicacion where Id_Publicacion = @Id_Publicacion) = @Id_User )
	begin
		raiserror ('No podes auto-ofertarte',16,1)
		return
	end

	if ((select count(*) from C_HASHTAG.Compra where Id_User = @Id_User and Id_Calif is null) >= 3)
	begin
		raiserror ('Tenes muchas calificaciones pendientes',16,1)
		return
	end

	if (@MontoOfertado > (select Monto from C_HASHTAG.Publicacion where Id_Publicacion = @Id_Publicacion))
	begin
	begin transaction
		--modifico el monto de la publicacion
		Update C_HASHTAG.Publicacion
		set Monto = @MontoOfertado
		where Id_Publicacion = @Id_Publicacion

		--registro la oferta

		insert into C_HASHTAG.Oferta(Id_Publicacion, Id_User, Monto_Ofertado, Fecha)
		values
		(@Id_Publicacion, @Id_User, @MontoOfertado, C_HASHTAG.obtenerFecha())
	
	commit transaction
	end
	else
		begin
		raiserror ('El monto ofertado debe superar el monto actual', 16, 1)
	end
GO

/****************************************************************
 *							obtenerComprasSinCalificar
 ****************************************************************/
 CREATE PROCEDURE C_HASHTAG.obtenerComprasSinCalificar @Id_User int
 as
	select c.*, Descripcion from C_HASHTAG.Compra c
		join C_HASHTAG.Publicacion p
		on(c.Id_Publicacion = p.Id_Publicacion)
		where Id_Calif is null and c.Id_User = @Id_User
go

/****************************************************************
 *							Calificar
 ****************************************************************/
 CREATE PROCEDURE C_HASHTAG.Calificar @Id_Compra int, @Cant_Estrellas int, @Descripcion nvarchar (255)
 as

	if((select Id_Calif from C_HASHTAG.Compra where Id_Compra = @Id_Compra) is not null)
	begin
		raiserror('La compra ya fue calificada',16,2)
		return
	end

	else
	begin
		begin transaction
			insert into C_HASHTAG.Calificacion (Cant_Estrellas, Descripcion) values
			(@Cant_Estrellas, @Descripcion)

			declare @Id_Calificacion int
			set @Id_Calificacion = (select top 1 Id_Calificacion from C_HASHTAG.Calificacion
									order by Id_Calificacion desc)

			update C_HASHTAG.Compra
			set Id_Calif = @Id_Calificacion
			where Id_Compra = @Id_Compra

			--actualizo la reputacion del usuario calificado
			declare @Id_User int

			set @Id_User =(select u.Id_User from C_HASHTAG.Usuario u
						   join C_HASHTAG.Publicacion p on (u.Id_User = p.Id_User)
						   join C_HASHTAG.Compra c on (c.Id_Publicacion = p.Id_Publicacion)
						   where Id_Compra = @Id_Compra)

			exec C_HASHTAG.modificarReputacion @Id_User

		commit transaction
	end
go

/****************************************************************
 *							ObtenerUltimasCalificaciones
 ****************************************************************/
 CREATE PROCEDURE C_HASHTAG.obtenerUltimasCalificaciones @Id_User int
 as
	select top 5 Cant_Estrellas, ca.Descripcion as 'Desc_calif', p.Descripcion as 'Desc_public' from C_HASHTAG.Calificacion ca
	join C_HASHTAG.Compra co
	on(ca.Id_Calificacion = co.Id_Calif)
	join C_HASHTAG.Publicacion p
	on(co.Id_Publicacion = p.Id_Publicacion)
	where co.Id_User = @Id_User
	order by Id_Calificacion desc
go

/****************************************************************
 *							ObtenerCalificaciones
 ****************************************************************/
 CREATE PROCEDURE C_HASHTAG.obtenerCalificaciones @Id_User int
 as
	select Cant_Estrellas, Count (Id_Calif) as 'cant_calif' from C_HASHTAG.Calificacion ca
	join C_HASHTAG.Compra co 
	on (co.Id_Calif = ca.Id_Calificacion)
	where Id_User = @Id_User
	group by Cant_Estrellas
go

/****************************************************************
 *							ObtenerCompras
 ****************************************************************/
 CREATE PROCEDURE C_HASHTAG.obtenerCompras @Id_User int
 as
	select p.Descripcion, c.Monto, c.Cantidad, c.Fecha
	from C_HASHTAG.Compra c
	join C_HASHTAG.Publicacion p
	on (c.Id_Publicacion = p.Id_Publicacion)
	where c.Id_User = @Id_User
	order by Id_Compra desc
go

/****************************************************************
 *							ObtenerOfertas
 ****************************************************************/
 CREATE PROCEDURE C_HASHTAG.obtenerOfertas @Id_User int
 as
	select p.Descripcion, o.Monto_Ofertado, o.Fecha
	from C_HASHTAG.Oferta o
	join C_HASHTAG.Publicacion p
	on (o.Id_Publicacion = p.Id_Publicacion)
	where o.Id_User = @Id_User
	order by Id_Oferta desc
go



/****************************************************************
 *					cambiarContraseniaPorAdmin
 ****************************************************************/
 CREATE PROCEDURE C_HASHTAG.cambiarContraseniaPorAdmin @Username nvarchar (255), @Contrasenia1 nvarchar (255), @Contrasenia2 nvarchar (255)
 as
	if(@Contrasenia1 != @Contrasenia2)
	begin
		raiserror ('Confirme bien la nueva contraseña',16,2)
		return
	end
	else
	begin
		update C_HASHTAG.Usuario
		set Contraseña = @Contrasenia1
		where Username = @Username
	end
go

/****************************************************************
 *					cambiarContraseniaPorUser
 ****************************************************************/
 CREATE PROCEDURE C_HASHTAG.cambiarContraseniaPorUser @Id_User nvarchar (255),  @ContraseniaVieja nvarchar (255), @Contrasenia1 nvarchar (255), @Contrasenia2 nvarchar (255)
 as
	if(@ContraseniaVieja != (select Contraseña from C_HASHTAG.Usuario where Id_User = @Id_User))
	begin
		raiserror ('Contraseña actual incorrecta',16,2)
		return
	end

	if(@Contrasenia1 != @Contrasenia2)
	begin
		raiserror ('Confirme bien la nueva contraseña',16,2)
		return
	end
	else
	begin
		update C_HASHTAG.Usuario
		set Contraseña = @Contrasenia1
		where Id_User = @Id_User
	end
go

/****************************************************************
 *					obtenerFechaInicioTrimestre
 ****************************************************************/
CREATE FUNCTION C_HASHTAG.obtenerFechaInicioTrimestre (@anio int, @trimestre int)
	RETURNS datetime
AS
BEGIN
	RETURN CAST(
		CAST(@anio AS varchar(4))
		+ RIGHT('0' + CAST((@trimestre - 1) * 3 + 1 AS varchar(2)), 2)
		+ '01'
		AS DATETIME)
END
GO


/****************************************************************
 *					obtenerFechaFinTrimestre
 ****************************************************************/
CREATE FUNCTION C_HASHTAG.obtenerFechaFinTrimestre (@anio int, @trimestre int) RETURNS datetime
AS
BEGIN
	DECLARE @FechaFin datetime
	IF @trimestre = 4
	BEGIN
		SET @FechaFin = CAST(CAST(@anio as varchar(4)) + '1231' AS datetime)
	END
	ELSE
	BEGIN
		SET @FechaFin = CAST(
			CAST(@anio AS varchar(4))
			+ RIGHT('0' + CAST(@trimestre * 3 + 1 AS varchar(2)), 2)
			+ '01'
			AS DATETIME) - 1
	END
	
	RETURN @FechaFin
END
GO

/****************************************************************
 *					vendedoresConMayorCantDeProdsNoVendidos
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.vendedoresConMayorCantDeProdsNoVendidos
	@anio int,
	@trimestre int,
	@visibilidad nvarchar(255)
AS
	
	IF C_HASHTAG.obtenerFechaInicioTrimestre(@anio, @trimestre) > C_HASHTAG.obtenerFecha()
	BEGIN
		RAISERROR('El trimestre seleccionado se encuentra en el futuro', 16, 1)
		RETURN
	END

	if(@visibilidad like 'Todas')
	begin
		SELECT TOP 5 u.Username as 'Username', p.Descripcion as 'Descripcion', p.Stock as 'Stock', Visibilidad_Desc as 'Visibilidad', p.Fecha_Final as 'Fecha_Final'
		from C_HASHTAG.Usuario u
		join C_HASHTAG.Publicacion p on (u.Id_User = p.Id_User)
		join C_HASHTAG.Visibilidad v on (p.Id_Visibilidad = v.Id_Visibilidad)
		where (Fecha_Final > =  C_HASHTAG.obtenerFechaInicioTrimestre(@anio, @trimestre) and Fecha_Final < =  C_HASHTAG.obtenerFechaFinTrimestre(@anio, @trimestre))
			or (Fecha_Inicial > =  C_HASHTAG.obtenerFechaInicioTrimestre(@anio, @trimestre) and Fecha_Inicial < =  C_HASHTAG.obtenerFechaFinTrimestre(@anio, @trimestre))
		order by 3 desc, p.Id_Visibilidad, 5 asc
	end

	else
	begin
		SELECT TOP 5 u.Username as 'Username', p.Descripcion as 'Descripcion', p.Stock as 'Stock', Visibilidad_Desc as 'Visibilidad', p.Fecha_Final as 'Fecha_Final'
		from C_HASHTAG.Usuario u
		join C_HASHTAG.Publicacion p on (u.Id_User = p.Id_User)
		join C_HASHTAG.Visibilidad v on (p.Id_Visibilidad = v.Id_Visibilidad)
		where (@visibilidad like (select v.Visibilidad_Desc from C_HASHTAG.Visibilidad v where p.Id_Visibilidad = v.Id_Visibilidad))
		and ((Fecha_Final > =  C_HASHTAG.obtenerFechaInicioTrimestre(@anio, @trimestre) and Fecha_Final < =  C_HASHTAG.obtenerFechaFinTrimestre(@anio, @trimestre))
			or (Fecha_Inicial > =  C_HASHTAG.obtenerFechaInicioTrimestre(@anio, @trimestre) and Fecha_Inicial < =  C_HASHTAG.obtenerFechaFinTrimestre(@anio, @trimestre)))
		order by 3 desc, p.Id_Visibilidad, 5 asc
	end
GO

/****************************************************************
 *					clientesConMayorCantDeProdsComprados
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.clientesConMayorCantDeProdsComprados
	@anio int,
	@trimestre int,
	@rubro nvarchar(255)
AS
	
	IF C_HASHTAG.obtenerFechaInicioTrimestre(@anio, @trimestre) > C_HASHTAG.obtenerFecha()
	BEGIN
		RAISERROR('El trimestre seleccionado se encuentra en el futuro', 16, 1)
		RETURN
	END

	SELECT TOP 5 u.Username, sum(Cantidad) as 'CantidadProductos',r1.Desc_Corta as 'Rubro'
	from C_HASHTAG.Usuario u
	join C_HASHTAG.Compra c on (u.Id_User = c.Id_User)
	join C_HASHTAG.Publicacion p on (c.Id_Publicacion = p.Id_Publicacion)
	join C_HASHTAG.Rubro_Publicacion rp on (p.Id_Publicacion = rp.Id_Publicacion)
	join C_HASHTAG.Rubro r1 on (rp.Id_Rubro = r1.Id_Rubro)
	where (Fecha > =  C_HASHTAG.obtenerFechaInicioTrimestre(@anio, @trimestre) and Fecha < =  C_HASHTAG.obtenerFechaFinTrimestre(@anio, @trimestre)
	and @rubro = (select Desc_Corta from C_HASHTAG.Rubro r where rp.Id_Rubro = r.Id_Rubro))
	group by u.Username, r1.Desc_Corta
	order by 2 desc

GO

/****************************************************************
 *					vendedoresConMayorCantDeFacturas
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.vendedoresConMayorCantDeFacturas
	@anio int,
	@trimestre int
AS
	
	IF C_HASHTAG.obtenerFechaInicioTrimestre(@anio, @trimestre) > C_HASHTAG.obtenerFecha()
	BEGIN
		RAISERROR('El trimestre seleccionado se encuentra en el futuro', 16, 1)
		RETURN
	END

	SELECT TOP 5 u.Username, count(f.Id_Factura) as 'CantidadFacturas'
	from C_HASHTAG.Usuario u
	join C_HASHTAG.Publicacion p on (u.Id_User = p.Id_User)
	join C_HASHTAG.Factura f on (f.Id_Publicacion = p.Id_Publicacion)
	where (Fecha > =  C_HASHTAG.obtenerFechaInicioTrimestre(@anio, @trimestre) and Fecha < =  C_HASHTAG.obtenerFechaFinTrimestre(@anio, @trimestre))
	group by u.Username
	order by 2 desc
GO

/****************************************************************
 *					vendedoresConMayorMontoFacturado
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.vendedoresConMayorMontoFacturado
	@anio int,
	@trimestre int
AS
	
	IF C_HASHTAG.obtenerFechaInicioTrimestre(@anio, @trimestre) > C_HASHTAG.obtenerFecha()
	BEGIN
		RAISERROR('El trimestre seleccionado se encuentra en el futuro', 16, 1)
		RETURN
	END

	SELECT TOP 5 u.Username, sum(c.Monto) as 'MontoTotal'
	from C_HASHTAG.Usuario u
	join C_HASHTAG.Publicacion p on (u.Id_User = p.Id_User)
	join C_HASHTAG.Compra c on (c.Id_Publicacion = p.Id_Publicacion)
	where (c.Fecha > =  C_HASHTAG.obtenerFechaInicioTrimestre(@anio, @trimestre) and c.Fecha < =  C_HASHTAG.obtenerFechaFinTrimestre(@anio, @trimestre))
	group by u.Username
	order by 2 desc
GO

 /****************************************************************
 *					obtenerBorradores
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.obtenerBorradores @Id_User int
as
	select p.*, tp.Descripcion as 'tipo_public' from C_HASHTAG.Publicacion p
	join C_HASHTAG.Tipo_Public tp
	on (p.Id_Tipo_Public = tp.Id_Tipo_Public)
	where Id_User = @Id_User and Id_Estado = 1
go

/****************************************************************
 *							ObtenerPublicacionAModificar
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.obtenerPublicacionAModificar @Id_Publicacion int
AS
	select top 1 p.*, Visibilidad_Desc from C_HASHTAG.Publicacion p
	join C_HASHTAG.Visibilidad v on(v.Id_Visibilidad = p.Id_Visibilidad)
	where Id_Publicacion = @Id_Publicacion
GO

/****************************************************************
 *							obtenerRubrosPublicacion
 ****************************************************************/
 CREATE PROCEDURE C_HASHTAG.obtenerRubrosPublicacion @Id_Publicacion int
 as
	select r.Desc_Corta from C_HASHTAG.Rubro r
	join C_HASHTAG.Rubro_Publicacion rp on (r.Id_Rubro = rp.Id_Rubro)
	join C_HASHTAG.Publicacion p on (rp.Id_Publicacion = p.Id_Publicacion) 
	where p.Id_Publicacion = @Id_Publicacion
go

/****************************************************************
 *						modificarCompra
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.modificarCompra
	@Monto numeric(18,2),
	@Preguntas bit,
	@Stock numeric(18,0),
	@Estado nvarchar(255),
	@Descripcion nvarchar(255),
	@Envio bit,
	@Id_Publicacion int
as
	if ((select Id_Estado from C_HASHTAG.Publicacion where Id_Publicacion = @Id_Publicacion) !=1)
	begin
		RAISERROR('Solo se pueden modificar borradores', 16, 1)
		return
	end

	if (@Monto < 0 or @Stock  <= 0 or @Descripcion is null)
	begin
		RAISERROR('Debe ingresar bien los parametros', 16, 1)
		return
	end

	begin transaction

		update C_HASHTAG.Publicacion
		set Monto = @Monto, Id_Estado = (select top 1 e.Id_Estado from C_HASHTAG.Estado e where e.Descripcion = @Estado),
		Preguntas = @Preguntas, Stock = @Stock, Descripcion = @Descripcion, Envio = @Envio
		where Id_Publicacion = @Id_Publicacion

	commit transaction
go

/****************************************************************
 *						ModificarSubasta
 ****************************************************************/
CREATE PROCEDURE C_HASHTAG.modificarSubasta
	@Monto numeric(18,2),
	@Preguntas bit,
	@Estado nvarchar(255),
	@Descripcion nvarchar(255),
	@Envio bit,
	@Id_Publicacion int
as
	if ((select Id_Estado from C_HASHTAG.Publicacion where Id_Publicacion = @Id_Publicacion) !=1)
	begin
		RAISERROR('Solo se pueden modificar borradores', 16, 1)
		return
	end

	if (@Monto < 0 or @Descripcion is null )
	begin
		RAISERROR('Debe ingresar bien los parametros', 16, 1)
		return
	end

	begin transaction

		update C_HASHTAG.Publicacion
		set Monto = @Monto, Id_Estado = (select top 1 e.Id_Estado from C_HASHTAG.Estado e where e.Descripcion = @Estado),
		Preguntas = @Preguntas, Descripcion = @Descripcion, Envio = @Envio
		where Id_Publicacion = @Id_Publicacion

	commit transaction
go


/****************************************************************
 *						EliminarRubrosDePublicacion
 ****************************************************************/

CREATE PROCEDURE C_HASHTAG.eliminarRubrosDePublicacion
	@Id_Publicacion int
AS
	delete C_HASHTAG.Rubro_Publicacion
	where Id_Publicacion = @Id_Publicacion
GO


/****************************************************************
 *						ModificarReputacion
 ****************************************************************/
 CREATE PROCEDURE C_HASHTAG.modificarReputacion @Id_User int
 as

	if((select count(Id_Compra) from C_HASHTAG.Compra c
		join C_HASHTAG.Publicacion p on c.Id_Publicacion = p.Id_Publicacion
		where p.Id_User = @Id_User) = 0)
	begin
		update C_HASHTAG.Usuario
		set Id_Reputacion = 1
		where Id_User = @Id_User
		return
	end

	declare @Promedio int

	set @Promedio = (select (sum(Cant_Estrellas) / count(Id_Compra)) from C_HASHTAG.Calificacion c
					join C_HASHTAG.Compra co on (c.Id_Calificacion = co.Id_Calif)
					join C_HASHTAG.Publicacion p on (p.Id_Publicacion = co.Id_Publicacion)
					join C_HASHTAG.Usuario u on (p.Id_User = u.Id_User)
					where u.Id_User = @Id_User)

	update C_HASHTAG.Usuario
	set Id_Reputacion = (select r.Id_Reputacion from C_HASHTAG.Reputacion r where Promedio = @Promedio)
	where Id_User = @Id_User
go	

/***********************************************************************
 *
 *						MIGRACION DE DATOS
 *
 ***********************************************************************/


USE GD1C2016

/****************************************************************/
--							FECHA_DEL_SISTEMA
/****************************************************************/
CREATE TABLE C_HASHTAG.[FECHA_DEL_SISTEMA](
	[Fecha][DateTime] PRIMARY KEY
)

-- Cargo la fecha en la que inicia el sistema
/*DECLARE @fecha datetime
SET @fecha = CAST('20150101' AS datetime)
EXEC C_HASHTAG.SetFecha @fecha
GO*/
insert into C_HASHTAG.FECHA_DEL_SISTEMA
(Fecha) values
(CAST('20161231' AS datetime))


/****************************************************************/
--							ROL
/****************************************************************/
CREATE TABLE C_HASHTAG.Rol
(
	Id_Rol numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Nombre_Rol nvarchar(255) NOT NULL,
	Habilitado bit NOT NULL DEFAULT 1
)
GO

INSERT INTO C_HASHTAG.Rol (Nombre_Rol, Habilitado) VALUES ('Administrador', 1)
INSERT INTO C_HASHTAG.Rol (Nombre_Rol, Habilitado) VALUES ('Cliente', 1)
INSERT INTO C_HASHTAG.Rol (Nombre_Rol, Habilitado) VALUES ('Empresa', 1)
--inserto rol que pide el enunciado para probar todas las funcionalidades
INSERT INTO C_HASHTAG.Rol (Nombre_Rol, Habilitado) VALUES ('Rol Especial', 1)

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
	Nombre_Funcionalidad nvarchar(255) UNIQUE NOT NULL
)

INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('ABMRol')
INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('ABMUsuario')
INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('ABMVisibilidad')
INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('GenerarPublicacion')
INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('Comprar/Ofertar')
INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('ObtenerHistorial')
INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('Calificar')
INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('ObtenerListadoEstadistico')
INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('ConsultarFacturas')
INSERT INTO C_HASHTAG.Funcionalidad (Nombre_Funcionalidad) VALUES ('CambiarContraseña')

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
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (1,10)


--Inserto funcionalidades del cliente
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (2,5)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (2,6)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (2,7)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (2,10)

--Inserto funcionalidades de la empresa
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (3,4)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (3,9)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (3,10)

--Inserto todas las funcionalidades al rol especial
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (4,1)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (4,2)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (4,3)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (4,4)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (4,5)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (4,6)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (4,7)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (4,8)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (4,9)
INSERT INTO C_HASHTAG.Funcionalidad_Rol (Id_Rol, Id_Funcionalidad) VALUES (4,10)

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
--							Reputacion
/****************************************************************/
CREATE TABLE C_HASHTAG.Reputacion
(
	Id_Reputacion numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Promedio numeric (18,0),
	Desc_Reputacion nvarchar(255) 
)

INSERT INTO C_HASHTAG.Reputacion (Promedio, Desc_Reputacion) VALUES (0, 'Sin reputacion')
INSERT INTO C_HASHTAG.Reputacion (Promedio, Desc_Reputacion) VALUES (1, 'Mala')
INSERT INTO C_HASHTAG.Reputacion (Promedio, Desc_Reputacion) VALUES (2, 'Regular')
INSERT INTO C_HASHTAG.Reputacion (Promedio, Desc_Reputacion) VALUES (3, 'Buena')
INSERT INTO C_HASHTAG.Reputacion (Promedio, Desc_Reputacion) VALUES (4, 'Muy buena')
INSERT INTO C_HASHTAG.Reputacion (Promedio, Desc_Reputacion) VALUES (5, 'Perfecta')

/****************************************************************/
--							Usuario
/****************************************************************/
CREATE TABLE C_HASHTAG.Usuario
(
	Id_User numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Username nvarchar(50) UNIQUE NOT NULL,
	Contraseña varchar(255) NOT NULL,
	Intentos_Fallidos numeric(18,0) DEFAULT 0,
	Habilitado bit DEFAULT 1,
	Publicacion_Gratis bit,
	Id_Reputacion numeric(18,0) NOT NULL FOREIGN KEY REFERENCES C_HASHTAG.Reputacion(Id_Reputacion)
)

--Creo administrador con Id_User:1, username:admin1 y password:administrador (hasheada)
INSERT INTO C_HASHTAG.Usuario (Username, Contraseña, Intentos_Fallidos, Habilitado, Publicacion_Gratis, Id_Reputacion) VALUES 
	('admin1', 'b20b0f63ce2ed361e8845d6bf2e59811aaa06ec96bcdb92f9bc0c5a25e83c9a6',0,1,0,1)

--Creo administrador con Id_User:2, username:admin2 y password:administrador (hasheada)
INSERT INTO C_HASHTAG.Usuario (Username, Contraseña, Intentos_Fallidos, Habilitado, Publicacion_Gratis, Id_Reputacion) VALUES 
	('admin2', 'b20b0f63ce2ed361e8845d6bf2e59811aaa06ec96bcdb92f9bc0c5a25e83c9a6',0,1,0,1)

--PEDIDO DEL ENUNCIADO: Creo usuario de rol especial con Id_User:3, username:admin y password:w23e (hasheada)
INSERT INTO C_HASHTAG.Usuario (Username, Contraseña, Intentos_Fallidos, Habilitado,Publicacion_Gratis, Id_Reputacion) VALUES 
	('admin', '6b1583aea70be6a605d44e3563880d1b833a1d25c7ac5cb0222101e83a76559f',0,1,0,1)


INSERT INTO C_HASHTAG.Usuario
(
	Username,
	Contraseña,
	Intentos_Fallidos,
	Habilitado,
	Publicacion_Gratis,
	Id_Reputacion
)
	SELECT DISTINCT
		'usuario.cliente.' + RIGHT(CONVERT(varchar(18),Cli_Dni),18),
		'5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', --SHA256 de "password"
		0,
		1,
		0, -- no posee ningun usuario migrado la publicacion gratis
		1 -- sin reputacion
		FROM gd_esquema.Maestra
		where Cli_Dni IS NOT NULL
	UNION
		SELECT DISTINCT
		'usuario.cliente.' + RIGHT(CONVERT(varchar(18),Publ_Cli_Dni),18),
		'5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', --SHA256 de "password"
		0,
		1,
		0, -- no posee ningun usuario migrado la publicacion gratis
		1 -- sin reputacion
		FROM gd_esquema.Maestra
		where Publ_Cli_Dni IS NOT NULL

	UNION
	SELECT DISTINCT
		'usuario.empresa.' + RIGHT(CONVERT(varchar(18),Publ_Empresa_Cuit),18),
		'5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', --SHA256 de "password"
		0,
		1,
		0, -- no posee ningun usuario migrado la publicacion gratis
		1 --sin reputacion
		FROM gd_esquema.Maestra
		where Publ_Empresa_Cuit IS NOT NULL

/****************************************************************/
--							Cliente
/****************************************************************/
CREATE TABLE C_HASHTAG.Cliente
(
	Id_Cliente numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Nombre nvarchar(255) NOT NULL,
	Apellido nvarchar(255) NOT NULL,
	Nro_Doc numeric(18,0) NOT NULL,
	Tipo_Doc numeric(18,0) NOT NULL FOREIGN KEY REFERENCES C_HASHTAG.Tipo_doc(Doc_Codigo),
	Mail nvarchar(255) NOT NULL,
	Telefono numeric(18,0),
	Domicilio_Calle nvarchar(100),
	Nro_Calle numeric(18,0),
	Piso numeric(18,0),
	Departamento nvarchar(50),
	Cod_Postal nvarchar(50),
	Nacimiento datetime,
	Localidad varchar (255),
	Creacion datetime,
	Id_User numeric(18,0) NOT NULL FOREIGN KEY REFERENCES C_HASHTAG.Usuario(Id_User),
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
		C_HASHTAG.obtenerFecha(),
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
		C_HASHTAG.obtenerFecha(),
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
	Razon_Social nvarchar(255) UNIQUE NOT NULL,
	Mail nvarchar(50) NOT NULL,
	Telefono numeric(18,0),
	Calle nvarchar(100),
	Cod_Postal nvarchar(50),
	Ciudad nvarchar(255),
	Localidad nvarchar(255),
	Cuit nvarchar(255) UNIQUE NOT NULL,
	Nombre_Contacto nvarchar(255),
	Rubro_Principal numeric(18,0) NOT NULL FOREIGN KEY REFERENCES C_HASHTAG.Rubro(Id_Rubro),
	Nro_Calle numeric(18,0),
	Piso numeric(18,0),
	Departamento nvarchar(50),
	Creacion DateTime,
	Id_User numeric(18,0) NOT NULL FOREIGN KEY REFERENCES C_HASHTAG.Usuario(Id_User)
)


INSERT INTO C_HASHTAG.Empresa
(
	Razon_Social, 
	Mail,
	Telefono,
	Calle,
	Cod_Postal,
	Ciudad,
	Localidad,
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
		NULL, -- no existe el campo localidad
		Publ_Empresa_Cuit,
		NULL, -- no poseen nombre contacto
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
		C_HASHTAG.obtenerFecha(),
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
	Descripcion nvarchar(255) NOT NULL
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
	Comision_Tipo_Public numeric(18,2),
	Habilitado bit 
)

/*Gratis no provee servicio de envío.
Para las restantes publicaciones independientemente de cual sea su visibilidad, se
debe elegir si dicha publicación brindará o no el servicio de envío sobre el producto
comprado.*/


INSERT INTO C_HASHTAG.Visibilidad (Visibilidad_Desc, Comision_Prod_Vend, Comision_Envio_Prod, Comision_Tipo_Public, Habilitado)
	VALUES ('Platino', 0.10, 0.05, 180.00, 1)

INSERT INTO C_HASHTAG.Visibilidad (Visibilidad_Desc, Comision_Prod_Vend, Comision_Envio_Prod, Comision_Tipo_Public, Habilitado)
	VALUES ('Oro', 0.15, 0.20, 140.00, 1)

INSERT INTO C_HASHTAG.Visibilidad (Visibilidad_Desc, Comision_Prod_Vend, Comision_Envio_Prod, Comision_Tipo_Public, Habilitado)
	VALUES ('Plata', 0.20, 0.20, 100.00, 1)

INSERT INTO C_HASHTAG.Visibilidad (Visibilidad_Desc, Comision_Prod_Vend, Comision_Envio_Prod, Comision_Tipo_Public, Habilitado)
	VALUES ('Bronce', 0.30, 0.20, 60.00, 1)

INSERT INTO C_HASHTAG.Visibilidad (Visibilidad_Desc, Comision_Prod_Vend, Comision_Envio_Prod, Comision_Tipo_Public, Habilitado)
	VALUES ('Gratis', 0.00, 0.00, 0, 1) 

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
	Descripcion nvarchar(255) NOT NULL 
)
INSERT INTO C_HASHTAG.Tipo_Public (Descripcion) VALUES ('Compra Inmediata')
INSERT INTO C_HASHTAG.Tipo_Public (Descripcion) VALUES ('Subasta')

/****************************************************************/
--							Publicacion
/****************************************************************/
CREATE TABLE C_HASHTAG.Publicacion
(
	Id_Publicacion numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Monto numeric(18,2) NOT NULL,
	Id_Visibilidad numeric(18,0) NOT NULL FOREIGN KEY REFERENCES C_HASHTAG.Visibilidad(Id_Visibilidad),
	Id_User numeric(18,0) NOT NULL FOREIGN KEY REFERENCES C_HASHTAG.Usuario(Id_User),
	Id_Estado numeric(18,0) NOT NULL FOREIGN KEY REFERENCES C_HASHTAG.Estado(Id_Estado),
	Id_Tipo_Public numeric(18,0) NOT NULL FOREIGN KEY REFERENCES C_HASHTAG.Tipo_Public(Id_Tipo_Public), 
	Fecha_Inicial datetime NOT NULL,
	Fecha_Final datetime NOT NULL,
	Preguntas bit,
	Stock numeric(18,0),
	Descripcion nvarchar(255) NOT NULL,
	Envio bit
)


SET IDENTITY_INSERT C_HASHTAG.Publicacion ON
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
	Descripcion,
	Envio
)
	SELECT DISTINCT
		Publicacion_Cod,
		Publicacion_Precio,
		Publicacion_Visibilidad_Cod,
		(SELECT Id_User
			FROM C_HASHTAG.Usuario
			WHERE Username = 'usuario.empresa.' + RIGHT(CONVERT(varchar(18),Publ_Empresa_Cuit),18)),
		4, -- estan todas finalizadas en la fecha de inicio del sistema
		(SELECT top 1 Id_Tipo_Public  
			FROM C_HASHTAG.Tipo_Public
			where Descripcion = Publicacion_Tipo),
		Publicacion_Fecha,
		Publicacion_Fecha_Venc,
		1, -- supongo que todas aceptan preguntas
		Publicacion_Stock,
		Publicacion_Descripcion,
		0 -- Ninguna publicacion de las migradas tiene envio, es una funcionalidad nueva
		FROM gd_esquema.Maestra
		WHERE Publ_Empresa_Cuit IS NOT NULL and Publicacion_Tipo is not null

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
	Descripcion,
	Envio 
)
	SELECT DISTINCT
		Publicacion_Cod,
		Publicacion_Precio,
		Publicacion_Visibilidad_Cod,
		(SELECT Id_User
			FROM C_HASHTAG.Usuario
			WHERE Username = 'usuario.cliente.' + RIGHT(CONVERT(varchar(18),Publ_Cli_Dni),18) ),
		4, -- estan todas finalizadas en la fecha de inicio del sistema
		(SELECT Id_Tipo_Public  
			FROM C_HASHTAG.Tipo_Public
			where Descripcion = Publicacion_Tipo),
		Publicacion_Fecha,
		Publicacion_Fecha_Venc,
		1, -- supongo que todas aceptan preguntas
		Publicacion_Stock,
		Publicacion_Descripcion,
		0 -- Ninguna publicacion de las migradas tiene envio, es una funcionalidad nueva
		FROM gd_esquema.Maestra
		WHERE Publ_Cli_Dni IS NOT NULL and Publicacion_Tipo is not null
SET IDENTITY_INSERT C_HASHTAG.Publicacion OFF


--CREO INDICE para optimizar filtrar por descripcion
CREATE NONCLUSTERED INDEX indicePublicacion
ON C_HASHTAG.Publicacion (Descripcion)

/****************************************************************/
--							Calificacion
/****************************************************************/
CREATE TABLE C_HASHTAG.Calificacion
(
	Id_Calificacion numeric(18,0) identity(1,1) PRIMARY KEY,
	Cant_Estrellas numeric(18,0) NOT NULL, 
	Descripcion nvarchar(255)
)

SET IDENTITY_INSERT C_HASHTAG.Calificacion ON
INSERT INTO C_HASHTAG.Calificacion
(
	Id_Calificacion,
	Cant_Estrellas,
	Descripcion
)
	SELECT DISTINCT
		g2.Calificacion_Codigo,
		(g2.Calificacion_Cant_Estrellas)/2,
		g2.Calificacion_Descripcion
	FROM gd_esquema.Maestra g2	
	WHERE Calificacion_Codigo IS NOT NULL

SET IDENTITY_INSERT C_HASHTAG.Calificacion OFF

/****************************************************************/
--							Compra
/****************************************************************/
CREATE TABLE C_HASHTAG.Compra
(
	Id_Compra numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Id_User numeric(18,0) NOT NULL FOREIGN KEY REFERENCES C_HASHTAG.Usuario(Id_User),
	Id_Publicacion numeric(18,0) NOT NULL FOREIGN KEY REFERENCES C_HASHTAG.Publicacion(Id_Publicacion),
	Monto numeric(18,2) NOT NULL,
	Cantidad numeric(18,0) NOT NULL,
	Fecha datetime,
	Id_Calif numeric(18,0) FOREIGN KEY REFERENCES C_HASHTAG.Calificacion(Id_Calificacion)
)

/*permitir la repeticion de valores no null, pero si null
CREATE UNIQUE NONCLUSTERED INDEX Id_Calif_NotNull 
ON C_HASHTAG.Compra(Id_Calif)
WHERE Id_Calif IS NOT NULL;*/

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
		WHERE Compra_Cantidad IS NOT NULL and Calificacion_Codigo is not null
		--and Compra_Fecha >= (select Fecha_Inicial from C_HASHTAG.Publicacion where Id_Publicacion = Publicacion_Cod)
		--and Compra_Fecha <= (select Fecha_Final from C_HASHTAG.Publicacion where Id_Publicacion = Publicacion_Cod)

/****************************************************************/
--							Factura
/****************************************************************/
CREATE TABLE C_HASHTAG.Factura
(
	Id_Factura numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Id_Publicacion numeric(18,0) NOT NULL FOREIGN KEY REFERENCES C_HASHTAG.Publicacion(Id_Publicacion),
	Fecha datetime NOT NULL,
	Total numeric(18,0) NOT NULL,
)

SET IDENTITY_INSERT C_HASHTAG.Factura ON
INSERT INTO C_HASHTAG.Factura
(
	Id_Factura,
	Id_Publicacion,
	Fecha,
	Total
)
	SELECT DISTINCT
		Factura_Nro,
		Publicacion_Cod,
		Factura_Fecha,
		Factura_Total
		FROM gd_esquema.Maestra
		WHERE Factura_Fecha is NOT NULL
		--and Factura_Fecha >= (select Fecha_Inicial from C_HASHTAG.Publicacion where Id_Publicacion = Publicacion_Cod)
		--and Factura_Fecha <= (select Fecha_Final from C_HASHTAG.Publicacion where Id_Publicacion = Publicacion_Cod)

SET IDENTITY_INSERT C_HASHTAG.Factura OFF

CREATE NONCLUSTERED INDEX indiceFactura
ON C_HASHTAG.Factura (Fecha,Total)


/****************************************************************/
--							Item
/****************************************************************/
CREATE TABLE C_HASHTAG.Item
(
	Id_Item numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Id_Factura numeric(18,0) NOT NULL FOREIGN KEY REFERENCES C_HASHTAG.Factura(Id_Factura),
	Descripcion nvarchar(255) DEFAULT 'Sin descripcion',
	Monto numeric(18,0) NOT NULL,
	Cantidad numeric(18,0) NOT NULL
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
			WHERE Factura_Nro = Id_Factura) as 'fact',
		'Sin descripcion', -- falta descripcion
		Item_Factura_Monto,
		Item_Factura_Cantidad
		FROM gd_esquema.Maestra
		where Item_Factura_Cantidad IS NOT NULL AND 'fact' IS NOT NULL --and Factura_Nro IS NOT NULL   -- no se si poner la restriccion de que el monto tampoco sea null


/****************************************************************/
--							Oferta
/****************************************************************/
CREATE TABLE C_HASHTAG.Oferta


(
	Id_Oferta numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Id_Publicacion numeric(18,0) NOT NULL FOREIGN KEY REFERENCES C_HASHTAG.Publicacion(Id_Publicacion), 
	Id_User numeric(18,0) NOT NULL FOREIGN KEY REFERENCES C_HASHTAG.Usuario(Id_User),
	Monto_Ofertado numeric(18,2) NOT NULL,
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

--PEDIDO DEL ENUNCIADO: Cargo Rol especial (Id_Rol = 4) a usuario con username admin (Id_User = 3)
INSERT INTO C_HASHTAG.Rol_Usuario(Id_User, Id_Rol) VALUES
	(3,4)


/*******SETEO UNA REPUTACION A LOS USUARIOS MIGRADOS***************/

	declare @Id_User int
	declare usuariosCreados cursor for
			select Id_User from C_HASHTAG.Usuario

	open usuariosCreados
	fetch next from usuariosCreados
		into @Id_User
	while @@FETCH_STATUS = 0
		begin
			exec C_HASHTAG.modificarReputacion @Id_User
		fetch next from usuariosCreados
			into @Id_User
		end
	close usuariosCreados
	deallocate usuariosCreados

/*******SETEO UNA REPUTACION A LOS USUARIOS MIGRADOS***************/

