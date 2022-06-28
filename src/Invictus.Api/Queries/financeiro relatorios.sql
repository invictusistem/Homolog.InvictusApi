-- FINANCEIRO	
-- RECEBIDAS

-- CONTAS RECEBIDAS

SELECT  -- ALUNOS OU COLABORADOR
Boletos.id,
ISNULL(Pessoas.nome, Matriculas.Nome) as Nome,
Boletos.Historico,
Boletos.DataPagamento,
Boletos.ValorPago,
SubContas.Descricao as Subconta,
FormasRecebimento.Descricao as Forma_Recebimento
FROM Boletos
LEFT JOIN Pessoas ON Boletos.PessoaId = Pessoas.id
LEFT JOIN Matriculas ON Boletos.PessoaId = Matriculas.Id
LEFT JOIN SubContas ON Boletos.SubContaId = SubContas.Id
LEFT JOIN FormasRecebimento ON Boletos.FormaRecebimentoId = FormasRecebimento.Id
WHERE Boletos.StatusBoleto = 'Pago'
AND Boletos.Tipo = 'Crédito'
--AND Boletos.DataPagamento...

-- POR PACOTES

SELECT  -- ALUNOS OU COLABORADOR
Boletos.id,
ISNULL(Pessoas.nome, Matriculas.Nome) as Nome,
Boletos.Historico,
Boletos.DataPagamento,
Boletos.ValorPago,
SubContas.Descricao as Subconta,
FormasRecebimento.Descricao as Forma_Recebimento
FROM Boletos
LEFT JOIN Pessoas ON Boletos.PessoaId = Pessoas.id
INNER JOIN Matriculas ON Boletos.PessoaId = Matriculas.Id
INNER JOIN Turmas ON Matriculas.TurmaId = Turmas.Id
LEFT JOIN SubContas ON Boletos.SubContaId = SubContas.Id
LEFT JOIN FormasRecebimento ON Boletos.FormaRecebimentoId = FormasRecebimento.Id
WHERE Boletos.StatusBoleto = 'Pago'
AND Boletos.Tipo = 'Crédito'
--AND Turmas.TypePacoteId = ''

-- POR TURMA

SELECT  -- ALUNOS OU COLABORADOR
Boletos.id,
ISNULL(Pessoas.nome, Matriculas.Nome) as Nome,
Boletos.Historico,
Boletos.DataPagamento,
Boletos.ValorPago,
SubContas.Descricao as Subconta,
FormasRecebimento.Descricao as Forma_Recebimento,
Turmas.Descricao
FROM Boletos
LEFT JOIN Pessoas ON Boletos.PessoaId = Pessoas.id
INNER JOIN Matriculas ON Boletos.PessoaId = Matriculas.Id
INNER JOIN Turmas ON Matriculas.TurmaId = Turmas.Id
LEFT JOIN SubContas ON Boletos.SubContaId = SubContas.Id
LEFT JOIN FormasRecebimento ON Boletos.FormaRecebimentoId = FormasRecebimento.Id
WHERE Boletos.StatusBoleto = 'Pago'
AND Boletos.Tipo = 'Crédito'
--WHERE Turmas.id = ''


-- FORMA DE RECEBIMENTO
SELECT  
Boletos.id,
ISNULL(Pessoas.nome, Matriculas.Nome) as Nome,
Boletos.Historico,
Boletos.DataPagamento,
Boletos.ValorPago,
SubContas.Descricao as Subconta,
FormasRecebimento.Descricao as Forma_Recebimento,
Turmas.Descricao
FROM Boletos
LEFT JOIN Pessoas ON Boletos.PessoaId = Pessoas.id
INNER JOIN Matriculas ON Boletos.PessoaId = Matriculas.Id
INNER JOIN Turmas ON Matriculas.TurmaId = Turmas.Id
LEFT JOIN SubContas ON Boletos.SubContaId = SubContas.Id
INNER JOIN FormasRecebimento ON Boletos.FormaRecebimentoId = FormasRecebimento.Id
WHERE Boletos.StatusBoleto = 'Pago'
AND Boletos.Tipo = 'Crédito'
--WHERE FormasRecebimento.id = ''

-- RECEBIMENTO NO PERIODO
SELECT  
Boletos.id,
ISNULL(Pessoas.nome, Matriculas.Nome) as Nome,
Boletos.Historico,
Boletos.DataPagamento,
Boletos.ValorPago,
SubContas.Descricao as Subconta,
FormasRecebimento.Descricao as Forma_Recebimento,
FormasRecebimento.EhCartao,
FormasRecebimento.DiasParaCompensacao,
Turmas.Descricao,
Bancos.Nome as BANCO
FROM Boletos
LEFT JOIN Pessoas ON Boletos.PessoaId = Pessoas.id
INNER JOIN Matriculas ON Boletos.PessoaId = Matriculas.Id
INNER JOIN Turmas ON Matriculas.TurmaId = Turmas.Id
LEFT JOIN SubContas ON Boletos.SubContaId = SubContas.Id
LEFT JOIN Bancos ON Boletos.BancoId = Bancos.Id
INNER JOIN FormasRecebimento ON Boletos.FormaRecebimentoId = FormasRecebimento.Id
WHERE Boletos.StatusBoleto = 'Pago'
AND Boletos.Tipo = 'Crédito'
--WHERE FormasRecebimento.id = ''
--WHERE Bancos.id = ''

-- A RECEBER
--CONTAS A RECEBER

SELECT  -- ALUNOS OU COLABORADOR
Boletos.id,
Boletos.StatusBoleto,
ISNULL(Pessoas.nome, Matriculas.Nome) as Nome,
Boletos.Historico,
Boletos.Vencimento,
Boletos.ValorPago,
SubContas.Descricao as Subconta
FROM Boletos
LEFT JOIN Pessoas ON Boletos.PessoaId = Pessoas.id
LEFT JOIN Matriculas ON Boletos.PessoaId = Matriculas.Id
LEFT JOIN SubContas ON Boletos.SubContaId = SubContas.Id
WHERE (Boletos.StatusBoleto = 'Em aberto' OR Boletos.StatusBoleto = 'Vencido')  
AND Boletos.Tipo = 'Crédito'
--AND Boletos.DataPagamento...

-- CONTAS RECEBER POR PACOTE

SELECT  -- ALUNOS OU COLABORADOR
Boletos.id,
Boletos.StatusBoleto,
ISNULL(Pessoas.nome, Matriculas.Nome) as Nome,
Boletos.Historico,
Boletos.Vencimento,
Boletos.ValorPago,
SubContas.Descricao as Subconta,
TypePacote.Nome
FROM Boletos
LEFT JOIN Pessoas ON Boletos.PessoaId = Pessoas.id
INNER JOIN Matriculas ON Boletos.PessoaId = Matriculas.Id
LEFT JOIN SubContas ON Boletos.SubContaId = SubContas.Id
LEFT JOIN Turmas ON Matriculas.TurmaId = Turmas.Id
LEFT JOIN TypePacote ON Turmas.TypePacoteId = TypePacote.Id
WHERE (Boletos.StatusBoleto = 'Em aberto' OR Boletos.StatusBoleto = 'Vencido')  
AND Boletos.Tipo = 'Crédito'
--AND TypePacote.id = ''


-- FATURAMENTO MENSAL

SELECT 
SUM(Boletos.ValorPago)
FROM Boletos
WHERE Boletos.StatusBoleto = 'Pago'
AND Boletos.Tipo = 'Crédito'
-- AND Boletos.DataPagamento...

-- CONTAS A PAGAR

SELECT  -- ALUNOS OU COLABORADOR
Boletos.id,
Boletos.StatusBoleto,
ISNULL(Pessoas.nome, Matriculas.Nome) as Nome,
Boletos.Historico,
Boletos.Vencimento,
Boletos.Valor,
SubContas.Descricao as Subconta
FROM Boletos
LEFT JOIN Pessoas ON Boletos.PessoaId = Pessoas.id
LEFT JOIN Matriculas ON Boletos.PessoaId = Matriculas.Id
LEFT JOIN SubContas ON Boletos.SubContaId = SubContas.Id
WHERE (Boletos.StatusBoleto = 'Em aberto' OR Boletos.StatusBoleto = 'Vencido')  
AND Boletos.Tipo = 'Débito'

-- DESPESAS POR PLANO DE CONTAS
SELECT  -- ALUNOS OU COLABORADOR
Boletos.id,
Boletos.StatusBoleto,
ISNULL(Pessoas.nome, Matriculas.Nome) as Nome,
Boletos.Historico,
Boletos.Vencimento,
Boletos.Valor,
SubContas.Descricao as Subconta,
PlanoContas.Descricao,
CentroCustos.Descricao
FROM Boletos
LEFT JOIN Pessoas ON Boletos.PessoaId = Pessoas.id
LEFT JOIN Matriculas ON Boletos.PessoaId = Matriculas.Id
LEFT JOIN SubContas ON Boletos.SubContaId = SubContas.Id
LEFT JOIN PlanoContas ON SubContas.PlanoContaId = PlanoContas.Id
INNER JOIN CentroCustos ON Boletos.CentroCustoId = CentroCustos.id
--WHERE Boletos.StatusBoleto = 'Pago'
--AND Boletos.Tipo = 'Débito'
--AND Boletos.CentroCustoId <> NULL AND Boletos.CentroCustoId <> '00000000-0000-0000-0000-000000000000'

SELECT * FROM Boletos WHERE Boletos.id = '485338D6-2DD7-499F-AD64-88F69D457090'

-- DESPESAS POR CENTRO DE CUSTO

