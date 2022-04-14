using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TGS.Cartorio.Application.Relatorios.Base
{
    public abstract class BaseReport : PdfPageEventHelper
    {
        protected List<PdfPTable> _pdfPTable { get; set; }
        protected PdfPTable _pdfPTableHeader { get; set; }
        protected Document _document { get; set; }
        protected PdfPCell _pdfPCell { get; set; }        
        protected Font _fontStyle { get; set; }
        protected MemoryStream _memorySteam { get; set; }
        protected string[] _colunas { get; set; }
        protected string _titulo { get; set; }
        protected PdfWriter _pw { get; set; }
        protected long _IdSolicitacao { get; set; }
        
        public BaseReport(string titulo,
                          long idSolicitacao)
        {
            _IdSolicitacao = idSolicitacao;

            _document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(20f, 20f, 20f, 20f);
            
            
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _memorySteam = new MemoryStream();
            _pw = PdfWriter.GetInstance(_document, _memorySteam);
            _titulo = titulo;            
        }

        public byte[] GerarReport()
        {
            CriarColunasTabela();
            CriarLarguraColunas();

            _pw.PageEvent = this;

            _document.Open();

            ReportBoby();

            foreach (var table in _pdfPTable)
            {
                _document.Add(table);
                _document.Add(new Paragraph());
                _document.Add(new Paragraph());
                _document.Add(new Paragraph());

            }

            _document.Close();

            return _memorySteam.ToArray();
        }

        protected virtual PdfPTable CreatePdfPTable(int qtdeColunasTabela)
        {
            try
            {
                var pdfPTable = new PdfPTable(qtdeColunasTabela);
                pdfPTable.WidthPercentage = 100;
                pdfPTable.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPTable.HeaderRows = 2;
                return pdfPTable;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected abstract void ReportBoby();
        protected abstract void CriarColunasTabela();
        protected abstract void CriarLarguraColunas();
        protected virtual void CriarHeaderTabela(PdfPTable pdfPTable)
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1, BaseColor.WHITE);

            for (int i = 0; i < _colunas.Length; i++)
            {
                _pdfPCell = new PdfPCell(new Phrase(_colunas[i], _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = new BaseColor(2, 97, 70);
                pdfPTable.AddCell(_pdfPCell);
            }
            pdfPTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 0);
        }

        protected void PopularCell(PdfPTable pdfPTable, string dados)
        {
            _pdfPCell = new PdfPCell(new Phrase(dados, _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            pdfPTable.AddCell(_pdfPCell);
        }

        protected virtual void IncluirHeaderNoDocumento(PdfWriter writer)
        {
            PdfPTable tbHeader = new PdfPTable(2);
            tbHeader.TotalWidth = _document.PageSize.Width - _document.LeftMargin - _document.RightMargin;
            tbHeader.DefaultCell.Border = 0;

            //var pathLogoSIGPC = Path.Combine(_webHostEnvironment.WebRootPath, "images", "logo-sigpc.png");
            //var image = Image.GetInstance(pathLogoSIGPC);
            //var image = Image.GetInstance("");
            //image.ScalePercent(20f);

            //PdfPCell _cellImgLogo = new PdfPCell(image, false) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, PaddingTop = -3 };
            //tbHeader.AddCell(_cellImgLogo);

            var subTable = new PdfPTable(new float[] { 10, 100 });

            var tituloCell = new PdfPCell(new Phrase(_titulo))
            {
                Colspan = 2,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                PaddingTop = 20
            };
            subTable.AddCell(tituloCell);
            subTable.AddCell(new PdfPCell() { Border = 0 });

            var periodoCell = new PdfPCell()
            {
                Colspan = 2,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                PaddingTop = 2
            };
            subTable.AddCell(periodoCell);

            PdfPCell _cell = new PdfPCell(new Paragraph()) { Border = 0 };
            tbHeader.AddCell(_cell);
            tbHeader.AddCell(subTable);

            tbHeader.WriteSelectedRows(0, -1, 0, writer.PageSize.GetTop(30) + 20, writer.DirectContent);
        }

        protected virtual void IncluirFooterNoDocumento(PdfWriter writer)
        {
            PdfPTable tbFooter = new PdfPTable(2)
            {
                TotalWidth = _document.PageSize.Width - _document.LeftMargin - _document.RightMargin
            };
            tbFooter.DefaultCell.Border = 0;

            tbFooter.AddCell(new Paragraph());

            PdfPCell _cell = new PdfPCell(new Paragraph()) { Border = 0 };
            _cell = new PdfPCell(new Paragraph("")) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER };
            tbFooter.AddCell(_cell);

            _cell = new PdfPCell(new Paragraph("pagina" + ": " + writer.PageNumber))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                PaddingBottom = 10
            };
            tbFooter.AddCell(_cell);

            tbFooter.AddCell(new Paragraph());

            tbFooter.WriteSelectedRows(0, -1, _document.LeftMargin, writer.PageSize.GetBottom(40) + -1, writer.DirectContent);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            IncluirHeaderNoDocumento(writer);
            IncluirFooterNoDocumento(writer);
        }
    }
}
