using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Player
    {
        //private User user;
        public string name { get; private set; }
        private Hand hand;
        public List<HashSet<Card>> tricks { get; set; }
        private HashSet<Card> threeCards = new HashSet<Card>();
        private HashSet<Card> winningHandCards = new HashSet<Card>();
        private bool isValidCardPlayed;
        private bool isValidCardSelected;
        private bool firstPlayer;
        private int score;


        public Player() { }

        //public Player(User user)
        //{
        //    this.user = user;
        //}

        public Player(string name)
        {
            this.name = name;
        }

        public void AssignHand(Hand hand, int roundNumber)
        {
            this.hand = hand;
            if (roundNumber != 3)
                SetThreeCards();
            score = 0;
        }
        public Card PlayCard()
        {
            Card card = new Card();
            do
            {
                int suit, number;
                string strSuit, strNumber;
                ShowHand();
                Console.WriteLine("{0}'s Turn:", this.name);
                Console.WriteLine("Select a card ( Suit, Number ): ");
                try
                {
                    string[] inputs = Console.ReadLine().Split(' ');
                    strSuit = inputs[0];
                    strNumber = inputs[1];
                    suit = CardInfo.GetSuit(strSuit);
                    number = CardInfo.GetNumber(strNumber);
                    card = this.hand.PullCard(suit, number);
                    CheckValidity(card, this);
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Demo valid format :clubs 2");
                }
            } while ((!isValidCardPlayed));
            return card;
        }

        private void CheckValidity(Card card, Player player)
        {
            if (card == null)
            {
                isValidCardPlayed = false;
                Console.WriteLine("Please select an existing card");
                return;
            }
            if (!Game.GetPlayingTable().Any() && !Game.firstPlayed)
            {
                if (card.suit == CardInfo.GetSuit("clubs") && card.number == CardInfo.GetNumber("2"))
                {
                    Game.firstPlayed = true;
                    isValidCardPlayed = true;
                }
                else
                {
                    Console.WriteLine("Please play 2 of clubs");
                    isValidCardPlayed = false;
                    player.hand.PushCard(card);
                }
            }
            else if (!Game.GetPlayingTable().Any())
            {
                if (card.suit == CardInfo.GetSuit("Hearts"))
                {
                    if (Game.heartsBreak)
                        isValidCardPlayed = true;
                    else
                    {
                        if (player.hand.Contains(CardInfo.GetSuit("Clubs"))
                            || player.hand.Contains(CardInfo.GetSuit("Spades"))
                            || player.hand.Contains(CardInfo.GetSuit("Diamonds")))
                        {
                            Console.WriteLine("Hearts has not been broken yet.");
                            isValidCardPlayed = false;
                        }
                        else
                        {
                            isValidCardPlayed = true;
                            Game.heartsBreak = true;
                        }
                    }
                }
                else
                    isValidCardPlayed = true;
            }
            else
            {
                Card temp = Game.GetPlayingTable().ElementAt(0);
                bool isSuitExit = player.hand.Contains(temp.suit);
                if (isSuitExit)
                {
                    if (temp.suit == card.suit)
                    {
                        isValidCardPlayed = true;
                    }
                    else
                    {
                        Console.WriteLine("Please play a valid card");
                        isValidCardPlayed = false;
                        player.hand.PushCard(card);
                    }
                }
                else
                {
                    if (card.suit == CardInfo.GetSuit("Hearts"))
                    {
                        if (Game.heartsBreak)
                            isValidCardPlayed = true;
                        else
                        {
                            if (player.hand.Contains(CardInfo.GetSuit("Clubs"))
                                || player.hand.Contains(CardInfo.GetSuit("Spades"))
                                || player.hand.Contains(CardInfo.GetSuit("Diamonds")))
                            {
                                Console.WriteLine("Hearts has not been broken yet.");
                                isValidCardPlayed = false;
                            }
                            else
                            {
                                isValidCardPlayed = true;
                                Game.heartsBreak = true;
                            }
                        }
                    }
                    else
                        isValidCardPlayed = true;
                }
            }
        }
        public void ShowHand()
        {
            Console.WriteLine("\n[ {0}'s HAND ]", name);
            Console.WriteLine(hand);
            Console.WriteLine();
        }

        public void SetFirstPlayer()
        {
            Card card = new Card();
            string strSuit = "Clubs";
            string strNumber = "2";
            //Find two of clubs
            
            if(this.hand.Contains(CardInfo.GetSuit(strSuit), CardInfo.GetNumber(strNumber)))
                    firstPlayer = true;
            else
                firstPlayer = false;
        }

        public bool GetFirstPlayer()
        {
            return this.firstPlayer;
        }

        public void SetThreeCards()
        {
            Card card = new Card();
            Console.WriteLine("\n\nSelect Three Cards to Interchage.");
            threeCards.Clear();
            for (int i = 0; i < 3; i++)
            {
                do
                {
                    int suit, number;
                    string strSuit, strNumber;
                    ShowHand();
                    Console.WriteLine("{0}'s Turn:", this.name);
                    Console.WriteLine("Select {0} Card ( Suit, Number ): ",Round(i));
                    try
                    {
                        string[] inputs = Console.ReadLine().Split(' ');
                        strSuit = inputs[0];
                        strNumber = inputs[1];
                        suit = CardInfo.GetSuit(strSuit);
                        number = CardInfo.GetNumber(strNumber);
                        card = this.hand.PullCard(suit, number);
                        CheckThreeCardsValidity(card, this);
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Demo valid format :clubs 2");
                    }
                } while (!isValidCardSelected);
                threeCards.Add(card);
            }
        }

        private void CheckThreeCardsValidity(Card card,Player player)
        {
            if (card == null)
            {
                isValidCardSelected = false;
                Console.WriteLine("Please select an existing card");
                return;
            }
            else
                isValidCardSelected = true;
        }

        public HashSet<Card> GiveThreeCards()
        {
            return this.threeCards;
        }

        public void GetThreeCards(HashSet<Card> cards)
        {
            Card card = new Card();
            for(int i=0;i<3;i++)
            {
                card = cards.ElementAt(i);
                if(card!=null)
                {
                    this.hand.PushCard(card);
                }
            }
        }

        private string Round(int round)
        {
            switch(round)
            {
                case 0:
                    return "First";
                case 1:
                    return "Second";
                case 2:
                    return "Third";
                default:
                    return null;
            }
        }

        public void SetWinningHand(HashSet<Card> cards)
        {
            foreach(Card card in cards)
            {
                winningHandCards.Add(card);
            }
        }

        public HashSet<Card> GetWinningHand()
        {
            return this.winningHandCards;
        }

        public int GetScore()
        {
            if(Contains(CardInfo.GetSuit("Spades"),CardInfo.GetNumber("Queen")))
            {
                score -= 13;
            }
            if(Contains(CardInfo.GetSuit("Diamonds"), CardInfo.GetNumber("Jack")))
            {
                score += 10;
            }
            foreach(Card card in winningHandCards)
            {
                if(card.suit == CardInfo.GetSuit("Hearts"))
                {
                    score++;
                }
            }
            return score;
        }

        private bool Contains(int suit, int number)
        {
            foreach (Card card in winningHandCards)
            {
                if (card.suit == suit && card.number == number)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
