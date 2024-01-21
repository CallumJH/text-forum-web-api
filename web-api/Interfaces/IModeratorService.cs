namespace Interfaces;

public interface IModeratorService
{

    Task TogglePostFalseInformation(int id);
    Task TogglePostMisleading(int id);
}
