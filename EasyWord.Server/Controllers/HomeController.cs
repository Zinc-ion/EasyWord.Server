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

    public HomeController(ISentenceComposingService sentenceComposingService, ITextComposingService textComposingService) {
        _sentenceComposingService = sentenceComposingService;
        _textComposingService = textComposingService;
    }


    [HttpPost]
    [Route("wordComposing")]
    public async Task<ServiceResultViewModel<string>> WordComposingAsync(
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
}