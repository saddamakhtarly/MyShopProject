using MyShopCommonLib;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyShop.Models
{
    public class GlobalFunctions
    {
        private string serviceURL = "";
        public GlobalFunctions()
        {
            serviceURL = GlobalVariables.serviceURL;
        }
        public async static Task<bool> GetCameraPermission()
        {
            bool resp = false;
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
            {
                // ask for permission
                status = await Permissions.RequestAsync<Permissions.Camera>();
                if (status == PermissionStatus.Granted)
                {
                    resp = true;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Message", "Camera Permission Denied", "Ok");
                }
            }
            else
            {
                resp = true;
            }
            return resp;
        }
        public async static Task<bool> GetStorageWritePermission()
        {
            bool resp = false;
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (status != PermissionStatus.Granted)
            {
                // ask for permission
                status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (status == PermissionStatus.Granted)
                {
                    resp = true;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Message", "StorageWrite Permission Denied", "Ok");
                }
            }
            else
            {
                resp = true;
            }
            return resp;
        }
        public async static Task<bool> GetStorageReadPermission()
        {
            bool resp = false;
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (status != PermissionStatus.Granted)
            {
                // ask for permission
                status = await Permissions.RequestAsync<Permissions.StorageRead>();
                if (status == PermissionStatus.Granted)
                {
                    resp = true;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Message", "StorageWrite Permission Denied", "Ok");
                }
            }
            else
            {
                resp = true;
            }
            return resp;
        }

        public async Task<RegisterResponse> RegisterUser(tblUser user)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{serviceURL}api/User/RegisterUser"))
                {
                    string jsonData = JsonConvert.SerializeObject(user);
                    request.Content = new StringContent(jsonData);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = await httpClient.SendAsync(request);
                    string jsonResp = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RegisterResponse>(jsonResp);
                }
            }
        }
        public async Task<LoginResponse> ValidateUser(LoginRequest rqst)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{serviceURL}api/User/ValidateUser"))
                {
                    string jsonData = JsonConvert.SerializeObject(rqst);
                    request.Content = new StringContent(jsonData);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = await httpClient.SendAsync(request);
                    string jsonResp = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<LoginResponse>(jsonResp);
                }
            }
        }
        public async Task<CategoryResponse> GetCategories()
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{serviceURL}api/Shop/GetCategories"))
                {
                    var resp = await httpClient.SendAsync(request);
                    string jsonResp = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CategoryResponse>(jsonResp);
                }
            }
        }

        public async Task<SaveCategoryResponse> SaveCategory(Category category)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{serviceURL}api/Shop/SaveCategory"))
                {
                    string jsonData = JsonConvert.SerializeObject(category);
                    request.Content = new StringContent(jsonData);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = await httpClient.SendAsync(request);
                    string jsonResp = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<SaveCategoryResponse>(jsonResp);
                }
            }
        }
        public async Task<ProductResponse> SaveProduct(Product product)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{serviceURL}api/Shop/SaveProduct"))
                {
                    string jsonData = JsonConvert.SerializeObject(product);
                    request.Content = new StringContent(jsonData);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = await httpClient.SendAsync(request);
                    string jsonResp = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ProductResponse>(jsonResp);
                }
            }
        }
        public async Task<ProductImageSaveResponse> SaveProductImages(int ProductId,List<MediaFile> images)
        {
            using (var httpClient = new HttpClient())
            {
                string url = $"{serviceURL}api/Shop/SaveProductImages?productId={ProductId}";
                var content = new MultipartFormDataContent();
                foreach (MediaFile media in images)
                {
                    content.Add(new StreamContent(media.GetStream()), "\"file\"", $"\"{DateTime.Now.ToBinary()}.jpg\"");
                }
                var httpResponseMessage = await httpClient.PostAsync(url, content);
                string resp = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductImageSaveResponse>(resp);
            }
        }
        public async Task<GetProductResponse> GetProducts(int categoryId = 0)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{serviceURL}api/Shop/GetProducts?categoryId={categoryId}"))
                {
                    var resp = await httpClient.SendAsync(request);
                    string jsonResp = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<GetProductResponse>(jsonResp);
                }
            }
        }
        public async Task<GetProductResponse> SearchProducts(string keyword)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{serviceURL}api/Shop/SearchProduct?keyword={keyword}"))
                {
                    var resp = await httpClient.SendAsync(request);
                    string jsonResp = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<GetProductResponse>(jsonResp);
                }
            }
        }
        public async Task<GetProductImageResponse> GetProductImage(int ProductId )
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{serviceURL}api/Shop/GetProductImage?productId={ProductId}"))
                {
                    var resp = await httpClient.SendAsync(request);
                    string jsonResp = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<GetProductImageResponse>(jsonResp);
                }
            }
        }

        public async Task<SaveCartResponse> SaveCart(Cart cart)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{serviceURL}api/Shop/SaveCart"))
                {
                    string jsonData = JsonConvert.SerializeObject(cart);
                    request.Content = new StringContent(jsonData);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = await httpClient.SendAsync(request);
                    string jsonResp = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<SaveCartResponse>(jsonResp);
                }
            }
        }

        public async Task<SaveAddressResponce> SaveAddress(Address adress)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{serviceURL}api/User/Saveaddress"))
                {
                    string jsonData = JsonConvert.SerializeObject(adress);
                    request.Content = new StringContent(jsonData);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = await httpClient.SendAsync(request);
                    string jsonResp = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<SaveAddressResponce>(jsonResp);
                }
            }
        }


        //public async Task<SaveAddressResponce> SaveShippingAddress(ShippingAddress adress)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{serviceURL}api/Order/SaveShippingAddress"))
        //        {
        //            string jsonData = JsonConvert.SerializeObject(adress);
        //            request.Content = new StringContent(jsonData);
        //            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        //            var response = await httpClient.SendAsync(request);
        //            string jsonResp = await response.Content.ReadAsStringAsync();
        //            return JsonConvert.DeserializeObject<SaveAddressResponce>(jsonResp);
        //        }
        //    }
        //}


        public async Task<GetCartResponse> GetCart(int UserId = 0)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{serviceURL}api/Shop/GetCart?UserId={UserId}"))
                {
                    var resp = await httpClient.SendAsync(request);
                    string jsonResp = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<GetCartResponse>(jsonResp);
                }
            }
        }

        public async Task<GetAddressResponce> GetAddress(int userId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{serviceURL}api/Shop/GetAddress?userId={userId}"))
                {
                    var resp = await httpClient.SendAsync(request);
                    string jsonResp = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<GetAddressResponce>(jsonResp);
                }
            }
        }

        public async Task<CreateOrderresponse> CreateOrder(Order order)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{serviceURL}api/Order/CreateOrder"))
                {
                    string jsonData = JsonConvert.SerializeObject(order);
                    request.Content = new StringContent(jsonData);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = await httpClient.SendAsync(request);
                    string jsonResp = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CreateOrderresponse>(jsonResp);
                }
            }
        }
        public async Task<GetOrderResponse> GetOrders(int UserId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{serviceURL}api/Order/GetOrders?UserId={UserId}"))
                {
                    var resp = await httpClient.SendAsync(request);
                    string jsonResp = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<GetOrderResponse>(jsonResp);
                }
            }
        }

        public async Task<GetOrderDetailsResponse> GetOrderDetails(int OrderId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{serviceURL}api/Order/GetOrdersDetails?OrderId={OrderId}"))
                {
                    var resp = await httpClient.SendAsync(request);
                    string jsonResp = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<GetOrderDetailsResponse>(jsonResp);
                }
            }
        }
    }
}
