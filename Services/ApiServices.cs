using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using Newtonsoft.Json;
using Pogodynka.Models;
using Xamarin.Essentials;

namespace Pogodynka.Services
{
    public class ApiServices
    {
        public async Task LoginAsync(string email, string password)
        {
            var body = new LoginModel(email, password);
            var response = await "https://pogodynka-api-app.azurewebsites.net/api/account/login"
                .PostJsonAsync(body);
            var jwt = await response.ResponseMessage.Content.ReadAsStringAsync();
            await SecureStorage.SetAsync("jwt", string.Concat("Bearer ", jwt));
        }

        public async Task RegisterAsync(string email, string password, string confirmPassword)
        {
            var body = new RegisterModel(email, password, confirmPassword);
            var response = await "https://pogodynka-api-app.azurewebsites.net/api/account/register"
                .PostJsonAsync(body);
        }

        public async Task AddPost(int TempC, string Description, FileResult photoResult)
        {
            byte[] photoInBytes;
            using(MemoryStream ms = new MemoryStream())
            {
                var stream = await photoResult.OpenReadAsync();
                stream.CopyTo(ms);
                photoInBytes = ms.ToArray();
            }

            var body = new PostModel(TempC, Description, photoInBytes);
            var response = await "https://pogodynka-api-app.azurewebsites.net/api/pogodynka"
                .WithHeader("Authorization", await SecureStorage.GetAsync("jwt"))
                .PostJsonAsync(body);
        }

        public async Task<ObservableCollection<GetPostModel>> GetPosts()
        {
            var response = await "https://pogodynka-api-app.azurewebsites.net/api/pogodynka"
                .WithHeader("Authorization", await SecureStorage.GetAsync("jwt"))
                .GetAsync()
                .ReceiveJson<ObservableCollection<GetPostModel>>();
            return response;
        }

        public async Task DeletePost(int id)
        {
            await $"https://pogodynka-api-app.azurewebsites.net/api/pogodynka/{id}"
                .WithHeader("Authorization", await SecureStorage.GetAsync("jwt"))
                .DeleteAsync();
        }
    }
}
