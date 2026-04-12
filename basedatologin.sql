create database lprueba;
use lprueba;

create table usuario (
  id int auto_increment primary key,
  username char (30) not null,
  nombres char(30) not null,
  apellidos	 char(30) not null,
  dni char(30) not null,
  clave char(30) not null,
  telefono char(9) not null,
  correo char(50) not null );

insert usuario values (
null, 'patriOg26', 'Patricio','Lopez','78512395', 'patopato', '987654321', 'patricioriginal@gmail.com' );


delimiter $$
create procedure login(
    in p_usuario char(30),
    in p_clave char(30)
)
begin
    select *
    from usuario
    where username = p_usuario
      and clave = p_clave;
end $$

create procedure registro(
	in r_username char (30),
	in r_nombres char(30),
	in r_apellidos	 char(30),
	in r_dni char(30),
	in r_clave char(30),
	in r_telefono char(9),
	in r_correo char(50)
)
begin
insert into usuario (username, nombres, apellidos, dni, clave, telefono, correo) values
(r_username, r_nombres, r_apellidos, r_dni, r_clave, r_telefono, r_correo);
end $$

create procedure mostrar(
	in m_id int
)
begin
	select * from usuario where m_id = id;
end $$
delimiter ;
