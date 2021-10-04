--var query = "SELECT Colaborador.Id, Colaborador.Nome, colaborador.email from Colaborador where Colaborador.Cargo = 'Professor' and " +
--                        "Colaborador.Unidade = @unidade " +
--                        "ORDER BY Colaborador.Nome ";



--            var query2 = @"select Colaborador.Id, Colaborador.Nome, colaborador.email from colaborador 
--where colaborador.id not in 
--(select professorId from ProfessoresTurma 
--where TurmaId = 59)";

SELECT * from Colaborador 
where colaborador.id not in 
(select professorId from ProfessoresTurma 
where TurmaId = 59)
GROUP BY Colaborador.Nome 
having Colaborador.Cargo = 'Professor' and 
Colaborador.Unidade = 'Campo Grande' 
ORDER BY Colaborador.Nome 
--                        "ORDER BY Colaborador.Nome ";