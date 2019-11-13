
/*-------------------------------- T A B L A S --------------------------------------------------------------------------------------------*/

create table Nota_Materia
(
	Nota_Materia_id int identity(1,1),
	Nota_id int,
	Materia_id int,

	foreign key (Nota_id) references Notas (Nota_id),
	foreign key (Materia_id) references Materias (Materia_id),
	primary key (Nota_Materia_id)
)


create table Deleted_Nota_Materia
(
	Deleted_Nota_Materia_id int identity(1,1),
	Nota_Materia_id int,
	Nota_id int,
	Materia_id int
	
	primary key (Deleted_Nota_Materia_id)
)

/*-------------------------------- V A L I D A T E      M A T E R I A S -------------------------------------------------------------------------------*/



create procedure sp_Validate_Nota_Materia

@Nota_id int,
@Materia_id int,
@mensaje int output

as
begin
	declare @busqueda int
	declare @busqueda2 varchar(max)

	set @busqueda = (select top 1 n.valor from Notas n where n.valor = @valor)
	set @busqueda2 = (select top 1 m.Nombre from Materias m where m.Nombre = @Nombre)

	if (@busqueda is null or @busqueda2 is null)
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

create procedure sp_Insert_Nota_Materia
@Nota_id int,
@Materia_id int,
@mensaje int output

as
begin
	begin transaction
	begin try

		insert into Nota_Materia (Nota_id,Materia_id)
		values (@Nota_id,@Materia_id)

		set @mensaje = (select m.Nota_Materia_id from Nota_Materia m where m.Nota_id = @Nota_id and m.Materia_id = @Materia_id)

	commit transaction
	end try

	begin catch

		rollback transaction
		set @mensaje = 0
	end catch
end
return

/*-------------------------------- U P D A T E      M A T E R I A S -------------------------------------------------------------------------------*/

create procedure sp_Update_Nota_Materia

	@Nota_Materia_id int,
	@Nota_id int,
	@Materia_id int,
	@mensaje int output

as
begin
	begin transaction
	begin try

		update Nota_Materia set Nota_id = @Nota_id, Materia_id = @Materia_id where Nota_Materia_id = @Nota_Materia_id
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

create procedure sp_Delete_Nota_Materia

	@Nota_Materia_id int,
	@mensaje int output

as
begin
	begin transaction
	begin try

		delete Nota_Materia where Nota_Materia_id = @Nota_Materia_id
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

create trigger TR_DeletedNota_Materia on Nota_Materia 
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
			set @table_name = 'Nota_Materia'
			set @item_id = select usuario_id from deleted
			set @user_id = get user_id()

			insert into Auditoria (Action,table_name,item_id,user_id)
			values (@Action,@table_name,@item_id,@user_id)

			insert into Deleted_Nota_Materia (Nota_Materia_id,Nota_id,Materia_id)
			select d.Nota_Materia_id,d.Nota_id,d.Materia_id from deleted d

		commit transaction
		end try

		begin catch

			rollback transaction

		end catch
	end


/*---------------------------------T R I G G E R   I N S E R T   M A T E R I A ----------------------------------------------------------------------------------------*/


create trigger TR_InsertNota_Materia on Nota_Materia
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
			set @table_name = 'Nota_Materia'
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

create trigger TR_UpdateNota_Materia on Nota_Materia
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
			set @table_name = 'Nota_Materia'
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

create procedure sp_Get_Nota_Materia
	@Nota_Materia_id int
as
begin
	
	select * from Nota_Materia n
	where n.Nota_Materia_id = @Nota_Materia_id
	
end
return

/*----------------- Get ALL -----------------------*/


create procedure sp_GetAll_Nota_Materia
as
begin
	select * from Nota_Materia
end
return