using LinqToDB;
using LinqToDB.Data;
using DataModels;

namespace DataAccessLayer;


/// <summary>
/// The root connector to our SQLite DB 
/// This will be extracted to a singleton connection as our SQLite DB 
/// cannot maintain concurrent connections
/// </summary>
public class DataBaseConnection : DataConnection
{
    public DataBaseConnection(DataOptions<DataBaseConnection> options) : base(options.Options)
    { 
    }

    public ITable<UserModel> Users => this.GetTable<UserModel>();
    public ITable<CommentModel> Comments => this.GetTable<CommentModel>();
    public ITable<PostModel> Posts => this.GetTable<PostModel>();
    public ITable<LikeModel> Likes => this.GetTable<LikeModel>();
}
