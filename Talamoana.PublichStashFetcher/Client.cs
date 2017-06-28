using Jil;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Talamoana.PublichStashFetcher
{
    public class Client
    {
        const string baseUri = "http://api.pathofexile.com/public-stash-tabs?id=";
        const string latestUrl = "http://poe-rates.com/actions/getLastChangeId.php";

        public async Task<string> GetLatestChangeId()
        {
            using (var cli = new HttpClient())
            {
                var response = await cli.GetStringAsync(latestUrl).ConfigureAwait(false);
                var json = JSON.DeserializeDynamic(response);
                return json.changeId;
            }
                
        }

        public async Task<Stream> GetPublicStashChange(string changeId)
        {
            using (var cli = new HttpClient(DefaultHandler, true))
            {
                var response = await cli.GetAsync($"{baseUri}{changeId}");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception($"Error: {response.StatusCode} {response.ReasonPhrase}");

                return await response
                    .Content
                    .ReadAsStreamAsync()
                    .ConfigureAwait(false);
            }
        }

        private static HttpMessageHandler DefaultHandler =>
            new HttpClientHandler
            {
                AutomaticDecompression = 
                    System.Net.DecompressionMethods.Deflate | 
                    System.Net.DecompressionMethods.GZip,
                UseDefaultCredentials = false,
                UseCookies = false
            };
    }
}
