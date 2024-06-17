using HR.Apografi.Constants;
using HR.Apografi.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace HR.Apografi.Services
{
    public class ApografiService
    {
        private static SemaphoreSlim _lockOrganizations = new SemaphoreSlim(1);
        private static SemaphoreSlim _lockOrganization = new SemaphoreSlim(1);
        private static SemaphoreSlim _lockDictionaries = new SemaphoreSlim(1);
        private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private static readonly JsonSerializerOptions _serializerOptionsSearch = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        public ApografiService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<List<OrganizationLite>> GetOrganizationsAsync()
        {
            var cacheKey = CacheKeys.Organizations;
            if (_cache.TryGetValue<List<OrganizationLite>>(cacheKey, out var cached))
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

                var result = await _httpClient.GetStringAsync("public/organizations");
                var apografiResponse = JsonSerializer.Deserialize<ApografiResponse<List<OrganizationLite>>>(result, _serializerOptions);

                _cache.Set(cacheKey, apografiResponse.Data, TimeSpan.FromDays(1));
                return apografiResponse.Data;
            }
            finally
            {
                _lockOrganizations.Release();
            }
        }

        public async Task<Organization> GetOrganizationAsync(string code)
        {
            var cacheKey = CacheKeys.OrganizationById(code);
            if (_cache.TryGetValue<Organization>(cacheKey, out var cached))
            {
                return cached;
            }

            try
            {
                await _lockOrganization.WaitAsync();
                if (_cache.TryGetValue(cacheKey, out cached))
                {
                    return cached;
                }

                var result = await _httpClient.GetStringAsync($"public/organizations/{code}");
                var apografiResponse = JsonSerializer.Deserialize<ApografiResponse<Organization>>(result, _serializerOptions);

                _cache.Set(cacheKey, apografiResponse.Data, TimeSpan.FromDays(1));
                return apografiResponse.Data;
            }
            finally
            {
                _lockOrganization.Release();
            }
        }

        public async Task<List<OrganizationLite>> SearchOrganizationsAsync(SearchOrganizationRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync($"public/organizations/search", request, options: _serializerOptionsSearch);
            var resultString = await result.Content.ReadAsStringAsync();
            var apografiResponse = JsonSerializer.Deserialize<ApografiResponse<List<OrganizationLite>>>(resultString, _serializerOptions);

            return apografiResponse.Data;
        }

        public async Task<List<OrganizationType>> GetOrganizationTypesAsync()
        {
            var cacheKey = CacheKeys.OrganizationTypes;
            if (_cache.TryGetValue<List<OrganizationType>>(cacheKey, out var cached))
            {
                return cached;
            }

            try
            {
                await _lockDictionaries.WaitAsync();
                if (_cache.TryGetValue(cacheKey, out cached))
                {
                    return cached;
                }

                var result = await _httpClient.GetStringAsync("public/metadata/dictionary/OrganizationTypes");
                var apografiResponse = JsonSerializer.Deserialize<ApografiResponse<List<OrganizationType>>>(result, _serializerOptions);

                _cache.Set(cacheKey, apografiResponse.Data, TimeSpan.FromDays(1));
                return apografiResponse.Data;
            }
            finally
            {
                _lockDictionaries.Release();
            }
        }

        public async Task<List<Function>> GetFunctionsAsync()
        {
            var cacheKey = CacheKeys.Functions;
            if (_cache.TryGetValue<List<Function>>(cacheKey, out var cached))
            {
                return cached;
            }

            try
            {
                await _lockDictionaries.WaitAsync();
                if (_cache.TryGetValue(cacheKey, out cached))
                {
                    return cached;
                }

                var result = await _httpClient.GetStringAsync("public/metadata/dictionary/Functions");
                var apografiResponse = JsonSerializer.Deserialize<ApografiResponse<List<Function>>>(result, _serializerOptions);

                _cache.Set(cacheKey, apografiResponse.Data, TimeSpan.FromDays(1));
                return apografiResponse.Data;
            }
            finally
            {
                _lockDictionaries.Release();
            }
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            var cacheKey = CacheKeys.Countries;
            if (_cache.TryGetValue<List<Country>>(cacheKey, out var cached))
            {
                return cached;
            }

            try
            {
                await _lockDictionaries.WaitAsync();
                if (_cache.TryGetValue(cacheKey, out cached))
                {
                    return cached;
                }

                var result = await _httpClient.GetStringAsync("public/metadata/dictionary/Countries");
                var apografiResponse = JsonSerializer.Deserialize<ApografiResponse<List<Country>>>(result, _serializerOptions);

                _cache.Set(cacheKey, apografiResponse.Data, TimeSpan.FromDays(1));
                return apografiResponse.Data;
            }
            finally
            {
                _lockDictionaries.Release();
            }
        }

        public async Task<List<City>> GetCitiesAsync()
        {
            var cacheKey = CacheKeys.Cities;
            if (_cache.TryGetValue<List<City>>(cacheKey, out var cached))
            {
                return cached;
            }

            try
            {
                await _lockDictionaries.WaitAsync();
                if (_cache.TryGetValue(cacheKey, out cached))
                {
                    return cached;
                }

                var result = await _httpClient.GetStringAsync("public/metadata/dictionary/Cities");
                var apografiResponse = JsonSerializer.Deserialize<ApografiResponse<List<City>>>(result, _serializerOptions);

                _cache.Set(cacheKey, apografiResponse.Data, TimeSpan.FromDays(1));
                return apografiResponse.Data;
            }
            finally
            {
                _lockDictionaries.Release();
            }
        }
    }
}
