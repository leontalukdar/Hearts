using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Game
    {
        private CardSet cardset = new CardSet();
        private static HashSet<Card> playingTable = new HashSet<Card>();
        private bool gameOver = false;
        private Player[] players;
        private int roundCount;
        public static bool firstPlayed = false;
        public static bool heartsBreak = false;
        private HashSet<Card> interchangingCards = new HashSet<Card>();
        private int[] trackPlayer = new int[4];
        private int[] playerScores = new int[4];
        private int gameWinner;

        public Game(Player[] players)
        {
            this.players = players;
            roundCount = 0;
            for (int i = 0; i < 4; i++)
                playerScores[i] = 0;
        }

        // Game Starts Here
        public void Play()
        {
            while (!gameOver)
            {
                Console.WriteLine("\n[ ROUND {0} ]\n", roundCount+1);
                ShuffleCards();
                DealCards(roundCount%4);

                if(roundCount%4==0)
                {
                    ChangeRight();
                }
                else if (roundCount % 4 == 1)
                {
                    ChangeLeft();
                }
                else if (roundCount % 4 == 2)
                {
                    ChangeFront();
                }

                //Set First Player
                for(int i=0;i<4;i++)
                {
                    players[i].SetFirstPlayer();
                }
                int startFromPlayer=0;
                for (int i=0;i<4;i++)
                {
                    if(players[i].GetFirstPlayer())
                    {
                        startFromPlayer = i;
                        break;
                    }
                }
                Console.WriteLine("First Player : {0}", players[startFromPlayer].name);
                Card card = new Card();
                for (int turns = 0; turns < 13; turns++)
                {
                    playingTable.Clear();
                    for (int i = 0; i < 4; i++)
                    {
                        card = players[startFromPlayer].PlayCard();
                        playingTable.Add(card);
                        trackPlayer[i] = startFromPlayer;
                        card = playingTable.ElementAt(0);
                        if(card!=null)
                            Console.WriteLine(card);
                        startFromPlayer++;
                        startFromPlayer %= 4;
                    }
                    int winner = GetPotWinner();
                    startFromPlayer = winner % 4;
                    players[winner].SetWinningHand(playingTable);
                    Console.WriteLine("\n\nCards on Table :");
                    foreach(Card cardOnTable in playingTable)
                    {
                        Console.WriteLine(cardOnTable);
                    }
                    Console.WriteLine("\nPot Winner : {0}",players[winner].name);
                }
                //TODO: Update Score of Each Player
                UpdateScore();
                ShowScore();
                if(CheckForWinner())
                {
                    gameWinner = GetGameWinner();
                    Console.WriteLine("\n\n\n\n\n*******************************************************");
                    Console.WriteLine("\n\t\t\t\tGAME OVER");
                    Console.WriteLine("\n\t\t\t\tWinner : {0}",players[gameWinner].name);
                    Console.WriteLine("\n\t\t\t\tScore : {0}",playerScores[gameWinner]);
                    gameOver = true;
                }
                roundCount++;
            }
        }

        // Deals cards among players
        private void DealCards(int j)
        {
            HashSet<Card>[] cardSegments = cardset.GetEqualSegments(players.Length);
            int roundNumber = j;

            for (int i = 0; i < cardSegments.Length; i++)
            {
                j++;
                j %= 4;
                players[j].AssignHand(new Hand(cardSegments[i]),roundNumber);
            }
        }

        private void ShuffleCards()
        {
            int shuffleCount = 9;

            for (int i = 0; i < shuffleCount; i++)
            {
                cardset.RiffleShuffle();
            }
        }

        private int GetPotWinner()
        {
            int winner = trackPlayer[0];
            Card[] cards = new Card[4];
            for(int i=0;i<4;i++)
            {
                cards[i] = playingTable.ElementAt(i);
            }
            Card card = new Card();
            card = cards[0];
            for(int i=1;i<4;i++)
            {
                card = Compare(card, cards[i]);
            }
            if(card!=null)
            {
                for(int i=0;i<4;i++)
                {
                    if(card == playingTable.ElementAt(i))
                    {
                        return trackPlayer[i];
                    }
                }
            }
            return 0;
        }

        public static HashSet<Card> GetPlayingTable()
        {
            return playingTable;
        }

        private void ChangeRight()
        {
            interchangingCards.Clear();
            for(int i=0;i<4;i++)
            {
                interchangingCards = players[i].GiveThreeCards();
                players[(i + 1) % 4].GetThreeCards(interchangingCards);
            }
        }

        private void ChangeLeft()
        {
            interchangingCards.Clear();
            for (int i = 4; i > 0; i--)
            {
                interchangingCards = players[i%4].GiveThreeCards();
                players[i - 1].GetThreeCards(interchangingCards);
            }
        }

        private void ChangeFront()
        {
            interchangingCards.Clear();
            for (int i = 0; i < 4; i++)
            {
                interchangingCards = players[i].GiveThreeCards();
                players[(i + 2) % 4].GetThreeCards(interchangingCards);
            }
        }

        private Card Compare(Card card1,Card card2)
        {
            if (card1.suit != card2.suit)
                return card1;
            else
            {
                if (card1.number > card2.number)
                    return card1;
                else
                    return card2;
            }
        }

        private void UpdateScore()
        {
            for(int i=0;i<4;i++)
            {
                playerScores[i] += players[i].GetScore();
            }
        }

        private void ShowScore()
        {
            Console.WriteLine("Name : Score\tName : Score\tName : Score\tName : Score");
            Console.WriteLine("{0} : {1}\t{2} : {3}\t{4} : {5}\t{6} : {7}",
                players[0].name,playerScores[0],players[1].name,playerScores[1],
                players[2].name, playerScores[2],players[3].name, playerScores[3]);
        }

        private bool CheckForWinner()
        {
            for(int i=0;i<4;i++)
            {
                if(playerScores[i]>=100)
                {
                    return true;
                }
            }
            return false;
        }

        private int GetGameWinner()
        {
            int score = 126, winner = -1;
            for(int i=0;i<4;i++)
            {
                if(score>playerScores[i])
                {
                    score = playerScores[i];
                    winner = i;
                }
            }
            return winner;
        }

    }
}