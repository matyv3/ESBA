

/*-------------------------------- T A B L A S --------------------------------------------------------------------------------------------*/

create table Materias
(
	Materia_id int identity(1,1),
	nombre varchar (max),
	Cant_Modulos int

	primary key (Materia_id)
)


create table Deleted_Materias
(
	Deleted_Materia_id int identity(1,1),
	Materia_id int,
	nombre varchar (max),
	Cant_Modulos int

	primary key (Deleted_Materia_id)
)



/*-------------------------------- V A L I D A T E      M A T E R I A S -------------------------------------------------------------------------------*/



create procedure sp_Validate_Materia

@materia_name varchar(max),
@mensaje int output

as
begin
	declare @busqueda varchar(max)
	set @busqueda = (select top 1 m.Materia_id from materias m where m.nombre = @materia_name)
	
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

create procedure sp_Insert_Materia 

	@materia_name varchar(max),
	@cant_modulos int,
	@mensaje int output
as
begin
	begin transaction
	begin try

		insert into Materias (nombre,Cant_Modulos)
		values (@materia_name,@cant_modulos)

		set @mensaje = (select Materia_id from Materias where nombre = @materia_name)

	commit transaction
	end try

	begin catch

		rollback transaction
		set @mensaje = 0
	end catch
end
return

/*-------------------------------- U P D A T E      M A T E R I A S -------------------------------------------------------------------------------*/

create procedure sp_Update_Materia

	@materia_id int,
	@materia_name varchar(max),
	@cant_modulos int,
	@mensaje int output

as
begin
	begin transaction
	begin try

		update Materias set nombre = @materia_name, Cant_Modulos = @cant_modulos where Materia_id = @materia_id
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

create procedure sp_Delete_Materia

	@Materia_id int,
	@mensaje int output

as
begin
	begin transaction
	begin try

		delete Materias where Materia_id = @Materia_id
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

create trigger TR_DeleteMaterias on Materias 
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
			set @table_name = 'Materias'
			set @item_id = select usuario_id from deleted
			set @user_id = get user_id()

			insert into Auditoria (Action,table_name,item_id,user_id)
			values (@Action,@table_name,@item_id,@user_id)

			insert into Deleted_Users (Materia_id,nombre,Cant_Modulos)
			select d.Materia_id,d.nombre,d.Cant_Modulos from deleted d

		commit transaction
		end try

		begin catch

			rollback transaction

		end catch
	end


/*---------------------------------T R I G G E R   I N S E R T   M A T E R I A ----------------------------------------------------------------------------------------*/


create trigger TR_InsertMaterias on Materias
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
			set @table_name = 'Materias'
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

create trigger TR_UpdateMaterias on Materias
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
			set @table_name = 'Materias'
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

create procedure sp_Get_Materias
	@Materia_id int
as
begin
	
	select * from Materias m
	where m.Materia_id = @Materia_id
	
end
return

/*----------------- Get ALL -----------------------*/


create procedure sp_GetAll_Materias
as
begin
	select * from Materias
end
return


create procedure sp_GetAll_Materias_Disponibles
	@user_id int
as
begin
	
	select m.* from Materias m
	where m.Materia_id not in (
		select Materia_id from Usuario_Materia where user_id = @user_id
	)
	
end
return

create procedure sp_GetAll_Materias_Alumno
	@user_id int
as
begin
	
	select m.*, em.* from Materias m
	inner join Usuario_Materia um on um.materia_id = m.Materia_id
	inner join Estado_Materia em on em.Estado_Materia_id = um.Estado_Materia_id
	where um.user_id = @user_id
	
end
return