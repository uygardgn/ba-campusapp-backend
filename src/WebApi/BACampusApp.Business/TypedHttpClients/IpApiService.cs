using System.Net.Http.Json;

namespace BACampusApp.Business.TypedHttpClients
{
    public class IpApiService
    {
        private readonly HttpClient _httpClient;
        public IpApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://ip-api.com/json/");
        }

        /// <summary>
        /// Kullanıcının IP'sine göre 2 haneli ülke kodunu döner.
        /// </summary>
        /// <returns>string 2 haneli ülke kodu döner.</returns>
        public async Task<string?> GetCountryCodeOfCurrentUserByIpAsync()
        {
            var countryCodeResponseModel = await _httpClient.GetFromJsonAsync<CountryCodeResponseModel>("?fields=countryCode");
            return countryCodeResponseModel?.countryCode;
        }

        private class CountryCodeResponseModel
        {
            public string? countryCode { get; set; }
        }
    }
}
