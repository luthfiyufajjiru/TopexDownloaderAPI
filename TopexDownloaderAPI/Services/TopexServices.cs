using System.Text;

namespace TopexDownloaderAPI.Services
{
    public static class TopexServices
    {
        public static async Task<List<TopexDataModel>> GetFromTopex(double north, double west, double east, double south, double mag)
        {
            List<TopexDataModel> result = new();
            try
            {
                HttpClient _Client = new();

                var uri = "https://topex.ucsd.edu/cgi-bin/get_data.cgi";

                var data = new Dictionary<string, string>();
                data.Add("north", $"{north}".Replace(",", "."));
                data.Add("west", $"{west}".Replace(",", "."));
                data.Add("east", $"{east}".Replace(",", "."));
                data.Add("south", $"{south}".Replace(",", "."));
                data.Add("mag", $"{mag}".Replace(",", "."));

                using HttpContent formContent = new FormUrlEncodedContent(data);
                var request = await _Client.PostAsync(uri, formContent);
                var _result = (string)(await request.Content.ReadAsStringAsync());

                result = TopexDataModel.Serialize(_result);
                
            }
            catch (Exception)
            {

                throw new ApplicationException("request invalid");
            }

            return result;
        }
    }
}
