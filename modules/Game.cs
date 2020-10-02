using System;
using System.Collections.Generic;

namespace PokerHand
{
    public class Game
    {
        // no. of players is determined by number of cards (e.g. 10 cards = 2 players, 15 cards = 3 players)
        // if personCount < noOfplayers in game (create new players)
        // assign hands to each player (based on id)
        // first hand is player ones, second hand is player twos, etc
        // assign hands to player for the duration of the game
        // determine the winner - increment the score of the winner

        public List<Hand> PokerHands;

        public Game(string[] cards)
        {
            // POKER VARIANT -- Five-Card draw

            const int handLength = 5; 
            this.PokerHands = new List<Hand>();

            for (int i = 0; i < cards.Length / handLength; i++)
            {
                // do something with each set of 5 cards
                // Create the hand
                
                var hand = new Hand();

                for (int j = 0; j < handLength; j++)
                {
                    // Console.Write(cards[handLength * i + j] + " ");
                    // var temp = new Card(cards[handLength * i + j]);
                    // Console.WriteLine(temp.Rank + " of " + temp.Suit);

                    hand.AddCard(new Card(cards[5 * i + j]));

                }

                this.PokerHands.Add(hand);
            }
        }

        public int DetermineWinner()
        {
            var playerHandRanks = new int[this.PokerHands.Count];

            for (int i = 0; i < this.PokerHands.Count; i++)
            {
                playerHandRanks[i] = (int) this.PokerHands[i].DetermineHand();
            }

            var max = playerHandRanks[0];
            var player = 1;

            for (int i = 1; i < this.PokerHands.Count; i++)
            {
                if (playerHandRanks[i] > max)
                {
                    max = playerHandRanks[i];
                    player = i + 1;
                }
            }

            // Determine if clear winner

            var clearWinner = Array.IndexOf(playerHandRanks, max) == Array.LastIndexOf(playerHandRanks, max);

            if (clearWinner)
            {
                return player;
            }
            else
            {
                // Determine who has the highest value hand

                return -1;

                // if hands are equal -> compare the value of the hands with each other

                // struct OnePair
                // {
                //    Pair: e.g. 5
                //   RemainingCardRanks: 10, 5, 3
                       
                // }

                // Comparison of same hand - pair has different values
                // player 1 has pair of kings -- player 2 has pair of aces
                // player 2 wins

                // Comparison of same hand - same pair
                // player 1 has pair of kings (10, 5, 3) -- player 2 has pair of kings (Q, 4, 6)
                // winner is determined based on highest card outside of pair
            }
        }
    }
}