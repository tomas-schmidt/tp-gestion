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
DROP PROCEDURE C_HASHTAG.cambiarContraseniaPorAdmin
DROP PROCEDURE C_HASHTAG.cambiarContraseniaPorUser
DROP PROCEDURE C_HASHTAG.vendedoresConMayorCantDeProdsNoVendidos
DROP PROCEDURE C_HASHTAG.clientesConMayorCantDeProdsComprados
DROP PROCEDURE C_HASHTAG.vendedoresConMayorCantDeFacturas
DROP PROCEDURE C_HASHTAG.vendedoresConMayorMontoFacturado
DROP PROCEDURE C_HASHTAG.obtenerBorradores
DROP PROCEDURE C_HASHTAG.obtenerPublicacionAModificar
DROP PROCEDURE C_HASHTAG.obtenerRubrosPublicacion
DROP PROCEDURE C_HASHTAG.modificarCompra
DROP PROCEDURE C_HASHTAG.modificarSubasta
DROP PROCEDURE C_HASHTAG.eliminarRubrosDePublicacion
DROP PROCEDURE C_HASHTAG.modificarReputacion
DROP PROCEDURE C_HASHTAG.obtenerTodasVisibilidades
DROP PROCEDURE C_HASHTAG.pausarPublicaciones

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
DROP TABLE C_HASHTAG.Reputacion
DROP TABLE C_HASHTAG.Tipo_Doc
DROP TABLE C_HASHTAG.Funcionalidad_Rol
DROP TABLE C_HASHTAG.Funcionalidad
DROP TABLE C_HASHTAG.Rol
DROP TABLE C_HASHTAG.FECHA_DEL_SISTEMA

--Drop esquema
DROP SCHEMA C_HASHTAG


select * from C_HASHTAG.Usuario
where Username like 'usuario.cliente.51469458'

select * from C_HASHTAG.Cliente
where Id_User = 12

select * from C_HASHTAG.Publicacion
order by Id_Publicacion desc