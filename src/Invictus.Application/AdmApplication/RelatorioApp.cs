using AutoMapper;
using ExcelDataReader;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Financeiro.Fornecedores;
using Invictus.Dtos.Financeiro;
using Invictus.Dtos.PedagDto;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class RelatorioApp : IRelatorioApp
    {
        private readonly IMapper _mapper;
        private readonly InvictusDbContext _db;
        private readonly IMatriculaApplication _matriculaApplication;
        private readonly IAspNetUser _aspNetUser;
        public RelatorioApp(IMapper mapper, InvictusDbContext db, IMatriculaApplication matriculaApplication, IAspNetUser aspNetUser)
        {
            _mapper = mapper;
            _db = db;
            _matriculaApplication = matriculaApplication;
            _aspNetUser = aspNetUser;
        }

        public List<MatriculaCommand> MatriculaExcel(MatriculaPlanilha matricula)
        {
            List<AlunoExcelDto> alunosDto = new List<AlunoExcelDto>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();
            var userId = _aspNetUser.ObterUsuarioId();
            //bool eHMenorIdade = false;
            //var unida de = new Guid("99301da0-f674-4810-b1cd-08d9d78d8577");
            // var turmaId = new Guid("7104d99b-142d-4d1c-9c41-f7c1dde52b0d");
            //using (var stream = new MemoryStream())
            using (var stream = File.Open(matricula.planilhaNome, FileMode.Open, FileAccess.Read))
            {
                //file.CopyTo(stream);
                stream.Position = 1;


                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        if (String.IsNullOrEmpty(reader?.GetValue(0)?.ToString())) break;

                        var naturalidade = reader?.GetValue(14)?.ToString().Split(" - ");
                        var endereco = reader?.GetValue(5)?.ToString().Split(",");
                        
                        //var logra = endereco[0];
                        //var numero = endereco[1];
                        //var compl = endereco[2];

                        var testar = reader?.GetValue(1)?.ToString();
                        var tertarNomePai = reader?.GetValue(15)?.ToString();

                        var alun = new AlunoExcelDto()
                        {
                            Nome = reader?.GetValue(1)?.ToString(),
                            CPF = reader?.GetValue(3)?.ToString(),
                            RG = reader?.GetValue(2)?.ToString(),
                            NomePai = reader?.GetValue(15)?.ToString(),
                            NomeMae = reader?.GetValue(16)?.ToString(),
                            Nascimento = Convert.ToDateTime(reader?.GetValue(4)?.ToString()),
                            Naturalidade = naturalidade[0],
                            NaturalidadeUF = naturalidade[1],
                            Email = reader?.GetValue(13)?.ToString(),

                            TelReferencia = reader?.GetValue(21)?.ToString(),
                            NomeContatoReferencia = reader?.GetValue(17)?.ToString(),

                            TelResidencial = reader?.GetValue(10)?.ToString(),
                            TelCelular = reader?.GetValue(11)?.ToString(),
                            TelWhatsapp = reader?.GetValue(12)?.ToString(),

                            DataCadastro = Convert.ToDateTime("01/08/2021"),
                            Ativo = true,
                            UnidadeId = unidadeId,

                            Bairro = reader?.GetValue(6)?.ToString(),
                            CEP = reader?.GetValue(9)?.ToString(),
                            Complemento = endereco[2],

                            Logradouro = endereco[0],
                            Numero = endereco[1],
                            Cidade = reader?.GetValue(7)?.ToString(),
                            UF = reader?.GetValue(8)?.ToString()
                        };

                        

                        var resp = reader?.GetValue(0)?.ToString();
                        if (resp == "SIM"){

                            var nasc = Convert.ToDateTime(reader?.GetValue(4)?.ToString());

                            var maisDezoito = nasc.AddYears(18);

                            if(maisDezoito > DateTime.Now)
                            {
                                
                                // resp menor e fin
                                var res = new RespExcel();
                                res.tipo = TipoResponsavel.ResponsavelMenor.DisplayName;
                                res.nome = reader?.GetValue(17)?.ToString();
                                res.cpf = reader?.GetValue(19)?.ToString();
                                res.rg = reader?.GetValue(18)?.ToString();
                                res.nascimento = Convert.ToDateTime(reader?.GetValue(20)?.ToString());
                                res.email = reader?.GetValue(24)?.ToString();
                                res.telCelular = reader?.GetValue(22)?.ToString();
                                res.telResidencial = reader?.GetValue(21)?.ToString();
                                res.telWhatsapp = reader?.GetValue(23)?.ToString();
                                res.temRespFin = false;

                                res.bairro = reader?.GetValue(6)?.ToString();
                                res.cep = reader?.GetValue(9)?.ToString();
                                res.complemento = endereco[2];
                                res.logradouro = endereco[0];
                                res.numero = endereco[1];
                                res.cidade = reader?.GetValue(7)?.ToString();
                                res.uf = reader?.GetValue(8)?.ToString();

                                alun.Responsavel = res;

                            }
                            else
                            {
                                var res = new RespExcel();
                                res.tipo = TipoResponsavel.ResponsavelFinanceiro.DisplayName;
                                res.nome = reader?.GetValue(17)?.ToString();
                                res.cpf = reader?.GetValue(19)?.ToString();
                                res.rg = reader?.GetValue(18)?.ToString();
                                res.nascimento = Convert.ToDateTime(reader?.GetValue(20)?.ToString());
                                res.email = reader?.GetValue(24)?.ToString();
                                res.telCelular = reader?.GetValue(22)?.ToString();
                                res.telResidencial = reader?.GetValue(21)?.ToString();
                                res.telWhatsapp = reader?.GetValue(23)?.ToString();
                                res.temRespFin = true;

                                res.bairro = reader?.GetValue(6)?.ToString();
                                res.cep = reader?.GetValue(9)?.ToString();
                                res.complemento = endereco[2];
                                res.logradouro = endereco[0];
                                res.numero = endereco[1];
                                res.cidade = reader?.GetValue(7)?.ToString();
                                res.uf = reader?.GetValue(8)?.ToString();
                                alun.Responsavel = res;
                            }

                        }

                        alunosDto.Add(alun);
                        // verificar Se Menor

                        //se sim = cadastrar resp
                        // se nao
                        // ver se o o nome do resp é difeirete, se for, cadastrar como resp fin
                    }
                }
            }


            List<Aluno> alunos = new List<Aluno>();

            foreach (var item in alunosDto)
            {
                if (item.CPF != null)
                    item.CPF = item.CPF.Replace(".", "").Replace("-", "").Replace(".", "");//; ; ; .Trim(new Char[] { ' ', '-', '.' });
                                                                                           // item.CPF = item.CPF.Replace(".", "").Replace("-", "").Replace("", "");
                                                                                           // item.CPF = item.CPF.Trim(new Char[] { ' ', '-', '.' });
                if (item.RG != null)
                    item.RG = item.RG.Replace(".", "").Replace("-", "").Replace(".", ""); //.Trim(new Char[] { ' ', '-', '.' });
                                                                                          // item.RG = item.RG.Trim(new Char[] { ' ', '-', '.' });
                                                                                          // item.RG = item.RG.Trim(new Char[] { ' ', '-', '.' });
                if (item.CEP != null)
                    item.CEP = item.CEP.Replace("-", "").Replace(" ", "").Replace(".", "");/// Trim(new Char[] { ' ', '-' });

                if (item.TelCelular != null)
                    item.TelCelular = item.TelCelular.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");//.Trim(new Char[] { ' ', '(', ')','-' });


                if (item.TelWhatsapp != null)
                    item.TelWhatsapp = item.TelWhatsapp.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");//item.TelResidencial = item.TelResidencial.Trim(new Char[] { ' ', '(', ')', '-' });
                                                                                                                            // item.TelWhatsapp = item.TelWhatsapp.Trim(new Char[] { ' ', '(', ')', '-' });
                if (item.TelResidencial != null)
                    item.TelResidencial = item.TelResidencial.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");//item.TelResidencial = item.TelResidencial.Trim(new Char[] { ' ', '(', ')', '-' });

                if (item.TelReferencia != null)
                    item.TelReferencia = item.TelReferencia.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");//item.TelResidencial = item.TelResidencial.Trim(new Char[] { ' ', '(', ')', '-' });

                // RESPONSAVEL
                if (item.Responsavel != null)
                {
                    if (item.Responsavel.cpf != null)
                        item.Responsavel.cpf = item.Responsavel.cpf.Replace(".", "").Replace("-", "").Replace(".", "");//; ; ; .Trim(new Char[] { ' ', '-', '.' });
                                                                                               // item.CPF = item.CPF.Replace(".", "").Replace("-", "").Replace("", "");
                                                                                               // item.CPF = item.CPF.Trim(new Char[] { ' ', '-', '.' });
                    if (item.Responsavel.rg != null)
                        item.Responsavel.rg = item.Responsavel.rg.Replace(".", "")?.Replace("-", "")?.Replace(".", ""); //.Trim(new Char[] { ' ', '-', '.' });
                                                                                              // item.RG = item.RG.Trim(new Char[] { ' ', '-', '.' });
                                                                                              // item.RG = item.RG.Trim(new Char[] { ' ', '-', '.' });
                    if (item.Responsavel.cep != null)
                        item.Responsavel.cep = item.Responsavel.cep.Replace("-", "").Replace(" ", "").Replace(".", "");/// Trim(new Char[] { ' ', '-' });

                    if (item.Responsavel.telCelular != null)
                        item.Responsavel.telCelular = item.Responsavel.telCelular.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");//.Trim(new Char[] { ' ', '(', ')','-' });


                    if (item.Responsavel.telWhatsapp != null)
                        item.Responsavel.telWhatsapp = item.Responsavel.telWhatsapp.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");//item.TelResidencial = item.TelResidencial.Trim(new Char[] { ' ', '(', ')', '-' });
                                                                                                                                // item.TelWhatsapp = item.TelWhatsapp.Trim(new Char[] { ' ', '(', ')', '-' });
                    if (item.Responsavel.telResidencial != null)
                        item.Responsavel.telResidencial = item.Responsavel.telResidencial.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");//item.TelResidencial = item.TelResidencial.Trim(new Char[] { ' ', '(', ')', '-' });

                    
                }

                var lead = _mapper.Map<Aluno>(item);
                /// lead.SetDateAndResponsavelInLead(user.Email + "/" + user.UserName);

                var endereco = new AlunoEndereco(item.Bairro?.ToUpper(), item.CEP, item.Complemento?.ToUpper(),
                    item.Logradouro?.ToUpper(), item.Numero?.ToUpper(), item.Cidade?.ToUpper(), item.UF?.ToUpper());

                var alunoAdd = new Aluno(item.Nome?.ToUpper(), null, item.CPF, item.RG,
                item.NomePai, item.NomeMae, item.Nascimento, item.Naturalidade?.ToUpper(), item.NaturalidadeUF?.ToUpper(), item.Email, null,
                   null, item.TelCelular, item.TelResidencial, item.TelWhatsapp, endereco);

                alunoAdd.AtivarAluno();
                alunoAdd.SetColaboradorResponsavelPeloCadastro(userId);
                alunoAdd.SetUnidadeId(unidadeId);
                var data = new DateTime(2021, 7, 1, 0, 0, 0);
                alunoAdd.SetDataCadastro(data);

                alunos.Add(alunoAdd);
            }

            _db.Alunos.AddRange(alunos);

            var alunosIds = new List<Guid>();

            foreach (var item in alunos)
            {
                alunosIds.Add(item.Id);
            }

            //_db.SaveChanges();


            ///////// FINANCEIRO
            //var listaNomes = new List<string>();
            var listaFin = new List<PlanilhafinDto>();

            using (var stream = File.Open(matricula.planilhaFin, FileMode.Open, FileAccess.Read))
            {
                stream.Position = 1;

                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        if (String.IsNullOrEmpty(reader?.GetValue(0)?.ToString())) break;

                        var nomeX = reader?.GetValue(1)?.ToString();
                        var cpfX = reader?.GetValue(2)?.ToString();
                        var primeiroDia = new DateTime(2022, 1, 10, 0, 0, 0);
                        var valorX = Convert.ToDecimal(reader?.GetValue(5)?.ToString());


                        listaFin.Add(new PlanilhafinDto()
                        {
                            nome = nomeX,
                            cpf = cpfX,
                            primeiroDiaPag = primeiroDia,
                            valor = valorX

                        });
                    }
                }
            }

            foreach (var item in listaFin)
            {
                item.cpf = item.cpf.Replace(".", "").Replace("-", "").Replace(".", "");
            }

            var listaFinDistinct = listaFin.DistinctBy(l => l.cpf);

            //var nomesDistinct = listaNomes.Distinct();
            //var grege = alunos.Count();

            var plano = _db.PlanosPgmTemplate.Find(matricula.planoId);

            var commands = new List<MatriculaCommand>();

            for (int i = 0; i < listaFinDistinct.Count(); i++)
            {

                var listaCompleta = listaFin.Where(l => l.cpf == listaFinDistinct.ToList()[i].cpf);

                var command = new MatriculaCommand();
                

                command.plano.bolsaId = "";
                command.plano.bonusPontualidade = plano.BonusPontualidade;
                command.plano.ciencia = "Instagram";
                command.plano.cienciaAlunoId = "";
                command.plano.codigoDesconto = "";
                command.plano.confirmacaoPagmMat = false;
                command.plano.infoParcelas = GenerateParcelas(listaFinDistinct.ToList()[i], listaCompleta);
                command.plano.parcelas = command.plano.infoParcelas.Count();
                command.plano.planoId = plano.Id;
                command.plano.taxaMatricula = 0;
                command.plano.valor = plano.Valor;
                command.plano.valorParcela = 0;


                var abcd = listaFinDistinct.ToList()[i].cpf;
                var alunoId = alunos.Where(a => a.CPF == listaFinDistinct.ToList()[i].cpf).Select(a => a.Id).FirstOrDefault();

                var alunoDto = alunosDto.Where(a => a.CPF == listaFinDistinct.ToList()[i].cpf).FirstOrDefault();

                if(alunoDto.Responsavel != null)
                {
                    var resp = alunoDto.Responsavel;

                    if(resp.tipo == "Responsável financeiro")
                    {
                        command.menorIdade = false;
                        command.temRespFin = true;

                        var respon = new MatForm()
                        {
                            nome = alunoDto.Responsavel.nome,
                            tipo = "Responsável financeiro",
                            cpf = alunoDto.Responsavel.cpf,
                            rg = alunoDto.Responsavel.rg,
                            nascimento = alunoDto.Responsavel.nascimento,
                            parentesco = null,
                            naturalidade = null,
                            naturalidadeUF = null,
                            email = alunoDto.Responsavel.email,
                            telCelular = alunoDto.Responsavel.telCelular,
                            telWhatsapp = alunoDto.Responsavel.telWhatsapp,
                            telResidencial = alunoDto.Responsavel.telResidencial,
                            cep = alunoDto.Responsavel.cep,
                            logradouro = alunoDto.Responsavel.logradouro,
                            numero = alunoDto.Responsavel.numero,
                            complemento = alunoDto.Responsavel.complemento,
                            cidade = alunoDto.Responsavel.cidade,
                            uf = alunoDto.Responsavel.uf,
                            bairro = alunoDto.Responsavel.bairro

                        };

                        command.respFin = respon; // Responsável menor
                    }
                    else
                    {
                        command.menorIdade = true;
                        command.temRespFin = false;

                        var respon = new MatForm()
                        {
                            nome = alunoDto.Responsavel.nome,
                            tipo = "Responsável menor",
                            cpf = alunoDto.Responsavel.cpf,
                            rg = alunoDto.Responsavel.rg,
                            nascimento = alunoDto.Responsavel.nascimento,
                            parentesco = null,
                            naturalidade = null,
                            naturalidadeUF = null,
                            email = alunoDto.Responsavel.email,
                            telCelular = alunoDto.Responsavel.telCelular,
                            telWhatsapp = alunoDto.Responsavel.telWhatsapp,
                            telResidencial = alunoDto.Responsavel.telResidencial,
                            cep = alunoDto.Responsavel.cep,
                            logradouro = alunoDto.Responsavel.logradouro,
                            numero = alunoDto.Responsavel.numero,
                            complemento = alunoDto.Responsavel.complemento,
                            cidade = alunoDto.Responsavel.cidade,
                            uf = alunoDto.Responsavel.uf,
                            bairro = alunoDto.Responsavel.bairro

                        };

                        command.respMenor = respon;
                    }
                }

                

                command.alunoId = alunoId;


                //var turmaId = new Guid("7a76c44a-e294-488f-a57c-6edd5abfb8ec");
                command.turmaId = matricula.turmaId;
                //_matriculaApplication.AddParams(turmaId, alunoId, command);
                //_matriculaApplication.Matricular().ConfigureAwait(true);

                commands.Add(command);
            }

            _db.SaveChanges();

            return commands;
        }

        public void ReadAndSaveExcel()
        {
            List<AlunoExcelDto> alunosDto = new List<AlunoExcelDto>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();
            var turmaId = new Guid("65779718-15aa-430d-a10a-e68686bff3b6");
            //using (var stream = new MemoryStream())
            using (var stream = File.Open("CADASTRO ITAGUAÍ ENF 01.xlsx", FileMode.Open, FileAccess.Read))
            {
                //file.CopyTo(stream);
                stream.Position = 1;


                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        if (String.IsNullOrEmpty(reader?.GetValue(0)?.ToString())) break;

                        var naturalidade = reader?.GetValue(14)?.ToString().Split(" - ");
                        var endereco = reader?.GetValue(5)?.ToString().Split(",");
                        var testar = reader?.GetValue(1)?.ToString();
                        var tertarNomePai = reader?.GetValue(15)?.ToString();
                        alunosDto.Add(new AlunoExcelDto()
                        {
                            Nome = reader?.GetValue(1)?.ToString(),
                            CPF = reader?.GetValue(3)?.ToString(),
                            RG = reader?.GetValue(2)?.ToString(),
                            NomePai = reader?.GetValue(15)?.ToString(),
                            NomeMae = reader?.GetValue(16)?.ToString(),
                            Nascimento = Convert.ToDateTime(reader?.GetValue(4)?.ToString()),
                            Naturalidade = naturalidade[0],
                            NaturalidadeUF = naturalidade[1],
                            Email = reader?.GetValue(13)?.ToString(),

                            TelReferencia = reader?.GetValue(21)?.ToString(),
                            NomeContatoReferencia = reader?.GetValue(17)?.ToString(),

                            TelResidencial = reader?.GetValue(10)?.ToString(),
                            TelCelular = reader?.GetValue(11)?.ToString(),
                            TelWhatsapp = reader?.GetValue(12)?.ToString(),

                            DataCadastro = Convert.ToDateTime("01/08/2021"),
                            Ativo = true,
                            UnidadeId = unidadeId,

                            Bairro = reader?.GetValue(6)?.ToString(),
                            CEP = reader?.GetValue(9)?.ToString(),
                            Complemento = endereco[1],

                            Logradouro = endereco[0],
                            Numero = "",
                            Cidade = reader?.GetValue(7)?.ToString(),
                            UF = reader?.GetValue(8)?.ToString(),


                        });
                    }
                }
            }


            List<Aluno> alunos = new List<Aluno>();

            foreach (var item in alunosDto)
            {
                if (item.CPF != null)
                    item.CPF = item.CPF.Replace(".", "").Replace("-", "").Replace(".", "");//; ; ; .Trim(new Char[] { ' ', '-', '.' });
                                                                                           // item.CPF = item.CPF.Replace(".", "").Replace("-", "").Replace("", "");
                                                                                           // item.CPF = item.CPF.Trim(new Char[] { ' ', '-', '.' });
                if (item.RG != null)
                    item.RG = item.RG.Replace(".", "").Replace("-", "").Replace(".", ""); //.Trim(new Char[] { ' ', '-', '.' });
                                                                                          // item.RG = item.RG.Trim(new Char[] { ' ', '-', '.' });
                                                                                          // item.RG = item.RG.Trim(new Char[] { ' ', '-', '.' });
                if (item.CEP != null)
                    item.CEP = item.CEP.Replace("-", "").Replace(" ", "").Replace(".", "");/// Trim(new Char[] { ' ', '-' });

                if (item.TelCelular != null)
                    item.TelCelular = item.TelCelular.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");//.Trim(new Char[] { ' ', '(', ')','-' });


                if (item.TelWhatsapp != null)
                    item.TelWhatsapp = item.TelWhatsapp.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");//item.TelResidencial = item.TelResidencial.Trim(new Char[] { ' ', '(', ')', '-' });
                                                                                                                            // item.TelWhatsapp = item.TelWhatsapp.Trim(new Char[] { ' ', '(', ')', '-' });
                if (item.TelResidencial != null)
                    item.TelResidencial = item.TelResidencial.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");//item.TelResidencial = item.TelResidencial.Trim(new Char[] { ' ', '(', ')', '-' });

                if (item.TelReferencia != null)
                    item.TelReferencia = item.TelReferencia.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");//item.TelResidencial = item.TelResidencial.Trim(new Char[] { ' ', '(', ')', '-' });




                var lead = _mapper.Map<Aluno>(item);
                /// lead.SetDateAndResponsavelInLead(user.Email + "/" + user.UserName);

                var endereco = new AlunoEndereco(item.Bairro?.ToUpper(), item.CEP, item.Complemento?.ToUpper(),
                    item.Logradouro?.ToUpper(), item.Numero?.ToUpper(), item.Cidade?.ToUpper(), item.UF?.ToUpper());

                var alunoAdd = new Aluno(item.Nome?.ToUpper(), null, item.CPF, item.RG,
                item.NomePai, item.NomeMae, item.Nascimento, item.Naturalidade?.ToUpper(), item.NaturalidadeUF?.ToUpper(), item.Email, null,
                   null, item.TelCelular, item.TelResidencial, item.TelWhatsapp, endereco);

                alunoAdd.AtivarAluno();
                alunoAdd.SetColaboradorResponsavelPeloCadastro(new Guid("e94d7dd8-8fef-4c14-8907-88ed8dc934c8"));
                alunoAdd.SetUnidadeId(unidadeId);
                var data = new DateTime(2021, 7, 1, 0, 0, 0);
                alunoAdd.SetDataCadastro(data);

                alunos.Add(alunoAdd);
            }

            _db.Alunos.AddRange(alunos);
            _db.SaveChanges();

            ///////// FINANCEIRO
            //var listaNomes = new List<string>();
            var listaFin = new List<PlanilhafinDto>();

            using (var stream = File.Open("FINANCEIRO ENF 01.xlsx", FileMode.Open, FileAccess.Read))
            {
                stream.Position = 1;

                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        if (String.IsNullOrEmpty(reader?.GetValue(0)?.ToString())) break;

                        listaFin.Add(new PlanilhafinDto()
                        {
                            nome = reader?.GetValue(1)?.ToString(),
                            cpf = reader?.GetValue(2)?.ToString(),
                            primeiroDiaPag = new DateTime(2022, 1, 10, 0, 0, 0),
                            valor = Convert.ToDecimal(reader?.GetValue(5)?.ToString())

                        });
                    }
                }
            }

            foreach (var item in listaFin)
            {
                item.cpf = item.cpf.Replace(".", "").Replace("-", "").Replace(".", "");
            }

            var listaFinDistinct = listaFin.DistinctBy(l => l.cpf);

            //var nomesDistinct = listaNomes.Distinct();
            //var grege = alunos.Count();

            var plano = _db.PlanosPgmTemplate.Find(new Guid("b5d92e45-ab36-4cc0-80e9-08d9b5b78d2a"));

            for (int i = 0; i < listaFinDistinct.Count(); i++)
            {

                var listaCompleta = listaFin.Where(l => l.cpf == listaFinDistinct.ToList()[i].cpf);

                var command = new MatriculaCommand();
                command.menorIdade = false;
                command.temRespFin = false;

                command.plano.bolsaId = "";
                command.plano.bonusPontualidade = plano.BonusPontualidade;
                command.plano.ciencia = "Instagram";
                command.plano.cienciaAlunoId = "";
                command.plano.codigoDesconto = "";
                command.plano.confirmacaoPagmMat = false;
                command.plano.infoParcelas = GenerateParcelas(listaFinDistinct.ToList()[i], listaCompleta);
                command.plano.parcelas = command.plano.infoParcelas.Count();
                command.plano.planoId = plano.Id;
                command.plano.taxaMatricula = 0;
                command.plano.valor = plano.Valor;
                command.plano.valorParcela = 0;

                var alunoId = alunos.Where(a => a.CPF == listaFinDistinct.ToList()[i].cpf).Select(a => a.Id).FirstOrDefault();

                _matriculaApplication.AddParams(turmaId, alunoId, command);
                _matriculaApplication.Matricular().ConfigureAwait(true);


            }
        }

        public List<Parcela> GenerateParcelas(PlanilhafinDto planilha, IEnumerable<PlanilhafinDto> planilhas)
        {
            var parcelas = new List<Parcela>();

            var data = planilhas.ToList()[0].primeiroDiaPag;

            for (int i = 0; i < planilhas.Count(); i++)
            {
                var parcela = new Parcela();
                parcela.parcelaNo = Convert.ToString(i + 1);
                parcela.valor = planilhas.ToList()[i].valor;
                parcela.vencimento = data.AddMonths(i);

                parcelas.Add(parcela);
            }


            return parcelas;
        }

        public void DeleteExcel()
        {
            List<string> alunosDto = new List<string>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open("CADASTRO ITAGUAÍ ENF 03.xlsx", FileMode.Open, FileAccess.Read))
            {
                stream.Position = 1;

                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        if (String.IsNullOrEmpty(reader?.GetValue(0)?.ToString())) break;

                        alunosDto.Add(reader?.GetValue(3)?.ToString());
                    }
                }
            }


            List<string> cpfs = new List<string>();

            foreach (var item in alunosDto)
            {
                var cpf = item.Replace(".", "").Replace("-", "").Replace(".", "");
                cpfs.Add(cpf);
            }

            foreach (var item in cpfs)
            {
                var aluno = _db.Alunos.Where(a => a.CPF == item).FirstOrDefault();

                if (aluno != null)
                {
                    _db.Alunos.Remove(aluno);

                    _db.SaveChanges();
                }

            }

            // var aluno = await _db.Alunos.Where(a => a.Id == alunoId).FirstOrDefaultAsync();


            // var alunoFoto = await _db.aluno.Where(a => a.a == alunoId).FirstOrDefaultAsync();



            //var alunos = _db.Alunos.

            //_db.Alunos.AddRange(alunos);
            //_db.SaveChanges();

        }

        public void SaveFornecedores()
        {
            List<FornecedorDto> fornecedoresDto = new List<FornecedorDto>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            
            using (var stream = File.Open("FORNECEDORES ITAGUAÍ.xlsx", FileMode.Open, FileAccess.Read))
            {
                stream.Position = 1;

                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        if (String.IsNullOrEmpty(reader?.GetValue(0)?.ToString())) break;
                        
                        fornecedoresDto.Add(new FornecedorDto()
                        {
                            razaoSocial = reader?.GetValue(1)?.ToString().ToUpper(),
                            ie_rg = reader?.GetValue(4)?.ToString(),
                            cnpj_cpf = reader?.GetValue(3)?.ToString(),
                            email = reader?.GetValue(12)?.ToString(),
                            telContato = reader?.GetValue(10)?.ToString(),
                            whatsApp = reader?.GetValue(11)?.ToString(),
                            nomeContato = "",
                            cep = reader?.GetValue(9)?.ToString(),
                            logradouro = reader?.GetValue(5)?.ToString().ToUpper(),
                            complemento = "",
                            cidade = reader?.GetValue(8)?.ToString().ToUpper(),
                            numero = reader?.GetValue(6)?.ToString().ToUpper(),
                            uf = null,
                            bairro = reader?.GetValue(7)?.ToString().ToUpper(),
                            ativo = true,
                            dataCadastro = Convert.ToDateTime(reader?.GetValue(2)?.ToString()),
                            unidadeId = new Guid("99301da0-f674-4810-b1cd-08d9d78d8577")
                        });
                    }
                }
            }

            List<Fornecedor> forncedores = new List<Fornecedor>();

            foreach (var item in fornecedoresDto)
            {
                if (item.cnpj_cpf != null)
                    item.cnpj_cpf = item.cnpj_cpf.Replace(".", "").Replace("-", "").Replace(".", "");

                if (item.ie_rg != null)
                    item.ie_rg = item.ie_rg.Replace(".", "").Replace("-", "").Replace(".", ""); 

                if (item.cep != null)
                    item.cep = item.cep.Replace("-", "").Replace(" ", "").Replace(".", "");

                if (item.telContato != null)
                    item.telContato = item.telContato.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");


                if (item.whatsApp != null)
                    item.whatsApp = item.whatsApp.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                               

                var fornecedor = _mapper.Map<Fornecedor>(item);


                forncedores.Add(fornecedor);
            }

            _db.Fornecedors.AddRange(forncedores);
            _db.SaveChanges();
        }
    }

    public class PlanilhafinDto
    {
        public string nome { get; set; }
        public string cpf { get; set; }
        public DateTime primeiroDiaPag { get; set; }
        public decimal valor { get; set; }
    }

    public class AlunoExcelDto
    {


        public string Nome { get; set; }
        public string NomePai { get; set; }
        public string NomeMae { get; set; }
        public string NumeroMatricula { get; set; }
        public string NomeSocial { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public DateTime Nascimento { get; set; }
        public string Naturalidade { get; set; }
        public string NaturalidadeUF { get; set; }
        public string Email { get; set; }
        public string TelReferencia { get; set; }
        public string NomeContatoReferencia { get; set; }
        public string CienciaCurso { get; set; }
        public string TelCelular { get; set; }
        public string TelResidencial { get; set; }
        public string TelWhatsapp { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Observacoes { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public Guid UnidadeId { get; set; }
        public bool temResp { get; set; }
        public RespExcel Responsavel { get; set; }

    }

    public class RespExcel
    {
         
        public string nome { get; set; }
        public string tipo { get; set; }
        public bool temRespFin { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public DateTime? nascimento { get; set; }
        public string parentesco { get; set; }
        public string naturalidade { get; set; }
        public string naturalidadeUF { get; set; }
        public string email { get; set; }
        public string telCelular { get; set; }
        public string telWhatsapp { get; set; }
        public string telResidencial { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string bairro { get; set; }
        public Guid matriculaId { get; set; }
    }
}
