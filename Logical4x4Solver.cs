using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
	public class Logical4x4Solver : SolverNxN
	{

		public Logical4x4Solver()
		{

		}

		// *Dictionary* and *List* work
		public override SolveResult Solve(ISudokuSquare[] group, TypeOfGroup groupKind)
		{
			SolveResult result = SolveResult.None;

            for (int first = 0; first < group.Length; first++)
			{
                for (int second = 0; second < group.Length; second++)
				{
                    if (second == first)
					{
						continue;
					}
                    else
                    {
                        for (int third = 0; third < group.Length; third++)
						{
                            if (third == first || third == second)
							{
								continue;
							}
                            else
                            {
                                for (int fourth = 0; fourth < group.Length; fourth++)
								{
                                    if (fourth == first || fourth == second || fourth == third)
									{
										continue;
									} 		
									else
									{
										List<int> indicesToCheck = new List<int>();
										indicesToCheck.Add(first);
										indicesToCheck.Add(second);
										indicesToCheck.Add(third);
										indicesToCheck.Add(fourth);
										result = SolveForMany(group, result, indicesToCheck, groupKind);
									}
								} 				
							}		
						} 		
					}		
				} 	
			} 		
			return result;
		}
	}
}
