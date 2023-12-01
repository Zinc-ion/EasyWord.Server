using EasyWord.Server.Services.ImplService;
using Microsoft.Extensions.Configuration;
using Moq;

namespace EasyWord.Server.Test.Service;

public class TextComposingServiceTest
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
        var textComposingService = new TextComposingService(chatCompletionFactory);
        // Act
        var result = await textComposingService.ComposeTextAsync(new string[] {"apple","ear"});

        // Assert
        Assert.NotNull(result);
    }
}