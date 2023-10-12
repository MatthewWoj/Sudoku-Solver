using System;
using System.Linq;

namespace Sudoku
{
	public static class SudokuBoard
	{
		public static ISudokuSquare[,] squares = new XamlSudokuSquare[9, 9];
		static ISudokuSquare selectedSquare;

		public static void Initialize()
		{
            for (int row = 0; row < 9; row++)
			{
				for (int column = 0; column < 9; column++)
				{
					squares[row, column].Row = row;
					squares[row, column].Column = column;
					int rowOffset;
                    if (row < 3)  // blocks 1, 2, or 3
					{
						rowOffset = 0;
					}
                    else if (row < 6)
					{
						rowOffset = 3;
					}
                    else
                    {
						rowOffset = 6;
					}
                    if (column < 3)
					{
						squares[row, column].Block = rowOffset + 1;
					}
                    else if (column < 6)
					{
						squares[row, column].Block = rowOffset + 2;
					}
                    else
                    {
						squares[row, column].Block = rowOffset + 3;
					}					
				}
			} 
		}

		public static XamlSudokuSquare SelectedSquare
		{
			get => SudokuBoard.selectedSquare as XamlSudokuSquare;
			set
			{
                if (selectedSquare == value)
				{
					return;
				} 
					
				ISudokuSquare oldSelectedSquare = selectedSquare;
                if (oldSelectedSquare is XamlSudokuSquare oldSquare)
				{
					oldSquare.HideSelection();
				} 
					
				selectedSquare = value;
                if (selectedSquare is XamlSudokuSquare sudokuSquare)
				{
					sudokuSquare.ShowSelection();
				} 				
			}
		}

		static void SelectSquare(ISudokuSquare sudokuSquare)
		{
			SelectedSquare = sudokuSquare as XamlSudokuSquare;
		}

		public static void SelectSquare(int row, int column)
		{
            if (column >= 9)
			{
				column -= 9;
			}
            if (column < 0)
			{
				column += 9;
			}
            if (row >= 9)
			{
				row -= 9;
			}
            if (row < 0)
			{
				row += 9;
			} 			
			SelectSquare(squares[row, column]);
		}

		public static ISudokuSquare[] GetColumn(int column)
		{
			ISudokuSquare[] result = new ISudokuSquare[9];
            for (int row = 0; row < 9; row++)
			{
				result[row] = SudokuBoard.squares[row, column];
			} 
			return result;
		}

		public static ISudokuSquare[] GetRow(int row)
		{
			ISudokuSquare[] result = new XamlSudokuSquare[9];
            for (int column = 0; column < 9; column++)
			{
				result[column] = SudokuBoard.squares[row, column];
			} 
			return result;
		}

		public static ISudokuSquare[] GetBlock(int row, int column)
		{
			int topRow = 3 * (int)Math.Floor((double)row / 3);
			int leftColumn = 3 * (int)Math.Floor((double)column / 3);
			ISudokuSquare[] result = new XamlSudokuSquare[9];
			int index = 0;
            for (int r = topRow; r < topRow + 3; r++)
			{
				for (int c = leftColumn; c < leftColumn + 3; c++)
				{
					result[index] = SudokuBoard.squares[r, c];
					index++;
				}
			} 				
			return result;
		}

		public static ISudokuSquare[] GetBlock(int blockNum)
		{
            if (blockNum == 1)
			{
				return GetBlock(0, 0);
			}
            else if (blockNum == 2)
			{
				return GetBlock(0, 3);
			}
            else if (blockNum == 3)
			{
				return GetBlock(0, 6);
			}
            else if (blockNum == 4)
			{
				return GetBlock(3, 0);
			}
            else if (blockNum == 5)
			{
				return GetBlock(3, 3);
			}
            else if (blockNum == 6)
			{
				return GetBlock(3, 6);
			}
            else if (blockNum == 7)
			{
				return GetBlock(6, 0);
			}
            else if (blockNum == 8)
			{
				return GetBlock(6, 3);
			}
            else if (blockNum == 9)
			{
				return GetBlock(6, 6);
			} 				
			else
			{
				System.Diagnostics.Debugger.Break();
				return GetBlock(0, 0);
			}
		}

		public static void SelectFirstSquare()
		{
			SelectSquare(squares[0, 0]);
		}

		public static void AddSquare(int row, int column, XamlSudokuSquare sudokuSquare)
		{
			squares[row, column] = sudokuSquare;
		}

		public static void ChangeText(int row, int column, string whatChanged)
		{
			squares[row, column].SetTextNoInternalEvents(whatChanged);
		}
	}
}
