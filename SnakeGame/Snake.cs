using Raylib_cs;

// using static Raylib_cs.Raylib;
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
        Down,
    }

    private readonly int _snakeSize;
    private int _direction;
    private int _speed;
    private double _timeSinceLastMove;
    private const double MoveInterval = 150; // milliseconds between moves
    private List<SnakeSegment> _snakeSegments;
    public int _snakeX => _snakeSegments[0].X;
    public int _snakeY => _snakeSegments[0].Y;
    private Texture2D _headRight;
    private Texture2D _headLeft;
    private Texture2D _headDown;
    private Texture2D _headUp;
    private Texture2D _bodyHorizontal;
    private Texture2D _bodyVertical;
    private Texture2D _tailRight;
    private Texture2D _tailLeft;
    private Texture2D _tailUp;
    private Texture2D _tailDown;

    public Snake(int initialSize, int initialX, int initialY)
    {
        _snakeSize = initialSize;
        _direction = (int)SnakeDirection.Right;
        _speed = 5;
        _snakeSegments = [new SnakeSegment(initialX, initialY), new SnakeSegment(0, 1)];
    }

    # region _snakeSegments
    public void LoadTextures()
    {
        _headRight = Raylib.LoadTexture("Graphics/head_right.png");
        _headLeft = Raylib.LoadTexture("Graphics/head_left.png");
        _headDown = Raylib.LoadTexture("Graphics/head_down.png");
        _headUp = Raylib.LoadTexture("Graphics/head_up.png");
        _bodyHorizontal = Raylib.LoadTexture("Graphics/body_horizontal.png");
        _bodyVertical = Raylib.LoadTexture("Graphics/body_vertical.png");
        _tailRight = Raylib.LoadTexture("Graphics/tail_right.png");
        _tailLeft = Raylib.LoadTexture("Graphics/tail_left.png");
        _tailUp = Raylib.LoadTexture("Graphics/tail_up.png");
        _tailDown = Raylib.LoadTexture("Graphics/tail_down.png");
    }

    public void UnloadTextures()
    {
        Raylib.UnloadTexture(_headRight);
        Raylib.UnloadTexture(_headLeft);
        Raylib.UnloadTexture(_headDown);
        Raylib.UnloadTexture(_headUp);
        Raylib.UnloadTexture(_bodyHorizontal);
        Raylib.UnloadTexture(_bodyVertical);
    }

    #endregion

    public void DrawSnake()
    {
        switch (_direction)
        {
            case (int)SnakeDirection.Right:
                Raylib.DrawTexture(
                    _headRight,
                    _snakeSegments[0].X,
                    _snakeSegments[0].Y,
                    Color.White
                );
                // Raylib.DrawTexture(
                //     _tailRight,
                //     _snakeSegments[^1].X,
                //     _snakeSegments[^1].Y,
                //     Color.White
                // );
                break;
            case (int)SnakeDirection.Left:
                Raylib.DrawTexture(
                    _headLeft,
                    _snakeSegments[0].X,
                    _snakeSegments[0].Y,
                    Color.White
                );
                break;
            case (int)SnakeDirection.Up:
                Raylib.DrawTexture(_headUp, _snakeSegments[0].X, _snakeSegments[0].Y, Color.White);
                break;
            case (int)SnakeDirection.Down:
                Raylib.DrawTexture(
                    _headDown,
                    _snakeSegments[0].X,
                    _snakeSegments[0].Y,
                    Color.White
                );
                break;
        }

        foreach (var segment in _snakeSegments.Skip(1))
        {
            switch (_direction)
            {
                case (int)SnakeDirection.Right:
                case (int)SnakeDirection.Left:
                    Raylib.DrawTexture(_bodyHorizontal, segment.X, segment.Y, Color.White);
                    break;
                case (int)SnakeDirection.Up:
                case (int)SnakeDirection.Down:
                    Raylib.DrawTexture(_bodyVertical, segment.X, segment.Y, Color.White);
                    break;
            }
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
            case (int)SnakeDirection.Right:
                newX += _snakeSize;
                break;
            case (int)SnakeDirection.Left:
                newX -= _snakeSize;
                break;
            case (int)SnakeDirection.Up:
                newY -= _snakeSize;
                break;
            case (int)SnakeDirection.Down:
                newY += _snakeSize;
                break;
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
        _snakeSegments = [new SnakeSegment(0, 0)];
        _direction = (int)SnakeDirection.Right;
    }

    public bool HasCollidedWithSelf()
    {
        if (_snakeSegments.Count > 4)
        {
            SnakeSegment head = _snakeSegments[0];
            foreach (SnakeSegment body in _snakeSegments.Skip(1))
            {
                if (head.X == body.X && head.Y == body.Y)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
