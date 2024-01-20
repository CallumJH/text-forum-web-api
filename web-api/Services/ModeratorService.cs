using DataAccessLayer;

public class ModeratorService
{
    private readonly DataBaseConnection _connection;

    public ModeratorService(DataBaseConnection connection)
    {
        _connection = connection;
    }
    public async Task TogglePostMisleading(int id)
    {
    }

    public async Task TogglePostFalseInformation(int id)
    {
    }

}