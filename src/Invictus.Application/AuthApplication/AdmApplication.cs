using AutoMapper;
using ExcelDataReader;
using Invictus.Application.AuthApplication.Interface;
using Invictus.Application.Dtos;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.AlunoAggregate;
//using iTextSharp.text;
//using iTextSharp.text.html.simpleparser;
//using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AuthApplication
{
    public class AdmApplication : IAdmApplication
    {
        private readonly IMapper _mapper;
        private InvictusDbContext _db;
        public AdmApplication(IMapper mapper, InvictusDbContext db)
        {
            _mapper = mapper;
            _db = db;
        }
        public void ReadAndSaveExcelAlunosCGI()
        {
            //List<UserModel> users = new List<UserModel>();
            List<AlunoExcelDto> alunosDto = new List<AlunoExcelDto>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            //using (var stream = new MemoryStream())
            using (var stream = File.Open("ALUNOS ENF01.xlsx", FileMode.Open, FileAccess.Read))
            {
                //file.CopyTo(stream);
                stream.Position = 1;


                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        if (String.IsNullOrEmpty(reader?.GetValue(0)?.ToString())) break;
                        alunosDto.Add(new AlunoExcelDto()
                        {
                            Nome = reader?.GetValue(1)?.ToString(),
                            DataCadastro = Convert.ToDateTime(reader?.GetValue(2)?.ToString()),
                            RG = reader?.GetValue(4)?.ToString(),
                            CPF = reader?.GetValue(5)?.ToString(),
                            Nascimento = Convert.ToDateTime(reader?.GetValue(6)?.ToString()),
                            Logradouro = reader?.GetValue(7)?.ToString(),
                            Bairro = reader?.GetValue(8)?.ToString(),
                            Cidade = reader?.GetValue(9)?.ToString(),
                            UF = reader?.GetValue(10)?.ToString(),
                            CEP = reader?.GetValue(11)?.ToString(),
                            TelCelular = reader?.GetValue(12)?.ToString(),
                            TelWhatsapp = reader?.GetValue(13)?.ToString(),
                            Email = reader?.GetValue(15)?.ToString(),
                            Naturalidade = "Rio de Janeiro",
                            NaturalidadeUF = "RJ"
                            //Ativo = true
                        });
                    }
                }
            }

            alunosDto.RemoveAt(0);

            //var user = _userManager.Users.FirstOrDefault(c => c.Email == userEmail);
            // or, if you have an async action, something like:


            //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            //var usuario = UserHttpContext.HttpContext.User.GetCurrentUserDetails();
            var unidadeId = _db.Unidades.Where(u => u.Sigla == "CGI").Select(u => u.Id).SingleOrDefault();
            List<Aluno> alunos = new List<Aluno>();

            //foreach (var item in alunosDto)
            //{
            //    item.CPF = item.CPF.Trim(new Char[] { ' ', '*', '.' })
            //}
            foreach (var item in alunosDto)
            {
                if(item.CPF != null)
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

                // var lead = _mapper.Map<Aluno>(item);
                /// lead.SetDateAndResponsavelInLead(user.Email + "/" + user.UserName);
                var alunoAdd = new Aluno(0, item.Nome, null, null, item.CPF, item.RG,
                    item.Nascimento, item.Naturalidade, item.NaturalidadeUF, item.Email, null, null,
                    item.TelWhatsapp, null, item.TelWhatsapp, item.CEP, item.Logradouro, item.Complemento,
                    item.Cidade, item.UF, item.Bairro, null, null, unidadeId);

                alunoAdd.AtivarAluno();
                alunoAdd.SetDataCadastro();

                alunos.Add(alunoAdd);
            }


           
            //String header = "* A Short String. *";
            //Console.WriteLine(header);
            //Console.WriteLine(header.Trim(new Char[] { ' ', '*', '.' }));

            _db.Alunos.AddRange(alunos);

            _db.SaveChanges();

            // return Ok();
        }


        public void ReadAndSaveExcelAlunosSJM()
        {
            //List<UserModel> users = new List<UserModel>();
            List<AlunoExcelDto> alunosDto = new List<AlunoExcelDto>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            //using (var stream = new MemoryStream())
            using (var stream = File.Open("ALUNOS ENF02.xlsx", FileMode.Open, FileAccess.Read))
            {
                //file.CopyTo(stream);
                stream.Position = 1;


                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        if (String.IsNullOrEmpty(reader?.GetValue(0)?.ToString())) break;
                        alunosDto.Add(new AlunoExcelDto()
                        {
                            Nome = reader?.GetValue(1)?.ToString(),
                            DataCadastro = Convert.ToDateTime(reader?.GetValue(2)?.ToString()),
                            RG = reader?.GetValue(4)?.ToString(),
                            CPF = reader?.GetValue(5)?.ToString(),
                            Nascimento = Convert.ToDateTime(reader?.GetValue(6)?.ToString()),
                            Logradouro = reader?.GetValue(7)?.ToString(),
                            Bairro = reader?.GetValue(8)?.ToString(),
                            Cidade = reader?.GetValue(9)?.ToString(),
                            UF = reader?.GetValue(10)?.ToString(),
                            CEP = reader?.GetValue(11)?.ToString(),
                            TelCelular = reader?.GetValue(12)?.ToString(),
                            TelWhatsapp = reader?.GetValue(13)?.ToString(),
                            Email = reader?.GetValue(15)?.ToString(),
                            Naturalidade = "Rio de Janeiro",
                            NaturalidadeUF = "RJ"
                            //Ativo = true
                        });
                    }
                }
            }

            alunosDto.RemoveAt(0);

            //var user = _userManager.Users.FirstOrDefault(c => c.Email == userEmail);
            // or, if you have an async action, something like:


            //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            //var usuario = UserHttpContext.HttpContext.User.GetCurrentUserDetails();
            var unidadeId = _db.Unidades.Where(u => u.Sigla == "SJM").Select(u => u.Id).SingleOrDefault();
            List<Aluno> alunos = new List<Aluno>();

            //foreach (var item in alunosDto)
            //{
            //    item.CPF = item.CPF.Trim(new Char[] { ' ', '*', '.' })
            //}
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

                // var lead = _mapper.Map<Aluno>(item);
                /// lead.SetDateAndResponsavelInLead(user.Email + "/" + user.UserName);
                var alunoAdd = new Aluno(0, item.Nome, null, null, item.CPF, item.RG,
                    item.Nascimento, item.Naturalidade, item.NaturalidadeUF, item.Email, null, null,
                    item.TelWhatsapp, null, item.TelWhatsapp, item.CEP, item.Logradouro, item.Complemento,
                    item.Cidade, item.UF, item.Bairro, null, null, unidadeId);

                alunoAdd.AtivarAluno();
                alunoAdd.SetDataCadastro();

                alunos.Add(alunoAdd);
            }



            //String header = "* A Short String. *";
            //Console.WriteLine(header);
            //Console.WriteLine(header.Trim(new Char[] { ' ', '*', '.' }));

            _db.Alunos.AddRange(alunos);

            _db.SaveChanges();

            // return Ok();
        }


    }


    public class AlunoExcelDto
    {

        public int Id { get; set; }
        public string Nome { get; set; }
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
        public string Observacoes { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public int UnidadeCadastrada { get; set; }
        // public virtual List<Responsavel> Responsaveis { get; set; }

    }
}
