using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    class Field
    {
		private char fieldCharacter = 'X';

		public char FieldCharacter
		{
			get { return fieldCharacter; }
			set { fieldCharacter = value; }
		}

		private int x;

		public int X
		{
			get { return x; }
			set { x = value; }
		}

		private int y;

		public int Y
		{
			get { return y; }
			set { y = value; }
		}

	}
}
