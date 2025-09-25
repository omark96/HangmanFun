namespace Hangman;

public class GameData
{
    string _secretWord;
    char[] _maskedWord;
    int _totalGuesses;

    public GameData(string secretWord)
    {
        _secretWord = secretWord;

    }
}