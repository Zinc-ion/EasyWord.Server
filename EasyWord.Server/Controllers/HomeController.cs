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

    public HomeController(ISentenceComposingService sentenceComposingService) {
        _sentenceComposingService = sentenceComposingService;
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
}