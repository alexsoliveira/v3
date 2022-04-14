using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.Relatorios.Extensions;

namespace TGS.Cartorio.Application.Relatorios.Base
{
    public abstract class BasePdfReport : PdfPageEventHelper
    {
        private Document _document { get; set; }
        private MemoryStream _memorySteam { get; set; }
        private string _titulo { get; set; }
        private PdfWriter _pw { get; set; }
        protected long _idSolicitacao { get; set; }
        protected string _nomeSolicitante { get; set; }
        protected string _documento { get; set; }


        public BasePdfReport(string titulo)
        {
            _titulo = titulo;
        }

        protected void SetData(long idSolicitacao, string nomeSolicitante, string documento)
        {
            try
            {
                _idSolicitacao = idSolicitacao;
                _nomeSolicitante = nomeSolicitante;
                _documento = documento;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public byte[] GerarReport(params byte[][] pdfFiles)
        {
            try
            {
                CreateDocument();

                _pw.PageEvent = this;

                _document.Open();

                CreateHeader(_nomeSolicitante, _documento);

                CreateBody();

                //AddOthersPdfToDocument(pdfFiles);

                _document.Close();

                return _memorySteam.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CreateHeader(string nomeSolicitante, string documento)
        {
            try
            {
                var pdfHeader = CreatePdfPTable(2);

                pdfHeader.AddTitle(_titulo,
                                   corFont: BaseColor.WHITE,
                                   backgroundColor: BaseColor.GRAY,
                                   borderColor: BaseColor.DARK_GRAY);

                pdfHeader.NewCell($"Solicitação Nº {_idSolicitacao}", corFont: BaseColor.BLACK, borderless: true);
                pdfHeader.NewCell($"Expedido em {DateTime.Now.ToString("dd/MM/yyyy")}", corFont: BaseColor.BLACK, borderless: true, textAlignRight: true);
                //Quando não utilizar COLSPAN, quando atingir o número de colunas, usar o CompleteRow
                pdfHeader.CompleteRow();

                pdfHeader.AddTitle("solicitante",
                                   corFont: BaseColor.WHITE,
                                   backgroundColor: CartorioColor.GREEN_CARTORIO,
                                   borderColor: BaseColor.DARK_GRAY);

                pdfHeader.NewCell(nomeSolicitante, corFont: BaseColor.BLACK, borderless: true, colspan: 2);

                pdfHeader.NewCell($"Documento {documento}", corFont: BaseColor.BLACK, borderless: true, colspan: 2);

                AddPdfPTableToDocument(pdfHeader);
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected abstract void CreateBody();

        private void CreateDocument()
        {
            _document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(20f, 20f, 20f, 20f);

            _memorySteam = new MemoryStream();
            _pw = PdfWriter.GetInstance(_document, _memorySteam);
        }

        protected PdfPTable CreatePdfPTable(int numColums)
        {
            try
            {
                var pdfTable = new PdfPTable(numColums);
                pdfTable.WidthPercentage = 100;
                return pdfTable;
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void AddPdfPTableToDocument(PdfPTable table)
        {
            try
            {
                if (_document.IsOpen())
                {
                    _document.Add(table);
                    return;
                }

                throw new Exception("Documento PDF de Solicitação para Cartório não foi construído corretamente!");
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected virtual byte[] CreateZipFileOfManyPdf(Dictionary<string, byte[]> dicPdfFiles)
        {
            try
            {
                MemoryStream outputMemStream = new MemoryStream();
                ZipOutputStream zipStream = new ZipOutputStream(outputMemStream);

                zipStream.SetLevel(3); //0-9, 9 being the highest level of compression
                var i = 1;
                // loops through the PDFs I need to create
                foreach (var pdfValuePair in dicPdfFiles)
                {
                    if (pdfValuePair.Value == null || pdfValuePair.Value.Length == 0)
                        continue;

                    var newEntry = new ZipEntry($"{pdfValuePair.Key}.pdf");
                    newEntry.DateTime = DateTime.Now;

                    zipStream.PutNextEntry(newEntry);

                    MemoryStream inStream = new MemoryStream(pdfValuePair.Value);
                    StreamUtils.Copy(inStream, zipStream, new byte[4096]);
                    inStream.Close();
                    zipStream.CloseEntry();
                }

                zipStream.IsStreamOwner = false;
                zipStream.Close();

                outputMemStream.Position = 0;

                return outputMemStream.ToArray();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
