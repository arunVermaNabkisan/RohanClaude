using PdfEditorBlazor.Models;
using PdfEditorBlazor.Services;

namespace PdfEditorBlazor.Controllers
{
    public class PdfController
    {
        private readonly PdfService _pdfService;
        private PdfDocument? _currentDocument;

        public PdfController(PdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public PdfDocument? CurrentDocument => _currentDocument;

        public async Task<bool> UploadPdfAsync(byte[] fileContent, string fileName)
        {
            try
            {
                _currentDocument = await _pdfService.LoadPdfAsync(fileContent, fileName);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading PDF: {ex.Message}");
                return false;
            }
        }

        public async Task<byte[]?> AddTextAnnotationsAsync(List<TextAnnotation> annotations)
        {
            if (_currentDocument == null || _currentDocument.FileContent == null)
                return null;

            try
            {
                var editedPdf = await _pdfService.AddTextToPdfAsync(_currentDocument.FileContent, annotations);
                _currentDocument.FileContent = editedPdf;
                return editedPdf;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding text annotations: {ex.Message}");
                return null;
            }
        }

        public async Task<byte[]?> ApplyEditsAsync(List<PdfTextEdit> edits)
        {
            if (_currentDocument == null || _currentDocument.FileContent == null)
                return null;

            try
            {
                var editedPdf = await _pdfService.EditPdfTextAsync(_currentDocument.FileContent, edits);
                _currentDocument.FileContent = editedPdf;
                return editedPdf;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error applying edits: {ex.Message}");
                return null;
            }
        }

        public byte[]? GetCurrentPdfContent()
        {
            return _currentDocument?.FileContent;
        }

        public string GetCurrentFileName()
        {
            return _currentDocument?.FileName ?? "document.pdf";
        }

        public void ClearDocument()
        {
            _currentDocument = null;
        }

        public int GetPageCount()
        {
            if (_currentDocument == null || _currentDocument.FileContent == null)
                return 0;

            return _pdfService.GetPageCount(_currentDocument.FileContent);
        }
    }
}
