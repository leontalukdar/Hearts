using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Program
    {
        static void Main(string[] args)
        {
            //CardSet cardset = new CardSet();
            //Console.WriteLine(cardset);


            //for (int i = 0; i < 11; i++)
            //{
            //    cardset.RiffleShuffle();
            //}


            //HashSet<Card>[] cardSegments = cardset.GetEqualSegments(4);
            //Hand[] hands = new Hand[4];

            //for (int i = 0; i < 4; i++)
            //{
            //    hands[i] = new Hand(cardSegments[i]);
            //    hands[i].Sort();
            //}

            //foreach (var hand in hands)
            //{
            //    Console.WriteLine("[ HAND ]");
            //    Console.WriteLine(hand);
            //    Console.WriteLine();
            //}


            //Console.WriteLine("[ PULLING CARD ]");
            //int handNo, suit, number;

            //Console.WriteLine("\nPlease enter hand, suit and number to pull card : ");
            //string input = Console.ReadLine();
            //string[] inputs = input.Split(' ');
            //handNo = int.Parse(inputs[0]);
            //suit = int.Parse(inputs[1]);
            //number = int.Parse(inputs[2]);

            //Console.WriteLine("\nPulling {0} of {1} from hand {2}", CardInfo.GetNumber(number), CardInfo.GetSuit(suit), handNo);
            //hands[handNo].PullCard(suit, number);
            //Console.WriteLine(hands[handNo]);



            //Console.WriteLine("\n[ PULLING CARD ALTERNATE ]");
            //int handNo, suit, number;
            //string strSuit, strNumber;

            //Console.WriteLine("\nPlease enter hand, suit and number to pull card : ");
            //string input = Console.ReadLine();
            //string[] inputs = input.Split(' ');
            //handNo = int.Parse(inputs[0]);
            //strSuit = inputs[1];
            //strNumber = inputs[2];
            //suit = CardInfo.GetSuit(strSuit);
            //number = CardInfo.GetNumber(strNumber);

            //Console.WriteLine("\nPulling {0} of {1} from hand {2}", number, suit, handNo);
            //hands[handNo].PullCard(suit, number);
            //Console.WriteLine(hands[handNo]);


            Player[] players = { new Player("Ayon"), new Player("Leon"), new Player("Sazid"), new Player("Mota") };
            Game hearts = new Game(players);

            hearts.Play();

            Console.ReadKey();
        }
    }
}
