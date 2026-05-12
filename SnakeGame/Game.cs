namespace SnakeGame;

public class Game
{
    public Snake _snake;
    public Food _food;
    
    public Game(int width, int height, int snakeSize)
    {
        _snake = new Snake(snakeSize, 0, 0 , width, height);
        _food = new Food(width, height, snakeSize);
    }

    public void Update()
    {
        _snake.Update();
        CheckCollision();
    }

    private void CheckCollision()
    {
        if (_snake._snakeX == _food._foodX &&
            _snake._snakeY == _food._foodY)
        {
            Console.WriteLine("Collision");
            _food.Respawn();
        }
    }
    public void ChangeDirection(Snake.SnakeDirection direction)
    {
        _snake.ChangeSnakeDirection(direction);
    }
    public void Draw()
    {
        _food.Draw();
        _snake.DrawSnake();
    }
}