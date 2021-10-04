select * from provasagenda where turmaid = 5 
select convert(varchar, AvaliacaoUm, 103) AvaliacaoUm from provasagenda --where turma.id = 3 
--and PrevisaoAtual is not null

select  min(datacriacao) AS DataCriação 
--case 
from turma where turma.id = 4
--select convert(varchar, DataCriacao, 103) DataCriacao from Turma where turma.id = 3 
DECLARE @Course_ID varchar = (select DataCriacao from Turma where turma.id = 3) 
--and DataCriacao is null
IF (@Course_ID = '0001-01-01 00:00:00:0000000')
PRINT 'IF STATEMENT: CONDITION IS TRUE'
ELSE
PRINT 'ELSE STATEMENT: CONDITION IS FALSE'

DECLARE @Course_ID INT = 4

IF (@Course_ID >=3)
	BEGIN
	Select * from aluno --where Tutorial_ID = 1
	--Select * from Guru99 where Tutorial_ID = 2
	END
ELSE
	BEGIN
	Select * from colaborador-- where Tutorial_ID = 3
	--Select * from Guru99 where Tutorial_ID = 4
	END

select convert(varchar, DataCriacao, 103) PrevisaoAtual from Turma where turma.id = 3 
and PrevisaoAtual is not null

--format(getdate(), 'yyyy-MM-dd hh:mm:ss') 
select * from ProvasAgenda where ProvasAgenda.TurmaId = 4


SELECT id, identificador, descricao, statusDaTurma, moduloId  
                          FROM Turma WHERE Turma.UnidadeId = 1  
                          AND Turma.statusDaTurma <> 'Aguardando início'

                            select 
                            Materias.id,
                            Materias.Descricao
                            from materias 
                            where id in (
                            select materiaId from materiasDaTurma 
                            where ProfId = 2 and 
                            MateriasDaTurma.id = 1
                            )

select 
MateriasDaTurma.MateriaId,
Materias.Descricao
from ProfessorNew
left join MateriasdaTurma on ProfessorNew.Id = MateriasDaTurma.ProfessorId
left join Materias on MateriasDaTurma.MateriaId = Materias.Id
--from MateriasDaTurma 
where MateriasDaTurma.ProfId = 2 
and ProfessorNew.TurmaId = 2



select 
Colaborador.Id,
Colaborador.Nome, 
Colaborador.Email,
--Colaborador.Id as ProfId, 
MateriasDaTurma.Id, 
Materias.Id as materiaId,
Materias.Descricao
 from ProfessorNew 
left join MateriasDaTurma on ProfessorNew.Id = MateriasDaTurma.ProfessorId
left join Colaborador on ProfessorNew.ProfId = Colaborador.Id
--left join MateriasDaTurma on ProfessorNew.Id = MateriasDaTurmaProfessorId 
left join Materias on MateriasDaTurma.MateriaId = Materias.Id 
where ProfessorNew.TurmaId = 2
