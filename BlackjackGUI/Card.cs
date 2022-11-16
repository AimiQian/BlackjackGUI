using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Author: Aimi Qian

namespace BlackJackProject
{
    public class Card
    {
        public string Suit { get; set; }  //Hearts, Diamonds, Spades, Clubs
        public int Rank { get; set; }// 1 - 13


        public Card(string suit, int rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public String GetName()
        {
            string name = Rank.ToString();
            if (Rank == 1) name = "Ace";
            if (Rank == 11) name = "Jack";
            if (Rank == 12) name = "Queen";
            if (Rank == 13) name = "King";
            return name + " of " + Suit;
        }

        public String GetImgFilename()
        {
            string name = Rank.ToString();
            if (Rank == 1) name = "A";
            if (Rank == 11) name = "J";
            if (Rank == 12) name = "Q";
            if (Rank == 13) name = "K";
            return name + Suit.Substring(0,1) + ".jpg";
        }

        public override string ToString()
        {
            return this.GetName();
        }
    }
}
