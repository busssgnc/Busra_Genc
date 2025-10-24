using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        bool playAgain = true;
        int highScore = int.MaxValue;
        int totalGames = 0;
        int totalWins = 0;
        int totalGuesses = 0;

        try
        {
            if (File.Exists("highscore.txt"))
                int.TryParse(File.ReadAllText("highscore.txt"), out highScore);
        }
        catch { }

        while (playAgain)
        {
            totalGames++;
            int maxTries;
            int minNumber, maxNumber;

            Console.WriteLine("Choose difficulty: 1-Easy (1-10,5 tries), 2-Normal (1-20,7 tries), 3-Hard (1-50,10 tries)");
            string difficulty = Console.ReadLine().Trim();
            switch (difficulty)
            {
                case "1":
                    minNumber = 1; maxNumber = 10; maxTries = 5; break;
                case "3":
                    minNumber = 1; maxNumber = 50; maxTries = 10; break;
                default:
                    minNumber = 1; maxNumber = 20; maxTries = 7; break;
            }

            Random rand = new Random();
            int winningNumber = rand.Next(minNumber, maxNumber + 1);
            int guess = 0;
            int validAttempts = 0;
            bool hasWon = false;

            Console.WriteLine($"\n Guess a number between {minNumber} and {maxNumber}!");
            Console.WriteLine($"You have {maxTries} valid tries.\n");

            Console.Write("Do you want 2-player mode? (y/n): ");
            bool twoPlayer = Console.ReadLine().Trim().ToLower() == "y";
            int currentPlayer = 1;

            while (!hasWon && validAttempts < maxTries)
            {
                bool answerIsValid = false;

                while (!answerIsValid)
                {
                    try
                    {
                        Console.Write($"Player {(twoPlayer ? currentPlayer : 1)}, type your answer: ");
                        guess = Convert.ToInt32(Console.ReadLine());

                        if (guess < minNumber || guess > maxNumber)
                            throw new ArgumentOutOfRangeException();

                        answerIsValid = true;
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" Invalid input. Try again.");
                        Console.ResetColor();
                    }
                }

                validAttempts++;
                totalGuesses++;

                if (guess == winningNumber)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n Player {currentPlayer} guessed it! Number: {winningNumber}");
                    Console.WriteLine($"Attempts: {validAttempts}\n");

                    if (validAttempts < highScore)
                    {
                        highScore = validAttempts;
                        try { File.WriteAllText("highscore.txt", highScore.ToString()); } catch { }
                        Console.WriteLine(" New High Score!");
                    }
                    else if (highScore != int.MaxValue)
                        Console.WriteLine($" Current High Score: {highScore}");

                    hasWon = true;
                    totalWins++;
                    Console.ResetColor();
                }
                else
                {
                    if (guess < winningNumber) Console.WriteLine(" Too low!");
                    else Console.WriteLine(" Too high!");

                    if (Math.Abs(guess - winningNumber) <= 2) Console.WriteLine(" Very close!");
                    if (winningNumber % 2 == 0) Console.WriteLine(" Hint: The number is even.");
                    else Console.WriteLine(" Hint: The number is odd.");
                    if (winningNumber % 5 == 0) Console.WriteLine(" Hint: The number is multiple of 5.");

                    Console.WriteLine($"Attempts left: {maxTries - validAttempts}\n");

                    if (twoPlayer) currentPlayer = currentPlayer == 1 ? 2 : 1;
                }
            }

            if (!hasWon)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n Game Over! Number was {winningNumber}\n");
                Console.ResetColor();
            }

            Console.WriteLine($" Total games: {totalGames}");
            Console.WriteLine($" Wins: {totalWins}");
            Console.WriteLine($" Win %: {(double)totalWins / totalGames * 100:0.##}%");
            Console.WriteLine($" Avg guesses/game: {(double)totalGuesses / totalGames:0.##}\n");

            Console.Write("Play again? (y/n): ");
            if (Console.ReadLine().Trim().ToLower() != "y") playAgain = false;

            Console.WriteLine("------------------------------------------\n");
        }
    }
}
