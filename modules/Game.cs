using System;
using System.Collections.Generic;

namespace PokerHand {
    public class Game {
        // no. of players is determined by number of cards (e.g. 10 cards = 2 players, 15 cards = 3 players)
        // if personCount < noOfplayers in game (create new players)
        // assign hands to each player (based on id)
        // first hand is player ones, second hand is player twos, etc
        // assign hands to player for the duration of the game
        // determine the winner - increment the score of the winner

        public List<Hand> PokerHands;

        public Game (string[] cards) {
            // POKER VARIANT --> Five-Card draw
            this.PokerHands = new List<Hand> ();
            const int handLength = 5;

            for (int i = 0; i < cards.Length / handLength; i++) {
                // do something with each set of 5 cards
                // Create the hand

                var hand = new Hand ();

                for (int j = 0; j < handLength; j++) {
                    // Console.Write(cards[handLength * i + j] + " ");
                    // var temp = new Card(cards[handLength * i + j]);
                    // Console.WriteLine(temp.Rank + " of " + temp.Suit);

                    hand.AddCard (new Card (cards[5 * i + j]));

                }

                this.PokerHands.Add (hand);
            }
        }

        public List<int> DetermineWinner () {
            var ranks = new int[this.PokerHands.Count];

            for (int i = 0; i < this.PokerHands.Count; i++) {
                ranks[i] = (int) this.PokerHands[i].DetermineHand ();
            }

            var max = ranks[0];
            var player = 0;

            for (int i = 1; i < this.PokerHands.Count; i++) {
                if (ranks[i] > max) {
                    max = ranks[i];
                    player = i;
                }
            }

            // Determine if clear winner

            var clearWinner = Array.IndexOf (ranks, max) == Array.LastIndexOf (ranks, max);

            if (clearWinner) {
                return new List<int> () { player };
            } else {
                // Determine who has the highest value hand

                // We know which hands are at the max from the ranks array (could be multiple)

                // Gather the indexes of players at max rank (to use as a final return)

                var toCompareIndexes = new List<int> ();
                for (int i = 0; i < ranks.Length; i++) {
                    if (ranks[i] == max) {
                        toCompareIndexes.Add (i); 
                    }
                }

                // utilise the index from the ranks array, to record which player wins
                // we can only iterate through the hands where the players hand is the max rank

                // once we know the max rank, we know which comparison to perform

                // Determine which sort of comparison to make

                List<List<int>> comparisonToPerform = null;

                switch (max) {
                    case 1:
                        // High card
                        comparisonToPerform = determineHighCard(toCompareIndexes);
                        break;

                    case 2:
                        // One pair
                        comparisonToPerform = onePairComparison(toCompareIndexes);
                        break;

                    case 3:
                        // Two pairs
                        comparisonToPerform = twoPairComparison(toCompareIndexes);
                        break;

                    case 4:
                        // Three of a kind
                        comparisonToPerform = threeOfAKindComparison(toCompareIndexes);
                        break;

                    case 5:
                        // Straight (same as high card)
                        comparisonToPerform = straightComparison(toCompareIndexes);
                        break;

                    case 6:
                        // Flush (same as high card)
                        comparisonToPerform = determineHighCard(toCompareIndexes);
                        break;

                    case 7:
                        // Full house
                        comparisonToPerform = fullHouseComparison(toCompareIndexes);
                        break;

                    case 8:
                        // Four of a kind
                        comparisonToPerform = fourOfAKindComparison(toCompareIndexes);
                        break;

                    case 9:
                        // Straight flush (same as high card)
                        comparisonToPerform = straightComparison(toCompareIndexes);
                        break;

                    case 10:
                        // Royal flush (all royal flushes are equal)
                        return toCompareIndexes;
                }

            

                // Perform the comparison

                // toCompareIndexes - indexes of players // 1 3 5

                // comparisonToPerform - int array to use for comparison //L1:0  L2 L3
                // 1 2 3 2 9 
                // 2 4 5 4 9
                // 1 4 7 8 9

                // we set the first value (whatever it is, to the max)
                // keep track of the locations of the max aswell

                // WHILE PLAYERS BEING COMPARED > 1

                // on next iteration

                    // if currentValue >
                    // remove the old value, update max

                    // if currentValue <
                    // remove current value

                    // if currentValue == 
                    // keep track of location
                    // continue

                int column = comparisonToPerform[0].Count - 1;
 
                while (column >= 0)
                {  
                    // if there's only one player left in list -- return that player
                    if (toCompareIndexes.Count == 1)
                    {
                        return toCompareIndexes;
                    }

                    // find the highest rank in current column
                    var highestRank = 0;
                    for (int i = toCompareIndexes.Count - 1; i >= 0; i--) {
                        if(comparisonToPerform[i][column] > highestRank){
                        highestRank = comparisonToPerform[i][column];
                        }
                    }

                    // remove lists where current column is not equal to max
                    for (int i = toCompareIndexes.Count - 1; i >= 0; i--) {
                        if(comparisonToPerform[i][column] != highestRank){
                            comparisonToPerform.RemoveAt(i);
                            toCompareIndexes.RemoveAt(i);
                        }
                    }

                    // if we run out of columns to compare, return the remaining playerse

                    column -= 1;

                }

                return toCompareIndexes;



                //  for (int i = this.handLength; i >= 0; i--) {
                //     foreach (var player in toCompareIndexes)
                //     {
                //         if (this players card is greater than the current max card)
                //         {
                //             if (this.PokerHands[player][i] > max)
                //             {
                //                 // max = this.PokerHands[player][i];
                //             }
                //         }   
                //     }

                // }

                // ideally - we end up with an array
                // walk backwards through that array
                // make comparison

                // high card
                // 2 3 4 5 6 <- walk through backwards

                // pair
                // 2 3 4 [5 (pair)]

                // two pairs
                // 2 [3 (pair)] [4 (pair)]

                // three of a kind
                // 2 3 [4 (three of a kind)]

                // four of kind
                // 2 [3 (four of a kind)]

                // full house
                // [2 (pair)] [3 (three of a kind)]

                // flush (as for high card)

                // straight (if ace low straight ace = 1, ace high = 14)
                // compare top rank
                // [high card]

                // straight flush (as above)

                // if winner undecidable - return both players at max (list of drawed players)


                // if hands are equal -> compare the value of the hands with each other
                // Comparison of same hand - pair has different values
                // player 1 has pair of kings -- player 2 has pair of aces
                // player 2 wins

                // Comparison of same hand - same pair
                // player 1 has pair of kings (10, 5, 3) -- player 2 has pair of kings (Q, 4, 6)
                // winner is determined based on highest card outside of pair
            }

           
        }

        public List<List<int>> determineHighCard(List<int> toCompareIndexes){

            var comparisonToPerform = new List<List<int>>();

            for (int i = 0; i < toCompareIndexes.Count; i++) {
                var list = new List<int> ();
                for (int j = 0; j < this.PokerHands[toCompareIndexes[i]].HandCards.Count; j++) {
                    list.Add ((int) this.PokerHands[toCompareIndexes[i]].HandCards[j].Rank);
                }
                list.Sort();
                comparisonToPerform.Add (list);

            }
            return comparisonToPerform;

        }

        public List<List<int>> onePairComparison(List<int> toCompareIndexes){

            var comparisonToPerform = new List<List<int>>();
            
            for (int i = 0; i < toCompareIndexes.Count; i++) {
                 var temp = new List<int> ();
                 var list = new List<int> ();

                 // add all cards
                 int pair = 0;

                 for (int j = 0; j < this.PokerHands[toCompareIndexes[i]].HandCards.Count; j++) {
                    temp.Add ((int) this.PokerHands[toCompareIndexes[i]].HandCards[j].Rank);
                    temp.Sort ();
                               
                }
                // Determine pair

                for (int l = 1; l < temp.Count; l++) {
                    if (temp[l] == temp[l - 1]) {
                        pair = temp[l];

                    // 1 3 9 [7 pair] | 2 3 4 9 9
                    }
                }            
        
                // Add non pair cards

                for (int k = 0; k < temp.Count; k++) {
                    if (temp[k] != pair) {
                        list.Add (temp[k]);
                }
                }
                
                // Add pair

                list.Add (pair);
                comparisonToPerform.Add (list);

            }
            return comparisonToPerform;                       
        }
        
        public List<List<int>> twoPairComparison(List<int> toCompareIndexes) {
            // array [lowCard, lowPair, highPair]
            var comparisonToPerform = new List<List<int>>();

            for (int i = 0; i < toCompareIndexes.Count; i++) {
                var ranks = new int[15];
                int lowPair = 0;
                int highPair = 0;
                int remainingCard = 0;

                foreach (var card in this.PokerHands[toCompareIndexes[i]].HandCards)
                {
                    ranks[(int) card.Rank] += 1;
                }

                lowPair = Array.IndexOf(ranks, 2);
                highPair = Array.LastIndexOf(ranks, 2);
                remainingCard = Array.IndexOf(ranks, 1);

                comparisonToPerform.Add(new List<int>() { remainingCard, lowPair, highPair });
            }
            return comparisonToPerform;
        }
        
        public List<List<int>> threeOfAKindComparison(List<int> toCompareIndexes) {
            // array [lowestCard, secondLowestCard, threeOfAKind]
            var comparisonToPerform = new List<List<int>>();

            for (int i = 0; i < toCompareIndexes.Count; i++) {
                var ranks = new int[15];
                int lowestCard = 0;
                int secondLowestCard = 0;
                int threeOfAKind = 0;

                foreach (var card in this.PokerHands[toCompareIndexes[i]].HandCards)
                {
                    ranks[(int) card.Rank] += 1;
                }

                lowestCard = Array.IndexOf(ranks, 1);
                secondLowestCard = Array.LastIndexOf(ranks, 1);
                threeOfAKind = Array.IndexOf(ranks, 3);

                comparisonToPerform.Add(new List<int>() { lowestCard, secondLowestCard, threeOfAKind });
            }
            return comparisonToPerform;
        }

        public List<List<int>> fullHouseComparison(List<int> toCompareIndexes) {
            // array [pair, threeOfAKind]
            var comparisonToPerform = new List<List<int>>();

            for (int i = 0; i < toCompareIndexes.Count; i++) {
                var ranks = new int[15];
                int pair = 0;
                int threeOfAKind = 0;

                foreach (var card in this.PokerHands[toCompareIndexes[i]].HandCards)
                {
                    ranks[(int) card.Rank] += 1;
                }

                pair = Array.IndexOf(ranks, 2);
                threeOfAKind = Array.IndexOf(ranks, 3);

                comparisonToPerform.Add(new List<int>() { pair, threeOfAKind });
            }
            return comparisonToPerform;
        }

        public List<List<int>> fourOfAKindComparison(List<int> toCompareIndexes) {
            // array [otherCard, fourOfAKind]
            var comparisonToPerform = new List<List<int>>();
            
            for (int i = 0; i < toCompareIndexes.Count; i++) {
                 var temp = new List<int> ();
                 var list = new List<int> ();

                 // add all cards
                 int fourOfAKind = 0;


                 for (int j = 0; j < this.PokerHands[toCompareIndexes[i]].HandCards.Count; j++) {
                    temp.Add ((int) this.PokerHands[toCompareIndexes[i]].HandCards[j].Rank);
                    temp.Sort ();
                               
                }
                // Determine four of a kind

                for (int l = temp.Count - 1; l > temp.Count - 2; l--) {
                    if (temp[l] == temp[l - 1] && temp[l] == temp[l-2]  && temp[l] == temp[l-3]) {
                        fourOfAKind = temp[l];
                    }
                }            
        
                // Add non pair cards

                for (int k = 0; k < temp.Count; k++) {
                    if (temp[k] != fourOfAKind) {
                        list.Add (temp[k]);
                    }
                }
                
                // Add four of a kind

                list.Add (fourOfAKind);
                comparisonToPerform.Add (list);

            }

            return comparisonToPerform;                       
        }

        public List<List<int>> straightComparison(List<int> toCompareIndexes)
        {
            var comparisonToPerform = new List<List<int>>();

            for (int i = 0; i < toCompareIndexes.Count; i++) {
                var ranks = new int[15];

                foreach (var card in this.PokerHands[toCompareIndexes[i]].HandCards)
                {
                    ranks[(int) card.Rank] += 1;
                }

                if (ranks[2] == 1 && ranks[14] == 1)
                {
                    comparisonToPerform.Add(new List<int>() { 5 });
                }
                else
                {
                    for (int j = ranks.Length - 1; j > 1; j--)
                    {
                        if (ranks[j] == 1)
                        {
                            comparisonToPerform.Add(new List<int>() { j });
                            break;
                        }
                    }
                }
            }

            return comparisonToPerform;
        }


    }
}