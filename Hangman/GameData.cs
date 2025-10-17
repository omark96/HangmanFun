using System.Diagnostics.CodeAnalysis;

namespace Hangman;

internal class GameData
{
    Difficulty _difficulty;
    int _volume;
    public string SecretWord { get; set; }
    public char[] MaskedWord { get; set; }
    public int TotalGuesses { get; set; }
    public WordCollection Words { get; set; }
    public bool Won { get; set; }
    public int Tick { get; set; }
    public string Name { get; set; }
    public Difficulty Difficulty
    {
        get { return _difficulty; }
        set
        {
            _difficulty = value;
            if (_difficulty < 0)
            {
                _difficulty = (Difficulty)(Enum.GetNames(typeof(Difficulty)).Length - 1);
            }
            else if ((int)_difficulty > Enum.GetNames(typeof(Difficulty)).Length - 1)
            {
                _difficulty = 0;
            }
        }
    }
    public int Volume
    {
        get
        {
            return _volume;
        }
        set
        {
            _volume = value;
            if (_volume <= 0)
            {
                _volume = 0;
            }
            else if (_volume >= 100)
            {
                _volume = 100;
            }
        }
    }

    public GameData()
    {
        Words = new();
        Words.LoadWords(@"./Assets/words.json");
        NewRound();
        Name = String.Empty;
    }
    [MemberNotNull(nameof(SecretWord), nameof(MaskedWord))]
    public void NewRound()
    {
        SecretWord = RandomWord(Difficulty);
        MaskedWord = new char[SecretWord.Length];
        Array.Fill(MaskedWord, '-');
        TotalGuesses = 0;
        Won = false;
    }

    private string RandomWord(Difficulty difficulty)
    {
        List<string> words;
        switch (difficulty)
        {
            case Difficulty.Easy:
                words = Words.Easy;
                break;
            case Difficulty.Medium:
                words = Words.Medium;
                break;
            case Difficulty.Hard:
                words = Words.Hard;
                break;
            default:
                words = new();
                break;
        }
        int wordIndex = Random.Shared.Next(words.Count);
        return words[wordIndex];
    }
}

internal enum Difficulty
{
    Easy,
    Medium,
    Hard
}
