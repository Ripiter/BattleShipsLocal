using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShips
{
    class Program
    {
        static Field[,] fields = new Field[24, 24];
        static Connection con = new Connection();
        static object _lock = new object();
        static void Main(string[] args)
        {
            con.Connect();
            con.gotField += Con_gotField;

            for (int x = 0; x < fields.GetLength(0); x++)
            {
                for (int y = 0; y < fields.GetLength(1); y++)
                {
                    fields[x, y] = new Field();
                }
            }

            DrawFiled();
            Thread inputThread = new Thread(Input);
            inputThread.Start();
        }
        static void Input()
        {
            string s = string.Empty;
            while (!string.IsNullOrEmpty((s = Console.ReadLine())))
            {
                con.SendMessage(s);
            }
        }

        static void DrawFiled()
        {
            Console.Clear();
            for (int x = 0; x < fields.GetLength(0); x++)
            {
                for (int y = 0; y < fields.GetLength(1); y++)
                {
                    Console.Write(fields[x, y].FieldCharacter);
                }
                Console.WriteLine();
            }
            Console.WriteLine("Type coords like 14" + con.spliter + "2");
        }

        private static void Con_gotField(Field field)
        {
            lock (_lock)
            {
                fields[field.X, field.Y] = field;
                DrawFiled();
            }
        }
    }
}
