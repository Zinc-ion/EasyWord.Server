using EasyWord.Server.Services;
using EasyWord.Server.Commands;
using Microsoft.AspNetCore.Mvc;
using TheSalLab.GeneralReturnValues;

namespace EasyWord.Server.Controllers;

[ApiController]
[Route("[controller]")]
// /Home
public class HomeController {
    private ISentenceComposingService _sentenceComposingService;
    private ITextComposingService _textComposingService;
    private IImage2WordService _image2WordService;

    public HomeController(ISentenceComposingService sentenceComposingService, ITextComposingService textComposingService, IImage2WordService image2WordService)
    {
        _sentenceComposingService = sentenceComposingService;
        _textComposingService = textComposingService;
        _image2WordService = image2WordService;
    }


    [HttpPost]
    [Route("sentenceComposing")]
    public async Task<ServiceResultViewModel<string>> SentenceComposingAsync(
        [FromForm] SentenceCommand command)
    {
        string sentence;
        try
        {
            sentence = await _sentenceComposingService.ComposeAsync(command.Word);
        }
        catch (Exception ex)
        {
            return ServiceResult<string>.CreateExceptionResult(ex, ex.Message).ToServiceResultViewModel();
        }
        return ServiceResult<string>.CreateSucceededResult(sentence).ToServiceResultViewModel();
    }

    [HttpPost]
    [Route("textComposing")]
    public async Task<ServiceResultViewModel<string>> TextComposingAsync(
        [FromForm] TextCommand command)
    {
        string text;
        try
        {
            text = await _textComposingService.ComposeTextAsync(command.words);
        }
        catch (Exception ex)
        {
            return ServiceResult<string>.CreateExceptionResult(ex, ex.Message).ToServiceResultViewModel();
        }
        return ServiceResult<string>.CreateSucceededResult(text).ToServiceResultViewModel();
    }

    [HttpPost]
    [Route("image2Word")]
    public async Task<ServiceResultViewModel<string>> Image2Word(
        [FromForm] ImageCommand command)
    {
        using var httpClient = new HttpClient();
        using var formData = new MultipartFormDataContent {
            {new StreamContent(command.File.OpenReadStream()), "file", "file"}
        };
        HttpResponseMessage response;
        try
        {
            response = await httpClient.PostAsync(
                "http://easyword.server.image2word:8000", formData);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            return ServiceResult<string>.CreateExceptionResult(e, e.Message)
                .ToServiceResultViewModel();
        }

        var desc = await response.Content.ReadAsStringAsync();
        string wordAndSentence;
        try
        {
            wordAndSentence = await _image2WordService.ComposeAsync(desc);
        }
        catch (Exception e)
        {
            return ServiceResult<string>.CreateExceptionResult(e, e.Message)
                .ToServiceResultViewModel();
        }

        return ServiceResult<string>.CreateSucceededResult(wordAndSentence)
            .ToServiceResultViewModel();
    }

}