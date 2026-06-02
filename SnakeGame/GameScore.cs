using Raylib_cs;
using static Raylib_cs.Raylib;

namespace SnakeGame;

public class GameScore
{
    private int _score = 0;
    private int _X;
    private int _Y;
    private Texture2D _snakeField;
    private Sound _eatingSound;

    public GameScore(int X, int Y)
    {
        _X = X;
        _Y = Y;
    }

    public void DrawGameScore()
    {
        DrawText($"{GetFPS()}", _X - 80, 10, 20, Color.White);
        DrawText("score:" + _score, 10, 10, 20, Color.White);
    }

    public void IncreaseScore()
    {
        _score++;
        Raylib.PlaySound(_eatingSound);
    }

    public void DrawGameOver()
    {
        DrawText("Game Over", _X / 2 - 50, _Y / 2, 20, Color.Red);
        DrawText("Press Enter to restart the game...", _X / 2 - 100, _Y / 2 + 30, 15, Color.White);
    }

    public void DrawHomeScreen()
    {
        DrawText("Press Enter to start the game...", _X - 600, _Y / 2, 20, Color.White);
    }

    public void ResetScore()
    {
        _score = 0;
    }

    public void LoadTexture()
    {
        _snakeField = Raylib.LoadTexture("Graphics/grass.png");
        _eatingSound = Raylib.LoadSound("Graphics/crunchybite.ogg");
    }

    public void DrawGrassBackground()
    {
        Rectangle source = new Rectangle(0, 0, _snakeField.Width, _snakeField.Height);

        Rectangle dest = new Rectangle(0, 0, GetScreenWidth(), GetScreenHeight());
        Raylib.DrawTexturePro(
            _snakeField,
            source,
            dest,
            System.Numerics.Vector2.Zero,
            0.0f,
            Color.White
        );
    }

    public void UnloadTexture()
    {
        Raylib.UnloadTexture(_snakeField);
        Raylib.UnloadSound(_eatingSound);
    }
}
