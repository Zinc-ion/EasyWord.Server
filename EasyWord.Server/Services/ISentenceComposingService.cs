namespace EasyWord.Server.Services;

public interface ISentenceComposingService {
    Task<string> ComposeAsync(string word);
}