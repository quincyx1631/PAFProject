using Krypton.Toolkit;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
public class PdfExporter
{
    public static void ExportToPdf(KryptonDataGridView dataGridView, string filePath, string branchName,
    string week, string date, string weeklyBudget, string proposedBudget, string shortOver)
    {
        try
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(branchName) || string.IsNullOrWhiteSpace(week))
            {
                throw new ArgumentException("Branch name and week are required.");
            }

            if (dataGridView == null || dataGridView.Rows.Count == 0)
            {
                throw new ArgumentException("DataGridView cannot be empty.");
            }

            // Set license type to Community (free for commercial use up to a certain revenue threshold)
            QuestPDF.Settings.License = LicenseType.Community;

            // Create a document model
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    // Use landscape orientation
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(50);

                    // Content layout
                    page.Content()
                    .Column(column =>
                    {
                        // Add header
                        column.Item().Row(row =>
                        {
                            // Left side - Branch, Week, and Date
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text($"Branch: {branchName}")
                                    .FontSize(16)
                                    .Bold();

                                col.Item().Text($"Week: {week}")
                                    .FontSize(12);

                                col.Item().Text($"Date: {date}")
                                    .FontSize(12);
                            });

                            // Right side - Budget Information
                            row.RelativeItem().Column(col =>
                                {
                                    decimal weeklyBudgetValue = decimal.TryParse(weeklyBudget, out decimal wb) ? wb : 0;
                                    decimal proposedBudgetValue = decimal.TryParse(proposedBudget, out decimal pb) ? pb : 0;
                                    decimal shortOverValue = decimal.TryParse(shortOver, out decimal so) ? so : 0;

                                    col.Item().Text($"Weekly Budget: {weeklyBudgetValue:N2}")
                                        .FontSize(12)
                                        .AlignRight();

                                    col.Item().Text($"Proposed Budget: {proposedBudgetValue:N2}")
                                        .FontSize(12)
                                        .AlignRight();

                                    col.Item().Text($"Short/Over: {shortOverValue:N2}")
                                        .FontSize(12)
                                        .AlignRight();
                                });
                        });

                        // Add table with **BORDERS**
                        column.Item().PaddingTop(20).Table(table =>
                        {
                            // Extract column headers
                            var columnHeaders = new List<string>();
                            foreach (DataGridViewColumn column in dataGridView.Columns)
                            {
                                columnHeaders.Add(column.HeaderText);
                            }

                            // Define column widths dynamically
                            table.ColumnsDefinition(columns =>
                            {
                                for (int i = 0; i < columnHeaders.Count; i++)
                                {
                                    if (i <= 3 || i == 10) // Text columns
                                    {
                                        columns.RelativeColumn(2);
                                    }
                                    else // Numeric columns
                                    {
                                        columns.RelativeColumn(1.5f);
                                    }
                                }
                            });

                            // ✅ ADD HEADER ROW WITH BORDERS
                            table.Header(header =>
                            {
                                foreach (var headerText in columnHeaders)
                                {
                                    header.Cell()
                                        .Border(1)
                                        .BorderColor(Colors.Black)
                                        .Background(Colors.Grey.Lighten3)
                                        .Padding(2)
                                        .Element(cell =>
                                        {
                                            // Create a container for the header text
                                            cell.Row(row =>
                                            {
                                                row.RelativeItem().Column(col =>
                                                {
                                                    // Add each word on a separate line if header contains spaces
                                                    if (headerText.Contains(" "))
                                                    {
                                                        var words = headerText.Split(' ');
                                                        foreach (var word in words)
                                                        {
                                                            col.Item().Text(word)
                                                                .Bold()
                                                                .FontSize(10)
                                                                .AlignCenter();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        col.Item().Text(headerText)
                                                            .Bold()
                                                            .FontSize(10)
                                                            .AlignCenter();
                                                    }
                                                });
                                            });
                                        });
                                }
                            });

                            // ✅ ADD DATA ROWS WITH BORDERS
                            for (int rowIndex = 0; rowIndex < dataGridView.Rows.Count; rowIndex++)
                            {
                                var row = dataGridView.Rows[rowIndex];
                                var isEvenRow = rowIndex % 2 == 0;

                                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                                {
                                    var cell = row.Cells[cellIndex];
                                    string cellValue = cell.Value?.ToString() ?? "";

                                    if (decimal.TryParse(cellValue, out decimal numericValue))
                                    {
                                        cellValue = numericValue.ToString("N2");
                                    }

                                    // Determine alignment
                                    bool isTextColumn = (cellIndex <= 3 || cellIndex == 10);
                                    var cellBackground = isEvenRow ? Colors.White : Colors.Grey.Lighten5;

                                    // ✅ APPLY BORDER TO EVERY CELL
                                    table.Cell()
                                        .Border(1)
                                        .BorderColor(Colors.Black)
                                        .Background(cellBackground)
                                        .Padding(5)
                                        .AlignMiddle()
                                        .Text(cellValue)
                                        .FontSize(9);
                                }
                            }
                        });
                    });
                });
            });

            // Generate PDF
            document.GeneratePdf(filePath);

            MessageBox.Show("PDF exported successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error exporting PDF: {ex.Message}\nStack Trace: {ex.StackTrace}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            throw;
        }
    }
}