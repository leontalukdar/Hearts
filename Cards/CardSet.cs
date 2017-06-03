using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cards
{
    public class CardSet
    {
        private int suitCount = CardInfo.suits.Length;
        private int numberCount = CardInfo.numbers.Length;
        private int cardCount = CardInfo.suits.Length * CardInfo.numbers.Length;

        private HashSet<Card> cards = new HashSet<Card>();

        public CardSet()
        {
            ReOrder();
        }

        public void ReOrder()
        {
            HashSet<Card> newCards = new HashSet<Card>();

            for (int i = 0; i < suitCount; i++)
            {
                for (int j = 0; j < numberCount; j++)
                {
                    newCards.Add(new Card(i, j));
                }
            }

            cards = newCards;
        }

        public void RiffleShuffle()
        {
            HashSet<Card> newCards = new HashSet<Card>();

            int segmentCount = 2;
            HashSet<Card>[] cardSegments = GetEqualSegments(segmentCount);
            Random randomGenerator = new Random((int)DateTime.Now.Ticks);

            for (int i = 0, k = 0; i < cardSegments[0].Count; i++)
            {
                int min = 1, max = 5;
                int shuffleCount = randomGenerator.Next(min, max);

                for (int j = 0; j < shuffleCount; j++, k++)
                {
                    if (k < cardSegments[1].Count)
                    {
                        newCards.Add(cardSegments[1].ElementAt(k));
                    }
                }

                newCards.Add(cardSegments[0].ElementAt(i));
            }

            cards = newCards;
        }

        public HashSet<Card>[] GetEqualSegments(int segmentCount)
        {
            HashSet<Card>[] cardSegments = new HashSet<Card>[segmentCount];
            int cardsPerSegment = cardCount / segmentCount;
            int k = 0, l = 0;

            for (int i = 0; i < cardSegments.Length; i++)
            {
                cardSegments[i] = new HashSet<Card>();

                for (int j = 0; j < cardsPerSegment; j++, k++)
                {

                    cardSegments[i].Add(cards.ElementAt(k));
                }
            }

            // This section of code handles unequal segmentation. If still cards remains 
            // unassigned then they are cyclically assigned from the beginning.
            // for example, 52 / 3 = 17.33 --> 17
            // now, 17 * 3 = 51
            // so 1 card remains unassigned
            while (k < cardCount)
            {
                cardSegments[l++].Add(cards.ElementAt(k++));
                l %= segmentCount;
            }

            return cardSegments;
        }

        public override string ToString()
        {
            string str = "";

            foreach (var card in cards)
            {
                str += card + "\n";
            }

            return str;
        }
    }
}
