using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
	public abstract class BaseGroupSolver
	{
		public abstract SolveResult Solve(ISudokuSquare[] group, TypeOfGroup groupKind);   

		//Getting numbers from notes
		// *Lists* 
		public static List<int> GetNumbers(string notes)
		{
			List<int> result = new List<int>();
			string[] splitStr = notes.Split(',');
            foreach (string item in splitStr)
			{
                if (int.TryParse(item.Trim(), out int parsedNumber))
				{
					result.Add(parsedNumber);
				} 				
			} 
			return result;
		}

		public static int NoteCount(string notes)
		{
            if (notes == null)
			{
				return 0;
			} 		
			return notes.Count(x => x == ',') + 1;
		}

		// Removing Note values from Notes according to logic
		// *Lists*
		public SolveResult RemoveNotesFrom(ISudokuSquare sudokuSquare, string notes)
		{
            if (string.IsNullOrEmpty(sudokuSquare.Notes))
			{
				return SolveResult.None;
			} 			
			else
			{
				List<int> toRemoveList = GetNumbers(notes);
				List<int> squareNotes = GetNumbers(sudokuSquare.Notes);
                foreach (int numberToRemove in toRemoveList)
				{
					squareNotes.Remove(numberToRemove);
				} 
		
				string newNotes = string.Join(", ", squareNotes);

				if (newNotes != sudokuSquare.Notes)
				{
					sudokuSquare.Notes = newNotes;
					return SolveResult.SquaresSolved;
				}
			}
			return SolveResult.None;
		}
	}
}
