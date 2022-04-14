using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using TGS.Cartorio.Application.DTO.Relatorios;
using TGS.Cartorio.Application.Relatorios.Base;

namespace TGS.Cartorio.Application.Relatorios
{
    public class PDFParaCartorioReport : BaseReport
    {
        ICollection<RelatorioPDFEnvioParaCartorioDto> _relatorio { get; set; }
        public PDFParaCartorioReport(
            ICollection<RelatorioPDFEnvioParaCartorioDto> relatorioPDFEnvioParaCartorioDto                                
        ) : base("Relatório PDF para envio a Cartório", 123) {
            _relatorio = relatorioPDFEnvioParaCartorioDto;
        }

        protected override void CriarHeaderTabela(PdfPTable pdfPTable)
        {
            //_fontStyle = FontFactory.GetFont("Tahoma", 8f, 1, BaseColor.WHITE);

            //for (int i = 0; i < _colunas.Length; i++)
            //{
            //    _pdfPCell = new PdfPCell(new Phrase(_colunas[i], _fontStyle));
            //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //    _pdfPCell.BackgroundColor = new BaseColor(2, 97, 70);
            //    _pdfPTable.AddCell(_pdfPCell);
            //}
            //_pdfPTable.CompleteRow();

            //_fontStyle = FontFactory.GetFont("Tahoma", 8f, 0);
        }

        protected override void CriarColunasTabela()
        {
            //string[] Colunas = new string[2];

            //Colunas[0] = "IdSolicitacao";
            //Colunas[1] = "IdProcuracaoParte";
            ////Colunas[2] = _localizadorResources.GetLocalizedHtmlString("unidadeAtendimento");
            ////Colunas[3] = _localizadorResources.GetLocalizedHtmlString("TipoProcesso");
            ////Colunas[4] = _localizadorResources.GetLocalizedHtmlString("Status");
            ////Colunas[5] = _localizadorResources.GetLocalizedHtmlString("Nome/Sobrenome");
            ////Colunas[6] = _localizadorResources.GetLocalizedHtmlString("NumeroDocumento/TipoDocumento");
            ////Colunas[7] = _localizadorResources.GetLocalizedHtmlString("IdPagamento");

            //_colunas = Colunas;
        }

        protected override void CriarLarguraColunas()
        {
            //_pdfPTable.SetWidths(new float[] { 1f, 1f });
        }

        protected override void ReportBoby()
        {
            foreach (var item in _relatorio)
            {
                //outorgantes
                //_fontStyle = FontFactory.GetFont("Tahoma", 8f, 1, BaseColor.WHITE);
                //_pdfPCell = new PdfPCell(new Phrase("Dados Outorgantes:", _fontStyle));
                //_pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //_pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //_pdfPCell.BackgroundColor = new BaseColor(2, 97, 70);
                //_pdfPCell.Colspan = 2;
                ////_pdfPTable.AddCell(_pdfPCell);
                //_fontStyle = FontFactory.GetFont("Tahoma", 8f, 0);
                //PopularCell("IdSolicitacao");
                //PopularCell("IdProcuracaoParte");
                ////_pdfPTable.CompleteRow();                
                //PopularCell(item.outorgantes.IdSolicitacao.ToString());
                //PopularCell(item.outorgantes.IdProcuracaoParte.ToString());      
                ////_pdfPTable.CompleteRow();

                //PopularCell("Documento");
                //PopularCell("Nome");
                ////_pdfPTable.CompleteRow();
                //PopularCell(item.outorgantes.Documento.ToString());
                //PopularCell(item.outorgantes.Nome.ToString());
                ////_pdfPTable.CompleteRow();

                //PopularCell("Email");
                ////_pdfPTable.CompleteRow();
                //PopularCell(item.outorgantes.Email.ToString());                
                //_pdfPTable.CompleteRow();

                //_pdfPCell = new PdfPCell(new Phrase("\n\n"));                
                //_pdfPCell.BackgroundColor = new BaseColor(2, 97, 70);
                //_pdfPCell.Colspan = 2;
                //_pdfPTable.CompleteRow();

                ////outogados
                //_fontStyle = FontFactory.GetFont("Tahoma", 8f, 1, BaseColor.WHITE);
                //_pdfPCell = new PdfPCell(new Phrase("Dados Outorgados:", _fontStyle));
                //_pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //_pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //_pdfPCell.BackgroundColor = new BaseColor(2, 97, 70);
                //_pdfPCell.Colspan = 2;
                //_pdfPTable.AddCell(_pdfPCell);
                //_fontStyle = FontFactory.GetFont("Tahoma", 8f, 0);
                //PopularCell("IdSolicitacao");
                //PopularCell("IdProcuracaoParte");
                //_pdfPTable.CompleteRow();
                //PopularCell(item.outorgados.IdSolicitacao.ToString());
                //PopularCell(item.outorgados.IdProcuracaoParte.ToString());
                //_pdfPTable.CompleteRow();

                //PopularCell("Documento");
                //PopularCell("Nome");
                //_pdfPTable.CompleteRow();
                //PopularCell(item.outorgados.Documento.ToString());
                //PopularCell(item.outorgados.Nome.ToString());
                //_pdfPTable.CompleteRow();

                //PopularCell("Email");
                //_pdfPTable.CompleteRow();
                //PopularCell(item.outorgados.Email.ToString());
                //_pdfPTable.CompleteRow();

                ////matrimonios requerente
                //_fontStyle = FontFactory.GetFont("Tahoma", 8f, 1, BaseColor.WHITE);
                //_pdfPCell = new PdfPCell(new Phrase("Dados Matrimonios:", _fontStyle));
                //_pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //_pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //_pdfPCell.BackgroundColor = new BaseColor(2, 97, 70);
                //_pdfPCell.Colspan = 2;
                //_pdfPTable.AddCell(_pdfPCell);
                //_fontStyle = FontFactory.GetFont("Tahoma", 8f, 0);
                //PopularCell("Nome");
                //PopularCell("Documento");
                //_pdfPTable.CompleteRow();
                //PopularCell(item.matrimonio.DadosRequerente.Requerente.Nome.ToString());
                //PopularCell(item.matrimonio.DadosRequerente.Requerente.Documento.ToString());
                //_pdfPTable.CompleteRow();

                //PopularCell("DataNascimento");
                //_pdfPTable.CompleteRow();
                //PopularCell(item.matrimonio.DadosRequerente.Requerente.DataNascimento.ToString());                
                //_pdfPTable.CompleteRow();

                //PopularCell("Nome");
                //PopularCell("Documento");
                //_pdfPTable.CompleteRow();
                //PopularCell(item.matrimonio.DadosRequerente.MaeRequerente.Nome.ToString());
                //PopularCell(item.matrimonio.DadosRequerente.MaeRequerente.Documento.ToString());
                //_pdfPTable.CompleteRow();

                //PopularCell("DataNascimento");
                //PopularCell("Situacao");
                //_pdfPTable.CompleteRow();
                //PopularCell(item.matrimonio.DadosRequerente.MaeRequerente.DataNascimento.ToString());
                //PopularCell(item.matrimonio.DadosRequerente.MaeRequerente.Situacao.ToString());
                //_pdfPTable.CompleteRow();

                //PopularCell("Nome");
                //PopularCell("Documento");
                //_pdfPTable.CompleteRow();
                //PopularCell(item.matrimonio.DadosRequerente.PaiRequerente.Nome.ToString());
                //PopularCell(item.matrimonio.DadosRequerente.PaiRequerente.Documento.ToString());
                //_pdfPTable.CompleteRow();

                //PopularCell("DataNascimento");
                //PopularCell("Situacao");
                //_pdfPTable.CompleteRow();
                //PopularCell(item.matrimonio.DadosRequerente.PaiRequerente.DataNascimento.ToString());
                //PopularCell(item.matrimonio.DadosRequerente.PaiRequerente.Situacao.ToString());
                //_pdfPTable.CompleteRow();

                ////matrimonio noivo(a)
                //PopularCell("Nome");
                //PopularCell("Documento");
                //_pdfPTable.CompleteRow();
                //PopularCell(item.matrimonio.DadosNoivos.Noivos.Nome.ToString());
                //PopularCell(item.matrimonio.DadosNoivos.Noivos.Documento.ToString());
                //_pdfPTable.CompleteRow();

                //PopularCell("DataNascimento");                
                //_pdfPTable.CompleteRow();
                //PopularCell(item.matrimonio.DadosNoivos.Noivos.DataNascimento.ToString());
                //_pdfPTable.CompleteRow();

                //PopularCell("Nome");
                //PopularCell("Documento");
                //_pdfPTable.CompleteRow();
                //PopularCell(item.matrimonio.DadosNoivos.MaeNoivos.Nome.ToString());
                //PopularCell(item.matrimonio.DadosNoivos.MaeNoivos.Documento.ToString());
                //_pdfPTable.CompleteRow();

                //PopularCell("DataNascimento");
                //PopularCell("Situacao");
                //_pdfPTable.CompleteRow();
                //PopularCell(item.matrimonio.DadosNoivos.MaeNoivos.DataNascimento.ToString());
                //PopularCell(item.matrimonio.DadosNoivos.MaeNoivos.Situacao.ToString());
                //_pdfPTable.CompleteRow();

                //PopularCell("Nome");
                //PopularCell("Documento");
                //_pdfPTable.CompleteRow();
                //PopularCell(item.matrimonio.DadosNoivos.PaiNoivos.Nome.ToString());
                //PopularCell(item.matrimonio.DadosNoivos.PaiNoivos.Documento.ToString());
                //_pdfPTable.CompleteRow();

                //PopularCell("DataNascimento");
                //PopularCell("Situacao");
                //_pdfPTable.CompleteRow();
                //PopularCell(item.matrimonio.DadosNoivos.PaiNoivos.DataNascimento.ToString());
                //PopularCell(item.matrimonio.DadosNoivos.PaiNoivos.Situacao.ToString());
                //_pdfPTable.CompleteRow();
            }            
        }
    }
}
