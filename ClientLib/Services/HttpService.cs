using ClientLib.Infra;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientLib.Services
{
    public class HttpService : IHttpService
    {
        private readonly string baseUrlAddress;
        public HttpService()
        {
            baseUrlAddress = "http://localhost:5001/api";
        }

        public async Task<R> PostAsync<T, R>(string url, T payload, string token = "")
        {
            try
            {
                R result = default(R);
                using (HttpClient httpClient = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(token))
                        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var paylodInfo = JsonConvert.SerializeObject(payload);
                    var jsonContent = new StringContent(paylodInfo,
                                        Encoding.UTF8,
                                        "application/json");

                    var response = await httpClient.PostAsync($"{baseUrlAddress}{url}", jsonContent);
                    var responseresult = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<R>(responseresult);
                }

                return result;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<R> GetAsync<R>(string url, string token = "")
        {
            try
            {
                R result = default(R);
                using (HttpClient httpClient = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(token))
                        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await httpClient.GetAsync($"{baseUrlAddress}{url}");
                    result = JsonConvert.DeserializeObject<R>(await response.Content.ReadAsStringAsync());
                }

                return result;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
