namespace Hangman;

public class GameData
{
    public string SecretWord { get; set; }
    public char[] MaskedWord { get; set; }
    public int TotalGuesses { get; set; }
    public bool Won { get; set; }
    public int Tick { get; set; }
    public string Name { get; set; }

    public GameData(string secretWord)
    {
        SecretWord = secretWord;
        MaskedWord = new char[SecretWord.Length];
        Array.Fill(MaskedWord, '-');
        TotalGuesses = 0;
        Won = false;
        Tick = 0;
        Name = "";
    }

    public void Reset(string secretWord)
    {
        SecretWord = secretWord;
        MaskedWord = new char[SecretWord.Length];
        Array.Fill(MaskedWord, '-');
        TotalGuesses = 0;
        Won = false;
    }
}