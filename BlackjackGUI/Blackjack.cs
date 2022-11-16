using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Author: Aimi Qian

namespace BlackJackProject
{
    public class Blackjack
    {
        public Deck Shoe = new Deck();
        public List<Card> PlayersCards { get; set; }
        public List<Card> DealerCards { get; set; }

        public Blackjack()
        {
            PlayersCards = new List<Card>();
            DealerCards = new List<Card>();
        }

        public void DealCardToPlayer()
        {
            //Remove one Card from the shoe card and 
            //insert it into the PlayerCards collection
            Card c = Shoe.GetTopCard();
            PlayersCards.Add(c);
        }

        public void DealCardToDealer()
        {
            Card c = Shoe.GetTopCard();
            DealerCards.Add(c);

        }

        public void Start()
        {
            DealCardToPlayer();
            DealCardToDealer();
            DealCardToPlayer();
            DealCardToDealer();
        }

        public int GetPlayerSum()
        {
            int sum = 0;
            foreach(Card card in PlayersCards)
            {
                sum += GetBlackjackValue(card);
            }
            return sum;
        }

        public int GetDealerSum()
        {
            int sum = 0;
            foreach (Card card in DealerCards)
            {
                sum += GetBlackjackValue(card);
            }
            return sum;
        }

        public int GetBlackjackValue(Card c)
        {
            if (c.Rank > 10)
            {
                return 10;
            }
            else
            {
                return c.Rank;
            }
        }

         public int Hit()
        {
            DealCardToPlayer();
            if(GetPlayerSum() > 21)
            {
                return -1;
            }
            else if(GetPlayerSum() == 21)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int DealerHit()
        {
            DealCardToDealer();
            if (GetDealerSum() > 16)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
