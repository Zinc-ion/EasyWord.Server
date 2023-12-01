using EasyWord.Server.Services.ImplService;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.ChatCompletion;
using Moq;

namespace EasyWord.Server.Test.Service;

public class AzureChatCompletionFactoryTest
{
    [Fact]
    public void GetChatCompletion_ReturnsValidInstance()
    {
        // Arrange
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(config => config["AzureChatCompletionFactory.ModelId"]).Returns("MockedModelId");
        configurationMock.Setup(config => config["AzureChatCompletionFactory.Endpoint"]).Returns("https://MockedEndpoint");
        configurationMock.Setup(config => config["AzureChatCompletionFactory.ApiKey"]).Returns("MockedApiKey");

        var azureChatCompletionFactory = new AzureChatCompletionFactory(configurationMock.Object);

        // Act
        var chatCompletion = azureChatCompletionFactory.GetChatCompletion();

        // Assert
        Assert.NotNull(chatCompletion);
        Assert.IsType<AzureChatCompletion>(chatCompletion);
    }

    [Fact]
    public void GetChatCompletion_ThrowsException_WhenConfigurationIsInvalid()
    {
        // Arrange
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(config => config["AzureChatCompletionFactory.ModelId"]).Returns((string)null);
        configurationMock.Setup(config => config["AzureChatCompletionFactory.Endpoint"]).Returns("https://MockedEndpoint");
        configurationMock.Setup(config => config["AzureChatCompletionFactory.ApiKey"]).Returns("MockedApiKey");

        var azureChatCompletionFactory = new AzureChatCompletionFactory(configurationMock.Object);

        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => azureChatCompletionFactory.GetChatCompletion());
    }
}