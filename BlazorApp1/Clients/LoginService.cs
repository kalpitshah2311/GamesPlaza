using BlazorApp1.Models;
using Newtonsoft.Json;

namespace BlazorApp1.Clients
{
    public interface ILoginService
    {
        //Task<Result<UserSession>> Login(Login loginObj);
    }

    public class LoginService : ILoginService
    {
        //private readonly IHttpClientFactory clientFactory;
        //public LoginService(IHttpClientFactory clientFactory)
        //{
        //    this.clientFactory = clientFactory;
        //}

        //public async Task<Result<UserSession>> Login(Login loginObj)
        //{
        //    var client = clientFactory.CreateClient("client");
        //    var response = await client.PostAsJsonAsync("UserAuth/auth", loginObj);
        //    response.EnsureSuccessStatusCode();
        //    string content = await response.Content.ReadAsStringAsync();
        //    var obj = JsonConvert.DeserializeObject<Result<UserSession>>(content);
        //    return obj!;
        //}
    }
}
