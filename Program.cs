using System;
using System.Collections.Generic;
using System.IO;

namespace PokerHand
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] lines = File.ReadAllLines("txtFile2");

            // foreach (var line in lines)
            // {
            //     // create a new game
            //     var cards = line.Split(" ");
            //     var currentGame = new Game(cards);
            //     currentGame.DetermineWinner();
                   // assign players to the game
            // }

            foreach (var line in lines)
            {
                string[] testGame = line.Split(" ");
                var game = new Game(testGame);

                foreach (var hand in game.PokerHands)
                {
                    Console.Write($" {hand.DetermineHand()}");
                }
                Console.WriteLine();
                var winner = game.DetermineWinner();
                if (winner.Count == 1)
                {
                    Console.WriteLine(winner[0]);
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
        }
    }
}
