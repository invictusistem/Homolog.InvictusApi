using AutoMapper;
using ExcelDataReader;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Dtos.PedagDto;
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
        public RelatorioApp(IMapper mapper, InvictusDbContext db, IMatriculaApplication matriculaApplication)
        {
            _mapper = mapper;
            _db = db;
            _matriculaApplication = matriculaApplication;
        }
        public void ReadAndSaveExcel()
        {
            List<AlunoExcelDto> alunosDto = new List<AlunoExcelDto>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var unidadeALCId = new Guid("99301da0-f674-4810-b1cd-08d9d78d8577");
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
                            UnidadeId = unidadeALCId,

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

            // alunosDto.RemoveAt(0);

            // var unidadeId = _db.Unidades.Where(u => u.Sigla == "CGI").Select(u => u.Id).SingleOrDefault();
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

                alunoAdd.SetUnidadeId(unidadeALCId);
                var data = new DateTime(2021, 7, 1, 0, 0, 0);
                alunoAdd.SetDataCadastro(data);


                //  alunoAdd.AtivarAluno();
                //  alunoAdd.SetDataCadastro();

                alunos.Add(alunoAdd);

                

                

            }

            _db.Alunos.AddRange(alunos);
            _db.SaveChanges();
            /*
            ///////// FINANCEIRO
            var listaNomes = new List<string>();

            using (var stream = File.Open("ALCANTARA - FINC_ENF01.xlsx", FileMode.Open, FileAccess.Read))
            {
                
                stream.Position = 1;


                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        if (String.IsNullOrEmpty(reader?.GetValue(0)?.ToString())) break;

                        listaNomes.Add(reader?.GetValue(1)?.ToString());
                    }
                }
            }


            var nomesDistinct = listaNomes.Distinct();
            var grege = alunos.Count();

            var plano = _db.PlanosPgmTemplate.Find(new Guid("dec6d7d1-1ba8-42b9-8bbd-08d9af7dad80"));

            for (int i = 0; i < alunos.Count(); i++)
            {
                Debug.WriteLine(alunos[i].Nome);
                Debug.WriteLine(nomesDistinct.ToList()[i]);
                Debug.WriteLine(listaNomes.Where(l => l == nomesDistinct.ToList()[i]).Count());

                var command = new MatriculaCommand();
                command.menorIdade = false;
                command.temRespFin = false;
                command.plano.bonusPontualidade = plano.BonusPontualidade;
                command.plano.confirmacaoPagmMat = true;
                command.plano.parcelas = 22;
                command.plano.planoId = plano.Id;
                command.plano.taxaMatricula = 0;
                command.plano.valor = plano.Valor;
                command.plano.valorParcela = 195;

                _matriculaApplication.AddParams(unidadeALCId, alunos[i].Id, command);
                _matriculaApplication.Matricular().ConfigureAwait(true);


            }
            
            Debug.WriteLine("");
          
            */

        }
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
        // public virtual List<Responsavel> Responsaveis { get; set; }

    }
}
