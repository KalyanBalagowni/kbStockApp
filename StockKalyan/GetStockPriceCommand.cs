using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace StockKalyan
{
    public class DetailQuote
    {
        [JsonProperty("05. price")]
        public string Price { get; set; }
    }
    
    public class GlobalQuote
    {
        [JsonProperty("Global Quote")]
        public DetailQuote Quote { get; set; }
    }

    public class GetStockPriceCommand
    {
        private string _apiURl = "https://www.alphavantage.co/";
        private string _ticker = "TSLA";
        private string _apiKey = "Z3WS9AXZH2XWNFEK";

        public GetStockPriceCommand(string ticker)
        {
            this._ticker = ticker;
        }

        public string Handle()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_apiURl);
            string query = "query?function=GLOBAL_QUOTE&symbol={0}&apikey={1}";

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            String response = client.GetStringAsync(string.Format(query, _ticker.ToLower(), _apiKey)).Result;
            GlobalQuote data = JsonConvert.DeserializeObject<GlobalQuote>(response);

            client.Dispose();
            return data.Quote.Price;
        }
    }

}
