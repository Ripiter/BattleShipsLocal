using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Player
    {
        private PlayerMap playerMap;

        public PlayerMap PlayersMap
        {
            get { return playerMap; }
            set { playerMap = value; }
        }


        private char character;

        public char Character
        {
            get { return character; }
            set { character = value; }
        }


    }
}
