using Microsoft.SemanticKernel.AI.ChatCompletion;

namespace EasyWord.Server.Services.ImplService;

public class Image2WordService : IImage2WordService
{
    private IChatCompletion _chatCompletion;

    public Image2WordService(
        IChatCompletionFactory chatCompletionFactory)
    {
        _chatCompletion = chatCompletionFactory.GetChatCompletion();
    }

    public async Task<string> ComposeAsync(string desc)
    {
        var chatHistory = _chatCompletion.CreateNewChat();

        chatHistory.AddSystemMessage(@"
你是一个从英文句子中提取出主体物，并根据主体物单词生成例句的机器人。
你从用户发来的英文描述语句中找出描述的主体物，并根据这个主体物的英文单词生成一个例句与例句对应的中文翻译，
输出：主体物单词，主体物单词对应的翻译，根据单词生成的例句与例句对应的中文翻译。当用户发来非英文单词内容时，你应该提示用户输入的不是英文。

===例子开始===
用户：
A black thermos on the table
你：
thermos
保温瓶
I always carry a thermos with hot tea to keep me warm during the winter
冬天我总是随身携带一个保温瓶，装着热茶，保持温暖。
===例子结束===

===例子开始===
用户：
A lion sitting on the lawn.
你：
lion
狮子
The lion is known as the king of the jungle.
狮子被誉为丛林之王。
===例子结束===

===例子开始===
用户：
bbd
你：
这不是英文
===例子结束===

===例子开始===
用户：
好
你：
这不是英文
===例子结束===

===例子开始===
用户：
英文
你：
这不英文
===例子结束===

===例子开始===
用户：
124313@#其￥5Qr
你：
这不是英文
===例子结束===


");

        chatHistory.AddUserMessage(desc);

        var reply = await _chatCompletion.GenerateMessageAsync(chatHistory);
        return reply;
    }
}