namespace Doctorly.EventManager.Api.Infastructure.Services.Implementations
{
    public class SearchCacheService
    {
        // Dictionary to hold cached results
        private readonly Dictionary<string, object> _cache = new();

        // Try to get cached result
        public bool TryGet(string query, out object? result)
        {
            return _cache.TryGetValue(query, out result);
        }

        // Add or update cache entry
        public void Set(string query, object result)
        {
            _cache[query] = result;
        }

        // Optional: clear cache
        public void Clear()
        {
            _cache.Clear();
        }
    }
}
