using Aoc2020.Lib.Util;

namespace Aoc2020.Lib.Day22
{
    public class CombatGame
    {
        public long Play(Deck player1, Deck player2)
        {
            while (player1.HasCards() && player2.HasCards())
            {
                var playerOneCard = player1.Draw();
                var playerTwoCard = player2.Draw();
                if (playerOneCard.Value > playerTwoCard.Value)
                {
                    player1.Win(playerOneCard, playerTwoCard);
                }
                else
                {
                    player2.Win(playerTwoCard, playerOneCard);
                }
            }

            return player1.HasCards() ? player1.Score() : player2.Score();
        }

        public (Deck, Deck) PlayRecursive(Deck player1, Deck player2)
        {
            while (player1.HasCards() && player2.HasCards())
            {
                var playerOneCard = player1.Draw();
                var playerTwoCard = player2.Draw();

                if (playerOneCard is Failure<int> || playerTwoCard is Failure<int>)
                {
                    return (player1, player1);
                }
                if (player1.RecursiveMode(playerOneCard) && player2.RecursiveMode(playerTwoCard))
                {
                    var (p1, _) = this.PlayRecursive(player1.CreateRecursiveDeck(playerOneCard), player2.CreateRecursiveDeck(playerTwoCard));
                    if (p1.HasCards())
                    {
                        player1.Win(playerOneCard, playerTwoCard);
                    }
                    else
                    {
                        player2.Win(playerTwoCard, playerOneCard);
                    }
                }
                else if (playerOneCard.Value > playerTwoCard.Value)
                {
                    player1.Win(playerOneCard, playerTwoCard);
                }
                else
                {
                    player2.Win(playerTwoCard, playerOneCard);
                }
            }

            return (player1, player2);
        }
    }
}
