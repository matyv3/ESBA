-- materias de alumno
select m.*
from Materias m
inner join Usuario_Materia um on um.materia_id = m.Materia_id
inner join users u on u.user_id = um.user_id
where u.user_id = ?

-- alumnos de una materia
select u.*, em.Descripcion as estado_materia
from Materias m
inner join Usuario_Materia um on um.materia_id = m.Materia_id
inner join users u on u.user_id = um.user_id
inner join Estado_Materia em on em.Estado_Materia_Id = um.Estado_Materia_id
where m.Materia_id = ?

-- obtener usuarios por rol
select u.*
from users u
where u.rol_id = ?