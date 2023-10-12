namespace Sudoku
{
	public class ChangeInTextCommand: Command
	{
		public ChangeInTextCommand()
		{
		}

		public string WhatChanged { get; }
		public int Row { get; }
		public int Column { get; }
		public string PreviousValue { get; set; }

		public ChangeInTextCommand(string previousValue, string whatChanged, int row, int column)
		{
			PreviousValue = previousValue;
			WhatChanged = whatChanged;
			Row = row;
			Column = column;
		}

		public override void Execute()
		{
			SudokuBoard.ChangeText(Row, Column, WhatChanged);
			SudokuBoard.SelectSquare(Row, Column);
		}

		public override void Undo()
		{
			SudokuBoard.ChangeText(Row, Column, PreviousValue);
			SudokuBoard.SelectSquare(Row, Column);
		}
	}
}
