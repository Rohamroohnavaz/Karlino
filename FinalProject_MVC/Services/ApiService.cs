using FinalProject_MVC.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<T> GetAsync<T>(string endpoint)
    {
        await SetAuthHeader();
        var response = await _httpClient.GetAsync(endpoint);
        return await HandleResponse<T>(response);
    }

    public async Task<T> PostAsync<T>(string endpoint, object data)
    {
        await SetAuthHeader();

        var fullUrl = new Uri(_httpClient.BaseAddress, endpoint).ToString();
        System.Console.WriteLine($"Calling API at: {fullUrl}");

        var content = new StringContent(
             JsonConvert.SerializeObject(data),
             Encoding.UTF8,
             "application/json");

        var response = await _httpClient.PostAsync(endpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            string friendlyMessage = "خطای ناشناخته‌ای رخ داده است. لطفاً دوباره تلاش کنید.";

            try
            {
                var errorObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(errorContent);
                if (errorObj != null)
                {
                    if (errorObj.ContainsKey("message"))
                        friendlyMessage = errorObj["message"].ToString();
                    else if (errorObj.ContainsKey("title"))
                        friendlyMessage = errorObj["title"].ToString();
                    else if (errorObj.ContainsKey("error"))
                        friendlyMessage = errorObj["error"].ToString();
                }
            }
            catch
            {
            }

            if (friendlyMessage == "خطای ناشناخته‌ای رخ داده است. لطفاً دوباره تلاش کنید.")
            {
                friendlyMessage = response.StatusCode switch
                {
                    System.Net.HttpStatusCode.Unauthorized => "نام کاربری یا رمز عبور اشتباه است.",
                    System.Net.HttpStatusCode.BadRequest => "اطلاعات وارد شده معتبر نیست.",
                    System.Net.HttpStatusCode.NotFound => "کاربر یافت نشد.",
                    System.Net.HttpStatusCode.Forbidden => "شما مجوز دسترسی به این بخش را ندارید.",
                    System.Net.HttpStatusCode.InternalServerError => "خطای داخلی سرور. لطفاً بعداً تلاش کنید.",
                    _ => $"خطا در ارتباط با سرور (کد: {response.StatusCode})"
                };
            }

            throw new Exception(friendlyMessage);
        }

        return await HandleResponse<T>(response);
    }




    public async Task<T> PutAsync<T>(string endpoint, object data)
    {
        await SetAuthHeader();
        var content = new StringContent(
            JsonConvert.SerializeObject(data),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PutAsync(endpoint, content);
        return await HandleResponse<T>(response);
    }

    public async Task DeleteAsync(string endpoint)
    {
        await SetAuthHeader();
        var response = await _httpClient.DeleteAsync(endpoint);
        response.EnsureSuccessStatusCode();
    }

    private async Task SetAuthHeader()
    {
        var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");

        System.Diagnostics.Debug.WriteLine($"Token: {token ?? "NULL"}");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }

    private async Task<T> HandleResponse<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
        throw new HttpRequestException($"Error: {response.StatusCode}");
    }
}