namespace Interfaces;

public interface IModeratorService
{

    Task<RequestWrapper> TogglePostFalseInformation(int id);
    Task<RequestWrapper> TogglePostMisleading(int id);
}
