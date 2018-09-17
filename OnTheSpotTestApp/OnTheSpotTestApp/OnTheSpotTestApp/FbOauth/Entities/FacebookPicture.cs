using Newtonsoft.Json;

namespace OnTheSpotTestApp.FbOauth.Entities
{
    internal class FacebookPicture
    {
        [JsonProperty("picture")]
        public Picture Picture { get; set; }
    }

    internal class Picture
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    internal class Data
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("is_silhouette")]
        public bool IsSilhouette { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }
}
