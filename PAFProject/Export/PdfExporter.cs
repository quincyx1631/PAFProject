using Krypton.Toolkit;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
                    // Use landscape orientation with wider A3 paper to accommodate more columns
                    page.Size(PageSizes.A3.Landscape());
                    page.Margin(40);

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

                                col.Item().Text($"Weekly Budget: {"₱" + weeklyBudgetValue:N2}")
                                    .FontSize(12)
                                    .AlignRight();

                                col.Item().Text($"Proposed Budget: {"₱" + proposedBudgetValue:N2}")
                                    .FontSize(12)
                                    .AlignRight();

                                col.Item().Text($"Short/Over: {"₱" + shortOverValue:N2}")
                                    .FontSize(12)
                                    .AlignRight();
                            });
                        });

                        // Add table with borders
                        column.Item().PaddingTop(20).Table(table =>
                        {
                            // Extract column headers - add new #No column
                            var columnHeaders = new List<string> { "No." };
                            foreach (DataGridViewColumn column in dataGridView.Columns)
                            {
                                columnHeaders.Add(column.HeaderText);
                            }

                            // Define column widths based on content type
                            table.ColumnsDefinition(columns =>
                            {
                                // #No column (narrow)
                                columns.RelativeColumn(0.7f);

                                // Description
                                columns.RelativeColumn(3.5f);

                                // BarCode
                                columns.RelativeColumn(2f);

                                // AverageDaily
                                columns.RelativeColumn(1.5f);

                                // PrefVendor
                                columns.RelativeColumn(2f);

                                // QuantityOnHand, DaysToGo, OverShortStocks
                                columns.RelativeColumn(1.2f);
                                columns.RelativeColumn(1.2f);
                                columns.RelativeColumn(1.2f);

                                // Purchase limits (7, 15, 30 days)
                                columns.RelativeColumn(1.2f);
                                columns.RelativeColumn(1.2f);
                                columns.RelativeColumn(1.2f);

                                // LimitSelection
                                columns.RelativeColumn(1.5f);

                                // AllowedPurchase, UserValue, PurchaseLimit
                                columns.RelativeColumn(1.2f);
                                columns.RelativeColumn(1.2f);
                                columns.RelativeColumn(1.2f);

                                // AveragePrice, BudgetAmount
                                columns.RelativeColumn(1.6f);
                                columns.RelativeColumn(1.6f);

                                // Remarks
                                columns.RelativeColumn(2.8f);
                            });

                            // Add header row with borders
                            table.Header(header =>
                            {
                                foreach (var headerText in columnHeaders)
                                {
                                    bool isPurchaseLimitHeader = headerText.Contains("7 days") ||
                                                                 headerText.Contains("15 days") ||
                                                                 headerText.Contains("30 days") ||
                                                                 headerText == "Allowed Purchase" ||
                                                                 headerText == "User Value" ||
                                                                 headerText == "Purchase Limit";

                                    header.Cell()
                                        .Border(1)
                                        .BorderColor(Colors.Black)
                                        .Background(Colors.Grey.Lighten3)
                                        .Padding(2)
                                        .Element(cell =>
                                        {
                                            cell.Row(row =>
                                            {
                                                row.RelativeItem().Column(col =>
                                                {
                                                    // Add group header for Purchase Limits columns
                                                    if (headerText.Contains("7 days"))
                                                    {
                                                        col.Item().Text("Purchase Limits")
                                                            .Bold()
                                                            .FontSize(10)
                                                            .AlignCenter();
                                                    }

                                                    // Add group header for Purchase Limit columns
                                                    if (headerText == "Allowed Purchase")
                                                    {
                                                        col.Item().Text("Purchase Limit")
                                                            .Bold()
                                                            .FontSize(10)
                                                            .AlignCenter();
                                                    }

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

                            // Add data rows with borders
                            // Add data rows with borders
                            for (int rowIndex = 0; rowIndex < dataGridView.Rows.Count; rowIndex++)
                            {
                                var row = dataGridView.Rows[rowIndex];
                                var isEvenRow = rowIndex % 2 == 0;
                                var rowNumber = rowIndex + 1; // Increment row counter

                                // Add row number cell first
                                table.Cell()
                                    .Border(1)
                                    .BorderColor(Colors.Black)
                                    .Background(isEvenRow ? Colors.White : Colors.Grey.Lighten5)
                                    .Padding(4)
                                    .AlignMiddle()
                                    .AlignCenter()
                                    .Text(rowNumber.ToString())
                                    .FontSize(10);

                                // Add actual data cells
                                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                                {
                                    var cell = row.Cells[cellIndex];
                                    string cellValue = cell.Value?.ToString() ?? "";
                                    bool isNumeric = decimal.TryParse(cellValue, out decimal numericValue);

                                    if (isNumeric)
                                    {
                                        cellValue = numericValue.ToString("N2");
                                    }

                                    // Determine if this is a text column that should be left-aligned
                                    // For the column indices, you need to verify these match your actual columns
                                    bool isTextColumn = cellIndex == 0 || cellIndex == 1 || cellIndex == 3 || cellIndex == 16;

                                    var cellBackground = isEvenRow ? Colors.White : Colors.Grey.Lighten5;

                                    // Apply border to every cell
                                    table.Cell()
                                        .Border(1)
                                        .BorderColor(Colors.Black)
                                        .Background(cellBackground)
                                        .Padding(4)
                                        .AlignMiddle()
                                        .Element(e =>
                                        {
                                            if (isTextColumn || !isNumeric)
                                            {
                                                e.Text(cellValue).FontSize(10).AlignLeft();
                                            }
                                            else
                                            {
                                                e.Text(cellValue).FontSize(10).AlignRight();
                                            }
                                        });
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