using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAFIA.Models
{
    class Deck
    {
        private Card[] _deck;

        public Card[] deck
        {
            get => _deck;
            set => _deck = value;
        }

        public void ShuffleDeck()
        {
            Random random = new Random();

            int temp = random.Next(0, deck.Length);

            int[] RandomSet = new int[deck.Length];

            for (int i = 0; i < RandomSet.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (RandomSet[j] == temp)
                    { temp = random.Next(0, deck.Length); j = -1; }
                }
                RandomSet[i] = temp;

                random.Next(0, deck.Length);
            }

            Card[] newdeck = new Card[deck.Length];

            for (int i = 0; i < deck.Length; i++)
            {
                newdeck[RandomSet[i]] = deck[i];
            }

            deck = newdeck;
        }

        public Deck(int CardsNumber)
        {
            deck = new Card[CardsNumber];
        }        
    }

}
