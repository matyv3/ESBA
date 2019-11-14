/*-------------------------------- T A B L A S --------------------------------------------------------------------------------------------*/

create table Roles
(
	rol_id int identity(1,1),
	descripcion varchar (max),
	

	primary key (rol_id)
)


create table Deleted_Roles
(
	Deleted_rol_id int identity(1,1),
	rol_id int,
	descripcion varchar (max),
	

	primary key (Deleted_rol_id)
)



/*-------------------------------- V A L I D A T E      M A T E R I A S -------------------------------------------------------------------------------*/



create procedure sp_Validate_Roles

@descripcion varchar(max),
@mensaje int output

as
begin
	declare @busqueda varchar(max)
	set @busqueda = (select top 1 m.rol_id from Roles m where m.descripcion = @descripcion)
	
	if (@busqueda is null)
		begin
			set @mensaje = 1

		end
	else
		begin
			set @mensaje = 0
		end
end
return




/*-------------------------------- I N S E R T      M A T E R I A S -------------------------------------------------------------------------------*/

create procedure sp_Insert_Roles 

	@descripcion varchar(max),
	@mensaje int output
as
begin
	begin transaction
	begin try

		insert into Roles (descripcion)
		values (@descripcion)

		set @mensaje = (select rol_id from Roles where descripcion = @descripcion)

	commit transaction
	end try

	begin catch

		rollback transaction
		set @mensaje = 0
	end catch
end
return

/*-------------------------------- U P D A T E      M A T E R I A S -------------------------------------------------------------------------------*/

create procedure sp_Update_Roles

	@rol_id int,
	@descripcion varchar(max),
	@mensaje int output

as
begin
	begin transaction
	begin try

		update Roles set descripcion = @descripcion where rol_id = @rol_id
		set @mensaje = 1

	commit transaction
	end try

	begin catch

		rollback transaction
		set @mensaje = 0

	end catch
end
return

/*-------------------------------- D E L E T E      M A T E R I A S -------------------------------------------------------------------------------*/

create procedure sp_Delete_Roles

	@rol_id int,
	@mensaje int output

as
begin
	begin transaction
	begin try

		delete Roles where rol_id = @rol_id
		set @mensaje = 1

	commit transaction
	end try

	begin catch

		rollback transaction
		set @mensaje = 0

	end catch
end
return


/*---------------------------------T R I G G E R   D E L E T E D   M A T E R I A ----------------------------------------------------------------------------------------*/

create trigger TR_DeleteRoles on Roles 
for delete
	as
	begin
		begin transaction
		begin try

			declare @Action varchar (max)
			declare @table_name varchar (max)
			declare @item_id int
			declare @user_id int

			set @Action = 'Delete'
			set @table_name = 'Roles'
			set @item_id = select usuario_id from deleted
			set @user_id = get user_id()

			insert into Auditoria (Action,table_name,item_id,user_id)
			values (@Action,@table_name,@item_id,@user_id)

			insert into Deleted_Users (rol_id,descripcion,Cant_Modulos)
			select d.rol_id,d.descripcion from deleted d

		commit transaction
		end try

		begin catch

			rollback transaction

		end catch
	end


/*---------------------------------T R I G G E R   I N S E R T   M A T E R I A ----------------------------------------------------------------------------------------*/


create trigger TR_InsertRoles on Roles
for insert
	as
	begin
		begin transaction
		begin try	
			declare @Action varchar (max)
			declare @table_name varchar (max)
			declare @item_id int
			declare @user_id int

			set @Action = 'Insert'
			set @table_name = 'Roles'
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

/*---------------------------------T R I G G E R   U P D A T E   M A T E R I A ----------------------------------------------------------------------------------------*/

create trigger TR_UpdateRoles on Roles
for update
	as
	begin
		begin transaction
		begin try	

			declare @Action varchar (max)
			declare @table_name varchar (max)
			declare @item_id int
			declare @user_id int

			set @Action = 'Update'
			set @table_name = 'Roles'
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


/*----------------------- G E T  M A T E R I A ----------------------------C O N F I R M A D O------------------------------------------------*/

create procedure sp_Get_Roles
	@rol_id int
as
begin
	
	select * from Roles m
	where m.rol_id = @rol_id
	
end
return

/*----------------- Get ALL -----------------------*/


create procedure sp_GetAll_Roles
as
begin
	select * from Roles
end
return