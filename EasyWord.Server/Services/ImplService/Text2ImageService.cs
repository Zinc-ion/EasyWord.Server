using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RestSharp;
using TheSalLab.GeneralReturnValues;

namespace EasyWord.Server.Services.ImplService;

public class Text2ImageService : IText2ImageService
{
    public async Task<string> ComposeAsync(string sentence)
    {
        var client = new RestClient($"https://aip.baidubce.com/rpc/2.0/ai_custom/v1/wenxinworkshop/text2image/sd_xl?access_token={GetAccessToken()}");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "application/json");
        //设置body参数
        var body = $"{{\"prompt\":\"{sentence}\",\"size\":\"1024x1024\",\"n\":1,\"steps\":20,\"sampler_index\":\"Euler a\"}}";
        request.AddParameter("application/json", body, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        Console.WriteLine(response.Content);
        return response.Content;

    }

    

    public string GetAccessToken()
    {
        //从UserSecret中获取敏感信息
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<Text2ImageService>();
        var configuration = builder.Build();

        var api_key = configuration["sd_xl.ApiKey"];
        var secret_key = configuration["sd_xl.SecretKey"];

        var client = new RestClient($"https://aip.baidubce.com/oauth/2.0/token");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddParameter("grant_type", "client_credentials");
        request.AddParameter("client_id", api_key);
        request.AddParameter("client_secret", secret_key);
        IRestResponse response = client.Execute(request);
        Console.WriteLine(response.Content);
        var result = JsonConvert.DeserializeObject<dynamic>(response.Content);
        return result.access_token.ToString();
    }
}