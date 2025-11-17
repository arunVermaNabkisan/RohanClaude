# PDF Editor Blazor Application

A Blazor Server application for editing PDF documents with MVC architecture.

## Features

- **Upload PDF files** - Support for PDF files up to 10MB
- **View PDF content** - Preview text content of each page
- **Add text annotations** - Add custom text to any page with configurable:
  - Position (X, Y coordinates)
  - Font size
  - Text content
- **Download edited PDFs** - Download your edited PDF with all annotations applied
- **Modern UI** - Clean, responsive interface built with Bootstrap 5

## Technology Stack

- **Framework**: .NET 8.0
- **UI**: Blazor Server
- **Architecture**: MVC (Model-View-Controller)
- **PDF Library**: iText7
- **Styling**: Bootstrap 5 + Custom CSS

## Project Structure

```
PdfEditorBlazor/
├── Models/              # Data models (PdfDocument, TextAnnotation, etc.)
├── Controllers/         # Business logic controllers (PdfController)
├── Services/           # PDF processing services (PdfService)
├── Pages/              # Blazor pages and components
│   ├── Index.razor     # Main PDF editor page
│   ├── Shared/         # Shared components (Layout, NavMenu)
│   ├── _Host.cshtml    # Host page
│   └── _Imports.razor  # Global imports
├── wwwroot/            # Static files
│   ├── css/            # Stylesheets
│   └── js/             # JavaScript files
├── Program.cs          # Application entry point
└── PdfEditorBlazor.csproj  # Project file
```

## How to Run in Visual Studio

### Prerequisites

- Visual Studio 2022 (or later) with ASP.NET and web development workload
- .NET 8.0 SDK or later

### Steps

1. **Open the Solution**
   - Open Visual Studio
   - Go to `File` > `Open` > `Project/Solution`
   - Navigate to the project folder and open `PdfEditorBlazor.sln`

2. **Restore NuGet Packages**
   - Visual Studio will automatically restore packages
   - Or manually: Right-click on the solution > `Restore NuGet Packages`

3. **Set as Startup Project**
   - Right-click on `PdfEditorBlazor` project in Solution Explorer
   - Select `Set as Startup Project`

4. **Run the Application**
   - Press `F5` or click the green `Start` button
   - The application will launch in your default browser
   - Default URL: `https://localhost:5001` or `http://localhost:5000`

## How to Use

1. **Upload a PDF**
   - Click the file input on the home page
   - Select a PDF file from your computer (max 10MB)
   - Wait for the upload to complete

2. **View PDF Content**
   - Once uploaded, you'll see the document information
   - Use the page selector to view content from different pages
   - The text content of each page will be displayed

3. **Add Text Annotations**
   - Fill in the "Add Text to PDF" form:
     - **Page Number**: Select which page to add text to
     - **Text**: Enter the text you want to add
     - **X Position**: Horizontal position (pixels from left)
     - **Y Position**: Vertical position (pixels from bottom)
     - **Font Size**: Size of the text (6-72)
   - Click "Add Text" to apply the annotation
   - Added annotations will appear in the list below

4. **Download Edited PDF**
   - Click "Download Edited PDF" button
   - The PDF with all annotations will be downloaded
   - Filename format: `[original-name]_edited.pdf`

5. **Upload New PDF**
   - Click "Upload New PDF" to clear current document
   - Start the process again with a new file

## MVC Architecture

This application follows the MVC pattern:

- **Models** (`Models/`): Data structures for PDF documents, pages, and annotations
- **Views** (`Pages/`): Blazor components for UI rendering
- **Controllers** (`Controllers/`): Business logic for PDF operations and coordination between services and views
- **Services** (`Services/`): Low-level PDF manipulation using iText7

## Key Components

### Models
- `PdfDocument`: Represents an uploaded PDF with metadata and pages
- `PdfPage`: Represents a single page with extracted text
- `TextAnnotation`: Represents a text annotation to be added
- `PdfTextEdit`: Represents a text edit operation

### Controllers
- `PdfController`: Manages PDF state and coordinates operations

### Services
- `PdfService`: Handles PDF reading, text extraction, and annotation using iText7

### Pages
- `Index.razor`: Main editor interface with upload, edit, and download features
- `MainLayout.razor`: Application layout structure
- `NavMenu.razor`: Navigation menu component

## Dependencies

The project uses the following NuGet packages:

- `itext7` (v8.0.2) - Core PDF processing library
- `itext7.pdfhtml` (v5.0.2) - HTML to PDF conversion support

## Notes

- PDF text extraction works best with text-based PDFs (not scanned images)
- Position coordinates for text annotations:
  - X: 0 is left edge, increases to the right
  - Y: 0 is bottom edge, increases upward (PDF coordinate system)
  - Typical page is about 595 x 842 points for A4
- File size limit is 10MB for uploads
- Only PDF files (application/pdf) are accepted

## Troubleshooting

**Issue**: Application won't start
- Ensure .NET 8.0 SDK is installed
- Restore NuGet packages
- Clean and rebuild solution

**Issue**: PDF upload fails
- Check file is a valid PDF
- Ensure file is under 10MB
- Verify file is not password-protected

**Issue**: Text appears in wrong location
- Remember PDF coordinates start from bottom-left
- Adjust Y coordinate (higher values = higher on page)
- Typical text position: X=50, Y=700 for near top of page

## License

This project is for educational and demonstration purposes.
