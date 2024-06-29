using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNEWSAPP.Service
{
    public class ApiKeyService
    {
        public ApiKeyService() { }


        public async Task SetApiKey(string apiKey)
        {
            await SecureStorage.SetAsync("apiKey", apiKey);
        }

    }
}
