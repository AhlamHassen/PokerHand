using System.Collections.Generic;

namespace PokerHand
{
    public class Person
    {
        static int personCount = 0;
        static List<Person> Persons = new List<Person>();

        public int Id;
        public int Score;

        public Person(){
            this.Id = Person.personCount + 1;
            this.Score = 0;
            
            Person.Persons.Add(this); 
        }

        public void IncreaseScore()
        {
            this.Score += 1;
        }
    }
}