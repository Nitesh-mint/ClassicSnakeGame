using Raylib_cs;

namespace SnakeGame;

public class Food
{
    private readonly int _windowWidth;
    private readonly int _windowHeight;
    private readonly int _foodSize;
    public int _foodX;
    public int _foodY;
    private Texture2D _food;

    private readonly Random _random = new();

    public Food(int windowWidth, int windowHeight, int foodSize)
    {
        _windowWidth = windowWidth;
        _windowHeight = windowHeight;
        _foodSize = foodSize;

        Respawn();
    }

    public void LoadTexture()
    {
        _food = Raylib.LoadTexture("Graphics/apple.png");
    }

    public void Respawn()
    {
        _foodX = _random.Next(0, _windowWidth / _foodSize) * _foodSize;
        _foodY = _random.Next(0, _windowHeight / _foodSize) * _foodSize;
    }

    public void Draw()
    {
        Raylib.DrawTexture(_food, _foodX, _foodY, Color.Green);
    }

    public void UnloadTextures()
    {
        Raylib.UnloadTexture(_food);
    }
}
