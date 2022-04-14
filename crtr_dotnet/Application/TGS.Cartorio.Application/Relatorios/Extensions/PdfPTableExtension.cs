using iTextSharp.text;
using iTextSharp.text.pdf;
using System;

namespace TGS.Cartorio.Application.Relatorios.Extensions
{
    public static class PdfPTableExtension
    {
        public static void AddTitle(this PdfPTable table,
            string title,
            BaseColor corFont = null,
            int? rowspan = null,
            BaseColor backgroundColor = null,
            BaseColor borderColor = null,
            float minHeight = 25)
        {
            try
            {
                var phrase = CreatePhrase(title.ToUpper(), null, 15, "Bold", corFont);
                var pdfCell = CreateCell(phrase, table.NumberOfColumns, rowspan, null, true, true, null, backgroundColor, borderColor, minHeight);
                
                table.AddSpace();
                table.AddCell(pdfCell);
                table.AddSpace();
                
                table.CompleteRow();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void AddSubTitle(this PdfPTable table,
            string title,
            BaseColor corFont = null,
            int? rowspan = null,
            float minHeight = 25)
        {
            try
            {
                var phrase = CreatePhrase(title.ToUpper(), null, 13, "Bold", corFont);
                var pdfCell = CreateCell(phrase, table.NumberOfColumns, rowspan, null, null, true, null, BaseColor.WHITE, null, minHeight);

                table.AddSpace();
                table.AddCell(pdfCell);
                table.AddSpace();

                table.CompleteRow();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void NewCell(this PdfPTable table,
            string texto,
            string strFont = null,
            float? sizeFont = null,
            BaseColor corFont = null,
            int? colspan = null,
            int? rowspan = null,
            bool? textAlignRight = null,
            bool? textAlignCenter = null,
            bool? borderless = null,
            float? borderWidth = null,
            BaseColor backgroundColor = null,
            BaseColor borderColor = null,
            float? minHeight = null)
        {
            try
            {
                var phrase = CreatePhrase(texto, strFont, sizeFont, "Normal", corFont);
                var pdfCell = CreateCell(phrase, colspan, rowspan, textAlignRight, textAlignCenter, borderless, borderWidth, backgroundColor, borderColor, minHeight);
                table.AddCell(pdfCell);

                if (colspan != null && colspan.HasValue && colspan.Value == table.NumberOfColumns)
                    table.CompleteRow();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void AddSpace(this PdfPTable table, int? rowspan = null)
        {
            try
            {
                table.AddCell(CreateCell(new Paragraph(" "), table.NumberOfColumns, rowspan, borderless: true));
                table.CompleteRow();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static Phrase CreatePhrase(string texto,
                                          string strFont = null,
                                          float? sizeFont = null,
                                          string styleFont = "Normal",
                                          BaseColor corFont = null)
        {
            try
            {
                var prhase = new Phrase(texto);

                if (!string.IsNullOrEmpty(strFont))
                {
                    var font = FontFactory.GetFont(strFont);
                    prhase.Font = font;
                }

                prhase.Font.SetStyle(styleFont);

                if (sizeFont.HasValue)
                    prhase.Font.Size = sizeFont.Value;
                else
                    prhase.Font.Size = 10;

                if (corFont != null)
                    prhase.Font.Color = corFont;

                return prhase;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static PdfPCell CreateCell(Phrase prhase = null,
                                          int? colspan = null,
                                          int? rowspan = null,
                                          bool? textAlignRight = null,
                                          bool? textAlignCenter = null,
                                          bool? borderless = null,
                                          float? borderWidth = null,
                                          BaseColor backgroundColor = null,
                                          BaseColor borderColor = null,
                                          float? minHeight = null)
        {
            try
            {
                PdfPCell pdfCell = null;
                if (prhase == null)
                    pdfCell = new PdfPCell();
                else
                    pdfCell = new PdfPCell(prhase);

                if (backgroundColor != null)
                    pdfCell.BackgroundColor = backgroundColor;

                if (borderColor != null)
                    pdfCell.BorderColor = borderColor;

                if (borderWidth != null && borderWidth.HasValue)
                    pdfCell.BorderWidth = borderWidth.Value;

                if (colspan.HasValue)
                    pdfCell.Colspan = colspan.Value;

                if (rowspan.HasValue)
                    pdfCell.Rowspan = rowspan.Value;

                if (textAlignRight == null && textAlignCenter == null)
                    pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                if (textAlignRight != null && textAlignRight.HasValue && textAlignRight.Value)
                    pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                if (textAlignCenter != null && textAlignCenter.HasValue && textAlignCenter.Value)
                    pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;

                pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                if (borderless != null && borderless.HasValue && borderless.Value)
                    pdfCell.BorderWidth = 0;

                if (minHeight != null && minHeight.HasValue)
                    pdfCell.MinimumHeight = minHeight.Value;

                return pdfCell;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
