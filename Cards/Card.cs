using System;
using System.Collections.Generic;




namespace Cards
{
    public class Card : IComparable<Card>, ICloneable
    {
        public int suit { get; set; }
        public int number { get; set; }

        public Card(int suit = -1, int number = -1)
        {
            this.suit = suit;
            this.number = number;
        }

        public string actualSuit
        {
            get { return CardInfo.GetSuit(suit); }
        }

        public string actualNumber
        {
            get { return CardInfo.GetNumber(number); }
        }

        public override string ToString()
        {
            string suit = actualSuit;
            string number = actualNumber;
            return string.Format("{0} of {1}", actualNumber, actualSuit);
        }

        public int CompareTo(Card c)
        {
            if (suit > c.suit)
            {
                return 1;
            }
            else if (suit == c.suit)
            {
                if (number > c.number)
                {
                    return 1;
                }
                else if (number == c.number)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        public Object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}