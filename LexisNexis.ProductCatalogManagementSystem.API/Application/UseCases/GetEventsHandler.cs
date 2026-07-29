
using Doctorly.EventManager.Api.Application.Dtos;
using Doctorly.EventManager.Api.Infastructure.Persistence.Repository.Interfaces;
using Doctorly.EventManager.Api.Infastructure.Services.Implementations;
using Doctorly.EventManager.Api.Infastructure.Services.Interfaces;

namespace Doctorly.EventManager.Application.UseCases
{
    public class GetEventsHandler
    {
        private readonly IEventRepository _repo;
        private readonly SearchCacheService _cache;
        private readonly IEventSearchService _searchEngine;

        public GetEventsHandler(IEventRepository repo, SearchCacheService cache, IEventSearchService searchEngine)
        {
            _repo = repo;
            _cache = cache;
            _searchEngine = searchEngine;
        }

        public (List<EventDto> Items, int TotalCount) Handle(SearchEventDto dto)
        {
            string cacheKey = $"{dto.Title}-{dto.AttendeeName}-{dto.PageNumber}-{dto.PageSize}";

            if (_cache.TryGet(cacheKey, out var cached))
            {
                return ((List<EventDto>)cached, ((List<EventDto>)cached).Count);
            }

            var query = _repo.GetAll().AsQueryable();

            // Title search
            if (!string.IsNullOrWhiteSpace(dto.Title))
            {
                var eventTitles = query.Select(e => e.Title).ToList();
                var matchedTitles = _searchEngine.SearchOption(
                    eventTitles.Select(t => t.ToLowerInvariant()).ToList(),
                    dto.Title.ToLowerInvariant()
                );
                query = query.Where(e => matchedTitles.Any(m =>
                    e.Title.Contains(m, StringComparison.OrdinalIgnoreCase)));
            }

            // Attendee search
            if (!string.IsNullOrWhiteSpace(dto.AttendeeName))
            {
                var attendeeNames = query.SelectMany(e => e.Attendees.Select(a => a.Name)).ToList();
                var matchedNames = _searchEngine.SearchOption(
                    attendeeNames.Select(n => n.ToLowerInvariant()).ToList(),
                    dto.AttendeeName.ToLowerInvariant()
                );
                query = query.Where(e => e.Attendees.Any(a =>
                    matchedNames.Any(m => a.Name.Contains(m, StringComparison.OrdinalIgnoreCase))));
            }

            var skip = (dto.PageNumber - 1) * dto.PageSize;
            var events = query.Skip(skip).Take(dto.PageSize).ToList();

            var result = events.Select(EventDto.FromEntity).ToList();

            _cache.Set(cacheKey, result);

            return (result, query.Count());
        }
    }
}

