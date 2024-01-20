using LinqToDB.Mapping;

public class CommonPostData : BaseTableData
{
    [Column, NotNull]
    public string Content { get; set; }

    [Column, NotNull]
    public int LikeCount { get; set; } // < 100 users wont be able to over like 2147483647 times

    [Column, NotNull]
    public int CreatedByUserID { get; set; } 
}