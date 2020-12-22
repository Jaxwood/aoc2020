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
                    player1.Win(playerOneCard, playerTwoCard);
                }
                else
                {
                    player2.Win(playerTwoCard, playerOneCard);
                }
            }

            return player1.HasCards() ? this.player1.Score() : this.player2.Score();
        }
    }
}
