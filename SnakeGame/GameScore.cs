using Raylib_cs;
using static Raylib_cs.Raylib;

namespace SnakeGame;

public class GameScore
{
    private int _score = 0;
    private int _X;
    private int _Y;
    private Texture2D _snakeField;

    public GameScore(int X, int Y)
    {
        _X = X;
        _Y = Y;
    }

    public void DrawGameScore()
    {
        DrawFPS(_X - 80, 10);
        DrawText("score:" + _score, 10, 10, 20, Color.White);
    }

    public void IncreaseScore()
    {
        _score++;
    }

    public void DrawGameOver()
    {
        DrawText("Game Over", _X / 2 - 50, _Y / 2, 20, Color.Red);
        DrawText("Press Enter to restart the game...", _X / 2 - 100, _Y / 2 + 30, 15, Color.White);
    }

    public void DrawHomeScreen()
    {
        DrawText("Press Enter to start the game...", _X - 600, _Y / 2, 20, Color.Green);
    }

    public void ResetScore()
    {
        _score = 0;
    }

    public void LoadTexture()
    {
        _snakeField = Raylib.LoadTexture("Graphics/grass.png");
    }

    public void DrawGrassBackground()
    {
        Raylib.DrawTexture(_snakeField, 0, 0, Color.White);
    }

    public void UnloadTexture()
    {
        Raylib.UnloadTexture(_snakeField);
    }
}

