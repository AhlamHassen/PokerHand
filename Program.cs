using System;
using System.Collections.Generic;
using System.IO;

namespace PokerHand
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] lines = File.ReadAllLines("poker.txt");

            // foreach (var line in lines)
            // {
            //     // create a new game
            //     var cards = line.Split(" ");
            //     var currentGame = new Game(cards);
            //     currentGame.DetermineWinner();
                   // assign players to the game
            // }

            var scores = new List<int>() { 0, 0 };

            string path = @".\output.txt";
            var output = new List<string>();


            foreach (var line in lines)
            {
                if (line[0] == '/')
                {
                    continue;
                }

                string[] testGame = line.Split(" ");
                var game = new Game(testGame);

                output.Add(line);

                foreach (var hand in game.PokerHands)
                {
                    Console.Write($"{hand.DetermineHand()}");
                    output.Add($"{hand.DetermineHand()}");
                }
                Console.WriteLine();
                
                var winner = game.DetermineWinner();
                if (winner.Count == 1)
                {
                    if (scores.Count <= winner[0])
                    {
                        // Count = 2, 3

                        while (scores.Count <= winner[0])
                        {
                            scores.Add(0);
                        }
                    }

                    scores[winner[0]] += 1;
                    Console.WriteLine(winner[0]);
                    output.Add(winner[0].ToString());
                }
                else
                {
                    Console.WriteLine("It's a draw between: ");
                    foreach (var player in winner)
                    {
                        Console.WriteLine(player);
                    }
                }
            }
            
            for (int i = 0; i < scores.Count; i++)
            {
                Console.WriteLine($"Player {i + 1}: {scores[i]}");
            }

            File.WriteAllLines(path, output);
        }
    }
}
