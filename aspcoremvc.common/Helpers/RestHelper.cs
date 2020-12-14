using System.Net.Http;
using System.Threading.Tasks;
using Common.Dto;
using RestSharp;

namespace Common.Helpers
{
    public class RestHelper
    {
        public static IRestResponse GetRequest(string apiKey, string requestPath, string apiUrl = null, 
            bool isPost = true, object body = null, string token = null)
        {
            apiUrl = apiUrl ?? HttpHelper.GetConfig<string>("AppConfig:WebApiPath");
            var client = new RestClient(apiUrl);

            var request = new RestRequest(requestPath)
            {
                RequestFormat = DataFormat.Json,
                Method = isPost ? Method.POST : Method.GET
            };

            if (body != null)
                request.AddBody(body);

            if (token != null)
                request.AddHeader("Authorization", $"Bearer {token}");

            request.AddHeader("ReqAdminUserId", HttpHelper.GetActiveUserId().ToString());

            var restResponse = client.Execute(request);

            if (restResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var tokenrequest = GetRequest(apiKey, "api/token", apiUrl, true, apiKey);

                if (tokenrequest.StatusCode != System.Net.HttpStatusCode.BadRequest)
                {
                    var dto = tokenrequest.Content.JsonDecode<TokenResponse>();
                    return GetRequest(apiKey, requestPath, apiUrl, isPost, body, dto.Token);
                }
            }

            return restResponse;
        }

        public static async Task<T1> ApiRequest<T1>(string endpoint, object postData, string token = null)
        {
            var uri = HttpHelper.GetConfig<string>("AppConfig:ApiUrl") + endpoint;

            using (var httpClient = new HttpClient())
            {
                if (token != null)
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                var response = await httpClient.PostAsJsonAsync(uri, postData);
                //var response = await httpClient.PostAsync("api/AgentCollection", new StringContent(postData.JsonEncode()));
                response.EnsureSuccessStatusCode();
                T1 content = await response.Content.ReadAsAsync<T1>();

                return content;
            }
        }
    }
}