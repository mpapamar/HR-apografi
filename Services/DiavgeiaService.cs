using HR.Apografi.Constants;
using HR.Apografi.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace HR.Apografi.Services
{
    public class DiavgeiaService
    {
        private static SemaphoreSlim _lockOrganizations = new SemaphoreSlim(1);
        private static SemaphoreSlim _lockOrganizationPos = new SemaphoreSlim(1);
        private static SemaphoreSlim _lockDecisions = new SemaphoreSlim(3);

        private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        public DiavgeiaService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<List<OrganizationDiavgeia>> GetOrganizationsAsync()
        {
            var cacheKey = CacheKeys.OrganizationsDiavgeia;
            if (_cache.TryGetValue<List<OrganizationDiavgeia>>(cacheKey, out var cached))
            {
                return cached;
            }

            try
            {
                await _lockOrganizations.WaitAsync();
                if (_cache.TryGetValue(cacheKey, out cached))
                {
                    return cached;
                }

                var result = await _httpClient.GetStringAsync("organizations");
                var response = JsonSerializer.Deserialize<OrganizationDiavgeiaResponse>(result, _serializerOptions);

                _cache.Set(cacheKey, response.Organizations, TimeSpan.FromDays(1));
                return response.Organizations;
            }
            finally
            {
                _lockOrganizations.Release();
            }
        }

        public async Task<List<Position>> GetOrganizationPositions(string organizationId)
        {
            var cacheKey = CacheKeys.OrganizationsDiavgeiaPositions(organizationId);
            if (_cache.TryGetValue<List<Position>>(cacheKey, out var cached))
            {
                return cached;
            }

            try
            {
                await _lockOrganizationPos.WaitAsync();
                if (_cache.TryGetValue(cacheKey, out cached))
                {
                    return cached;
                }

                var result = await _httpClient.GetStringAsync($"organizations/{organizationId}/positions");
                var response = JsonSerializer.Deserialize<OrganizationPositionsResponse>(result, _serializerOptions);

                _cache.Set(cacheKey, response.Positions, TimeSpan.FromHours(1));
                return response.Positions;
            }
            finally
            {
                _lockOrganizationPos.Release();
            }
        }

        public async Task<List<Decision>> GetOrganizationDiorismoi(string organizationId)
        {
            var cacheKey = CacheKeys.OrganizationsDiavgeiaDiorismoi(organizationId);
            if (_cache.TryGetValue<List<Decision>>(cacheKey, out var cached))
            {
                return cached;
            }

            try
            {
                await _lockDecisions.WaitAsync();
                if (_cache.TryGetValue(cacheKey, out cached))
                {
                    return cached;
                }

                var result = await _httpClient.GetStringAsync($"search?org={organizationId}&type=Γ.3.3&sort=recent");
                var response = JsonSerializer.Deserialize<DecisionSearchResponse>(result, _serializerOptions);

                _cache.Set(cacheKey, response.Decisions, TimeSpan.FromDays(1));
                return response.Decisions;
            }
            finally
            {
                _lockDecisions.Release();
            }
        }

        public async Task<List<Decision>> GetOrganizationProkiriksi(string organizationId)
        {
            var cacheKey = CacheKeys.OrganizationsDiavgeiaProkiriksi(organizationId);
            if (_cache.TryGetValue<List<Decision>>(cacheKey, out var cached))
            {
                return cached;
            }

            try
            {
                await _lockDecisions.WaitAsync();
                if (_cache.TryGetValue(cacheKey, out cached))
                {
                    return cached;
                }

                var result = await _httpClient.GetStringAsync($"search?org={organizationId}&type=Γ.3.1&sort=recent");
                var response = JsonSerializer.Deserialize<DecisionSearchResponse>(result, _serializerOptions);

                _cache.Set(cacheKey, response.Decisions, TimeSpan.FromDays(1));
                return response.Decisions;
            }
            finally
            {
                _lockDecisions.Release();
            }
        }

        public async Task<List<Decision>> GetOrganizationPinakesEpitixonton(string organizationId)
        {
            var cacheKey = CacheKeys.OrganizationsDiavgeiaApotelesmata(organizationId);
            if (_cache.TryGetValue<List<Decision>>(cacheKey, out var cached))
            {
                return cached;
            }

            try
            {
                await _lockDecisions.WaitAsync();
                if (_cache.TryGetValue(cacheKey, out cached))
                {
                    return cached;
                }

                var result = await _httpClient.GetStringAsync($"search?org={organizationId}&type=Γ.3.2&sort=recent");
                var response = JsonSerializer.Deserialize<DecisionSearchResponse>(result, _serializerOptions);

                _cache.Set(cacheKey, response.Decisions, TimeSpan.FromDays(1));
                return response.Decisions;
            }
            finally
            {
                _lockDecisions.Release();
            }
        }

        public async Task<Decision> GetDecision(string ada)
        {
            var cacheKey = CacheKeys.DecisionById(ada);
            if (_cache.TryGetValue<Decision>(cacheKey, out var cached))
            {
                return cached;
            }

            try
            {
                await _lockDecisions.WaitAsync();
                if (_cache.TryGetValue(cacheKey, out cached))
                {
                    return cached;
                }

                var result = await _httpClient.GetStringAsync($"decisions/{ada}");
                var response = JsonSerializer.Deserialize<Decision>(result, _serializerOptions);

                _cache.Set(cacheKey, response, TimeSpan.FromDays(1));
                return response;
            }
            finally
            {
                _lockDecisions.Release();
            }
        }
    }
}