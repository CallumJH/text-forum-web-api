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
            if(!db.DataProvider.GetSchemaProvider().GetSchema(db).Tables.Any(x => x.TableName == "Users"))
            {
                db.CreateTable<UserModel>();
            }
            if(!db.DataProvider.GetSchemaProvider().GetSchema(db).Tables.Any(x => x.TableName == "Posts"))
            {
                db.CreateTable<PostModel>();
            }
            if(!db.DataProvider.GetSchemaProvider().GetSchema(db).Tables.Any(x => x.TableName == "Comments"))
            {
                db.CreateTable<CommentModel>();
            }
            if(!db.DataProvider.GetSchemaProvider().GetSchema(db).Tables.Any(x => x.TableName == "Likes"))
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