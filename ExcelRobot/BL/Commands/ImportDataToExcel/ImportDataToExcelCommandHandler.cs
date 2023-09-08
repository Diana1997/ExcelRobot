using System.Data.SqlClient;
using OfficeOpenXml;

namespace ExcelRobot.BL.Commands.ImportDataToExcel;

public class ImportDataToExcelCommandHandler
{
    public IList<string> Handle(ImportDataToExcelCommand command)
    {
        FileInfo existingFile = new FileInfo(command.Path);
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        IList<string> errorRows = new List<string>();

        using (ExcelPackage package = new ExcelPackage(existingFile))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets[command.Sheet];

            int columnCount = worksheet.Dimension.Columns;


            string insertQuery = $"INSERT INTO {command.Table}";

            var tableColumns = command.Columns.Keys.ToList();
            var columnTypes = command.Columns.Values.ToArray();

            if (tableColumns == null)
            {
                throw new Exception("Please write table columns");
            }

            string values = "";

            for (int i = 0; i < tableColumns.Count; ++i)
            {
                if (i == 0 && tableColumns.Count == 1)
                {
                    insertQuery += $" ( {tableColumns[i]} )";
                    values += $"(@Value{i + 1})";
                }

                else if (i == 0)
                {
                    insertQuery += $"( {tableColumns[i]},";
                    values += $"( @Value{i + 1}, ";
                }
                else if (i == tableColumns.Count - 1)
                {
                    insertQuery += $"{tableColumns[i]} )";
                    values += $" @Value{i + 1} ) ";
                }
                else
                {
                    insertQuery += $"{tableColumns[i]} ,";
                    values += $" @Value{i + 1}, ";
                }
            }

            insertQuery += $"VALUES {values}";


            using (var sqlConnection = new SqlConnection(command.ConnectionString))
            {
                sqlConnection.Open();
                for (int row = command.Start; row < command.Rows + 1; row++)
                {
                    SqlCommand sqlCommand = new SqlCommand(insertQuery, sqlConnection);

                    for (int col = 0; col < columnCount; col++)
                    {
                        var columnType = columnTypes[col];
                        var columnValue = worksheet.Cells[row, col+1].Value;

                        if (columnValue == null)
                        {
                            errorRows.Add($"Row {row + 1}, Column {col+1}");
                        }
                        else
                        {
                            if (columnType == ColumnTypes.String)
                            {
                                sqlCommand.Parameters.AddWithValue($"@Value{col+1}", columnValue);
                            }
                            else if (columnType == ColumnTypes.Boolean)
                            {
                                sqlCommand.Parameters.AddWithValue($"@Value{col+1}", Convert.ToBoolean(columnValue));
                            }
                            else if (columnType == ColumnTypes.DateTime)
                            {
                                sqlCommand.Parameters.AddWithValue($"@Value{col+1}", Convert.ToDateTime(columnValue));
                            }
                            else if (columnType == ColumnTypes.Int)
                            {
                                sqlCommand.Parameters.AddWithValue($"@Value{col+1}", Convert.ToInt32(columnValue));
                            }
                        }
                    }

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }


        return errorRows;
    }
}

public class ColumnTypes
{
    public const string String = "string";
    public const string Boolean = "boolean";
    public const string Int = "int";
    public const string DateTime = "datetime";
}