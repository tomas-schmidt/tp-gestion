--drop procedures
DROP PROCEDURE C_HASHTAG.SetFecha
DROP FUNCTION C_HASHTAG.obtenerFecha
DROP PROCEDURE C_HASHTAG.crearRol
DROP PROCEDURE C_HASHTAG.actualizarYBorrarFuncionalidadesRol
DROP PROCEDURE C_HASHTAG.obtenerRoles
DROP PROCEDURE C_HASHTAG.obtenerFuncionalidades
DROP PROCEDURE C_HASHTAG.bajaRol
DROP PROCEDURE C_HASHTAG.agregarFuncionalidadARol
DROP PROCEDURE C_HASHTAG.Login
DROP PROCEDURE C_HASHTAG.obtenerRolesDeUsuario
DROP PROCEDURE C_HASHTAG.obtenerFuncionalidadesDeRol
DROP PROCEDURE C_HASHTAG.obtenerTiposDocumento
DROP PROCEDURE C_HASHTAG.crearUsuarioYCliente
DROP PROCEDURE C_HASHTAG.crearUsuarioYEmpresa
DROP PROCEDURE C_HASHTAG.obtenerRubros
DROP PROCEDURE C_HASHTAG.cambiarEstadoEmpresa
DROP PROCEDURE C_HASHTAG.obtenerRubroDeEmpresa
DROP PROCEDURE C_HASHTAG.obtenerEmpresaYUsername
DROP PROCEDURE C_HASHTAG.modificarEmpresa
DROP PROCEDURE C_HASHTAG.cambiarEstadoCliente
DROP PROCEDURE C_HASHTAG.obtenerClienteYUsername
DROP PROCEDURE C_HASHTAG.obtenerDocDeCliente
DROP PROCEDURE C_HASHTAG.modificarCliente
DROP PROCEDURE C_HASHTAG.crearVisibilidad
DROP PROCEDURE C_HASHTAG.obtenerVisibilidades
DROP PROCEDURE C_HASHTAG.cambiarEstadoVisibilidad
DROP PROCEDURE C_HASHTAG.obtenerVisibilidad
DROP PROCEDURE C_HASHTAG.modificarVisibilidad
DROP PROCEDURE C_HASHTAG.generarCompra
DROP PROCEDURE C_HASHTAG.generarSubasta
DROP PROCEDURE C_HASHTAG.obtenerEstadosElegibles
DROP PROCEDURE C_HASHTAG.agregarRubroAPublicacion
DROP PROCEDURE C_HASHTAG.obtenerPublicaciones
DROP PROCEDURE C_HASHTAG.obtenerPublicacion
DROP PROCEDURE C_HASHTAG.realizarCompra
DROP PROCEDURE C_HASHTAG.realizarOferta
DROP PROCEDURE C_HASHTAG.FacturarYResgistrarCompra
DROP PROCEDURE C_HASHTAG.obtenerComprasSinCalificar
DROP PROCEDURE C_HASHTAG.Calificar
DROP PROCEDURE C_HASHTAG.obtenerUltimasCalificaciones
DROP PROCEDURE C_HASHTAG.obtenerCalificaciones
DROP PROCEDURE C_HASHTAG.obtenerCompras
DROP PROCEDURE C_HASHTAG.obtenerOfertas
DROP PROCEDURE C_HASHTAG.obtenerRol
DROP PROCEDURE C_HASHTAG.facturarPublicacion
DROP FUNCTION C_HASHTAG.obtenerFechaInicioTrimestre
DROP FUNCTION C_HASHTAG.obtenerFechaFinTrimestre
DROP PROCEDURE C_HASHTAG.vendedoresConMayorCantDeProdsNoVendidos
DROP PROCEDURE C_HASHTAG.cambiarContraseniaPorAdmin
DROP PROCEDURE C_HASHTAG.cambiarContraseniaPorUser


--Drops tablas
DROP TABLE C_HASHTAG.Rol_Usuario
DROP TABLE C_HASHTAG.Rubro_Publicacion
DROP TABLE C_HASHTAG.Oferta
DROP TABLE C_HASHTAG.Item
DROP TABLE C_HASHTAG.Factura
DROP TABLE C_HASHTAG.Compra
DROP TABLE C_HASHTAG.Calificacion
DROP TABLE C_HASHTAG.Publicacion
DROP TABLE C_HASHTAG.Tipo_Public
DROP TABLE C_HASHTAG.Visibilidad
DROP TABLE C_HASHTAG.Estado
DROP TABLE C_HASHTAG.Empresa
DROP TABLE C_HASHTAG.Rubro
DROP TABLE C_HASHTAG.Cliente
DROP TABLE C_HASHTAG.Usuario
DROP TABLE C_HASHTAG.Tipo_Doc
DROP TABLE C_HASHTAG.Funcionalidad_Rol
DROP TABLE C_HASHTAG.Funcionalidad
DROP TABLE C_HASHTAG.Rol
DROP TABLE C_HASHTAG.FECHA_DEL_SISTEMA

--Drop esquema
DROP SCHEMA C_HASHTAG

Descripcion Publicación 212351

select * from C_HASHTAG.Publicacion

update C_HASHTAG.Publicacion
set Id_Estado = 2
where Id_Publicacion = 12353

select * from C_HASHTAG.Rubro_Publicacion
where Id_Publicacion = 12353

select * from C_HASHTAG.Rubro
where Id_Rubro = 18