using DataModels;
using LinqToDB;
using LinqToDB.Data;

namespace DataAccessLayer;

public interface IDataBaseConnection : IDataContext
{
    ITable<CommentModel> Comments { get; }
    ITable<LikeModel> Likes { get; }
    ITable<PostModel> Posts { get; }
    ITable<UserModel> Users { get; }
}


/// <summary>
/// The root connector to our SQLite DB 
/// This will be extracted to a singleton connection as our SQLite DB 
/// cannot maintain concurrent connections
/// </summary>
public class DataBaseConnection : DataConnection, IDataBaseConnection
{
    public DataBaseConnection(DataOptions<DataBaseConnection> options) : base(options.Options)
    {

    }

    public ITable<UserModel> Users => this.GetTable<UserModel>();
    public ITable<CommentModel> Comments => this.GetTable<CommentModel>();
    public ITable<PostModel> Posts => this.GetTable<PostModel>();
    public ITable<LikeModel> Likes => this.GetTable<LikeModel>();
}
