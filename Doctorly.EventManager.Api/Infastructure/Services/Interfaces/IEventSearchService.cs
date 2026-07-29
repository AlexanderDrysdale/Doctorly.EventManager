namespace Doctorly.EventManager.Api.Infastructure.Services.Interfaces
{
    public interface IEventSearchService
    {
        List<string> SearchOption(List<string> words, string query);
    }
}
