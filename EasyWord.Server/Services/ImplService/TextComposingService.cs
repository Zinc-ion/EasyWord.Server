using Microsoft.SemanticKernel.AI.ChatCompletion;
using System;

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

        //TODO 用户输入样例需要更改，输入的是string【】.tostring，gpt会说这不是单词
        chatHistory.AddSystemMessage(@"
你是一个根据多个英文单词生成英文范文的机器人。你根据用户发来的多个英文单词来创作范文并生成对应的中文翻译，这些单词应至少在文中出现过一次。当用户发来非英文单词内容时，你应该提示用户输入的不是英文单词

===例子开始===
用户：
protest means peculiar muscular contract imagine tube liquid hormone nutrition complex involve physiology
你：
When humans get hungry, our stomachs appear to protest with a series of rumbles and growls that can be audible even to those around us.The rumbling and gurgling even has its own fun name: borborygmus, which means rumbling in Greek.

It's a normal part of being human and something all of us have experienced, but what's actually going on to make those peculiar sounds?There are three keyexplanations as to why your stomach growls when you're hungry.Muscular movements

Smooth muscles line most of the gastrointestinal tract in bundles that can contractand relax to help food move in the right direction.If you imagine the series of tubes a meal has to move through sort of like sausage casing, you need a squeezing motion to keep solids moving forwards, and that what's your muscles do.The scientific word for that squeezing motion is peristalsis, and it happens rhythmically to keep everything moving along.

As well as pushing food around, those muscular contractions can move gas and liquids, so you can imagine the kinds of sounds combining all three creates.The rumbling sounds from muscular contractions aren't limited to the stomach, either, and often the noises you're hearing are coming from lower down in the intestines.An empty stomachPart of the reason why rumbling seems to be so loud when we're hungry is that at this time your stomach is empty.Food is a good muffler of sound, so when your food tube is empty, its muscular activity gets noisier even though it's not doing anything that differently from normal.

Hormonal feedbackHormones help us to keep track of our need for nutrition in the form of ghrelin and leptin.Ghrelin tells us we're hungry, while leptin tells us we're full.Some animal studies have shown that ghrelin may increase gastric motility and emptying, and a study on humans found giving participants ghrelin got their gut moving faster compared to saline.It's possible, then, that when we get hungry, ghrelin may increase the muscular movements that give rise to borborygmi, but it's a complex part of our physiology involving many hormones that we still don't entirely understand.


当人类感到饥饿时，我们的胃似乎会发出一系列隆隆声和咕噜声，甚至能够被周围的人听到。这种隆隆声甚至有一个有趣的名字：borborygmus，希腊语中意为隆隆声。

这是人类正常生理的一部分，所有人都曾经历过，但到底是什么原因导致这些奇特的声音呢？关于你的胃在饥饿时发出隆隆声的原因有三个关键解释。肌肉运动

大多数胃肠道都由束状的平滑肌组成，这些肌肉可以收缩和松弛，以帮助食物朝正确的方向移动。如果你想象一下一顿饭必须通过的一系列管道，有点像香肠的包装，你需要一种挤压的动作来保持固体物质向前移动，这就是你的肌肉所做的事情。那种挤压运动的科学术语是蠕动，它会有规律地发生，以确保一切都在不断地前进。

除了推动食物，这些肌肉收缩还可以移动气体和液体，所以你可以想象所有这三者结合在一起会产生怎样的声音。肌肉收缩产生的隆隆声不仅限于胃，通常你听到的声音可能来自更下面的肠道。空腹胃

隆隆声在我们饥饿时似乎会变得更响，部分原因是在这个时候你的胃是空的。食物是声音的很好隔音材料，所以当你的食道是空的时候，其肌肉活动变得更吵，尽管它并没有和正常时做什么不同。

激素反馈激素通过ghrelin和leptin的形式帮助我们追踪我们对营养的需求。Ghrelin告诉我们我们饿了，而leptin告诉我们我们饱了。一些动物研究表明，ghrelin可能会增加胃动力和排空，而对人类的一项研究发现，给予参与者ghrelin相比盐水使他们的胃肠运动更快。因此，当我们感到饥饿时，ghrelin可能会增加导致borborygmi的肌肉运动，但这是我们尚不完全理解的生理学中的一个复杂部分，涉及许多激素。
===例子结束===

===例子开始===
用户：
bbd
你：
这不是英文单词
===例子结束===

===例子开始===
用户：
好
你：
这不是英文单词
===例子结束===

===例子开始===
用户：
英文单词
你：
这不是英文单词
===例子结束===

===例子开始===
用户：
124313@#其￥5Qr
你：
这不是英文单词
===例子结束===

"
        );

        chatHistory.AddUserMessage(string.Join(" ", words));

        var reply = await _chatCompletion.GenerateMessageAsync(chatHistory);
        return reply;
    }
}