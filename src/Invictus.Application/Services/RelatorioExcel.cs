using ClosedXML.Excel;
using Invictus.Application.Dtos;
using Invictus.Application.Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Application.Services
{
    public class RelatorioExcel : IRelatorioExcel
    {
        public async Task<byte[]> ExportLivroMatricula(IEnumerable<AlunoDto> alunos)
        {


            byte[] content;

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Matriculas");

                var headerTitulo = worksheet.Range(1, 1, 1, 11);
                headerTitulo.Style.Font.FontColor = XLColor.Black;
                headerTitulo.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                headerTitulo.Style.Fill.BackgroundColor = XLColor.LightGreen;
                headerTitulo.Style.Font.FontSize = 14;
                worksheet.Row(1).Height = 15;

                worksheet.Column(1).Width = 25;
                worksheet.Cell(1, 1).Value = "Nome";
                worksheet.Column(2).Width = 22;
                worksheet.Cell(1, 2).Value = "Matricula";
                worksheet.Column(3).Width = 20;
                worksheet.Cell(1, 3).Value = "Nome Social";
                worksheet.Column(4).Width = 20;
                worksheet.Cell(1, 4).Value = "CPF";
                worksheet.Column(5).Width = 18;
                worksheet.Cell(1, 5).Value = "RG";
                worksheet.Column(6).Width = 25;
                worksheet.Cell(1, 6).Value = "Nascimento";
                worksheet.Column(7).Width = 18;
                worksheet.Cell(1, 7).Value = "Naturalidade";
                worksheet.Column(8).Width = 18;
                worksheet.Cell(1, 8).Value = "Naturalidade UF";
                worksheet.Column(9).Width = 23;
                worksheet.Cell(1, 9).Value = "Email";
                worksheet.Column(10).Width = 18;
                worksheet.Cell(1, 10).Value = "Ativo";
                worksheet.Column(11).Width = 18;
                worksheet.Cell(1, 11).Value = "Turma";

                for (int i = 0; i < alunos.Count(); i++)
                {
                    worksheet.Cell(2 + i, 1).Value = alunos.ToArray()[i]?.nome;
                    worksheet.Cell(2 + i, 2).Value = alunos.ToArray()[i]?.numeroMatricula;
                    worksheet.Cell(2 + i, 3).Value = alunos.ToArray()[i]?.nomeSocial;
                    worksheet.Cell(2 + i, 4).Value = alunos.ToArray()[i]?.cpf;
                    worksheet.Cell(2 + i, 5).Value = alunos.ToArray()[i]?.rg;
                    worksheet.Cell(2 + i, 6).Value = alunos.ToArray()[i]?.nascimento.ToString("dd/MM/yyyy");
                    worksheet.Cell(2 + i, 7).Value = alunos.ToArray()[i]?.naturalidade;
                    worksheet.Cell(2 + i, 8).Value = alunos.ToArray()[i]?.naturalidadeUF;
                    worksheet.Cell(2 + i, 9).Value = alunos.ToArray()[i]?.email;
                    worksheet.Cell(2 + i, 10).Value = alunos.ToArray()[i]?.ativo.ToString();
                    worksheet.Cell(2 + i, 11).Value = alunos.ToArray()[i]?.observacoes;
                }

                await using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    content = stream.ToArray();
                }

                return content;
            }
        }

    }
}

