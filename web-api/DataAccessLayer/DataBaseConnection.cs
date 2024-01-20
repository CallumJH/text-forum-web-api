using LinqToDB;
using LinqToDB.Data;
using Pocos;

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
    //public ITable<Person> People => this.GetTable<Person>();
}
