using EasyWord.Server.Services.ImplService;
using Microsoft.Extensions.Configuration;
using Moq;

namespace EasyWord.Server.Test.Service;

public class SentenceComposingServiceTest
{
    [Fact]
    public async Task ComposeAsync_ReturnsValidResult()
    {
        //调用Test项目中的UserSecrets
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<SentenceComposingServiceTest>();
        var configuration = builder.Build();
        // 创建一个模拟 IConfiguration
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(config => config["AzureChatCompletionFactory.ModelId"]).Returns(configuration["AzureChatCompletionFactory.ModelId"]);
        configurationMock.Setup(config => config["AzureChatCompletionFactory.Endpoint"]).Returns(configuration["AzureChatCompletionFactory.Endpoint"]);
        configurationMock.Setup(config => config["AzureChatCompletionFactory.ApiKey"]).Returns(configuration["AzureChatCompletionFactory.ApiKey"]);

        var _configuration = configurationMock.Object;


        var chatCompletionFactory = new AzureChatCompletionFactory(_configuration);
        var sentenceComposingService = new SentenceComposingService(chatCompletionFactory);
        // Act
        var result = await sentenceComposingService.ComposeAsync("A black thermos on the table");

        // Assert
        Assert.NotNull(result);
    }
}