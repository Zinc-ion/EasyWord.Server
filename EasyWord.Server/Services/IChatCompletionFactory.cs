using Microsoft.SemanticKernel.AI.ChatCompletion;

namespace EasyWord.Server.Services;

public interface IChatCompletionFactory 
{
    IChatCompletion GetChatCompletion();
    // AzureChatCompletion : IChatCompletion
    // OpenAIChatCompletion : IChatCompletion
}