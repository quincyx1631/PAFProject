using ClosedXML.Excel;

namespace PAFProject.Export
{
    public class ExcelExporter
    {
        public static void ExportToExcel(DataGridView dataGridView, string branchName, string week, string weeklyBudget, string proposedBudget, string shortOver)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Purchase Approval");

                    // Title Section - Merged & Centered
                    worksheet.Cell(1, 1).Value = "PURCHASE APPROVAL FORM";
                    var titleRange = worksheet.Range("A1:L1");
                    titleRange.Merge();
                    titleRange.Style.Font.Bold = true;
                    titleRange.Style.Font.FontSize = 14;
                    titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    titleRange.Style.Fill.BackgroundColor = XLColor.LightBlue;

                    // Week & Budget Details
                    worksheet.Cell(4, 1).Value = "Week:";
                    worksheet.Cell(4, 2).Value = week;
                    worksheet.Cell(4, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                    worksheet.Cell(2, 1).Value = "Branch:";
                    worksheet.Cell(2, 2).Value = branchName;
                    worksheet.Cell(2, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                    worksheet.Cell(2, 2).Style.Fill.BackgroundColor = XLColor.Yellow;
                    worksheet.Cell(4, 2).Style.Fill.BackgroundColor = XLColor.Yellow;

                    // ✅ **Weekly Budget (Corrected)**
                    worksheet.Cell(2, 11).Value = "Weekly Budget:";
                    if (decimal.TryParse(weeklyBudget, out decimal weeklyBudgetValue))
                    {
                        worksheet.Cell(2, 12).Value = weeklyBudgetValue;
                        worksheet.Cell(2, 12).Style.NumberFormat.Format = "\"₱\" #,##0.00";
                        worksheet.Cell(2, 12).Style.Fill.BackgroundColor = XLColor.LightGoldenrodYellow;
                    }

                    // ✅ **Proposed Budget (Corrected)**
                    worksheet.Cell(3, 11).Value = "Proposed Budget:";
                    if (decimal.TryParse(proposedBudget, out decimal proposedBudgetValue))
                    {
                        worksheet.Cell(3, 12).Value = proposedBudgetValue;
                        worksheet.Cell(3, 12).Style.NumberFormat.Format = "\"₱\" #,##0.00";
                        worksheet.Cell(3, 12).Style.Fill.BackgroundColor = XLColor.GreenYellow;
                    }

                    // ✅ **Short/Over Budget (Corrected)**
                    worksheet.Cell(4, 11).Value = "Short/Over:";
                    if (decimal.TryParse(shortOver, out decimal shortOverValue))
                    {
                        worksheet.Cell(4, 12).Value = shortOverValue;
                        worksheet.Cell(4, 12).Style.NumberFormat.Format = "\"₱\" #,##0.00";
                        if (shortOverValue < 0)
                        {
                            worksheet.Cell(4, 12).Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }

                    // ----------------------------
                    // New two-row header with a first "No." column
                    // ----------------------------
                    int headerStartRow = 7;
                    int headerEndRow = 8;
                    int dataStartRow = 9;  // Data rows will start here

                    // Create the first header column ("No.") merged across two rows.
                    var noHeaderRange = worksheet.Range(worksheet.Cell(headerStartRow, 1), worksheet.Cell(headerEndRow, 1));
                    noHeaderRange.Merge();
                    noHeaderRange.Value = "No.";
                    noHeaderRange.Style.Font.FontSize = 12;
                    noHeaderRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    noHeaderRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                    noHeaderRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    noHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    // Add remaining headers from the DataGridView.
                    for (int i = 0; i < dataGridView.Columns.Count; i++)
                    {
                        int colIndex = i + 2; // Shift by one because column 1 is "No."
                        var headerRange = worksheet.Range(worksheet.Cell(headerStartRow, colIndex), worksheet.Cell(headerEndRow, colIndex));
                        headerRange.Merge();
                        headerRange.Value = dataGridView.Columns[i].HeaderText;
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Font.FontSize = 12;
                        headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                        headerRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                        headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }

                    // ----------------------------
                    // Populate Data with Formatting
                    // ----------------------------
                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        // Insert serial number in the first column ("No.")
                        var noCell = worksheet.Cell(i + dataStartRow, 1);
                        noCell.Value = (i + 1).ToString();
                        noCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        noCell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        // Write data from the DataGridView into subsequent columns.
                        for (int j = 0; j < dataGridView.Columns.Count; j++)
                        {
                            var cell = worksheet.Cell(i + dataStartRow, j + 2);
                            var cellValue = dataGridView.Rows[i].Cells[j].Value;

                            if (cellValue != null)
                            {
                                cell.Value = cellValue.ToString();

                                int[] targetColumnsRight = { 4, 5, 6, 7, 8, 9 };
                                int[] targetColumnLeft = { 0, 2, 3, 10 };
                                // For example, if column index 9 (j==8) is a currency value,
                                // align it to the right and color negatives red.
                                if (targetColumnsRight.Contains(j))
                                {
                                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                                    if (decimal.TryParse(cellValue.ToString(), out decimal numValue) && numValue < 0)
                                    {
                                        cell.Style.Font.FontColor = XLColor.Red;
                                    }
                                }
                                else if (targetColumnLeft.Contains(j))
                                {
                                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                                }
                                else
                                {
                                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                }
                                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            }
                        }
                    }

                    // ----------------------------
                    // Remove Auto-Sizing of Columns
                    // ----------------------------
                    worksheet.Column(1).Width = 10;   // "No." Column
                    worksheet.Column(2).AdjustToContents();// Description column
                    worksheet.Column(3).Width = 25;  // Bar Code column
                    worksheet.Column(4).Width = 25; // Pref Vendor
                    worksheet.Column(5).Width = 30;  // Average Daily column
                    worksheet.Column(6).Width = 20;  // Qty On Hand
                    worksheet.Column(7).Width = 20;  // Days to Go
                    worksheet.Column(8).Width = 20;  // Over/Short Stocks   
                    worksheet.Column(9).Width = 20;  // Purchase Limit
                    worksheet.Column(10).Width = 20;  // Average Price
                    worksheet.Column(11).Width = 20; // Budget Amount
                    worksheet.Column(12).Width = 40; // Remarks

                    // Ensure the entire column (all rows) adjusts to content


                    // Save File
                    string fileName = $"PurchaseApprovalForm_{DateTime.Now:yyyyMMdd}.xlsx";
                    string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                    workbook.SaveAs(filePath);
                    MessageBox.Show($"Excel file saved to {filePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting to Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
