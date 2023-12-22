namespace EasyWord.Server.Services;

public interface IText2ImageService
{
    //调用百度千帆大模型服务，根据英文例句生成对应的图片
    Task<string> ComposeAsync(string sentence);
}