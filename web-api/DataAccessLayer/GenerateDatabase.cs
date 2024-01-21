using DataAccessLayer;
using DataModels;
using LinqToDB;

public class GenerateDatabase
{
	private readonly DataBaseConnection db;
	public GenerateDatabase(DataBaseConnection dataBaseConnection)
	{
		db = dataBaseConnection;
	}
    public void CreateDataBaseTables()
    {
        try
        {
            //Check if table exists in db
            var tables = db.DataProvider.GetSchemaProvider().GetSchema(db).Tables;
            if (!tables.Any(x => x.TableName == "Users"))
            {
                db.CreateTable<UserModel>();
            }
            if(!tables.Any(x => x.TableName == "Posts"))
            {
                db.CreateTable<PostModel>();
            }
            if(!tables.Any(x => x.TableName == "Comments"))
            {
                db.CreateTable<CommentModel>();
            }
            if(!tables.Any(x => x.TableName == "Likes"))
            {
                db.CreateTable<LikeModel>();
            }

        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }

}