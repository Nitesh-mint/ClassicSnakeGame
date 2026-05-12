using Raylib_cs;
using static Raylib_cs.Raylib;
namespace SnakeGame;

public class Food
{
    private readonly int _windowWidth;
    private readonly int _windowHeight;
    private readonly int _foodSize;
    public int _foodX;
    public int _foodY;
    
    private readonly Random _random = new Random();
    
    public Food(int windowWidth, int windowHeight, int foodSize)
    {
        _windowWidth = windowWidth;
        _windowHeight = windowHeight;
        _foodSize = foodSize;
        
        Respawn();
    }

    public void Respawn()
    {
        _foodX = _random.Next(0, _windowWidth / _foodSize) * _foodSize;
        _foodY = _random.Next(0, _windowHeight / _foodSize) * _foodSize;
    }

    public void Draw()
    {
        DrawRectangle(_foodX,_foodY, _foodSize, _foodSize, Color.Green);       
    }
}