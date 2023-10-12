using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
	public class Logical3x3Solver : SolverNxN
	{

		public Logical3x3Solver()
		{

		}
		// *Dictionary* and *List* work
		public override SolveResult Solve(ISudokuSquare[] group, TypeOfGroup groupKind)
		{
			SolveResult result = SolveResult.None;

            //Find 3 Squares which are unique 
            for (int first = 0; first < group.Length; first++) //Itterates through the group
			{
                for (int second = 0; second < group.Length; second++) //Itterates through the group AGAIN
				{
                    if (second == first) //Makes sure they are unique
					{
						continue;
					}
                    else
                    {
						for (int third = 0; third < group.Length; third++) //Itterates through the group AGAIN AGAIN
						{
							if (third == first || third == second) //Makes sure that they are unique 
							{
								continue;
							}
							else
							{
								//Fianlly there are three unique indicies which can be added to a list of indicies
								List<int> indicesToCheck = new List<int>();
								indicesToCheck.Add(first);
								indicesToCheck.Add(second);
								indicesToCheck.Add(third);
								result = SolveForMany(group, result, indicesToCheck, groupKind);
							}
						}
					}                    				
				}				
			} 				
			return result;
		}
	}
}
