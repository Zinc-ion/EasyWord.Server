using EasyWord.Server.Services;
using EasyWord.Server.Commands;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<string> WordComposingAsync(
        [FromForm] SentenceCommand command) {
        return await _sentenceComposingService.ComposeAsync(command.Word);
    }
}