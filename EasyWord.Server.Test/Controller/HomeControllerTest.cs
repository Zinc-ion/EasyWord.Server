using EasyWord.Server.Commands;
using EasyWord.Server.Controllers;
using EasyWord.Server.Services;
using TheSalLab.GeneralReturnValues;
using Moq;
using Microsoft.AspNetCore.Http;

namespace EasyWord.Server.Test.Controller;

public class HomeControllerTest
{
    [Fact]
    public async Task WordComposingAsync_ReturnsValidResult()
    {
        // Arrange
        var sentenceComposingServiceMock = new Mock<ISentenceComposingService>();
        sentenceComposingServiceMock
            .Setup(service => service.ComposeAsync(It.IsAny<string>()))
            .ReturnsAsync("MockedSentence");

        var controller = new HomeController(
            sentenceComposingServiceMock.Object,
            Mock.Of<ITextComposingService>(),
            Mock.Of<IImage2WordService>());

        var command = new SentenceCommand { Word = "TestWord" };

        // Act
        var result = await controller.SentenceComposingAsync(command);

        // Assert
        Assert.IsType<ServiceResultViewModel<string>>(result);
        Assert.Equal(ServiceResultStatus.Succeeded, result.Status);
    }

    [Fact]
    public async Task TextComposingAsync_ReturnsValidResult()
    {
        // Arrange
        var textComposingServiceMock = new Mock<ITextComposingService>();
        textComposingServiceMock
            .Setup(service => service.ComposeTextAsync(It.IsAny<string>()))
            .ReturnsAsync("MockedText");

        var controller = new HomeController(
            Mock.Of<ISentenceComposingService>(),
            textComposingServiceMock.Object,
            Mock.Of<IImage2WordService>());

        var command = new TextCommand() { words = "Word1 Word2"};

        // Act
        var result = await controller.TextComposingAsync(command);

        // Assert
        Assert.IsType<ServiceResultViewModel<string>>(result);
        Assert.Equal(ServiceResultStatus.Succeeded, result.Status);
    }

    // [Fact]
    // public async Task Image2Word_ReturnsValidResult()
    // {
    //     // Arrange
    //     var image2WordServiceMock = new Mock<IImage2WordService>();
    //     image2WordServiceMock
    //         .Setup(service => service.ComposeAsync(It.IsAny<string>()))
    //         .ReturnsAsync("MockedWordAndSentence");
    //
    //     var httpClientMock = new Mock<HttpClient>();
    //     httpClientMock
    //         .Setup(client => client.PostAsync(It.IsAny<string>(), It.IsAny<MultipartFormDataContent>()))
    //         .ReturnsAsync(new HttpResponseMessage());
    //
    //     var controller = new HomeController(
    //         Mock.Of<ISentenceComposingService>(),
    //         Mock.Of<ITextComposingService>(),
    //         image2WordServiceMock.Object);
    //
    //     var command = new ImageCommand { File = Mock.Of<IFormFile>() };
    //
    //     // Act
    //     var result = await controller.Image2Word(command);
    //
    //     // Assert
    //     Assert.IsType<ServiceResultViewModel<string>>(result);
    //     Assert.Equal(ServiceResultStatus.Succeeded, result.Status);
    // }

}