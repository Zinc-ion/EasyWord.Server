using Azure.AI.OpenAI;
using EasyWord.Server.Services;
using Microsoft.SemanticKernel.AI.ChatCompletion;

namespace EasyWord.Server.Services.ImplService;

public class SentenceComposingService : ISentenceComposingService
{
    private IChatCompletion _chatCompletion;

    public SentenceComposingService(
        IChatCompletionFactory chatCompletionFactory)
    {
        _chatCompletion = chatCompletionFactory.GetChatCompletion();
    }

    public async Task<string> ComposeAsync(string word)
    {
        var chatHistory = _chatCompletion.CreateNewChat();

        chatHistory.AddSystemMessage(@"
你是一个根据英文单词生成英文例句的机器人。你根据用户发来的英文单词来创作例句与对应的中文翻译。当用户发来非英文单词内容时，你应该提示用户输入的不是英文单词

===例子开始===
用户：
recur
你：
Working overtime for several weeks caused a recurring headache for him.
连续几周的加班让他频频出现了头痛
===例子结束===

===例子开始===
用户：
act
你：
He knew he had to act quickly
他知道自己必须快速行动
===例子结束===

===例子开始===
用户：
bbd
你：
这不是一个英文单词
===例子结束===

===例子开始===
用户：
好
你：
这不是一个英文单词
===例子结束===

===例子开始===
用户：
英文单词
你：
这不是一个英文单词
===例子结束===

===例子开始===
用户：
124313@#其￥5Qr
你：
这不是一个英文单词
===例子结束===


");

        chatHistory.AddUserMessage(word);

        var reply = await _chatCompletion.GenerateMessageAsync(chatHistory);
        return reply;
    }
}