create database base_equipos;
go;

create table t_equipos (
	id_equipos int not null identity primary key,
	nombre varchar(50),
	descripcion varchar(200),
	tipo_equipo_id int,
	marca_id int,
	modelo varchar(50),
	anio_compra int,
	costo numeric(18,4),
	vida_util int,
	estado_equipo_id int,
	estado char(1)
)
go;
insert into t_equipos (nombre,descripcion,tipo_equipo_id,marca_id,modelo,anio_compra,costo,vida_util,estado_equipo_id,estado)
values ('Lgv60','Telefono Movil',1,1,'AT&T',2020,140.0,1,1,'A');
go;
insert into t_equipos (nombre,descripcion,tipo_equipo_id,marca_id,modelo,anio_compra,costo,vida_util,estado_equipo_id,estado)
values ('Lgv65','Telefono Movil',1,1,'AT&T',2010,40.0,3,2,'A');