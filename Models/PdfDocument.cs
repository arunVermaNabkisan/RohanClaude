namespace PdfEditorBlazor.Models
{
    public class PdfDocument
    {
        public string FileName { get; set; } = string.Empty;
        public byte[] FileContent { get; set; } = Array.Empty<byte>();
        public List<PdfPage> Pages { get; set; } = new List<PdfPage>();
        public DateTime UploadedDate { get; set; } = DateTime.Now;
    }

    public class PdfPage
    {
        public int PageNumber { get; set; }
        public string Text { get; set; } = string.Empty;
        public List<PdfTextEdit> Edits { get; set; } = new List<PdfTextEdit>();
    }

    public class PdfTextEdit
    {
        public int PageNumber { get; set; }
        public string OldText { get; set; } = string.Empty;
        public string NewText { get; set; } = string.Empty;
        public float X { get; set; }
        public float Y { get; set; }
        public string FontFamily { get; set; } = "Helvetica";
        public float FontSize { get; set; } = 12;
    }

    public class PdfEditRequest
    {
        public string FileName { get; set; } = string.Empty;
        public List<PdfTextEdit> Edits { get; set; } = new List<PdfTextEdit>();
    }
}
