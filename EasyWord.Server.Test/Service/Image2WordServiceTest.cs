using EasyWord.Server.Services.ImplService;
using EasyWord.Server.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.ChatCompletion;
using Moq;

namespace EasyWord.Server.Test.Service;

public class Image2WordServiceTest
{
    
    [Fact]
    public async Task ComposeAsync_ReturnsValidResult()
    {
        // 创建一个模拟 IConfiguration
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(config => config["AzureChatCompletionFactory.ModelId"]).Returns("gpt-35-turbo");
        configurationMock.Setup(config => config["AzureChatCompletionFactory.Endpoint"]).Returns("https://openai-demo-internal-gateway.azure-api.net/");
        configurationMock.Setup(config => config["AzureChatCompletionFactory.ApiKey"]).Returns("123");

        var _configuration = configurationMock.Object;


        var chatCompletionFactory = new AzureChatCompletionFactory(_configuration);
        var image2WordService = new Image2WordService(chatCompletionFactory);
        // Act
        var result = await image2WordService.ComposeAsync("A black thermos on the table");

        // Assert
        Assert.NotNull(result);
    }
}