using System;
using System.Linq;

namespace Sudoku
{
	public abstract class Command
	{
		public abstract void Execute();
		public abstract void Undo();
	}
}
