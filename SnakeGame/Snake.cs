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
    private int _direction;
    private int _speed;
    private double _timeSinceLastMove;
    private const double MoveInterval = 150; // milliseconds between moves
    private List<SnakeSegment> _snakeSegments;
    public int _snakeX => _snakeSegments[0].X;
    public int _snakeY => _snakeSegments[0].Y;


    public Snake(int initialSize, int initialX, int initialY)
    {
        _snakeSize = initialSize;
        _direction = (int)SnakeDirection.Right;
        _speed = 5;
        _snakeSegments =
        [
            new SnakeSegment(initialX, initialY)
        ];
    }

    public void DrawSnake()
    {
        foreach (var segment in _snakeSegments)
        {
            DrawRectangle(segment.X, segment.Y, _snakeSize, _snakeSize, Color.White);
        }
    }

    public void GrowSnakeSize()
    {
        var tail = _snakeSegments[^1]; // last segment
        _snakeSegments.Add(new SnakeSegment(tail.X, tail.Y));
    }

    public bool Update(double deltaTime)
    {
        _timeSinceLastMove += deltaTime;

        if (_timeSinceLastMove < MoveInterval)
        {
            return false;
        }

        _timeSinceLastMove -= MoveInterval;

        int newX = _snakeSegments[0].X;
        int newY = _snakeSegments[0].Y;

        switch (_direction)
        {
            case (int)SnakeDirection.Right: newX += _snakeSize; break;
            case (int)SnakeDirection.Left: newX -= _snakeSize; break;
            case (int)SnakeDirection.Up: newY -= _snakeSize; break;
            case (int)SnakeDirection.Down: newY += _snakeSize; break;
        }

        for (int i = _snakeSegments.Count - 1; i > 0; i--)
        {
            _snakeSegments[i].X = _snakeSegments[i - 1].X;
            _snakeSegments[i].Y = _snakeSegments[i - 1].Y;
        }
        _snakeSegments[0].X = newX;
        _snakeSegments[0].Y = newY;

        return true;
    }

    public void ChangeSnakeDirection(SnakeDirection snakeDirection)
    {
        _direction = (int)snakeDirection;
    }

    public void ResetSnake()
    {
        _snakeSegments = [
            new SnakeSegment(0,0)
        ];
        _direction = (int)SnakeDirection.Right;

    }

    public bool HasCollidedWithSelf()
    {
        SnakeSegment head = _snakeSegments[0];

        foreach (SnakeSegment body in _snakeSegments.Skip(1))
        {
            if (head.X == body.X && head.Y == body.Y)
            {
                Console.WriteLine("Snake Collided With Self");
                return true;
            }
        }

        return false;
    }
}