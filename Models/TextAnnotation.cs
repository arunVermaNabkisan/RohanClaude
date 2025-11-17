namespace PdfEditorBlazor.Models
{
    public class TextAnnotation
    {
        public int PageNumber { get; set; }
        public string Text { get; set; } = string.Empty;
        public float X { get; set; }
        public float Y { get; set; }
        public string FontFamily { get; set; } = "Helvetica";
        public float FontSize { get; set; } = 12;
        public string Color { get; set; } = "#000000";
    }
}
