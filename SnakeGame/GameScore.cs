using Raylib_cs;
using static Raylib_cs.Raylib;
namespace SnakeGame;

public class GameScore
{
    private int _score = 0;
    private int _X;
    private int _Y;
    public GameScore(int X, int Y)
    {
        _X = X;
        _Y = Y;
    }
    public void DrawGameScore()
    {
        DrawFPS(_X - 80, 10);
        DrawText("score:" + _score, 10,10,20, Color.White);
    }
    
    public void IncreaseScore()
    {
        _score++;
    }

    public void DrawGameOver()
    {
        DrawText("Game Over", _X / 2, _Y/2, 20, Color.Red);
    }

    public void DrawHomeScreen()
    {
        DrawText("Press Enter to start the game...", _X / 2, _Y / 2, 20, Color.Green);
    }
}