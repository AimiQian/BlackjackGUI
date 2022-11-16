using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Author: Aimi Qian

namespace BlackJackProject
{
    public class Deck
    {
        List<Card> cards = new List<Card>();

        public Deck()
        {
            Init();
            Shuffle(1000); // shuffle 1000 times.
        }
        public void Init()
        {
            int[] rankList = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13};
            string[] suitList = { "Hearts", "Spades", "Diamonds", "Clubs"};
            for(int i = 0; i < rankList.Length; i++)
            {
                for (int j = 0; j < suitList.Length; j++)
                    cards.Add(new Card(suitList[j],rankList[i]));
            }
        }

        //Generate two random indices, and swap those two cards
        //repeat this operation for n times. 
        public void Shuffle(int shuffleTimes)
        {
            Random random = new Random();
            int randomIndexA, randomIndexB;
            for (int i = 0; i < shuffleTimes; i++)
            {
               randomIndexA = random.Next(0, cards.Count - 1);
               randomIndexB = random.Next(0, cards.Count - 1);

                Card tmp = cards[randomIndexA];
                cards[randomIndexA] = cards[randomIndexB];
                cards[randomIndexB] = tmp;
            }
        }

        public Card GetTopCard()

        {
            Card topCard = cards[0];
            this.cards.Remove(topCard);
            return topCard;
        }

        public bool RemoveCard(Card c)
        {
            return this.cards.Remove(c);
        }

        public void PrintCards()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Console.WriteLine(cards[i]);
            }
        }
    }
   
}
