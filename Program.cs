using System;
using System.Collections.Generic;
using System.IO;

namespace PokerHand
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] lines = File.ReadAllLines("txtFile.txt");

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
                Console.WriteLine(game.PokerHands[0].DetermineHand() + " " + game.PokerHands[1].DetermineHand());
                var winner = game.DetermineWinner();
                Console.WriteLine(winner);
            }
        }
    }
}
