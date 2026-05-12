using Raylib_cs;
using static Raylib_cs.Raylib;
namespace SnakeGame;

public static class DrawGameWindow
{
    public static void DrawGameBackground(int windowSizeX, int windowSizeY, int snakeSize)
    {
        for (int i = snakeSize; i <= windowSizeX; i = i + snakeSize)
        {
            DrawLine(0,i,windowSizeX, i, Color.Gray);
        }
        
        for (int i = 0; i <= windowSizeX; i = i + snakeSize)
        {
            DrawLine(i,0,i, windowSizeX, Color.Gray);
        }
    }       
}