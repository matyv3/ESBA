

/*-------------------------------- T A B L A S --------------------------------------------------------------------------------------------*/

create table Notas
(
	Nota_id int identity(1,1),
	valor decimal (9,2)

	primary key (Nota_id)
)


create table Deleted_Notas
(
	Deleted_Nota_id int identity(1,1),
	Nota_id int,
	valor decimal (9,2)
	

	primary key (Deleted_Nota_id)
)

/*-------------------------------- V A L I D A T E      M A T E R I A S -------------------------------------------------------------------------------*/



create procedure sp_Validate_Notas

@nota_valor int,
@mensaje int output

as
begin
	declare @busqueda int
	set @busqueda = (select top 1 n.valor from notas n where n.valor = @nota_valor)
	
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

create procedure sp_Insert_Notas 

	@nota_valor int,
	@mensaje int output
as
begin
	begin transaction
	begin try

		insert into Notas (valor)
		values (@nota_valor)
		set @mensaje = (select Nota_id from Notas where valor = @nota_valor)

	commit transaction
	end try

	begin catch

		rollback transaction
		set @mensaje = 0
	end catch
end
return

/*-------------------------------- U P D A T E      M A T E R I A S -------------------------------------------------------------------------------*/

create procedure sp_Update_Notas

	@Nota_id int,
	@nota_valor int,
	@mensaje int output

as
begin
	begin transaction
	begin try

		update Notas set valor = @nota_valor where Nota_id = @Nota_id
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

create procedure sp_Delete_Notas

	@Nota_id int,
	@mensaje int output

as
begin
	begin transaction
	begin try

		delete Notas where Nota_id = @Nota_id
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

create trigger TR_DeletedNotas on Notas 
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
			set @table_name = 'Notas'
			set @item_id = select usuario_id from deleted
			set @user_id = get user_id()

			insert into Auditoria (Action,table_name,item_id,user_id)
			values (@Action,@table_name,@item_id,@user_id)

			insert into Deleted_Notas (Nota_id,valor)
			select d.Nota_id,d.valor from deleted d

		commit transaction
		end try

		begin catch

			rollback transaction

		end catch
	end


/*---------------------------------T R I G G E R   I N S E R T   M A T E R I A ----------------------------------------------------------------------------------------*/


create trigger TR_InsertNotas on Notas
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
			set @table_name = 'Notas'
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

create trigger TR_UpdateNotas on Notas
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
			set @table_name = 'Notas'
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

/*----------------------- G E T  N O T A S ----------------------------C O N F I R M A D O------------------------------------------------*/

create procedure sp_Get_Notas
	@Nota_id int
as
begin
	
	select * from Notas n
	where n.Nota_id = @Nota_id
	
end
return

/*----------------- Get ALL -----------------------*/


create procedure sp_GetAll_Notas
as
begin
	select * from Notas
end
return
