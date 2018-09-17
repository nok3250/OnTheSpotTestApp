using Newtonsoft.Json;
using OnTheSpotTestApp.FbOauth.Entities;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnTheSpotTestApp.Services
{
    public class FacebookService
    {
        public async Task<string> GetPictureAsync(string accessToken)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=picture.type(large)&access_token={accessToken}");
            var response = JsonConvert.DeserializeObject<FacebookPicture>(json);
            var url = response.Picture.Data.Url;
            return url;
        }
    }
}
