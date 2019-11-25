-- Obtiene los datos a partir de un User ID

create procedure sp_Query_User_id
	@user_id int
as
begin


select U.user_id as User_id,R.descripcion as Rol_Descripcion,U.name as User_Nombre,U.surname as User_Apellido,M.Materia_id as Materia_id, M.nombre as Materia_Nombre,N.valor as Nota_Valor,EM.Descripcion as Estado_Materia
from users U


inner join Usuario_Materia um on um.user_id = U.user_id
inner join Estado_Materia EM on EM.Estado_Materia_Id = um.Estado_Materia_id
inner join Nota_Materia NM on NM.Materia_id = um.materia_id
inner join Notas N on N.Nota_id = NM.Nota_id
inner join Materias M on M.Materia_id = um.materia_id
inner join Roles R on R.rol_id = U.rol_id


where U.user_id = @user_id

end
return



-- Obtiene los datos a partir de un Materia_ID


create procedure sp_Query_Materia_id
	@Materia_id int
as
begin


select U.user_id as User_id,U.name as User_Nombre,U.document, U.mail, U.phone, U.address, U.surname as User_Apellido,R.descripcion as Rol_Descripcion,M.Materia_id as Materia_id, M.nombre as Materia_Nombre,N.valor as Nota_Valor,EM.Descripcion as Estado_Materia
from users U


inner join Usuario_Materia um on um.user_id = U.user_id
inner join Estado_Materia EM on EM.Estado_Materia_Id = um.Estado_Materia_id
left join Nota_Materia NM on NM.Materia_id = um.materia_id
left join Notas N on N.Nota_id = NM.Nota_id
inner join Materias M on M.Materia_id = um.materia_id
inner join Roles R on R.rol_id = U.rol_id


where Um.Materia_id = @Materia_id

end
return




-- obtener usuarios por rol

create procedure sp_Get_ALLUser_Rol
	@rol_id int
as
begin


select u.*, r.descripcion
from users u
inner join roles r on r.rol_id = u.rol_id
where u.rol_id = @rol_id


end
return


