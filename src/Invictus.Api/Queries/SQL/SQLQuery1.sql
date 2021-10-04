use invictusdb
--insert into colaborador(nome,Email,CPF,Celular,Cargo,Unidade,Perfil,PerfilAtivo,Ativo,CEP,Logradouro,Complemento,Bairro,Cidade,UF) 
--values('Teste','teste@gmail.com','12345678912','(21) 98733-3442','Analista','CGI','',1,1,'22620171','teste','teste','Barra','Rio de Janeiro','RJ');


--SELECT * from Colaborador where LOWER(colaborador.nome) like LOWER('%teste%') collate SQL_Latin1_General_CP1_CI_AI AND 
--LOWER(colaborador.email) like LOWER('%') collate SQL_Latin1_General_CP1_CI_AI AND 
--LOWER(colaborador.cpf) like LOWER('%%') collate SQL_Latin1_General_CP1_CI_AI 
--ORDER BY Colaborador.Nome 
--OFFSET(2 - 1) * 5 ROWS FETCH NEXT 5 ROWS ONLY


SELECT Count(*) from Colaborador where LOWER(colaborador.nome) like LOWER('%teste%') collate SQL_Latin1_General_CP1_CI_AI AND 
LOWER(colaborador.email) like LOWER('%') collate SQL_Latin1_General_CP1_CI_AI AND 
LOWER(colaborador.cpf) like LOWER('%%') collate SQL_Latin1_General_CP1_CI_AI 


                    