using System;
using System.Text.Json;
using System.Threading;
using System.IO;
using System.Security.Cryptography;

class PongGame
{
    static int ballX = 10, ballY = 5, ballDirectionX = 1, ballDirectionY = 1;
    static int paddle1Y = 5, paddle2Y = 5;
    static int score1 = 0, score2 = 0;
    static int width = 20, height = 10;

    static void Main()
    {
        Console.Title = "Pong";
        Console.CursorVisible = false;
        while (true)
        {
            Draw();
            Input();
            Logic();
            Thread.Sleep(100);
        }
    }

    static void Draw()
    {
        Console.Clear();
        for (int i = 0; i < width + 2; i++)
            Console.Write("#");
        Console.WriteLine();

        for (int y = 0; y < height; y++)
        {
            Console.Write("#");
            for (int x = 0; x < width; x++)
            {
                if (x == ballX && y == ballY)
                    Console.Write("O");
                else if (x == 0 && y >= paddle1Y && y < paddle1Y + 3)
                    Console.Write("|");
                else if (x == width - 1 && y >= paddle2Y && y < paddle2Y + 3)
                    Console.Write("|");
                else
                    Console.Write(" ");
            }
            Console.WriteLine("#");
        }

        for (int i = 0; i < width + 2; i++)
            Console.Write("#");
        Console.WriteLine();
        Console.SetCursorPosition(0, height + 2);
        Console.WriteLine($"Score: Player 1: {score1} | Player 2: {score2}");
    }

    static void Input()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.W && paddle1Y > 0) paddle1Y--;
            if (key == ConsoleKey.S && paddle1Y < height - 3) paddle1Y++;
            if (key == ConsoleKey.UpArrow && paddle2Y > 0) paddle2Y--;
            if (key == ConsoleKey.DownArrow && paddle2Y < height - 3) paddle2Y++;
        }
    }

    static void Logic()
    {
        ballX += ballDirectionX;
        ballY += ballDirectionY;

        if (ballY <= 0 || ballY >= height - 1) ballDirectionY *= -1;

        if (ballX == 0)
        {
            if (ballY >= paddle1Y && ballY < paddle1Y + 3) ballDirectionX *= -1;
            else { score2++; ResetBall(); }
        }

        if (ballX == width - 1)
        {
            if (ballY >= paddle2Y && ballY < paddle2Y + 3) ballDirectionX *= -1;
            else { score1++; ResetBall(); }
        }
    }

    static void ResetBall()
    {
        ballX = width / 2;
        ballY = height / 2;
        ballDirectionX *= -1;
    }
}


// Written By UnsyncedMaster