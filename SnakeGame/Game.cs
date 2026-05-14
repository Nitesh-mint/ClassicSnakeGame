namespace SnakeGame;

public class Game(int width, int height, int snakeSize)
{
    private readonly Snake _snake = new Snake(snakeSize, 0, 0 , width, height);
    private readonly Food _food = new Food(width, height, snakeSize);
    
    //public Game(int width, int height, int snakeSize)
    //{
     //   _snake = new Snake(snakeSize, 0, 0 , width, height);
      //  _food = new Food(width, height, snakeSize);
    //}

    public void Update(double deltaTime)
    {
        _snake.Update(deltaTime);
        CheckCollision();
    }

    private void CheckCollision()
    {
        if (_snake._snakeX == _food._foodX &&
            _snake._snakeY == _food._foodY)
        {
            Console.WriteLine("Collision");
            _snake.GrowSnakeSize();
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