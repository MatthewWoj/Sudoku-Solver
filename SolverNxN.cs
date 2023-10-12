using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
	public abstract class SolverNxN : BaseGroupSolver
	{

		public SolverNxN()
		{

		}

		//Implementation of NxN
		// "advanced" *Dictionary* and *List* work

		protected SolveResult SolveForMany(ISudokuSquare[] group, SolveResult result, List<int> indicesToCheck, TypeOfGroup groupKind)
		{
            if (!NoteCountsAreSufficient(group, indicesToCheck, out List<List<int>> allNoteNumbers))
			{
				return SolveResult.None;
			} 				
			//  N squares that all contain N notes or fewer (in indicesToCheck)
			return SolveForManyWithIndicesToCheck(group, ref result, indicesToCheck, groupKind, allNoteNumbers);
		}

		bool NoteCountsAreSufficient(ISudokuSquare[] group, List<int> indicesToCheck, out List<List<int>> allNoteNumbers)
		{
			allNoteNumbers = new List<List<int>>();
			foreach (int index in indicesToCheck)
			{
				ISudokuSquare square = group[index];
				List<int> squareNoteNumbers = GetNumbers(square.Notes);
				allNoteNumbers.Add(squareNoteNumbers);
                if (squareNoteNumbers.Count < 2 || squareNoteNumbers.Count > indicesToCheck.Count)
				{
					return false;
				} 			
			}
			return true;
		}

		// Solve for many
		private SolveResult SolveForManyWithIndicesToCheck(ISudokuSquare[] group, ref SolveResult result, List<int> indicesToCheck, TypeOfGroup groupKind, List<List<int>> allNoteNumbers)
		{
			
			if (groupKind == TypeOfGroup.Column || groupKind == TypeOfGroup.Row)
			{
				Dictionary<int, List<int>> blockMap = new Dictionary<int, List<int>>();

				foreach (int index in indicesToCheck)
				{
					int key = group[index].Block;
                    if (!blockMap.ContainsKey(key))
					{
						blockMap.Add(key, new List<int>());
					} 
						
					List<int> allBlockNotes = blockMap[key];
					List<int> notes = GetNumbers(group[index].Notes);
                    foreach (int note in notes)
					{
                        if (allBlockNotes.IndexOf(note) < 0)
						{
							allBlockNotes.Add(note);
						}						
					} 				
				}

				if (blockMap.Count > 1)
				{
					foreach (int key in blockMap.Keys)
					{
						List<int> targetBlock = blockMap[key];
						List<int> resultsForThisSubtraction = new List<int>();
						resultsForThisSubtraction = targetBlock.ToList();
						foreach (int sourceKey in blockMap.Keys)
						{
							List<int> sourceBlock = blockMap[sourceKey];
                            if (targetBlock == sourceBlock)
							{
								continue;
							} 							

							foreach (int note in sourceBlock)
							{
                                if (resultsForThisSubtraction.IndexOf(note) >= 0)
								{
									resultsForThisSubtraction.Remove(note);
								}								
							}
						}

						if (resultsForThisSubtraction.Count > 0)
						{
							ISudokuSquare[] blocks = SudokuBoard.GetBlock(key);

						}
					}
				}

			}

			List<int> allNumbersFound = new List<int>();
            foreach (List<int> noteNumbers in allNoteNumbers)
			{
				AddNumbersTo(allNumbersFound, noteNumbers);
			}

            if (allNumbersFound.Count > indicesToCheck.Count)
			{
				return SolveResult.None;
			} 			
			else
				for (int i = 0; i < group.Length; i++)
				{
                    if (indicesToCheck.Contains(i))
					{
						continue;
					}

                    if (RemoveNotesFrom(group[i], string.Join(", ", allNumbersFound)) == SolveResult.SquaresSolved)
					{
						result = SolveResult.SquaresSolved;
					} 			
				}
			return result;
		}

		private static void AddNumbersTo(List<int> targetNumbers, List<int> sourceNumbers)
		{
            foreach (int number in sourceNumbers)
			{
                if (!targetNumbers.Contains(number))
				{
					targetNumbers.Add(number);
				} 				
			} 				
		}
	}
}
