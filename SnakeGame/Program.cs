using System.Diagnostics;
using Raylib_cs;
using SnakeGame;
using static Raylib_cs.Raylib;

const int windowWidth = 800;
const int windowHeight = 600;
const int snakeSize = 30;

InitWindow(windowWidth, windowHeight, "SnakeGame");
SetTargetFPS(60);

var game = new Game(windowWidth, windowHeight, snakeSize);
game.LoadTextures();
Stopwatch stopwatch = Stopwatch.StartNew();

while (!WindowShouldClose())
{
    if (IsKeyPressed(KeyboardKey.Enter))
    {
        game.StartGame();
    }

    double deltaTime = stopwatch.Elapsed.TotalMilliseconds;
    // 1. INPUT
    if (IsKeyPressed(KeyboardKey.D))
        game.ChangeDirection(Snake.SnakeDirection.Right);

    if (IsKeyPressed(KeyboardKey.A))
        game.ChangeDirection(Snake.SnakeDirection.Left);

    if (IsKeyPressed(KeyboardKey.S))
        game.ChangeDirection(Snake.SnakeDirection.Down);

    if (IsKeyPressed(KeyboardKey.W))
        game.ChangeDirection(Snake.SnakeDirection.Up);

    // 2. UPDATE
    game.Update(deltaTime);

    // 3. DRAW
    BeginDrawing();
    ClearBackground(Color.Black);

    game.Draw();
    stopwatch.Restart();
    EndDrawing();
}

game.UnloadTexture();

CloseWindow();
