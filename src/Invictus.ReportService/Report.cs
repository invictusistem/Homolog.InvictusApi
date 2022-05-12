using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.ReportService
{
    //public interface IDownloadReport
    //{
    //    ClientRequestDto parametros { get; set; }
    //    Task<byte[]> ExportFile(IEnumerable<MediaPulseReturn> workOrders, string site);
    //}
    public class ExportExcelCDE //: IDownloadReport
    {
       // public ParametrosDTO parametros { get; set; }

        public async Task<byte[]> ExportFile(string site)
        {
            //  var todasWorkOrders = DTOPainelCDEExtensions.ToDTOPainelCDE(workOrders);
            int maximoItemsPorColuna = 15;

            byte[] content;

            string[] var1 = { "Recurso", "W.O.", "Data Início", "Data Fim", "Produto", "Descrição", "Demais Informações" }; // 7
            double[] var1width = { 25.8, 12, 18.15, 16.15, 25, 22, 32 };
            string[] var2 = { "Instruções Equipe", "Instruções Logística (CDE)" }; // bookin = Instruções Equipe e instrucoes: Instruções Logística CDE
            double[] var2width = { 39, 39 };
            var colunasIniciais = new List<string>();
            colunasIniciais.AddRange(var1);
            var colunas1Width = new List<double>();
            colunas1Width.AddRange(var1width);
            var colunasFinais = new List<string>();
            colunasFinais.AddRange(var2);
            var colunas2Width = new List<double>();
            colunas2Width.AddRange(var2width);

            // int qntColunasEquipe = CalcularQauntidadeDeColunas(todasWorkOrders.Select(n => n.equipe), maximoItemsPorColuna);
            // int qntColunasEquipamentos = CalcularQauntidadeDeColunas(todasWorkOrders.Select(n => n.equipamentos), maximoItemsPorColuna);
            // int totalColunas = 1 + colunasIniciais.Count() + qntColunasEquipe + qntColunasEquipamentos + colunasFinais.Count();// 12
            //  int totalColunasSemMargem = colunasIniciais.Count() + qntColunasEquipe + qntColunasEquipamentos + colunasFinais.Count();
            int posicaoInicialColuna1 = 1 + 1;
            int posicaoInicioColunaEquipe = 1 + colunasIniciais.Count() + 1; // 9
            // int posicaoInicioColunaEquipamentos = 1 + colunasIniciais.Count() + qntColunasEquipe + 1; //10
            // int posicaoInicioColunas2 = 1 + colunasIniciais.Count() + qntColunasEquipe + qntColunasEquipamentos + 1; //11

            var arrayColunasEquipes = new List<string>();
            var arrayColunasEquipamentos = new List<string>();

            // arrayColunasEquipes = NomeColunas(qntColunasEquipe, "Equipe");
            //arrayColunasEquipamentos = NomeColunas(qntColunasEquipamentos, "Equipamentos");

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet =
                workbook.Worksheets.Add("Eventos");

                //var imagePath = Path.Combine($"wwwroot\\logo_globo.png");
                //var image = worksheet.AddPicture(imagePath)
                //.MoveTo(worksheet.Cell(2, 2));

                //image.Name = "rede-globo-logo";
                //image.ScaleWidth(1.3);
                //image.ScaleHeight(1.3);

                worksheet.Row(1).Height = 3.5;
                worksheet.Row(2).Height = 16.5;
                worksheet.Row(3).Height = 5.5;

                worksheet.Column(1).Width = 0.2;
                worksheet.Column(1).Style.Fill.BackgroundColor = XLColor.White;

                for (int i = 0; i < colunasIniciais.Count(); i++)
                {
                    worksheet.Cell(5, (posicaoInicialColuna1 + i)).Value = colunasIniciais.ToArray()[i].ToString();
                    worksheet.Cell(5, (posicaoInicialColuna1 + i)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(5, (posicaoInicialColuna1 + i)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(5, (posicaoInicialColuna1 + i)).Style.Font.Bold = true;
                    worksheet.Cell(5, (posicaoInicialColuna1 + i)).Style.Font.FontSize = 14;

                    worksheet.Column((posicaoInicialColuna1 + i)).Width = colunas1Width.ToArray()[i];
                    worksheet.Column((posicaoInicialColuna1 + i)).Style.Fill.BackgroundColor = XLColor.White;

                    worksheet.Cell(5, i + 2).Style.Fill.BackgroundColor = XLColor.LightBlue;
                }

                for (int i = 0; i < arrayColunasEquipes.Count(); i++)
                {
                    worksheet.Cell(5, (posicaoInicioColunaEquipe + i)).Value = arrayColunasEquipes.ToArray()[i].ToString();
                    worksheet.Cell(5, (posicaoInicioColunaEquipe + i)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(5, (posicaoInicioColunaEquipe + i)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(5, (posicaoInicioColunaEquipe + i)).Style.Font.Bold = true;
                    worksheet.Cell(5, (posicaoInicioColunaEquipe + i)).Style.Font.FontSize = 14;

                    worksheet.Column((posicaoInicioColunaEquipe + i)).Width = 56;
                    worksheet.Column((posicaoInicioColunaEquipe + i)).Style.Fill.BackgroundColor = XLColor.White;

                    worksheet.Cell(5, (posicaoInicioColunaEquipe + i)).Style.Fill.BackgroundColor = XLColor.LightBlue;

                }

                for (int i = 0; i < arrayColunasEquipamentos.Count(); i++)
                {
                    //worksheet.Cell(5, (posicaoInicioColunaEquipamentos + i)).Value = arrayColunasEquipamentos.ToArray()[i].ToString();
                    //worksheet.Cell(5, (posicaoInicioColunaEquipamentos + i)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //worksheet.Cell(5, (posicaoInicioColunaEquipamentos + i)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //worksheet.Cell(5, (posicaoInicioColunaEquipamentos + i)).Style.Font.Bold = true;
                    //worksheet.Cell(5, (posicaoInicioColunaEquipamentos + i)).Style.Font.FontSize = 14;

                    //worksheet.Column((posicaoInicioColunaEquipamentos + i)).Width = 56;
                    //worksheet.Column((posicaoInicioColunaEquipamentos + i)).Style.Fill.BackgroundColor = XLColor.White;

                    //worksheet.Cell(5, (posicaoInicioColunaEquipamentos + i)).Style.Fill.BackgroundColor = XLColor.LightBlue;
                }

                for (int i = 0; i < colunasFinais.Count(); i++)
                {
                    //worksheet.Cell(5, (posicaoInicioColunas2 + i)).Value = colunasFinais.ToArray()[i].ToString();
                    //worksheet.Cell(5, (posicaoInicioColunas2 + i)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //worksheet.Cell(5, (posicaoInicioColunas2 + i)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    //worksheet.Cell(5, (posicaoInicioColunas2 + i)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //worksheet.Cell(5, (posicaoInicioColunas2 + i)).Style.Font.Bold = true;
                    //worksheet.Cell(5, (posicaoInicioColunas2 + i)).Style.Font.FontSize = 14;

                    //worksheet.Column((posicaoInicioColunas2 + i)).Width = colunas2Width.ToArray()[i];
                    //worksheet.Column((posicaoInicioColunas2 + i)).Style.Fill.BackgroundColor = XLColor.White;

                    //worksheet.Cell(5, (posicaoInicioColunas2 + i)).Style.Fill.BackgroundColor = XLColor.LightBlue;
                }

                //worksheet.Column(totalColunas + 1).Width = 70;
                //worksheet.Column(totalColunas + 1).Style.Fill.BackgroundColor = XLColor.White;

                var headerTitulo = worksheet.Range(4, 2, 4, 0).Merge();// ("B4:K4").Merge(); 
                headerTitulo.Cell(1, 1).Value = "CDE - Lista de recursos (" + site + "), dia " + DateTime.Now.ToString("dd/MM/yyyy");
                headerTitulo.Cell(1, 1).Style.Font.FontColor = XLColor.White;
                headerTitulo.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                headerTitulo.Style.Fill.BackgroundColor = XLColor.Blue;

                var headerTable = worksheet.Range(5, 2, 5, 0);  //("B5:K5
                headerTable.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                headerTable.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                headerTable.RangeUsed().SetAutoFilter();


                //for (int index = 1; index <= todasWorkOrders.Count(); index++)
                //{
                //    var range = worksheet.Range(index + 5, 2, index + 5, totalColunas);
                //    range.Style.Alignment.WrapText = true;
                //    range.Style.Alignment.Vertical =
                //    XLAlignmentVerticalValues.Center;

                //    worksheet.Cell(index + 5, 2).Value =
                //    todasWorkOrders.ToArray()[index - 1].recurso;
                //    worksheet.Cell(index + 5, 2).Style.Alignment.Horizontal =
                //    XLAlignmentHorizontalValues.Left;
                //    worksheet.Cell(index + 5, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                //    worksheet.Cell(index + 5, 3).Value =
                //    todasWorkOrders.ToArray()[index - 1].codigoWorkOrder;
                //    worksheet.Cell(index + 5, 3).Style.Alignment.Horizontal =
                //    XLAlignmentHorizontalValues.Center;
                //    worksheet.Cell(index + 5, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                //    worksheet.Cell(index + 5, 4).Value =
                //    todasWorkOrders.ToArray()[index - 1].dataInicio.ToString("dd/MM/yyyy HH:mm");
                //    worksheet.Cell(index + 5, 4).Style.Alignment.Horizontal =
                //    XLAlignmentHorizontalValues.Center;
                //    worksheet.Cell(index + 5, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                //    worksheet.Cell(index + 5, 5).Value =
                //    todasWorkOrders.ToArray()[index - 1].datafim.ToString("dd/MM/yyyy HH:mm");
                //    worksheet.Cell(index + 5, 5).Style.Alignment.Horizontal =
                //    XLAlignmentHorizontalValues.Center;
                //    worksheet.Cell(index + 5, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                //    worksheet.Cell(index + 5, 6).Value =
                //    todasWorkOrders.ToArray()[index - 1].produto;
                //    worksheet.Cell(index + 5, 6).Style.Alignment.Horizontal =
                //    XLAlignmentHorizontalValues.Center;
                //    worksheet.Cell(index + 5, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                //    worksheet.Cell(index + 5, 7).Value =
                //    todasWorkOrders.ToArray()[index - 1].descricao;
                //    worksheet.Cell(index + 5, 7).Style.Alignment.Horizontal =
                //    XLAlignmentHorizontalValues.Center;
                //    worksheet.Cell(index + 5, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                //    worksheet.Cell(index + 5, 8).Value =
                //    todasWorkOrders.ToArray()[index - 1].informacoes;
                //    worksheet.Cell(index + 5, 8).Style.Alignment.Horizontal =
                //    XLAlignmentHorizontalValues.Center;
                //    worksheet.Cell(index + 5, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                //    var equipeArray = new List<string>();

                //    if (todasWorkOrders.ToArray()[index - 1].equipe != null)
                //    {
                //        var array = todasWorkOrders.ToArray()[index - 1].equipe.Split("|");
                //        equipeArray.AddRange(array);
                //    }

                //    var indexDoElemento = 0;
                //    for (int i = 0; i < arrayColunasEquipes.Count(); i++)
                //    {
                //        int n = 0;
                //        while (n < maximoItemsPorColuna)
                //        {
                //            if (indexDoElemento < equipeArray.Count())
                //            {
                //                worksheet.Cell(index + 5, posicaoInicioColunaEquipe + i).RichText.AddText
                //                        (equipeArray[indexDoElemento].TrimEnd().TrimStart());
                //                worksheet.Cell(index + 5, posicaoInicioColunaEquipe + i).RichText.AddText(Environment.NewLine);
                //            }
                //            else
                //            {
                //                break;
                //            }
                //            indexDoElemento++;
                //            n++;
                //        }

                //        worksheet.Cell(index + 5, posicaoInicioColunaEquipe + i).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        n = 0;
                //    }

                //    var equipamentosArray = new List<string>();

                //    if (todasWorkOrders.ToArray()[index - 1].equipamentos != null)
                //    {
                //        var array = todasWorkOrders.ToArray()[index - 1].equipamentos.Split("|");
                //        equipamentosArray.AddRange(array);
                //    }

                //    indexDoElemento = 0;
                //    for (int i = 0; i < arrayColunasEquipamentos.Count(); i++)
                //    {
                //        int n = 0;
                //        while (n < maximoItemsPorColuna)
                //        {
                //            if (indexDoElemento < equipamentosArray.Count())
                //            {
                //                worksheet.Cell(index + 5, posicaoInicioColunaEquipamentos + i).RichText.AddText
                //                        (equipamentosArray[indexDoElemento].TrimEnd().TrimStart());
                //                worksheet.Cell(index + 5, posicaoInicioColunaEquipamentos + i).RichText.AddText(Environment.NewLine);
                //            }
                //            else
                //            {
                //                break;
                //            }
                //            indexDoElemento++;
                //            n++;
                //        }

                //        worksheet.Cell(index + 5, posicaoInicioColunaEquipamentos + i).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                //        n = 0;
                //    }

                //    worksheet.Cell(index + 5, posicaoInicioColunas2).Value =
                //    todasWorkOrders.ToArray()[index - 1].reserva;
                //    worksheet.Cell(index + 5, posicaoInicioColunas2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                //    worksheet.Cell(index + 5, posicaoInicioColunas2 + 1).Value =
                //    todasWorkOrders.ToArray()[index - 1].instrucoes;
                //    worksheet.Cell(index + 5, posicaoInicioColunas2 + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                //}

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
