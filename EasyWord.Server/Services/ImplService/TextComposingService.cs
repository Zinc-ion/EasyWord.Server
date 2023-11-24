using Microsoft.SemanticKernel.AI.ChatCompletion;

namespace EasyWord.Server.Services.ImplService;

public class TextComposingService : ITextComposingService
{
    private IChatCompletion _chatCompletion;

    public TextComposingService(
        IChatCompletionFactory chatCompletionFactory)
    {
        _chatCompletion = chatCompletionFactory.GetChatCompletion();
    }

    public async Task<string> ComposeTextAsync(string[] words)
    {
        var chatHistory = _chatCompletion.CreateNewChat();

        chatHistory.AddSystemMessage(@"
你是一个根据多个英文单词生成英文范文的机器人。你根据用户发来的多个英文单词来创作范文，这些单词应至少在文中出现过一次。当用户发来非英文单词内容时，你应该提示用户输入的不是英文单词

===例子开始===
用户：
protest means peculiar muscular contract imagine tube liquid hormone nutrition complex involve physiology
你：
When humans get hungry, our stomachs appear to protest with a series of rumbles 
and growls that can be audible even to those around us.The rumbling and gurgling 
even has its own fun name: borborygmus, which means rumbling in Greek.

It's a normal part of being human and something all of us have experienced, 
but what's actually going on to make those peculiar sounds?There are three key
explanations as to why your stomach growls when you're hungry.Muscular movements

Smooth muscles line most of the gastrointestinal tract in bundles that can contract
and relax to help food move in the right direction.If you imagine the series of tubes 
a meal has to move through sort of like sausage casing, you need a squeezing motion 
to keep solids moving forwards, and that what's your muscles do.The scientific word 
for that squeezing motion is peristalsis, and it happens rhythmically to keep everything moving along.

As well as pushing food around, those muscular contractions can move gas and liquids, 
so you can imagine the kinds of sounds combining all three creates.The rumbling sounds 
from muscular contractions aren't limited to the stomach, either, and often the noises 
you're hearing are coming from lower down in the intestines.An empty stomachPart of the 
reason why rumbling seems to be so loud when we're hungry is that at this time your stomach 
is empty.Food is a good muffler of sound, so when your food tube is empty, its muscular 
activity gets noisier even though it's not doing anything that differently from normal.

Hormonal feedbackHormones help us to keep track of our need for nutrition in the form 
of ghrelin and leptin.Ghrelin tells us we're hungry, while leptin tells us we're full.
Some animal studies have shown that ghrelin may increase gastric motility and emptying, 
and a study on humans found giving participants ghrelin got their gut moving faster compared 
to saline.It's possible, then, that when we get hungry, ghrelin may increase the muscular 
movements that give rise to borborygmi, but it's a complex part of our physiology involving 
many hormones that we still don't entirely understand.
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

        chatHistory.AddUserMessage(words.ToString());

        var reply = await _chatCompletion.GenerateMessageAsync(chatHistory);
        return reply;
    }
}