namespace PokerHand
{
    public enum CardSuit
    {
        Clubs = 1,
        Diamonds = 2,
        Hearts = 3,
        Spades = 4
    }

    public enum CardRank
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11 ,
        Queen = 12,
        King = 13,
        Ace = 14
    }
    
    public class Card
    {
        public CardRank Rank;
        public CardSuit Suit;
        
        public Card(string card){
            switch (card.Substring(0, 1))
            {
               case "2":
                  this.Rank = CardRank.Two;
                  break;

               case "3":
                  this.Rank = CardRank.Three;
                  break;

               case "4":
                  this.Rank = CardRank.Four;
                  break;

               case "5":
                  this.Rank = CardRank.Five;
                  break;
                  
               case "6":
                  this.Rank = CardRank.Six;
                  break;
                  
               case "7":
                  this.Rank = CardRank.Seven;
                  break;
            
               case "8":
                  this.Rank = CardRank.Eight;
                  break;
                  
               case "9":                  
                  this.Rank = CardRank.Nine;
                  break;

               case "T":
                   this.Rank = CardRank.Ten;
                  break;
                  
               case "J":
                  this.Rank = CardRank.Jack;
                  break;

                case "Q":
                    this.Rank = CardRank.Queen;
                  break;
                  
               case "K":
                   this.Rank = CardRank.King;
                  break;
                  
                case "A":
                  this.Rank = CardRank.Ace;
                  break;
            }

            switch (card.Substring(1,1))
            {
                case "C":
                    this.Suit = CardSuit.Clubs;
                    break;

                case "D":
                    this.Suit = CardSuit.Diamonds;
                    break;

                case "H":
                    this.Suit = CardSuit.Hearts;
                    break;
                    
                case "S":
                    this.Suit = CardSuit.Spades;
                    break;
            }
           
        }
        
        public Card(CardSuit suit, CardRank rank)
        {
            this.Suit = suit;
            this.Rank = rank;
        }
    }
}