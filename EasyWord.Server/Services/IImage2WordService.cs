namespace EasyWord.Server.Services;

public interface IImage2WordService
{
    Task<string> ComposeAsync(string desc);
}