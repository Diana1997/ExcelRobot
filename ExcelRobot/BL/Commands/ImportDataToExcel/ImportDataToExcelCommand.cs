namespace ExcelRobot.BL.Commands.ImportDataToExcel;

public class ImportDataToExcelCommand
{
    public int Start { set; get; }
    public int Sheet { set; get; }
    public int Rows { set; get; }
    public string Path { set; get; }
    public string ConnectionString { set; get; }
    public string Table { set; get; }
    /// <summary>
    /// Dictionary key will be table column name
    /// Dictionary value will be table column type
    /// </summary>
    public IDictionary<string, string> Columns { set; get; }
}