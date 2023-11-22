
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.ChatCompletion;

namespace EasyWord.Server.Services.ImplService;

public class AzureChatCompletionFactory : IChatCompletionFactory
{
    private IConfiguration _configuration;

    public AzureChatCompletionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IChatCompletion GetChatCompletion() =>
        new AzureChatCompletion(
            _configuration["AzureChatCompletionFactory.ModelId"],
            _configuration["AzureChatCompletionFactory.Endpoint"],
            _configuration["AzureChatCompletionFactory.ApiKey"]);
}