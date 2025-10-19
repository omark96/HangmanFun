
using Hangman;
using HangmanFun.Graphics;

namespace HangmanFun.Scenes;
internal class Options : Scene
{
    OptionSelection _selected;
    OptionSelection Selected
    {
        get { return _selected; }
        set
        {
            _selected = value;
            if (_selected < 0)
            {
                _selected = (OptionSelection)(Enum.GetNames(typeof(OptionSelection)).Length - 1);
            }
            else if ((int)_selected > Enum.GetNames(typeof(OptionSelection)).Length - 1)
            {
                _selected = 0;
            }
        }
    }
    int OptionsCount => Enum.GetValues(typeof(OptionSelection)).Length;
    public Options(GameData data, Renderer renderer) : base(data, renderer)
    {
    }
    internal override void Draw()
    {
        int screenMidX = Renderer.Buffer.Width / 2;



        string difficultyLabel = "Difficulty";
        int difficultyLabelY = 6;
        int difficultyLabelX = screenMidX - difficultyLabel.Length / 2;
        Renderer.DrawText(difficultyLabelX, difficultyLabelY, difficultyLabel, Color.White);

        int difficultyWidth = -5;
        foreach (string difficulty in Enum.GetNames(typeof(Difficulty)))
        {
            difficultyWidth += difficulty.Length + 5;
        }
        int difficultyX = screenMidX - difficultyWidth / 2;
        int difficultyY = difficultyLabelY + 2;

        var difficulties = Enum.GetValues(typeof(Difficulty));
        foreach (var difficulty in difficulties)
        {
            string difficultyText = difficulty.ToString()!;
            Color difficultySelectionColor;
            if ((Difficulty)difficulty == Data.Difficulty)
            {
                if (Selected == OptionSelection.Difficulty)
                {
                    difficultySelectionColor = Color.Yellow;
                }
                else
                {
                    difficultySelectionColor = Color.White;
                }
            }
            else
            {
                difficultySelectionColor = Color.DarkGray;
            }
            Renderer.DrawTextBox(difficultyX, difficultyY, difficultyText, Color.Black, difficultySelectionColor, difficultySelectionColor);
            difficultyX += difficultyText.Length + 5;
        }

        int lineWidth = 40;
        int lineY = difficultyY + 4;
        Renderer.DrawHorizontalLine(screenMidX - lineWidth / 2, lineY, lineWidth, Color.White);

        string startText = "Press ENTER to start!";
        int startTextY = lineY + 2;
        int startTextX = screenMidX - startText.Length / 2;
        Renderer.DrawText(startTextX, startTextY, startText, Color.White);

    }

    internal override GameScene Update(ConsoleKeyInfo input)
    {
        if (input.Key == ConsoleKey.UpArrow)
        {
            Selected -= 1;
        }
        else if (input.Key == ConsoleKey.DownArrow)
        {
            Selected += 1;
        }
        else if (input.Key == ConsoleKey.LeftArrow)
        {
            if (Selected == OptionSelection.Difficulty)
            {
                Data.Difficulty -= 1;
            }
        }
        else if (input.Key == ConsoleKey.RightArrow)
        {
            if (Selected == OptionSelection.Difficulty)
            {
                Data.Difficulty += 1;
            }
        }
        else if (input.Key == ConsoleKey.Enter)
        {
            return GameScene.TitleScene;
        }
        return GameScene.None;
    }
}

internal enum OptionSelection
{
    Difficulty
}
