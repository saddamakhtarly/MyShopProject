using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models
{
    public class ApiFunctions
    {
        public async Task<ApiLoginResponce> apilogin(string username, string pass)
        {
            var handler = new HttpClientHandler();
            handler.UseCookies = false;

            using (var httpClient = new HttpClient(handler))
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://shipmunkauth.azurewebsites.net/connect/token"))
                {
                    request.Headers.TryAddWithoutValidation("Cookie", "ARRAffinity=9f76b54f03145f3a2973c78876ae5d8ec0437e55210a9a68758a05c81f2d2a74");

                    request.Content = new StringContent("grant_type=password&username=" + username + "&password=" + pass + "&client_id=Shipmunk.Password&client_secret=secret&scope=openid coreapis profile");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);
                    string jsonResp = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiLoginResponce>(jsonResp);
                }
            }
        }

        public async Task<ProductResponce> apiproduct(string token)
        {
            var handler = new HttpClientHandler();
            handler.UseCookies = false;

            using (var httpClient = new HttpClient(handler))
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://shipmunkapi.azurewebsites.net/items/products?orderBy=createdAt&orderByDir=-1&page=1&pageSize=10"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + token);
                    request.Headers.TryAddWithoutValidation("Cookie", "ARRAffinity=9f76b54f03145f3a2973c78876ae5d8ec0437e55210a9a68758a05c81f2d2a74");

                    var response = await httpClient.SendAsync(request);
                    string jsonResp = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ProductResponce>(jsonResp);
                }
            }
        }
    }
}
