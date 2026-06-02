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
    private Texture2D _bodyTopLeft;
    private Texture2D _bodyTopRight;
    private Texture2D _bodyBottomLeft;
    private Texture2D _bodyBottomRight;
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
        _bodyTopLeft = Raylib.LoadTexture("Graphics/body_topleft.png");
        _bodyTopRight = Raylib.LoadTexture("Graphics/body_topright.png");
        _bodyBottomLeft = Raylib.LoadTexture("Graphics/body_bottomleft.png");
        _bodyBottomRight = Raylib.LoadTexture("Graphics/body_bottomright.png");
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
        Raylib.UnloadTexture(_bodyTopLeft);
        Raylib.UnloadTexture(_bodyTopRight);
        Raylib.UnloadTexture(_bodyBottomLeft);
        Raylib.UnloadTexture(_bodyBottomRight);
        Raylib.UnloadTexture(_tailRight);
        Raylib.UnloadTexture(_tailLeft);
        Raylib.UnloadTexture(_tailUp);
        Raylib.UnloadTexture(_tailDown);
    }

    #endregion

    public void DrawSnake()
    {
        // 1. Draw the Head
        var head = _snakeSegments[0];
        Texture2D headTexture = _direction switch
        {
            (int)SnakeDirection.Right => _headRight,
            (int)SnakeDirection.Left => _headLeft,
            (int)SnakeDirection.Up => _headUp,
            (int)SnakeDirection.Down => _headDown,
            _ => _headRight,
        };
        Raylib.DrawTexture(headTexture, head.X, head.Y, Color.White);

        if (_snakeSegments.Count <= 1)
        {
            return;
        }

        // 2. Draw the Tail (the last segment)
        var tail = _snakeSegments[^1];
        var segmentBeforeTail = _snakeSegments[^2];
        int tailDx = segmentBeforeTail.X - tail.X;
        int tailDy = segmentBeforeTail.Y - tail.Y;

        Texture2D tailTexture = _tailRight; // default fallback
        if (tailDx > 0)
            tailTexture = _tailRight;
        else if (tailDx < 0)
            tailTexture = _tailLeft;
        else if (tailDy > 0)
            tailTexture = _tailDown;
        else if (tailDy < 0)
            tailTexture = _tailUp;

        Raylib.DrawTexture(tailTexture, tail.X, tail.Y, Color.White);

        // 3. Draw the Body Segments (index 1 to Count - 2)
        for (int i = 1; i < _snakeSegments.Count - 1; i++)
        {
            var current = _snakeSegments[i];
            var prev = _snakeSegments[i + 1]; // towards tail
            var next = _snakeSegments[i - 1]; // towards head

            int dpx = prev.X - current.X;
            int dpy = prev.Y - current.Y;
            int dnx = next.X - current.X;
            int dny = next.Y - current.Y;

            Texture2D bodyTexture = _bodyHorizontal; // default

            if (dpx != 0 && dnx != 0) // Straight horizontal
            {
                bodyTexture = _bodyHorizontal;
            }
            else if (dpy != 0 && dny != 0) // Straight vertical
            {
                bodyTexture = _bodyVertical;
            }
            else // Corner / bend
            {
                if ((dpx < 0 && dny < 0) || (dnx < 0 && dpy < 0))
                {
                    bodyTexture = _bodyTopLeft;
                }
                else if ((dpx > 0 && dny < 0) || (dnx > 0 && dpy < 0))
                {
                    bodyTexture = _bodyTopRight;
                }
                else if ((dpx < 0 && dny > 0) || (dnx < 0 && dpy > 0))
                {
                    bodyTexture = _bodyBottomLeft;
                }
                else if ((dpx > 0 && dny > 0) || (dnx > 0 && dpy > 0))
                {
                    bodyTexture = _bodyBottomRight;
                }
            }

            Raylib.DrawTexture(bodyTexture, current.X, current.Y, Color.White);
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
