using LinqToDB.Mapping;

public class BaseTableData
{
    public BaseTableData()
    {
        
    }

    [Column, PrimaryKey]
    [NotNull, Identity, SkipValuesOnInsert]
    public int Id { get; set; }

    [Column, NotNull]
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    [Column, NotNull]
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}