using System;
using System.Collections.Generic;

namespace PokerHand
{
    public enum PokerHand
    {
        HighCard = 1,
        OnePair = 2,
        TwoPairs = 3,
        ThreeOfAKind = 4,
        Straight = 5,
        Flush = 6,
        FullHouse = 7,
        FourOfAKind = 8,
        StraightFlush = 9,
        RoyalFlush = 10
    }
    
    public class Hand
    {
        public List<Card> HandCards;

        public Hand()
        {
          this.HandCards = new List<Card>(); 
        }

        public void AddHand(List<Card> cards)
        {
            this.HandCards = cards;
        }

        public void AddCard(Card card){
            this.HandCards.Add(card);
        }

        public void AddCard(CardSuit suit, CardRank rank)
        {
            Card newCard = new Card(suit, rank);
            this.HandCards.Add(newCard);
        }

        public PokerHand DetermineHand()
        {
            // determine each hand rank (e.g. High Card, Royal Flush, Straight)
            // determine if hand has duplicates --> HashSet

            var uniqueCards = new HashSet<CardRank>();
            var ranks = new int[15];
            List<int> uniqueCardsSuite = new List<int>();

            foreach (var card in HandCards)
            {
                uniqueCards.Add(card.Rank);
                uniqueCardsSuite.Add((int) card.Suit);
                ranks[(int) card.Rank] += 1;
            }

            //Branch B (no duplicates) -- straight, flush, straight flush, royal flush, high card --> (the last possibility)

            if (uniqueCards.Count == 5)
            {
                    var clubsCount = 0;
                    var diamondsCount = 0;
                    var hearstCount = 0;
                    var spadeCount = 0;
                    
                    var hasStraight = false;
                    var hasFlush = false;
                    var hasAce = false;
                    var hasAceLow = false;

                    List<int> hand = new List<int>();
                    
                    foreach(var cardRank in uniqueCards){

                        hand.Add((int)cardRank);
                        hand.Sort();
                        if ((int) cardRank == 14)
                        {
                            hasAce = true;
                        }
                    
                    }

                    if (hand[0] == 2 && hasAce)
                    {
                        for (int i = 1; i < hand.Count - 1; i++)
                        {
                            if (hand[i] == hand[i - 1] + 1)
                            {
                                if (i == hand.Count - 2)
                                {
                                    hasAceLow = true;
                                    hasStraight = true;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i < hand.Count; i++){
                            if (hand[i] == hand[i - 1] + 1)
                            {
                                if (i == hand.Count - 1)
                                {
                                    hasStraight = true;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    foreach(var suit in uniqueCardsSuite){
                        switch (suit)
                        {
                            case 1:
                                clubsCount ++;
                                break;
                            case 2:
                                diamondsCount ++;
                                break;
                            case 3: 
                                hearstCount ++;
                                break;
                            case 4:
                                spadeCount ++;
                                break;
                        }

                        if(clubsCount == 5 || diamondsCount == 5 || hearstCount == 5 || spadeCount == 5){
                            hasFlush = true;
                        }
                    
                    }

                    if (hasStraight && hasFlush && hasAce && !hasAceLow)
                    {
                        return PokerHand.RoyalFlush;
                    }
                    else if (hasStraight && hasFlush)
                    {
                        return PokerHand.StraightFlush;
                    }
                    else if (hasFlush)
                    {
                        return PokerHand.Flush;
                    }
                    else if (hasStraight){
                        return PokerHand.Straight;
                    }
                    else
                    {
                        return PokerHand.HighCard;
                    }


            }

            // Branch A (has duplicates) -- one pair, two pair, three of a kind, full house, four of a kind

            else
            {
                var hasOnePair = false;
                var firstPairIndex = 0;

                if (Array.IndexOf(ranks, 2) > 0)
                {
                    firstPairIndex = Array.IndexOf(ranks, 2);
                    hasOnePair = true;
                }

                if (hasOnePair && Array.LastIndexOf(ranks, 2) != firstPairIndex)
                {
                    return PokerHand.TwoPairs;
                }

                if (Array.IndexOf(ranks, 3) > 0 && !hasOnePair)
                {
                    return PokerHand.ThreeOfAKind;
                }

                if (Array.IndexOf(ranks, 3) > 0 && hasOnePair)
                {
                    return PokerHand.FullHouse;
                }
                
                if (hasOnePair)
                {
                    return PokerHand.OnePair;
                }
                else
                {
                    return PokerHand.FourOfAKind;
                }
            }
        }
    }
}