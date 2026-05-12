using Raylib_cs;
using static Raylib_cs.Raylib;
namespace SnakeGame;


public class Snake
{
    public enum SnakeDirection
    {
        Right,
        Left,
        Up,
        Down
    }
    
    private readonly int _snakeSize;
    private readonly int _windowWidth;
    private readonly int _windowHeight;
    public int _snakeX;
    public int _snakeY;
    private int _direction;
    private int _speed;
    
    public Snake(int initialSize, int initialX, int initialY, int windowWidth, int windowHeight)
    {
        _snakeSize = initialSize;
        _snakeX = initialX;
        _snakeY = initialY;
        _windowWidth = windowWidth;
        _windowHeight = windowHeight;
        _direction = (int)SnakeDirection.Right;
        _speed = 5;
    }

    public void DrawSnake()
    {
        DrawRectangle(_snakeX,_snakeY,_snakeSize,_snakeSize, Color.White);
    }

    public void GrowSnakeSize()
    {
        // Should work on both x and y axis.
        // The snake can possibly head toward 2 possible direction X and Y.
        Console.WriteLine("GrowSnake");
    }

    public void MoveSnake(SnakeDirection snakeDirection)
    {
        int direction = (int)snakeDirection;
        
        switch (direction)
        {
            case  (int)SnakeDirection.Right:
                MoveRight();
                break;
            case  (int)SnakeDirection.Left:
                MoveLeft();
                break;
            case  (int)SnakeDirection.Up:
                MoveUp();
                break;
            case  (int)SnakeDirection.Down:
                MoveDown();
                break;
        }
    }
    
    public void Update()
    {
        switch (_direction)
        {
            case (int)SnakeDirection.Right:
                if (_snakeX >= _windowWidth - _snakeSize)
                {
                    break;
                }
                _snakeX += _snakeSize;
                break;

            case (int)SnakeDirection.Left:
                if (_snakeX <= 0)
                {
                    break;
                }
                _snakeX -= _snakeSize;
                break;

            case (int)SnakeDirection.Up:
                if (_snakeY <= 0)
                {
                    break;
                }
                _snakeY -= _snakeSize;
                break;

            case (int)SnakeDirection.Down:
                if (_snakeY >= _windowHeight - _snakeSize)
                {
                    break;
                }
                _snakeY += _snakeSize;
                break;
        }
    }

    public void ChangeSnakeDirection(SnakeDirection snakeDirection)
    {
        _direction = (int)snakeDirection;
    }

    #region snakeMovement

    private void MoveRight()
    {
        _direction = (int)SnakeDirection.Right;
        if (_snakeX >= _windowWidth - 50)
        {
            return;
        }
        _snakeX += 50;
    }
    private void MoveLeft()
    {
        _direction = (int)SnakeDirection.Left;
        if (_snakeX <= 0)
        {
            return;
        }
        _snakeX -= 50;
    }
    private void MoveDown()
    {
        _direction = (int)SnakeDirection.Down;
        if (_snakeY >= _windowHeight - 50)
        {
            return;
        }
        _snakeY += 50;
    }
    private void MoveUp()
    {
        _direction = (int)SnakeDirection.Up;
        if (_snakeY <= 0)
        {
            return;
        }
        _snakeY -= 50;
    }

    #endregion
}