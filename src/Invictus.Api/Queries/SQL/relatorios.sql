select 
Aluno.Nome,
Aluno.NumeroMatricula,
Aluno.NomeSocial,
Aluno.CPF,
Aluno.RG,
Aluno.Nascimento,
Aluno.Naturalidade,
Aluno.NaturalidadeUF,
Aluno.Email,
Aluno.Ativo,
Turma.Identificador as Observacoes 
from aluno
inner join Turma on Turma.Id = 2   
where aluno.id in (
select AlunoId from matriculados 
where matriculados.TurmaId = 2 and Turma.UnidadeId = 1)

-- TODAS TURMAS

select 
Aluno.Nome,
Aluno.NumeroMatricula,
Aluno.NomeSocial,
Aluno.CPF,
Aluno.RG,
Aluno.Nascimento,
Aluno.Naturalidade,
Aluno.NaturalidadeUF,
Aluno.Email,
Aluno.Ativo,
Turma.Identificador as Observacoes 
from aluno
inner join Matriculados 
on Aluno.Id = Matriculados.AlunoId
inner join Turma on Matriculados.TurmaId = Turma.Id
where Turma.UnidadeId = 1

-- CALENDARIOS

select * from Calendarios
where Calendarios.UnidadeId = 1

select * from Calendarios
where Calendarios.TurmaId = 1

select * from Calendarios
where Calendarios.salaId = 2

select * from Calendarios
where Calendarios.ProfessorId = 2

-- DOCS PENDENTES
-- select * from Documento_Aluno
select 
Aluno.Nome,
documento_Aluno.Descricao,
documento_Aluno.Comentario,
documento_Aluno.DocEnviado,
documento_Aluno.DataCriacao,
Turma.Id
from documento_Aluno
inner join Aluno on Documento_Aluno.AlunoId = aluno.id
inner join Turma on Documento_Aluno.TurmaId = Turma.Id
-- filter
--where Documento_Aluno.TurmaId = 1
where Documento_Aluno.alunoId = 24

-- CALENDARIOS

select
turma.Descricao as curso,
calendarios.diaaula,  
calendarios.DiaDaSemana,
calendarios.HoraInicial,
calendarios.HoraFinal,
Materias.descricao as materia,
turma.identificador as turma,
salas.titulo as sala
from calendarios  
inner join materias on Calendarios.MateriaId = materias.Id 
inner join turma on Calendarios.TurmaId = turma.Id
inner join salas on Calendarios.SalaId = Salas.id
where calendarios.TurmaId = 10 order by diaaula asc

