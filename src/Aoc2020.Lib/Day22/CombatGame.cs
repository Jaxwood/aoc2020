using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day22
{
    public class CombatGame
    {
        private readonly Deck player1;
        private readonly Deck player2;

        public CombatGame(IEnumerable<Deck> decks)
        {
            this.player1 = decks.First();
            this.player2 = decks.Last();
        }

        public long Play()
        {
            while (this.player1.HasCards() && this.player2.HasCards())
            {
                var playerOneCard = this.player1.Draw();
                var playerTwoCard = this.player2.Draw();
                if (playerOneCard > playerTwoCard)
                {
                    this.player1.Win(playerOneCard, playerTwoCard);
                }
                else
                {
                    this.player2.Win(playerTwoCard, playerOneCard);
                }
            }

            return this.player1.HasCards() ? this.player1.Score() : this.player2.Score();
        }

        public (Deck, Deck) PlayRecursive(Deck play1, Deck play2)
        {
            while (play1.HasCards() && play2.HasCards())
            {
                if (play1.PreviousHand() || play2.PreviousHand())
                {
                    return (play1, play1);
                }

                var playerOneCard = play1.Draw();
                var playerTwoCard = play2.Draw();

                if (play1.RecursiveMode(playerOneCard) && play2.RecursiveMode(playerTwoCard))
                {
                    var (p1, _) = this.PlayRecursive(
                        play1.CreateRecursiveDeck(playerOneCard, play1.Game + 1),
                        play2.CreateRecursiveDeck(playerTwoCard, play2.Game + 1));
                    if (p1.HasCards())
                    {
                        play1.Win(playerOneCard, playerTwoCard);
                    }
                    else
                    {
                        play2.Win(playerTwoCard, playerOneCard);
                    }
                }
                else if (playerOneCard > playerTwoCard)
                {
                    play1.Win(playerOneCard, playerTwoCard);
                }
                else
                {
                    play2.Win(playerTwoCard, playerOneCard);
                }
            }

            return (play1, play2);
        }
    }
}
