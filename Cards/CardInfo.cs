using System.Collections;
using System.Collections.Generic;

namespace Cards
{
    public static class CardInfo
    {
        public static string[] suits = { "Spades", "Hearts", "Diamonds", "Clubs" };
        public static string[] numbers = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

        public static string GetSuit(int suit)
        {
            if (suit >= 0 && suit < suits.Length)
            {
                return suits[suit];
            }
            else
            {
                return "None";
            }
        }
        
        public static int GetSuit(string suit)
        {
            suit = suit.ToLower();

            for (int i=0; i<suits.Length; i++)
            {
                if(suits[i].ToLower().Equals(suit))
                {
                    return i;
                }
            }

            return -1;
        }

        public static string GetNumber(int number)
        {
            if (number >= 0 && number < numbers.Length)
            {
                return numbers[number];
            }
            else
            {
                return "None";
            }
        }

        public static int GetNumber(string number)
        {
            number = number.ToLower();

            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i].ToLower().Equals(number))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}