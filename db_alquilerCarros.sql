use master
go
drop database rentcar
go
create database rentcar
go
USE rentcar
go

create table empleados(
codempleado int identity(1,1),
nombreemple varchar(30) not null,
apellidoemple varchar(30) not null,
usuario varchar(20) not null,
contrasena varchar(20) not null,
tipousuario varchar(20) not null,
constraint pk_codempleado primary key(codempleado)
);
go

create table clientes(
codcliente int identity(1,1),
nombrec varchar(30) not null,
apellidoc varchar(30) not null,
DUI varchar(15) not null,
numlicencia varchar(15) not null,
tipolicencia varchar(20) not null,
fechavenci date not null, 
genero varchar(15) not null,
tel varchar(10) not null,
correo varchar(30) not null
constraint pk_codcliente primary key(codcliente)
);
go

create table autos(
codigoauto int identity(1,1),
tipoauto varchar(20) not null, 
marca varchar(20) not null,
modelo varchar(30) not null,
color varchar(20) not null,
transmision varchar(20) not null, --estandar o automatica
motor varchar(20) not null,
cantpasajeros varchar(20),
placa varchar(20) not null,
precio decimal(10,2) not null,
estado varchar(20) not null, --ACA TIENE QUE IR POR DEFAULT DISPONIBLE
imagen image 
constraint pk_codigoauto primary key(codigoauto)
);
go

create table mantenimiento(
codmantenimiento int identity (1,1),
codigoauto int constraint fk_codigoautos foreign key references autos(codigoauto),
tipoauto varchar(20) not null, 
marca varchar(20) null,
modelo varchar(30) null,
placa varchar(20)  null,
estado varchar(50)  null,
fechingtaller  date default getdate()  null,
fechsaltaller date  null,
tiparreglo varchar(80)  null
constraint pk_codmantenimeinto primary key(codmantenimiento)
);
go

create table alquiler(
codentrealquiler int identity(1,1),
fecharegistro date default getdate(),--NO MOSTRAR EN EL GRIDVIEW
codcliente int constraint fk_codclientes foreign key references clientes(codcliente),
nombrec varchar(30) not null,
apellidoc varchar(30) not null,
DUI varchar(15) not null,
numlicencia varchar(15) not null,
codigoauto int constraint fk_codigoauto foreign key references autos(codigoauto),
tipoauto varchar(20) not null, 
marca varchar(20) not null,
modelo varchar(30) not null,
placa varchar(20) not null,
estado varchar(20) default 'disponible',
diaretiro date not null,--dia que sale el carro
diaentrada date not null, --dia que se espera el carro sea devuelto
diasalquilado int not null,---Cantidad de dias que el cliente tendra el carro
precio decimal(10,2) not null,--Precio por dia a pagar por el alquiler
total decimal(10,2) not null, ---TOTAL DE LOS DIAS POR EL PRECIO POR DIA
depositoSeguro decimal(10,2) not null, --cobro de seguro
totalGlobal decimal(10,2) not null,--TOTAL GLOBAL DEL TOTAL ANTERIOR MAS EL COSTO SEGURO
codempleado int constraint fk_codempleado foreign key references empleados(codempleado), 
modo varchar(20) default 'Activo', --CUANDO SE EMITA EL DE DEVOLUCION SER� INACTIVO
constraint pk_codentrealquiler  primary key(codentrealquiler)
);
go

create table devolucion(
codevoalquiler int identity(1,1),
codentrealquiler int constraint fk_coodentrealquiler foreign key references alquiler(codentrealquiler),
nombrec varchar(30),
apellidoc varchar(30),
DUI varchar(15),
numlicencia varchar(15),
tipoauto varchar(20), 
marca varchar(20),
modelo varchar(30),
placa varchar(20),
---INICIA
diasalida date not null,---EL DIA QUE EL CARRO SE DIO AL CLIENTE ( DE TABLA ALQUILER)
diaentrega date not null,--EL DIA QUE EL CARRO QUE EL CLIENTE LO IBA A DAR (DE TABLA ALQUILER)
diasalquilado int not null,--EL TOTAL DE DIAS QUE EL CLIENTE IBA A TENER EL CARRO (DE TABLA ALQUILER)
diaentrada date not null,--EL DIA EN SI QUE EL CARRO SE FUE ENTREGADO POR EL CLIENTE
ttlDiasAlquilados int not null, --LA CUENTA DE LOS DIAS QUE EL CLIENTE LO TUVO, YA SEA SI LO ENTREGO TARDE O NO
precio decimal(10,2) not null,--PRECIO POR DIA A PAGAR DE PARTE DEL CIENTE(DE TABLA ALQUILER)
mora decimal(10,2) null,--ES UN COSTO EXTRA PARA EL CLIENTE SI SE TARDO EN ENTREGARLO, AC� SE DETALLERA
depositoSeguro decimal(10,2) not null,--(DE TABLA ALQUILER)
costodano decimal(10,2)null,--SI EL CLIENTE HIZO UN RASGU�O O ALGO AL CARRO, SE LE DESCUENTA DEL SEGURO
devloseguro decimal(10,2)null,--PERO SI NO, SE LE DEVUELVE LO QUE SE LE DESCUENTA O COMPLETO, DEPENDE
pagoExtraDano decimal(10,2) null,--PERO SI EL SEGURO DEL CLIENTE NO ES SUFICIENTE PARA CUBRIR POR EL DA�O, SE LE COBRA LO QUE FALTA AL QUITARLE LO DEL SEGURO
totalPagar decimal(10,2) null,--PUEDA QUE SE SUME LO DE LA MORA Y LO DEL PAGO EXTRA PERO ESTO SUCEDERA EN ALGUNOS CASOS
observacion varchar(100),
estado varchar(20) default 'disponible',
fecharegistro date default getdate(),--NO MOSTRAR EN EL GRIDVIEW
constraint pk_codevoalquiler primary key(codevoalquiler )
);
go

--Procedimeinto para el login
go
CREATE PROCEDURE ProcedimientoLogin
@usuario varchar(50),
@pass varchar (50)
AS
BEGIN
	Select nombreemple,apellidoemple,tipousuario,codempleado from empleados where usuario = @usuario and contrasena = @pass
END

--procedimientos empleados--

go
create procedure ListarEmpleados
as
begin
select codempleado as 'Codigo', nombreemple as 'Nombre', apellidoemple as 'Apellido', usuario as 'Usuario', contrasena as 'Contraseña', tipousuario as 'Tipo de usuario' 
from empleados
end
go

create procedure AgregarEmpleado
@nombreemp varchar (30),
@apellidoemp varchar (30),
@usuemp varchar (20),
@contemp varchar (20),
@tipoemp varchar (20)
as
begin
insert into empleados (nombreemple, apellidoemple, usuario, contrasena,tipousuario) 
values (@nombreemp,	@apellidoemp, @usuemp, @contemp, @tipoemp);
end 
go

create procedure ModificarEmpleado
@codigo int,
@nombreemp varchar (30),
@apellidoemp varchar (30),
@usuemp varchar (20),
@contemp varchar (20),
@tipoemp varchar (20)
as
begin
update empleados set  nombreemple=@nombreemp, apellidoemple=@apellidoemp, usuario=@usuemp, contrasena=@contemp,tipousuario=@tipoemp where codempleado=@codigo;
end 
go


create procedure BorrarEmpleado
@codempleado int
as
begin
delete from empleados where codempleado=@codempleado;
end
go

Create procedure BuscarEmpleado
@usuario varchar(20)
as
begin
select * from empleados where USUARIO=@usuario;
end
go


--Procedimientos autos--
go
create procedure ListarAutos
as
begin 
select codigoauto as 'Codigo', tipoauto as 'Tipo',marca as 'Marca', modelo as
'Modelo',color as 'Color', transmision as 'Transmision', motor as 'Motor',cantpasajeros as 'Pasajeros',
placa as 'Placa',precio as 'Precio', estado as 'Estado'
from autos
end

go
create procedure ListarAutosEmpleado
as
begin 
select codigoauto as 'Codigo', tipoauto as 'Tipo',marca as 'Marca', modelo as
'Modelo',color as 'Color', transmision as 'Transmision', motor as 'Motor',cantpasajeros as 'Pasajeros',
placa as 'Placa',precio as 'Precio', estado as 'Estado'
from autos where estado = 'Disponible'
end

GO

go
Create procedure BuscarAutos
@placa varchar(50)
as
begin
select * from autos where placa=@placa;
end

go
create procedure AgregarAuto
@tipo varchar(30),
@marca varchar(30),
@modelo varchar(30),
@color varchar(30),
@transmision varchar(30),
@motor varchar(30),
@pasajeros varchar(10),
@placa varchar(15),
@precio decimal(10,2),
@estado varchar(30),
@imagen image
as 
begin
insert into autos (tipoauto,marca,modelo,color,transmision,motor,cantpasajeros,placa,precio,estado,imagen)
values(@tipo,@marca,@modelo,@color,@transmision,@motor,@pasajeros,@placa,@precio,@estado,@imagen);
end

go
create procedure ActualizarAuto
@cod varchar(30),
@tipo varchar(30),
@marca varchar(30),
@modelo varchar(30),
@color varchar(30),
@transmision varchar(30),
@motor varchar(30),
@pasajeros varchar(10),
@placa varchar(15),
@precio decimal(10,2),
@estado varchar(30),
@imagen image
as 
begin
update autos set tipoauto = @tipo, marca= @marca, modelo = @modelo, color = @color, transmision= @transmision,
motor = @motor, cantpasajeros = @pasajeros, placa = @placa, precio = @precio, estado = @estado, imagen = @imagen
where codigoauto = @cod
end

go
Create procedure EliminarAuto
@cod varchar(30)
as
begin
delete from autos where codigoauto=@cod
end

--procedimientos clientes--
go
create procedure ListarClientes
as
begin 
select codcliente as 'Codigo', nombrec as 'Nombre', apellidoc as 'Apellidos', DUI as
'DUI', numlicencia as 'Numero de Licencia', tipolicencia as 'Tipo de Licencia', fechavenci as 'Fecha de Vencimiento', genero as 'G�nero',
tel 'T�lefono', correo as 'Correo'
from clientes
end

go
Create procedure BuscarClientes
@dui varchar(50)
as
begin
select * from clientes where DUI=@dui;
end

go
create procedure AgregarCliente
@nombre varchar(30),
@apellido varchar(30),
@dui varchar(9),
@licencia varchar(30),
@tipolicencia varchar(30),
@vencimiento date,
@genero varchar(10),
@telefono int,
@correo varchar(30)
as 
begin
insert into clientes(nombrec,apellidoc,DUI ,numlicencia,tipolicencia,
fechavenci , genero ,tel,correo)
values(@nombre,@apellido,@dui,@licencia,@tipolicencia,@vencimiento,@genero,@telefono,@correo);
end

go
create procedure ActualizarCliente
@cod varchar(30),
@nombre varchar(30),
@apellido varchar(30),
@dui varchar (9),
@licencia varchar(30),
@tipolicencia varchar(30),
@vencimiento date,
@genero varchar(10),
@telefono int,
@correo varchar(30)
as 
begin
update clientes set nombrec=@nombre,apellidoc=@apellido,DUI=@dui ,numlicencia=@licencia,
tipolicencia=@tipolicencia, fechavenci=@vencimiento , genero=@genero ,tel=@telefono,correo=@correo
where codcliente = @cod
end

go
Create procedure EliminarCliente
@cod varchar(30)
as
begin
delete from clientes where codcliente=@cod
end

-------------------------------- PROCEDIMIENTO PARA EL BOTON --------------------------------------
go
Create procedure BuscarAuto
@codAuto varchar(50)
as
begin
select * from autos where codigoauto=@codAuto;
end

go
------------------------

Create procedure BuscarMantoAuto
@cod varchar(50)
as
begin
select * from mantenimiento where codigoauto=@cod;
end



--- MANTENIMIENTO AUTO 
go
create procedure ListarMantenimiento
as
begin 
select codmantenimiento as 'Codigo Mantenimiento', codigoauto as 'Codigo', tipoauto as 'Tipo',marca as 'Marca', modelo as
'Modelo', placa as 'Placa', estado as 'Estado', fechingtaller as 'Fecha Ingreso', fechsaltaller as 'Fecha Salida', tiparreglo as 'Tipo Arreglo'
from mantenimiento
end
go
create procedure AgregarMantoAuto
@codigoauto int,
@tipoauto varchar(30),
@marca varchar(30),
@modelo varchar(30),
@placa varchar(15),
@estado varchar(50),
@fechsaltaller date = null,
@tiparreglo varchar(80) = null
as 
begin
insert into mantenimiento(codigoauto,tipoauto,marca,modelo,placa,estado,fechsaltaller,tiparreglo)
values(@codigoauto,@tipoauto,@marca,@modelo,@placa,@estado,@fechsaltaller,@tiparreglo);
end

go
create procedure ActualizarMantoAuto
@codmanto int,
@codigoauto int,
@tipoauto varchar(30),
@marca varchar(30),
@modelo varchar(30),
@placa varchar(15),
@estado varchar(50),
@fechsaltaller date  =null,
@tiparreglo varchar(80)  =null
as 
begin
update mantenimiento set tipoauto = @tipoauto, marca= @marca, modelo = @modelo, placa = @placa, estado = @estado, fechsaltaller = @fechsaltaller,
tiparreglo = @tiparreglo
where codmantenimiento= @codmanto
end

go
Create procedure EliminarMantoAuto
@cod varchar(30)
as
begin
update autos set estado = 'Disponible' where codigoauto = @cod;
delete from mantenimiento where codmantenimiento=@cod
end

--------------------ALQUILER
go
Create procedure BuscarCliente
@codCliente int
as
begin
select * from clientes where codcliente=@codCliente
end
GO

Create procedure EliminarAlquiler
@codalquiler int,
@estado varchar(20)='Eliminado'
as
begin
update alquiler set estado=@estado where codentrealquiler=@codalquiler
end
GO

create procedure AgregarAlquiler
@codcliente int,
@nombrec varchar(30),
@apellidoc varchar(30),
@DUI varchar(15),
@numlicencia varchar(15),
@codigoauto int,
@tipoauto varchar(20), 
@marca varchar(20),
@modelo varchar(30),
@placa varchar(20),
@diaretiro date,
@diaentrada date,
@diasalquilado int,
@precio decimal(10,2),
@total decimal(10,2), 
@depositoSeguro decimal(10,2), 
@totalGlobal decimal(10,2),
@codempleado int 
as 
begin
insert into alquiler(codcliente,nombrec,apellidoc,DUI,numlicencia,codigoauto,tipoauto, marca,modelo,placa,
diaretiro,diaentrada,diasalquilado, precio,total, depositoSeguro, totalGlobal,codempleado)
values(@codcliente,@nombrec,@apellidoc,@DUI,@numlicencia,@codigoauto,@tipoauto, @marca,@modelo,@placa,
@diaretiro,@diaentrada,@diasalquilado,@precio,@total,@depositoSeguro, @totalGlobal, @codempleado);
end
go

create procedure ActualizarAlquiler
@codalquiler int,
@diaretiro date,
@diaentrada date,
@diasalquilado int,
@precio decimal(10,2),
@total decimal(10,2), 
@depositoSeguro decimal(10,2), 
@totalGlobal decimal(10,2)
as 
begin

update alquiler set diaretiro = @diaretiro, diaentrada = @diaentrada, diasalquilado = @diasalquilado, precio = @precio, total=@total,
depositoSeguro =@depositoSeguro, totalGlobal=@totalGlobal where codentrealquiler = @codalquiler
end
go

create procedure ListarAlquileres
as
begin 
select codentrealquiler as 'ID', codempleado as 'Cod. empleado',codcliente as 'Codigo Cliente', nombrec as 'Nombre', apellidoc as 'Apellido',
DUI as 'DUI',numlicencia as 'N� Licencia',codigoauto as 'Codigo Auto', 
tipoauto as 'Tipo auto',marca as 'Marca', modelo as 'Modelo', placa as 'Placa', diaretiro as 'Fecha Retiro', 
diaentrada as 'Fecha entrada', diasalquilado as 'Dias por alquilar', precio as 'Precio por dia', total as 'Total por los dias',
depositoSeguro as 'Deposito Seguro', totalGlobal as 'Total Global', modo as 'Modo' from alquiler where estado='disponible'
end
go

-------------------------------------DEVOLUCION
Create procedure BuscarAlquiler
@codalquiler varchar(50)
as
begin
select * from alquiler where codentrealquiler=@codalquiler;
end
go


create procedure IngresarDevolucion
@codentrealquiler int,
@nombrec varchar(30),
@apellidoc varchar(30),
@DUI varchar(15),
@numlicencia varchar(15),
@tipoauto varchar(20), 
@marca varchar(20),
@modelo varchar(30),
@placa varchar(20),
@diasalida date,
@diaentrega date,
@diasalquilado int,
@diaentrada date,
@ttlDiasAlquilados int ,
@precio decimal(10,2),
@mora decimal(10,2),
@depositoSeguro decimal(10,2),
@costodano decimal(10,2),
@devloseguro decimal(10,2),
@pagoExtraDano decimal(10,2),
@totalPagar decimal(10,2),
@observacion varchar(100)
as
begin
INSERT INTO devolucion(codentrealquiler, nombrec, apellidoc, DUI, numlicencia, tipoauto, marca, modelo, placa,
diasalida, diaentrega, diasalquilado, diaentrada, ttlDiasAlquilados, precio, mora, depositoSeguro, costodano, devloseguro,
pagoExtraDano, totalPagar, observacion)
values(@codentrealquiler, @nombrec, @apellidoc, @DUI, @numlicencia, @tipoauto, @marca, @modelo, @placa, @diasalida, @diaentrega, 
@diasalquilado, @diaentrada, @ttlDiasAlquilados, @precio, @mora, @depositoSeguro, @costodano, @devloseguro, @pagoExtraDano, 
@totalPagar, @observacion)
end;
go

create procedure ListarDevolucion
as
begin
select codevoalquiler as 'ID devolucion', codentrealquiler as 'Id alquiler', nombrec as 'Nombres', apellidoc as 'Apellidos',
DUI as 'DUI', numlicencia AS 'Licencia', tipoauto as 'Tipo Auto', marca as 'Marca', modelo as 'Modelo', placa as 'Placa', diasalida as 'Fecha Salida', 
diaentrega as 'Fecha Entrega', diasalquilado as 'Dias alquilados', diaentrada as 'Fecha entrada', ttlDiasAlquilados as 'Total dias retraso', 
precio as 'Precio', mora as 'Mora', depositoSeguro as 'Deposito Seguro', costodano as 'Costo Da�o', devloseguro as 'Devolucion seguro',
pagoExtraDano as 'Pagos Extras', totalpagar 'Total de pagos', observacion as 'Observacion'
from devolucion where estado = 'disponible'
end

go
Create procedure EliminarDevolucion
@codevoalquiler int,
@estado varchar(20)='Eliminado'
as
begin
update devolucion set estado=@estado where codevoalquiler=@codevoalquiler
end
GO

create procedure ActualizarDevolucion
@cod int,
@observacion varchar(100)
as 
begin

update devolucion set observacion = @observacion where codevoalquiler = @cod
end
go
-----------------------------TRIGGERS
create trigger DevolucionInsertada
on devolucion for insert
AS
declare @codAlquiler int
select @codAlquiler = codentrealquiler from inserted 
update alquiler set modo = 'Inactivo' where codentrealquiler = @codAlquiler 
go

create trigger AlquilerAuto
on alquiler for insert
AS
declare @codAuto int
select @codAuto = codigoauto from inserted 
update autos set estado = 'Alquilado' where codigoauto = @codAuto 
go

create trigger DevolucionAuto
on devolucion for insert
AS
declare @codAlquiler int
declare @codAuto int
select @codAlquiler = codentrealquiler from inserted 
select @codAuto = codigoauto from alquiler where codentrealquiler = @codAlquiler
update autos set estado = 'Disponible' where codigoauto = @codAuto 
go

go
create trigger MantenimientoAuto_Insert
on mantenimiento for insert
AS
declare @codAuto int
select @codAuto = codigoauto from inserted 
update autos set estado = 'En Mantenimiento' where codigoauto = @codAuto 
go

---Procedimientos de validaciones--
create procedure BusquedaEmpleado_alquiler
@cod varchar(15)
as
begin
select * from alquiler where codempleado = @cod;
end
GO

create procedure BusquedaAuto_alquiler
@cod varchar(15)
as
begin
select * from alquiler where codigoauto = @cod;
end
GO

create procedure BusquedaAuto_mantenimiento
@cod varchar(15)
as
begin
select * from mantenimiento where codigoauto = @cod;
end
GO

create procedure IdMaxAlquiler
as
select MAX(codentrealquiler)+1 as 'Id' from alquiler
go

create procedure BusquedaCliente_alquiler
@cod varchar(15)
as
begin
select * from alquiler where codcliente = @cod;
end
GO
--Procedmientos de reportes--

go
create procedure AutosporMarca
@marca varchar(25)
as
begin

Select * from autos where marca = @marca and estado ='Disponible'
end

go
create procedure ReporteAutosAdmin
@estado varchar (20)
as
begin
Select * from autos where estado = @estado
end
GO

create proc Devolucion_fechas
@fecha1 date,
@fecha2 date
as
begin
	select* from devolucion where fecharegistro between @fecha1 and @fecha2 and estado = 'disponible'
end
go

go
create procedure ListarClientesReporte
as
begin 
select codcliente,  nombrec  ,apellidoc, DUI , numlicencia , tipolicencia , fechavenci 
,genero,tel,correo
from clientes
end
go

create procedure ReporteAlquiler
@fecha1 date,
@fecha2 date
as
begin
select* from alquiler where fecharegistro between @fecha1 and @fecha2 and estado = 'disponible'
end

go
create procedure ReporteEmpleados
@tipo varchar(25)
as
begin
select* from empleados where tipousuario = @tipo
end

go
create procedure ReporteMantenimiento_Fecha
(
@fecha1 date,
@fecha2 date
)
as
begin
select* from mantenimiento where fechingtaller between @fecha1 and @fecha2
end
GO

-- usuarios ejemplos
exec AgregarEmpleado 'Marcela', 'Menjivar', 'mgmarce', 'clave123', 'Administrador';
go
exec AgregarEmpleado 'Carolina', 'Menjivar', 'mgcaro', 'clave123', 'Empleado';
go

select* from empleados
go
