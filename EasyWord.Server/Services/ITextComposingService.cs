namespace EasyWord.Server.Services;

public interface ITextComposingService
{
    Task<string> ComposeTextAsync(string words);
}