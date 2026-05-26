namespace SnakeGame;

public class Game(int width, int height, int snakeSize)
{
    private enum GameState
    {
        START,
        PLAYING,
        Gameover
    }

    private readonly Snake _snake = new Snake(snakeSize, 0, 0);
    private readonly Food _food = new Food(width, height, snakeSize);
    private readonly GameScore _gameScore = new GameScore(width, height);
    private int _gameState = (int)GameState.START;

    public void Update(double deltaTime)
    {
        if (_gameState == (int)GameState.PLAYING)
        {
            _snake.Update(deltaTime);
            CheckCollision();
        }
    }

    private void CheckCollision()
    {
        if (_gameState == (int)GameState.PLAYING)
        {
            if (_snake._snakeX >= width - snakeSize || _snake._snakeX < 0 ||
                _snake._snakeY >= height - snakeSize || _snake._snakeY < 0)
            {
                _gameState = (int)GameState.Gameover;
            }

            if (_snake.HasCollidedWithSelf())
            {
                Console.WriteLine("Snake Collided with self");
                _gameState = (int)GameState.Gameover;
            }

            if (_snake._snakeX == _food._foodX &&
                _snake._snakeY == _food._foodY)
            {
                _snake.GrowSnakeSize();
                _food.Respawn();
                _gameScore.IncreaseScore();
            }
        }
    }

    public void ChangeDirection(Snake.SnakeDirection direction)
    {
        if (_gameState == (int)GameState.PLAYING)
        {
            _snake.ChangeSnakeDirection(direction);
        }
    }

    public void Draw()
    {
        if (_gameState == (int)GameState.Gameover)
        {
            _gameScore.DrawGameScore();
            _gameScore.DrawGameOver();
        }

        if (_gameState == (int)GameState.PLAYING)
        {
            _food.Draw();
            _snake.DrawSnake();
            _gameScore.DrawGameScore();
        }

        if (_gameState == (int)GameState.START)
        {
            _gameScore.DrawHomeScreen();
        }
    }

    public void StartGame()
    {
        if (_gameState == (int)GameState.Gameover)
        {
            Reset();
        }
        _gameState = (int)GameState.PLAYING;
    }
    public void Reset()
    {
        _snake.ResetSnake();
        _gameScore.ResetScore();
        _food.Respawn();
    }
}
