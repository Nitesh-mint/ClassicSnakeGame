using Raylib_cs;
using static Raylib_cs.Raylib;
namespace SnakeGame;


public class SnakeSegment(int x, int y)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
}

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
    private int _direction;
    private int _speed;
    private double _timeSinceLastMove;
    private const double MoveInterval = 150; // milliseconds between moves
    private List<SnakeSegment> _snakeSegments;
    public int _snakeX => _snakeSegments[0].X;
    public int _snakeY => _snakeSegments[0].Y;
    
    
    public Snake(int initialSize, int initialX, int initialY, int windowWidth, int windowHeight)
    {
        _snakeSize = initialSize;
        _windowWidth = windowWidth;
        _windowHeight = windowHeight;
        _direction = (int)SnakeDirection.Right;
        _speed = 5;
        _snakeSegments = new List<SnakeSegment>
        {
            new SnakeSegment(initialX, initialY),
        };
    }

    public void DrawSnake()
    {
        foreach (var segment in _snakeSegments)
        {
            DrawRectangle(segment.X,segment.Y,_snakeSize,_snakeSize, Color.White);
        }
    }

    public void GrowSnakeSize()
    {
        var tail = _snakeSegments[^1]; // last segment
        _snakeSegments.Add(new SnakeSegment(tail.X, tail.Y));
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
    
    public void Update(double deltaTime)
    {
        _timeSinceLastMove += deltaTime;

        if (_timeSinceLastMove < MoveInterval)
        {
            return;
        }
        
        _timeSinceLastMove -= MoveInterval;
        
        int newX = _snakeSegments[0].X;
        int newY = _snakeSegments[0].Y;
        
        switch (_direction)
        {
            case (int)SnakeDirection.Right: newX += _snakeSize; break;
            case (int)SnakeDirection.Left:  newX -= _snakeSize; break;
            case (int)SnakeDirection.Up:    newY -= _snakeSize; break;
            case (int)SnakeDirection.Down:  newY += _snakeSize; break;
        }
        
        for (int i = _snakeSegments.Count - 1; i > 0; i--)
        {
            _snakeSegments[i].X = _snakeSegments[i - 1].X;
            _snakeSegments[i].Y = _snakeSegments[i - 1].Y;
        }
        _snakeSegments[0].X = newX;
        _snakeSegments[0].Y = newY;
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
        //_snakeX += 50;
    }
    private void MoveLeft()
    {
        _direction = (int)SnakeDirection.Left;
        if (_snakeX <= 0)
        {
            return;
        }
        //_snakeX -= 50;
    }
    private void MoveDown()
    {
        _direction = (int)SnakeDirection.Down;
        if (_snakeY >= _windowHeight - 50)
        {
            return;
        }
        //_snakeY += 50;
    }
    private void MoveUp()
    {
        _direction = (int)SnakeDirection.Up;
        if (_snakeY <= 0)
        {
            return;
        }
        //_snakeY -= 50;
    }

    #endregion
}