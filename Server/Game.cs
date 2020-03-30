using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Game
    {
        Player[] players;
        public Game()
        {
            // Initialize game
        }

        public int GetAmountofPlayersInGame()
        {
            int amount = 0;

            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] == null)
                    amount++;
            }
            return amount;
        }

        
    }
}
