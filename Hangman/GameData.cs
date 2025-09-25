namespace Hangman;

public class GameData
{
    public string SecretWord { get; set; }
    public char[] MaskedWord { get; set; }
    public int TotalGuesses { get; set; }

    public GameData(string secretWord)
    {
        SecretWord = "ABC";
        MaskedWord = new char[SecretWord.Length];
        Array.Fill(MaskedWord, '-');
        TotalGuesses = 0;
    }
}