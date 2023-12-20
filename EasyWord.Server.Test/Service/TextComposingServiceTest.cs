using EasyWord.Server.Services.ImplService;
using Microsoft.Extensions.Configuration;
using Moq;

namespace EasyWord.Server.Test.Service;

public class TextComposingServiceTest
{
    [Fact]
    public async Task ComposeAsync_ReturnsValidResult()
    {
        //调用Test项目中的UserSecrets
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<Image2WordServiceTest>();
        var configuration = builder.Build();
        // 创建一个模拟 IConfiguration
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(config => config["AzureChatCompletionFactory.ModelId"]).Returns(configuration["AzureChatCompletionFactory.ModelId"]);
        configurationMock.Setup(config => config["AzureChatCompletionFactory.Endpoint"]).Returns(configuration["AzureChatCompletionFactory.Endpoint"]);
        configurationMock.Setup(config => config["AzureChatCompletionFactory.ApiKey"]).Returns(configuration["AzureChatCompletionFactory.ApiKey"]);

        var _configuration = configurationMock.Object;


        var chatCompletionFactory = new AzureChatCompletionFactory(_configuration);
        var textComposingService = new TextComposingService(chatCompletionFactory);
        // Act
        var result = await textComposingService.ComposeTextAsync("apple ear");

        // Assert
        Assert.NotNull(result);
    }
}