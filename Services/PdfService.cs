using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Font;
using PdfEditorBlazor.Models;

namespace PdfEditorBlazor.Services
{
    public class PdfService
    {
        public async Task<PdfDocument> LoadPdfAsync(byte[] fileContent, string fileName)
        {
            var pdfDocument = new PdfDocument
            {
                FileName = fileName,
                FileContent = fileContent,
                UploadedDate = DateTime.Now
            };

            try
            {
                using (var ms = new MemoryStream(fileContent))
                using (var pdfReader = new PdfReader(ms))
                using (var pdf = new iText.Kernel.Pdf.PdfDocument(pdfReader))
                {
                    for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
                    {
                        var page = pdf.GetPage(i);
                        var strategy = new SimpleTextExtractionStrategy();
                        var text = PdfTextExtractor.GetTextFromPage(page, strategy);

                        pdfDocument.Pages.Add(new PdfPage
                        {
                            PageNumber = i,
                            Text = text
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading PDF: {ex.Message}", ex);
            }

            return pdfDocument;
        }

        public async Task<byte[]> AddTextToPdfAsync(byte[] originalPdf, List<TextAnnotation> annotations)
        {
            using (var inputStream = new MemoryStream(originalPdf))
            using (var outputStream = new MemoryStream())
            {
                using (var pdfReader = new PdfReader(inputStream))
                using (var pdfWriter = new PdfWriter(outputStream))
                using (var pdf = new iText.Kernel.Pdf.PdfDocument(pdfReader, pdfWriter))
                {
                    foreach (var annotation in annotations)
                    {
                        if (annotation.PageNumber > 0 && annotation.PageNumber <= pdf.GetNumberOfPages())
                        {
                            var page = pdf.GetPage(annotation.PageNumber);
                            var canvas = new PdfCanvas(page);

                            canvas.BeginText();
                            canvas.SetFontAndSize(PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), annotation.FontSize);
                            canvas.MoveText(annotation.X, annotation.Y);
                            canvas.ShowText(annotation.Text);
                            canvas.EndText();
                        }
                    }
                }

                return outputStream.ToArray();
            }
        }

        public async Task<byte[]> EditPdfTextAsync(byte[] originalPdf, List<PdfTextEdit> edits)
        {
            // For text replacement, we'll add new text on top
            // Note: True text replacement in PDFs is complex and often requires complete page reconstruction
            var annotations = edits.Select(edit => new TextAnnotation
            {
                PageNumber = edit.PageNumber,
                Text = edit.NewText,
                X = edit.X,
                Y = edit.Y,
                FontFamily = edit.FontFamily,
                FontSize = edit.FontSize
            }).ToList();

            return await AddTextToPdfAsync(originalPdf, annotations);
        }

        public async Task<byte[]> CreateBlankPdfAsync(int pageCount = 1)
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new PdfWriter(ms))
                using (var pdf = new iText.Kernel.Pdf.PdfDocument(writer))
                using (var document = new Document(pdf))
                {
                    for (int i = 0; i < pageCount; i++)
                    {
                        pdf.AddNewPage();
                        if (i == 0)
                        {
                            document.Add(new Paragraph("Blank PDF Document"));
                        }
                    }
                }

                return ms.ToArray();
            }
        }

        public int GetPageCount(byte[] pdfContent)
        {
            using (var ms = new MemoryStream(pdfContent))
            using (var pdfReader = new PdfReader(ms))
            using (var pdf = new iText.Kernel.Pdf.PdfDocument(pdfReader))
            {
                return pdf.GetNumberOfPages();
            }
        }
    }
}
