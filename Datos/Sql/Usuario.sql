
/*-------------------------------- T A B L A S --------------------------------------------------------------------------------------------*/

create table usuario
(
	usuario_id int identity(1,1),
	documento varchar (max),
	nombre varchar (max),
	apellido varchar (max),
	sexo Char,
	direccion varchar (max),
	telefono varchar (max),
	create_at datetime default getdate(),
	mail varchar (max),
	password varchar(max),
	tipo_usuario_id int

  foreign key (tipo_usuario_id) references tipo_usuario(tipo_usuario_id)
  primary key(usuario_id)
)

create table tipo_usuario
(
	tipo_usuario_id int identity(1,1),
	descripcion varchar(max),

	primary key(tipo_usuario_id)

)


create table Deleted_Users
(
	Deleted_Users int identity(1,1),
	user_ID int,
	documento varchar (max),
	nombre varchar (max),
	apellido varchar (max),
	sexo Char,
	direccion varchar (max),
	telefono varchar (max),
	create_at datetime,
	deleted_at datetime default getdate(),
	mail varchar (max),
	password varchar(max),
	tipo_usuario_id int

  primary key(Deleted_Users)
)


/*------------------------------V A L I D A T E   U S E R    M A I L--------------------------------------------------------------------------------*/

create procedure sp_Validate_UserName

	@mail_usuario varchar(max),
	@true_false bit output

as
begin
	declare @busqueda varchar(max)
	set @busqueda = (select top 1 s.mail from usuario s where s.mail = @mail_usuario)
	
	if (@busqueda is null)
		begin
			set @true_false = 0

		end
	else
		begin
			set @true_false = 1
		end
end
return

/*prueba de SP*/
declare @res bit
exec dbo.validate_usuario 'carlos',@res output
select @res


/*------------------------------V A L I D A T E   U S E R   A N D   P A S S W O R D----------------------------------------------------------------------*/


create procedure sp_Validate_User

	@mail_usuario varchar(max),
	@password_usuario varchar(max),
	@true_false bit output,
	@mensaje varchar(max) output
as
begin
	begin transaction
	begin try
		declare @busqueda varchar(max)
		set @busqueda = (select top 1 s.mail from usuario s where s.mail = @mail_usuario and s.password = @password_usuario)
	
		if (@busqueda is null)
			begin
				set @true_false = 0

			end
		else
			begin
				set @true_false = 1
			end

	commit transaction
	end try

	begin catch

		rollback transaction
		set @mensaje = 'Erorr al Validar Usuario, intente nuevamente'

	end catch
end
return

/*prueba de SP*/
declare @res bit
declare @mensaje varchar(max)
exec dbo.sp_Validate_User 'carlos','23421asdq',@res output,@mensaje output
select @res, @mensaje



/*-----------------------I N S E R T   U S U A R I O----------------------------------------------------------------------------*/

create procedure sp_Insert_User
	@documento varchar(max),
	@nombre varchar(max),
	@apellido varchar(max),
	@sexo char
	@direccion varchar(max),
	@telefono varchar(max),
	@mail_usuario varchar(max),
	@password_usuario varchar(max),
	@tipo_usuario_id varchar(max),
	@mensaje varchar(max) output
as
begin
	begin transaction
	begin try

		insert into usuario (documento,nombre,apellido,sexo,direccion,telefono,mail,password)
		values (@documento,@nombre,@apellido,@sexo,@direccion,@telefono,@mail_usuario,@password_usuario)

		update usuario set tipo_usuario_id = tu.tipo_usuario_id from usuario u
		inner join tipo_usuario tu on tu.tipo_usuario_id = u.tipo_usuario_id
		where tu.descripcion = @tipo_usuario_id


		set @mensaje = 'Usuario creado Exitosamente'

	commit transaction
	end try

	begin catch

		rollback transaction
		set @mensaje = 'Erorr al crear Usuario, intente nuevamente'

	end catch

end
return

/*prueba de SP*/
declare @salida varchar(max);
exec dbo.insert_usuario 'jose','432asd1',@salida output
select @salida


/*-----------------------D E L E T E   U S U A R I O----------------------------------------------------------------------------*/

create procedure sp_Delete_User
	@mail_usuario varchar(max),
	@mensaje varchar(max) output
as
begin
	begin transaction
	begin try

		delete usuario where mail = @mail_usuario
		set @mensaje = 'Usuario Borrado Exitosamente'

	commit transaction
	end try

	begin catch

		rollback transaction
		set @mensaje = 'Erorr al Borrar Usuario, intente nuevamente'

	end catch
end
return

/*prueba de SP*/
declare @salida varchar(max);
exec dbo.sp_Baja_Usuario 'jose',@salida output
select @salida


/*-----------------------U P D A T E   U S E R M A I L----------------------------------------------------------------------------*/


create procedure sp_Update_UserName
	@mail_usuario varchar(max),
	@password_usuario varchar(max),
	@new_userMail varchar(max),
	@mensaje varchar(max) output
as
begin
	begin transaction
	begin try
		update usuario set mail = @new_userMail from usuario where mail = @mail_usuario and @password_usuario = password
		set @mensaje = 'Mail de la cuenta Cambiado Exitosamente'
	commit transaction
	end try

	begin catch
		rollback transaction
		set @mensaje = 'Erorr al Actualizar el Mail de la cuenta, intente nuevamente'
	end catch
	
end
return

/*-----------------------U P D A T E   U S E R   P A S S W O R D----------------------------------------------------------------------------*/


create procedure sp_Update_UserPassword

	@mail_usuario varchar(max),
	@password_usuario varchar(max),
	@new_userPassword varchar(max),
	@mensaje varchar(max) output
as
begin
	begin transaction
	begin try	

		update usuario set password = @new_userPassword from usuario where mail = @mail_usuario and @password_usuario = password
		set @mensaje = 'Contraseña de la cuenta Cambiado Exitosamente'

	commit transaction
	end try

	begin catch

		rollback transaction
		set @mensaje = 'Erorr al Actualizar la Contraseña, intente nuevamente'

	end catch
	
end
return


/*-----------------------------G E T  U S E R----------------------------------------------------------------------------------------------*/


create procedure sp_Get_User

	@mail_usuario varchar(max),
	@password_usuario varchar(max)
as
begin
	
	select * from usuario where mail = @mail_usuario and password = @password_usuario
	
end
return

/* Prueba SP */
exec dbo.sp_Get_User 'jose','432asd1'


/*---------------------------------T R I G G E R   D E L E T E D   U S E R----------------------------------------------------------------------------------------*/

create trigger usuario on usuario 
after delete
	as
	begin
		begin transaction
		begin try

			declare @Action varchar (max)
			declare @table_name varchar (max)
			declare @item_id int
			declare @user_id int

			set @Action = 'Delete'
			set @table_name = 'Usuario'
			set @item_id = select usuario_id from deleted
			set @user_id = get user_id()

			insert into Auditoria (Action,table_name,item_id,user_id)
			values (@Action,@table_name,@item_id,@user_id)

			insert into Deleted_Users (user_ID,documento,nombre,apellido,sexo,direccion,telefono,create_at,mail,password)
			select d.usuario_id,d.documento,d.nombre,d.apellido,d.sexo,d.direccion,d.telefono,d.create_at,d.mail,d.password from deleted d

		commit transaction
		end try

		begin catch

			rollback transaction

		end catch
	end


/*---------------------------------T R I G G E R   I N S E R T   U S E R----------------------------------------------------------------------------------------*/


create trigger usuario on usuario
after insert
	as
	begin
		begin transaction
		begin try	
			declare @Action varchar (max)
			declare @table_name varchar (max)
			declare @item_id int
			declare @user_id int

			set @Action = 'Insert'
			set @table_name = 'Usuario'
			set @item_id = select usuario_id from inserted
			set @user_id = get user_id()

			insert into Auditoria (Action,table_name,item_id,user_id)
			values (@Action,@table_name,@item_id,@user_id)

		commit transaction
		end try

		begin catch

			rollback transaction

		end catch

	end

/*---------------------------------T R I G G E R   U P D A T E   U S E R----------------------------------------------------------------------------------------*/

create trigger usuario on usuario
after update
	as
	begin
		begin transaction
		begin try	
			declare @Action varchar (max)
			declare @table_name varchar (max)
			declare @item_id int
			declare @user_id int

			set @Action = 'Update'
			set @table_name = 'Usuario'
			set @item_id = select usuario_id from inserted
			set @user_id = get user_id()

			insert into Auditoria (Action,table_name,item_id,user_id)
			values (@Action,@table_name,@item_id,@user_id)

		commit transaction
		end try

		begin catch

			rollback transaction

		end catch

	end