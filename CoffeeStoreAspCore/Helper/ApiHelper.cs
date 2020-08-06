using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoffeeStoreAspCore.Helper
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }
        public static void inint()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("");
            ApiClient.DefaultRequestHeaders.Accept.Clear();

        }
    }
}
