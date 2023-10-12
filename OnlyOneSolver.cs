using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
	public class OnlyOneSolver : BaseGroupSolver
	{

		public OnlyOneSolver()
		{

		}


		bool GroupAlreadyIsShowing(ISudokuSquare[] group, int note)
		{
			char noteChar = note.ToString()[0];

			for (int i = 0; i < group.Length; i++)
			{
				ISudokuSquare sudokuSquare = group[i];
				if (sudokuSquare.Value == noteChar)
				{
					return true;
				}
			}

			return false;
		}

		// *Dictionary* and *List* work

		public override SolveResult Solve(ISudokuSquare[] group, TypeOfGroup groupKind)
		{
			SolveResult result = SolveResult.None;
			
			Dictionary<int, int> numbersFound = new Dictionary<int, int>();

			for (int i = 0; i < group.Length; i++)
			{
				ISudokuSquare sudokuSquare = group[i];
				List<int> notes = GetNumbers(sudokuSquare.Notes);
				foreach (int note in notes)
				{
                    if (!numbersFound.ContainsKey(note))
					{
						numbersFound.Add(note, 1);
					}
                    else
                    {
						numbersFound[note]++;
					}					
				}
			}
			
			foreach (int note in numbersFound.Keys)
			{
				if (numbersFound[note] == 1)
				{
					int onlyOneNote = note;
                    if (GroupAlreadyIsShowing(group, onlyOneNote))
					{
						continue;
					} 						
					// note has to be in this square
					for (int i = 0; i < group.Length; i++)
					{
						ISudokuSquare sudokuSquare = group[i];
						List<int> notes = GetNumbers(sudokuSquare.Notes);
						if (notes.Contains(onlyOneNote))
						{
							sudokuSquare.Notes = "";
							sudokuSquare.Value = onlyOneNote.ToString()[0];
							result = SolveResult.SquaresSolved;
						}
					}
				}
			}
			return result;
		}
	}
}
