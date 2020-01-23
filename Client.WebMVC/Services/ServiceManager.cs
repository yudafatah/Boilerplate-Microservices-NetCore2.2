using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Service.WebApi.Catalog.Services
{
    public class ServiceManager
    {
        private readonly HttpClient _httpClient;
        public ServiceManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Object> GetAsync(string baseUrl, )
        {
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
