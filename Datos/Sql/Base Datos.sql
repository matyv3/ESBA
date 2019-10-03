

/*--------------------------    T   A   B   L   A   S   ----------------------------------------*/

create database ESBA_WEB
use ESBA_WEB


create table Auditoria
(
	Auditoria_id int identity(1,1),
	Action varchar (max),
	table_name varchar(max),
	item_id int,
	action_time datetime default getdate(),
	userNow_id varchar (max),

	primary key(Auditoria_id)
)



create table users
(
	user_id int identity(1,1),
	document varchar (max),
	name varchar (max),
	surname varchar (max),
	sexo Char,
	address varchar (max),
	phone varchar (max),
	create_at datetime default getdate(),
	mail varchar (max),
	password varchar(max),
	rol_id int

  foreign key (rol_id) references roles (rol_id)
  primary key(user_id)
)

create table roles
(
	rol_id int identity(1,1),
	descripcion varchar(max),

	primary key(rol_id)

)


create table Deleted_users
(
	Deleted_user int identity(1,1),
	user_id int,
	document varchar (max),
	name varchar (max),
	surname varchar (max),
	sexo Char,
	address varchar (max),
	phone varchar (max),
	create_at datetime,
	deleted_at datetime default getdate(),
	mail varchar (max),
	password varchar(max),
	rol_id int

  primary key(Deleted_user)
)



insert into roles (description)
	values ('alumno')
	values ('maestro')
	values ('administrativo')


insert into users (document,name,surname,sexo,address,phone,mail,password,rol_id)
	values('12345','pepe','robles','M','mar acha 348','3355-2211','pep@gmail.com','asd',2)
	values('4234','jose','romero','F','del pino 283','6354-9878','jose@gmail.com','dsa',1)
	values('64564','ramon','gomez','M','monroe 123','2312-3453','ramon@gmail.com','das',3)


	select * from Auditoria
	select * from users



/*------------------------------V A L I D A T E   U S E R   A N D   P A S S W O R D---------------C O N F I R M A D O-------------------------------------*/


create procedure sp_Validate_User
	@user_mail varchar(max),
	@user_password varchar(max),
	@user_id int output,
	@mensaje varchar(max) output
as
begin
		declare @busqueda varchar(max)
			set @busqueda = (select s.mail from users s where s.mail = @user_mail)
	
		if (@busqueda is null)
			begin
				set @user_id = 0
				set	@mensaje = 'El Usuario es Incorrecto'

			end
		else
			begin
				set @user_id = (select s.user_id from users s where s.mail = @user_mail and s.password = @user_password)
	
				if (@user_id is null)
				begin
					set @user_id = 0
					set	@mensaje = 'La contraseña es Incorrecta'
				end
			end
end
return

/*
declare @mensaje varchar(max)
declare @user_id int
exec dbo.sp_validate_user 'ramon@gmail.com','das',@user_id output,@mensaje output
select @user_id,@mensaje */




/*-----------------------I N S E R T   U S U A R I O---------------------------C O N F I R M A D O-----------------------------------*/

create procedure sp_Insert_User
	@document varchar(max),
	@name varchar(max),
	@surname varchar(max),
	@sexo char,
	@address varchar(max),
	@phone varchar(max),
	@user_mail varchar(max),
	@user_password varchar(max),
	@rol_id int,
	@mensaje varchar(max) output,
	@user_id int output
as
begin
	begin transaction
	begin try

		insert into users(document,name,surname,sexo,address,phone,mail,password,rol_id)
		values (@document,@name,@surname,@sexo,@address,@phone,@user_mail,@user_password,@rol_id)

		set @user_id = (select user_id from users where mail = @user_mail and password = @user_password)
		set @mensaje = 'Usuario creado Exitosamente'

	commit transaction
	end try

	begin catch

		rollback transaction
		set @mensaje = 'Erorr al crear Usuario, intente nuevamente'

	end catch

end
return

select * from users inner join roles on roles.rol_id = users.rol_id
/*
declare @mensaje varchar(max)
declare @user_id int
exec dbo.sp_Insert_User '123123','charly','rolf','m','garcia del rio 4455','3213-1424','hasZs@gmail.com','1234asd',2,@mensaje output,@user_id output
select @user_id,@mensaje
*/

/*-----------------------D E L E T E   U S U A R I O--------------------------C O N F I R M A D O--------------------------------------------------*/

create procedure sp_Delete_User
	@user_id int,
	@mensaje varchar(max) output
as
begin
	begin transaction
	begin try

		delete users where user_id = @user_id
		set @mensaje = 'Usuario Borrado Exitosamente'

	commit transaction
	end try

	begin catch

		rollback transaction
		set @mensaje = 'Erorr al Borrar Usuario, intente nuevamente'

	end catch
end
return

/*
select * from users inner join roles on roles.rol_id = users.rol_id
declare @mensaje varchar(max)
exec dbo.sp_Delete_User 7,@mensaje output
select @mensaje
*/


/*----------------------- U P D A T E   U S E R ----------------------------C O N F I R M A D O------------------------------------------------*/


create procedure sp_Update_User
	@user_id int,
	@document varchar (max),
	@name varchar (max),
	@surname varchar (max),
	@sexo Char,
	@address varchar (max),
	@phone varchar (max),
	@mail varchar (max),
	@password varchar(max),
	@rol_id int,
	@mensaje varchar(max) output
as
begin
	begin transaction
	begin try
		update users set document = @document, name = @name, surname = @surname, sexo = @sexo, address = @address, phone = @phone, mail = @mail, password = @password, rol_id = @rol_id where user_id = @user_id
		set @mensaje = 'Datos cambiados Exitosamente'
	commit transaction
	end try

	begin catch
		rollback transaction
		set @mensaje = 'Erorr al Actualizar los datos de la cuenta, intente nuevamente'
	end catch
	
end
return

/*
select * from users inner join roles on roles.rol_id = users.rol_id
declare @mensaje varchar(max)
exec dbo.sp_Update_User 4,'3123123','jose vald','gomez','L','la hoja 111','321-53','adshhh@g.com','6699tt','1',@mensaje output
select @mensaje
*/



/*---------------------------------T R I G G E R   D E L E T E D   U S E R------------------------------C O N F I R M A D O-----------------------------------------*/

create trigger TR_DeleteUser on users 
for delete
	as
	begin
		begin transaction
		begin try

			declare @action varchar (max)
			declare @table_name varchar (max)
			declare @item_id int
			declare @userNow_id varchar (max)

			set @Action = 'Delete'
			set @table_name = 'Users'
			set @item_id = (select user_id from deleted)
			set @userNow_id = current_user

			insert into Auditoria (Action,table_name,item_id,userNow_id)
			values (@Action,@table_name,@item_id,@userNow_id)

			insert into Deleted_users (user_id,document,name,surname,sexo,address,phone,create_at,mail,password,rol_id)
			select d.user_id,d.document,d.name,d.surname,d.sexo,d.address,d.phone,d.create_at,d.mail,d.password,d.rol_id from deleted d

		commit transaction
		end try

		begin catch

			rollback transaction

		end catch
	end


/*---------------------------------T R I G G E R   I N S E R T   U S E R-------------------------C O N F I R M A D O-----------------------------------------*/


create trigger TR_InsertUser on users
for insert
	as
	begin
		begin transaction
		begin try	
			declare @Action varchar (max)
			declare @table_name varchar (max)
			declare @item_id int
			declare @userNow_id varchar (max)

			set @Action = 'Insert'
			set @table_name = 'Users'
			set @item_id = (select user_id from inserted)
			set @userNow_id = current_user

			insert into Auditoria (Action,table_name,item_id,userNow_id)
			values (@Action,@table_name,@item_id,@userNow_id)

		commit transaction
		end try

		begin catch

			rollback transaction

		end catch

	end

/*---------------------------------T R I G G E R   U P D A T E   U S E R----------------------------C O N F I R M A D O----------------------------------------*/

create trigger TR_UpdateUsers on users
for update
	as
	begin
		begin transaction
		begin try	
			declare @Action varchar (max)
			declare @table_name varchar (max)
			declare @item_id int
			declare @userNow_id varchar (max)

			set @Action = 'Update'
			set @table_name = 'Users'
			set @item_id = (select user_id from inserted)
			set @userNow_id = current_user

			insert into Auditoria (Action,table_name,item_id,userNow_id)
			values (@Action,@table_name,@item_id,@userNow_id)

		commit transaction
		end try

		begin catch

			rollback transaction

		end catch

	end