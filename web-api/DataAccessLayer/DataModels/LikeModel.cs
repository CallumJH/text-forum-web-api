using LinqToDB.Mapping;

namespace DataModels;

[Table(Name = "Likes")]
public class LikeModel : BaseTableData 
{
    public LikeModel(int UserId, int contentId, int LikeType)
    {
        this.UserId = UserId;
        this.LikeType = LikeType;
        if(LikeType == (int)ContentTypeEnum.Post)
        {
            PostId = contentId;
        }
        else if(LikeType == (int)ContentTypeEnum.Comment)
        {
            CommentId = contentId;
        }
    }
    public LikeModel()
    {
        
    }

    [Column]
    //[Association(ThisKey = "PostId", OtherKey = "Id")]
    public int PostId { get; set; }

    [Column]
    //[Association(ThisKey = "CommentId", OtherKey = "Id")]
    public int CommentId { get; set; }
    
    [Column, NotNull]
    //[Association(ThisKey = "UserId", OtherKey = "Id")]
    public int UserId { get; set; }

    // Like type 0 post 1 comment for the time being
    [Column, NotNull]
    public int LikeType { get; set; }
}