using System;
using System.Collections.Generic;
using System.Text;
using Jil;
using System.Threading.Tasks;
using Talamoana.Domain.Core.Stash;
using System.IO;

namespace Talamoana.PublichStashFetcher
{
    public class StashFetcher
    {
        public string NextChangeId { get; set; }

        public async Task InitializeWithLastChangeId()
        {
            var cli = new Client();
            NextChangeId = await cli
                .GetLatestChangeId()
                .ConfigureAwait(false);
        }

        public async Task<StashResponse> GetNextAsync()
        {
            var cli = new Client();
            using (var stream = await cli.GetPublicStashChange(NextChangeId).ConfigureAwait(false))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var response = JSON.Deserialize<StashResponse>(reader);
                NextChangeId = response.NextChangeId;

                return response;
            }
        }
    }
}
