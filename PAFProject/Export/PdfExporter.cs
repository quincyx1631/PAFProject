using iTextSharp.text;
using iTextSharp.text.pdf;
using Krypton.Toolkit;

namespace PAFProject.Export
{
    public class PdfExporter
    {
        public static void ExportToPdf(KryptonDataGridView dataGridView, string branchName, string week,
            string weeklyBudget, string proposedBudget, string shortOver)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"{branchName}_Week{week}_Budget.pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        // Changed to portrait by removing .Rotate()
                        Document document = new Document(PageSize.A4, 25f, 25f, 50f, 50f);
                        PdfWriter writer = PdfWriter.GetInstance(document, fs);

                        // Open document
                        document.Open();

                        // Add header
                        AddHeader(document, branchName, week, weeklyBudget, proposedBudget, shortOver);

                        // Add table with adjusted column count
                        PdfPTable table = new PdfPTable(dataGridView.Columns.Count);
                        table.WidthPercentage = 100;

                        // Set relative column widths for better portrait layout
                        float[] columnWidths = new float[dataGridView.Columns.Count];
                        for (int i = 0; i < columnWidths.Length; i++)
                        {
                            // Make text columns slightly wider than number columns
                            columnWidths[i] = (i <= 3 || i == 10) ? 1.5f : 1f;
                        }
                        table.SetWidths(columnWidths);

                        // Add headers
                        AddTableHeaders(table, dataGridView);

                        // Add data rows
                        AddTableData(table, dataGridView);

                        document.Add(table);
                        document.Close();
                    }

                    MessageBox.Show("PDF exported successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting PDF: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void AddHeader(Document document, string branchName, string week,
            string weeklyBudget, string proposedBudget, string shortOver)
        {
            // Adjusted font sizes for portrait layout
            iTextSharp.text.Font headerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14f, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font normalFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL);

            Paragraph header = new Paragraph();
            header.Add(new Chunk($"Branch: {branchName}\n", headerFont));
            header.Add(new Chunk($"Week: {week}\n", normalFont));

            decimal weeklyBudgetValue = 0;
            decimal proposedBudgetValue = 0;
            decimal shortOverValue = 0;

            decimal.TryParse(weeklyBudget, out weeklyBudgetValue);
            decimal.TryParse(proposedBudget, out proposedBudgetValue);
            decimal.TryParse(shortOver, out shortOverValue);

            header.Add(new Chunk($"Weekly Budget: {weeklyBudgetValue:N2}\n", normalFont));
            header.Add(new Chunk($"Proposed Budget: {proposedBudgetValue:N2}\n", normalFont));
            header.Add(new Chunk($"Short/Over: {shortOverValue:N2}\n\n", normalFont));

            document.Add(header);
        }

        private static void AddTableHeaders(PdfPTable table, KryptonDataGridView dataGridView)
        {
            // Adjusted font size for portrait layout
            iTextSharp.text.Font headerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9f, iTextSharp.text.Font.BOLD);

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, headerFont))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(240, 240, 240),
                    Padding = 4,
                    MinimumHeight = 25f
                };
                table.AddCell(cell);
            }
        }

        private static void AddTableData(PdfPTable table, KryptonDataGridView dataGridView)
        {
            // Adjusted font size for portrait layout
            iTextSharp.text.Font cellFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL);

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string cellValue = cell.Value?.ToString() ?? "";

                    if (decimal.TryParse(cellValue, out decimal numericValue))
                    {
                        cellValue = numericValue.ToString("N2");
                    }

                    PdfPCell pdfCell = new PdfPCell(new Phrase(cellValue, cellFont))
                    {
                        HorizontalAlignment = cell.ColumnIndex <= 3 || cell.ColumnIndex == 10
                            ? Element.ALIGN_LEFT
                            : Element.ALIGN_RIGHT,
                        Padding = 3,
                        MinimumHeight = 20f
                    };
                    table.AddCell(pdfCell);
                }
            }
        }
    }
}