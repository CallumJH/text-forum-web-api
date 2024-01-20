using LinqToDB.Mapping;

public class BaseTableData
{
    [Column, PrimaryKey, NotNull]
    public int Id { get; set; }

    [Column, NotNull]
    public DateTime DateCreated { get; set; }

    [Column, NotNull]
    public DateTime LastUpdated { get; set; }
}