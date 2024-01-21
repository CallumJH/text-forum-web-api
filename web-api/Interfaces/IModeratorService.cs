namespace Interfaces;

public interface IModeratorService
{
    Task<RequestWrapper> ToggleContentFlag(ContentToggle request);
}
