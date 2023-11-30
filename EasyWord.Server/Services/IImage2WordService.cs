namespace EasyWord.Server.Services;

public interface IImage2WordService
{
    //调用GPT服务，识别出内容的主体物并根据主体物生成对应例句以及中文翻译
    Task<string> ComposeAsync(string desc);
}