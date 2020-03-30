using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    enum CommandType
    {
        PlayerAdded,
        GameStart,
        GameOver
    }
    class DataManager
    {
        public CommandType GetCommandType(string data)
        {
            if (data.Contains("Start"))
            {
                return CommandType.GameStart;
            }
            else if (data.Contains("Player"))
            {
                return CommandType.PlayerAdded;
            }
            return CommandType.GameOver;
        }
    }
}
