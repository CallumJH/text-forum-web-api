using DataAccessLayer;
using Interfaces;

public class ModeratorService : IModeratorService
{
    private readonly DataBaseConnection _connection;

    public ModeratorService(DataBaseConnection connection)
    {
        _connection = connection;
    }
    public async Task<RequestWrapper> TogglePostMisleading(int id)
    {
        var request = new RequestWrapper();
        return request;
    }

    public async Task<RequestWrapper> TogglePostFalseInformation(int id)
    {
        var request = new RequestWrapper();
        return request;
    }

}