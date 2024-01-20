using DataAccessLayer;

public class UsersService
{
    private readonly DataBaseConnection _connection;

    public UsersService(DataBaseConnection connection)
    {
        _connection = connection;
    }
    public async Task Login(int id)
    {
    }

    public async Task SignUp(int id)
    {
    }

}