using ClosedXML.Excel;

namespace PAFProject.Export
{
    public class ExcelExporter
    {
        public static void ExportToExcel(DataGridView dataGridView, string filePath, string branchName,
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

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Purchase Approval");

                    // Title Section - Merged & Centered
                    worksheet.Cell(1, 1).Value = "PURCHASE APPROVAL FORM";
                    var titleRange = worksheet.Range("A1:R1");
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

                    worksheet.Cell(3, 1).Value = "Date:";
                    worksheet.Cell(3, 2).Value = date;
                    worksheet.Cell(3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                    worksheet.Cell(3, 2).Style.Fill.BackgroundColor = XLColor.Yellow;
                    worksheet.Cell(2, 2).Style.Fill.BackgroundColor = XLColor.Yellow;
                    worksheet.Cell(4, 2).Style.Fill.BackgroundColor = XLColor.Yellow;

                    // Weekly Budget
                    worksheet.Cell(2, 16).Value = "Weekly Budget:";
                    if (decimal.TryParse(weeklyBudget, out decimal weeklyBudgetValue))
                    {
                        worksheet.Cell(2, 17).Value = weeklyBudgetValue;
                        worksheet.Cell(2, 17).Style.NumberFormat.Format = "\"₱\" #,##0.00";
                        worksheet.Cell(2, 17).Style.Fill.BackgroundColor = XLColor.LightGoldenrodYellow;
                    }

                    // Proposed Budget
                    worksheet.Cell(3, 16).Value = "Proposed Budget:";
                    if (decimal.TryParse(proposedBudget, out decimal proposedBudgetValue))
                    {
                        worksheet.Cell(3, 17).Value = proposedBudgetValue;
                        worksheet.Cell(3, 17).Style.NumberFormat.Format = "\"₱\" #,##0.00";
                        worksheet.Cell(3, 17).Style.Fill.BackgroundColor = XLColor.GreenYellow;
                    }

                    // Short/Over Budget
                    worksheet.Cell(4, 16).Value = "Short/Over:";
                    if (decimal.TryParse(shortOver, out decimal shortOverValue))
                    {
                        worksheet.Cell(4, 17).Value = shortOverValue;
                        worksheet.Cell(4, 17).Style.NumberFormat.Format = "\"₱\" #,##0.00";
                        if (shortOverValue < 0)
                        {
                            worksheet.Cell(4, 17).Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }

                    // ----------------------------
                    // Header with Purchase Limit grouping
                    // ----------------------------
                    int headerStartRow = 6;
                    int headerRow1 = 6;    // First header row
                    int headerRow2 = 7;    // Second header row
                    int dataStartRow = 8;  // Data rows will start here

                    // Create the first header column ("No.") merged across two rows
                    var noHeaderRange = worksheet.Range(worksheet.Cell(headerRow1, 1), worksheet.Cell(headerRow2, 1));
                    noHeaderRange.Merge();
                    noHeaderRange.Value = "No.";
                    noHeaderRange.Style.Font.FontSize = 12;
                    noHeaderRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    noHeaderRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    noHeaderRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    noHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    // Define column mappings for headers - Modified to work with KryptonDataGridView
                    var columnMappings = new List<(string Header, bool IsMerged, int? GroupStartCol, int? GroupEndCol, string GroupTitle)>
                    {
                        ("Description", true, null, null, null),
                        ("Bar Code", true, null, null, null),
                        ("Average Daily", true, null, null, null),
                        ("Pref Vendor", true, null, null, null),
                        ("Qty On Hand", true, null, null, null),
                        ("Days to Go", true, null, null, null),
                        ("Over/Short Stocks", true, null, null, null),
                        ("7 Days", true, null, null, null),        // Modified to IsMerged = true
                        ("15 Days", true, null, null, null),       // Modified to IsMerged = true
                        ("30 Days", true, null, null, null),       // Modified to IsMerged = true
                        ("Limit Selection", true, null, null, null),
                        ("System", true, null, null, null),
                        ("User", true, null, null, null),
                        ("Total", true, null, null, null),
                        ("Average Price", true, null, null, null),
                        ("Budget Amount", true, null, null, null),
                        ("Remarks", true, null, null, null)
                    };

                    // Set headers based on mappings - Simplified approach for KryptonDataGridView
                    int colIndex = 2; // Start from column 2 (after "No.")

                    // Create the first group header for "Purchase Limit" (7 Days, 15 Days, 30 Days)
                    worksheet.Cell(headerRow1, 9).Value = "Purchase Limit";
                    var firstLimitGroup = worksheet.Range(headerRow1, 9, headerRow1, 11);
                    firstLimitGroup.Merge();
                    firstLimitGroup.Style.Font.Bold = true;
                    firstLimitGroup.Style.Fill.BackgroundColor = XLColor.LightCyan;
                    firstLimitGroup.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    firstLimitGroup.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    // Create the second group header for "Purchase Limit" (System, User, Total)
                    worksheet.Cell(headerRow1, 13).Value = "Purchase Limit";
                    var secondLimitGroup = worksheet.Range(headerRow1, 13, headerRow1, 15);
                    secondLimitGroup.Merge();
                    secondLimitGroup.Style.Font.Bold = true;
                    secondLimitGroup.Style.Fill.BackgroundColor = XLColor.LightCyan;
                    secondLimitGroup.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    secondLimitGroup.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    for (int i = 0; i < columnMappings.Count; i++)
                    {
                        int currentCol = colIndex + i;

                        // Set the header in row 2
                        worksheet.Cell(headerRow2, currentCol).Value = columnMappings[i].Header;
                        worksheet.Cell(headerRow2, currentCol).Style.Font.Bold = true;
                        worksheet.Cell(headerRow2, currentCol).Style.Fill.BackgroundColor = XLColor.LightGray;
                        worksheet.Cell(headerRow2, currentCol).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(headerRow2, currentCol).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        // For columns not in "Purchase Limit" groups, merge the cell with the one above
                        bool isInFirstPurchaseLimitGroup = (currentCol >= 9 && currentCol <= 11);
                        bool isInSecondPurchaseLimitGroup = (currentCol >= 13 && currentCol <= 15);

                        if (!isInFirstPurchaseLimitGroup && !isInSecondPurchaseLimitGroup)
                        {
                            // Merge the cell in row 1 with the one in row 2
                            var cellRange = worksheet.Range(headerRow1, currentCol, headerRow2, currentCol);
                            cellRange.Merge();
                            cellRange.Value = columnMappings[i].Header;
                            cellRange.Style.Font.Bold = true;
                            cellRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                            cellRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            cellRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            cellRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }
                    }

                    // ----------------------------
                    // Populate Data with Formatting
                    // ----------------------------
                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        // Set entire row to have middle vertical alignment
                        var rowNum = i + dataStartRow;
                        worksheet.Row(rowNum).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        // Insert serial number in the first column ("No.")
                        var noCell = worksheet.Cell(rowNum, 1);
                        noCell.Value = (i + 1).ToString();
                        noCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        noCell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        // Define column data types and alignments
                        var columnProperties = new List<(int DataGridViewIndex, XLAlignmentHorizontalValues Alignment, bool IsNumeric, bool IsCurrency)>
                        {
                            (0, XLAlignmentHorizontalValues.Left, false, false),    // Description
                            (1, XLAlignmentHorizontalValues.Left, false, false),    // BarCode
                            (2, XLAlignmentHorizontalValues.Left, false, false),    // AverageDaily
                            (3, XLAlignmentHorizontalValues.Left, false, false),    // PrefVendor
                            (4, XLAlignmentHorizontalValues.Right, true, false),    // QuantityOnHand
                            (5, XLAlignmentHorizontalValues.Right, true, false),    // DaysToGo
                            (6, XLAlignmentHorizontalValues.Right, true, false),    // OverShortStocks
                            (7, XLAlignmentHorizontalValues.Right, false, false),    // PurchaseLimit7Days
                            (8, XLAlignmentHorizontalValues.Right, false, false),    // PurchaseLimit15Days
                            (9, XLAlignmentHorizontalValues.Right, false, false),    // PurchaseLimit30Days
                            (10, XLAlignmentHorizontalValues.Center, false, false), // LimitSelection
                            (11, XLAlignmentHorizontalValues.Right, false, false),   // AllowedPurchase
                            (12, XLAlignmentHorizontalValues.Right, false, false),   // UserValue
                            (13, XLAlignmentHorizontalValues.Right, false, false),   // PurchaseLimit (Total)
                            (14, XLAlignmentHorizontalValues.Right, true, true),    // AveragePrice
                            (15, XLAlignmentHorizontalValues.Right, true, true),    // BudgetAmount
                            (16, XLAlignmentHorizontalValues.Fill, false, false)    // Remarks - Changed to Fill alignment
                        };

                        // Write data from the DataGridView into subsequent columns
                        for (int j = 0; j < columnProperties.Count; j++)
                        {
                            var columnProp = columnProperties[j];
                            var dgvIdx = columnProp.DataGridViewIndex;

                            // Skip if we're past the available columns
                            if (dgvIdx >= dataGridView.Columns.Count)
                                continue;

                            var cell = worksheet.Cell(rowNum, j + 2); // +2 because first column is "No."
                            var cellValue = dataGridView.Rows[i].Cells[dgvIdx].Value;

                            if (cellValue != null)
                            {
                                var strValue = cellValue.ToString();

                                // Set value based on type
                                if (columnProp.IsNumeric && decimal.TryParse(strValue, out decimal numValue))
                                {
                                    cell.Value = numValue;

                                    // Format as currency if needed
                                    if (columnProp.IsCurrency)
                                    {
                                        cell.Style.NumberFormat.Format = "\"₱\" #,##0.00";
                                    }
                                    else
                                    {
                                        cell.Style.NumberFormat.Format = "#,##0.00";
                                    }

                                    // Red font for negative values
                                    if (numValue < 0)
                                    {
                                        cell.Style.Font.FontColor = XLColor.Red;
                                    }
                                }
                                else
                                {
                                    cell.Value = strValue;

                                    // For Remarks column, use Fill alignment instead of trimming
                                    if (j == 16) // Remarks column
                                    {
                                        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Fill;
                                        // Don't trim text as per client requirement
                                    }
                                }

                                // Set the alignment based on column properties
                                cell.Style.Alignment.Horizontal = columnProp.Alignment;
                                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            }
                        }
                        int lastDataRow = dataStartRow + dataGridView.Rows.Count + 2; // Add some space

                        // Create the merged cell for the WEEKLY PURCHASE BUDGET REQUEST
                        var requestTitleRange = worksheet.Range(lastDataRow, 1, lastDataRow, 16); // Columns A to P
                        requestTitleRange.Merge();
                        requestTitleRange.Value = "WEEKLY PURCHASE BUDGET REQUEST";
                        requestTitleRange.Style.Font.Bold = true;
                        requestTitleRange.Style.Font.FontSize = 12;
                        requestTitleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        requestTitleRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        // Add the budget amount in the last column (same as proposed budget)
                        worksheet.Cell(lastDataRow, 17).Value = proposedBudgetValue; // Reuse the proposedBudgetValue from earlier
                        worksheet.Cell(lastDataRow, 17).Style.NumberFormat.Format = "\"₱\" #,##0.00";
                        worksheet.Cell(lastDataRow, 17).Style.Font.Bold = true;
                        worksheet.Cell(lastDataRow, 17).Style.Fill.BackgroundColor = XLColor.GreenYellow;
                        worksheet.Cell(lastDataRow, 17).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(lastDataRow, 17).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    }

                    // ----------------------------
                    // Set Column Widths
                    // ----------------------------
                    worksheet.Column(1).Width = 8;    // "No." Column
                    worksheet.Column(2).AdjustToContents();   // Description
                    worksheet.Column(3).Width = 20;   // Bar Code
                    worksheet.Column(4).Width = 30;   // Average Daily
                    worksheet.Column(5).Width = 20;   // Pref Vendor
                    worksheet.Column(6).Width = 15;   // Qty On Hand
                    worksheet.Column(7).Width = 15;   // Days to Go
                    worksheet.Column(8).Width = 20;   // Over/Short Stocks
                    worksheet.Column(9).Width = 12;   // 7 Days
                    worksheet.Column(10).Width = 12;  // 15 Days
                    worksheet.Column(11).Width = 12;  // 30 Days
                    worksheet.Column(12).Width = 15;  // Limit Selection
                    worksheet.Column(13).Width = 15;  // Allowed Purchase
                    worksheet.Column(14).Width = 15;  // User Value
                    worksheet.Column(15).Width = 15;  // Purchase Limit
                    worksheet.Column(16).Width = 15;  // Average Price
                    worksheet.Column(17).Width = 15;  // Budget Amount
                    worksheet.Column(18).Width = 30;  // Remarks

                    // Save File
                    workbook.SaveAs(filePath);
                    MessageBox.Show("Excel file exported successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting to Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}