
/*-------------------------------- T A B L A S --------------------------------------------------------------------------------------------*/

create table Estado_Materia
(
	Estado_Materia_id int identity(1,1),
	Descripcion varchar(max),

	primary key (Estado_Materia_id)
)


create table Deleted_Estado_Materia
(
	Deleted_Estado_Materia_id int identity(1,1),
	Estado_Materia_id int,
	Descripcion varchar(max),
	
	primary key (Deleted_Estado_Materia_id)
)

/*-------------------------------- V A L I D A T E      M A T E R I A S -------------------------------------------------------------------------------*/



create procedure sp_Validate_Estado_Materia

@Descripcion varchar(max),
@mensaje int output

as
begin


	declare @busqueda varchar(max)


	set @busqueda = (select top 1 n.Descripcion from Estado_Materia n where n.Descripcion = @Descripcion)


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

create procedure sp_Insert_Estado_Materia
@Descripcion varchar(max),
@mensaje int output

as
begin
	begin transaction
	begin try

		insert into Estado_Materia (Descripcion)
		values (@Descripcion)

		set @mensaje = (select m.Estado_Materia_id from Estado_Materia m where m.Descripcion = @Descripcion)

	commit transaction
	end try

	begin catch

		rollback transaction
		set @mensaje = 0
	end catch
end
return

/*-------------------------------- U P D A T E      M A T E R I A S -------------------------------------------------------------------------------*/

create procedure sp_Update_Estado_Materia

	@Estado_Materia_id int,
	@Descripcion_new varchar(max),
	@mensaje int output

as
begin
	begin transaction
	begin try

		update Estado_Materia set Descripcion = @Descripcion_new where Estado_Materia_id = @Estado_Materia_id
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

create procedure sp_Delete_Estado_Materia

	@Estado_Materia_id int,
	@mensaje int output

as
begin
	begin transaction
	begin try

		delete Estado_Materia where Estado_Materia_id = @Estado_Materia_id
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

create trigger TR_DeletedEstado_Materia on Estado_Materia 
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
			set @table_name = 'Estado_Materia'
			set @item_id = select usuario_id from deleted
			set @user_id = get user_id()

			insert into Auditoria (Action,table_name,item_id,user_id)
			values (@Action,@table_name,@item_id,@user_id)

			insert into Deleted_Estado_Materia (Estado_Materia_id,Descripcion)
			select d.Estado_Materia_id,d.Descripcion from deleted d

		commit transaction
		end try

		begin catch

			rollback transaction

		end catch
	end


/*---------------------------------T R I G G E R   I N S E R T   M A T E R I A ----------------------------------------------------------------------------------------*/


create trigger TR_InsertEstado_Materia on Estado_Materia
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
			set @table_name = 'Estado_Materia'
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

create trigger TR_UpdateEstado_Materia on Estado_Materia
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
			set @table_name = 'Estado_Materia'
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

/*----------------------- G E T  N O T A - M A T E R I A ----------------------------C O N F I R M A D O------------------------------------------------*/

create procedure sp_Get_Estado_Materia
	@Estado_Materia_id int
as
begin
	
	select * from Estado_Materia n
	where n.Estado_Materia_id = @Estado_Materia_id
	
end
return

/*----------------- Get ALL -----------------------*/


create procedure sp_GetAll_Estado_Materia

as
begin
	select * from Estado_Materia
end
return