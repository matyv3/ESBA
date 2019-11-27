
/*-------------------------------- T A B L A S --------------------------------------------------------------------------------------------*/

create table Usuario_Materia
(
	Usuario_Materia_id int identity(1,1),
	user_id int,
	materia_id int,
	Estado_Materia_id int,
	Nota_Valor int

	foreign key (user_id) references users (user_id),
	foreign key (Materia_id) references Materias (Materia_id),
	foreign key (Estado_Materia_id) references Estado_Materia (Estado_Materia_id),
	primary key (Usuario_Materia_id)
)


create table Deleted_Usuario_Materia
(
	Deleted_Usuario_Materia_id int identity(1,1),
	Usuario_Materia_id int,
	user_id int,
	materia_id int,
	Estado_Materia_id int,
	Nota_Valor int
	
	primary key (Deleted_Usuario_Materia_id)
)

/*-------------------------------- V A L I D A T E      M A T E R I A S -------------------------------------------------------------------------------*/



create procedure sp_Validate_Usuario_Materia

@user_id int,
@materia_id int,

@mensaje int output

as
begin


	declare @busqueda int


	set @busqueda = (select top 1 n.Usuario_Materia_id from Usuario_Materia n where n.user_id = @user_id and n.materia_id = @materia_id)


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

create procedure sp_Insert_Usuario_Materia
@user_id int,
@materia_id int,
@Estado_Materia_id int = null,
@Nota_Valor int = null,
@mensaje int output

as
begin
	begin transaction
	begin try

		insert into Usuario_Materia (user_id,materia_id,Estado_Materia_id,Nota_Valor)
		values (@user_id,@materia_id,@Estado_Materia_id,@Nota_Valor)

		set @mensaje = (select m.Usuario_Materia_id from Usuario_Materia m where m.user_id = @user_id and m.materia_id = @materia_id)

	commit transaction
	end try

	begin catch

		rollback transaction
		set @mensaje = 0
	end catch
end
return

/*-------------------------------- U P D A T E      M A T E R I A S -------------------------------------------------------------------------------*/

create procedure sp_Update_Usuario_Materia

	@Usuario_Materia_id int,
	@user_id int,
	@materia_id int,
	@Estado_Materia_id int,
	@Nota_Valor int,
	@mensaje int output

as
begin
	begin transaction
	begin try

		update Usuario_Materia set user_id = @user_id, materia_id = @materia_id, Estado_Materia_id = @Estado_Materia_id, Nota_Valor = @Nota_Valor where Usuario_Materia_id = @Usuario_Materia_id
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

create procedure sp_Delete_Usuario_Materia

	@Usuario_Materia_id int,
	@mensaje int output

as
begin
	begin transaction
	begin try

		delete Usuario_Materia where Usuario_Materia_id = @Usuario_Materia_id
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

create trigger TR_DeletedUsuario_Materia on Usuario_Materia 
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
			set @table_name = 'Usuario_Materia'
			set @item_id = select usuario_id from deleted
			set @user_id = get user_id()

			insert into Auditoria (Action,table_name,item_id,user_id)
			values (@Action,@table_name,@item_id,@user_id)

			insert into Deleted_Usuario_Materia (Usuario_Materia_id,user_id,materia_id,Estado_Materia_id,Nota_Valor)
			select d.Usuario_Materia_id,d.user_id,d.materia_id, d.Estado_Materia_id, d.Nota_Valor from deleted d

		commit transaction
		end try

		begin catch

			rollback transaction

		end catch
	end


/*---------------------------------T R I G G E R   I N S E R T   M A T E R I A ----------------------------------------------------------------------------------------*/


create trigger TR_InsertUsuario_Materia on Usuario_Materia
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
			set @table_name = 'Usuario_Materia'
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

create trigger TR_UpdateUsuario_Materia on Usuario_Materia
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
			set @table_name = 'Usuario_Materia'
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

create procedure sp_Get_Usuario_Materia
	@Usuario_Materia_id int
as
begin
	
	select * from Usuario_Materia n
	where n.Usuario_Materia_id = @Usuario_Materia_id
	
end
return

/*----------------- Get ALL -----------------------*/


create procedure sp_GetAll_Usuario_Materia
as
begin
	select * from Usuario_Materia
end
return

create procedure sp_Get_Usuario_Materia_por_user_materia
	@user_id int,
	@materia_id int
as
begin
	
	select * from Usuario_Materia n
	where n.user_id = @user_id and n.materia_id = @materia_id
	
end
return