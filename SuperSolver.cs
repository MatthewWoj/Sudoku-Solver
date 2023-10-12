using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sudoku
{
    namespace Solving
    {
        class SolverV2
        {
            // heavy *Dictionary* and *List* work throughout 

            public static string SolveResultAI;
            public static string[] Squares;
            public static string columns;
            public static string rows;
            private static SortedList<string, string[]> peers;
            private static List<string[]> unitList;
            public static string UserString;

            public static SortedList<string, string> UpdateValidity { get; private set; }

            public static void InitiateSolverV2()
            {
                //Implementation of units, peers and squares
                
                columns = "123456789";
                rows = "ABCDEFGHI";
                Squares = CrossProduct(rows, columns);

                var UnitsofRow = new List<string[]>();
                foreach (var c in columns)
                {
                    UnitsofRow.Add(CrossProduct(rows, c.ToString()));
                }

                var UnitsofColumns = new List<string[]>();
                foreach (var r in rows)
                {
                    UnitsofColumns.Add(CrossProduct(r.ToString(), columns));
                }

                var squareUnits = new List<string[]>();
                foreach (var rX in new[] { "ABC", "DEF", "GHI" })
                {
                    squareUnits.AddRange(new[] { "123", "456", "789" }.Select(cX => CrossProduct(rX, cX)));
                }

                unitList = new List<string[]>();
                unitList.AddRange(UnitsofRow);
                unitList.AddRange(UnitsofColumns);
                unitList.AddRange(squareUnits);

                var units = new SortedList<string, string[][]>();
                foreach (var s in Squares)
                    units.Add(s, unitList.Where(x => x.Contains(s)).ToArray());

                peers = new SortedList<string, string[]>();
                foreach (var s in Squares)
                {
                    var peer = new List<string>();
                    foreach (var row in units[s])
                    {
                        var elemStrings = row.Where(x => x != s).ToArray();
                        foreach (var elem in elemStrings)
                        {
                            if (!peer.Contains(elem))
                                peer.Add(elem);
                        }
                    }

                    peers.Add(s, peer.ToArray());
                }
                //...
                var userPuzzle = UserString;
                RuleOut(ParseGrid(userPuzzle));
                Search(ParseGrid(userPuzzle), DateTime.Now.AddSeconds(10));
                if (UpdateValidity != null)
                {
                    Display(UpdateValidity);
                }              
                (UpdateValidity) = null;
            }

            //Cross product of elements in X and elements in Y"
            public static string[] CrossProduct(string x, string y)
            {
                var results = new List<string>();

                foreach (var charX in x)
                {
                    foreach (var charY in y)
                    {
                        results.Add(charX + "" + charY);
                    } 
                        
                } 
                return results.ToArray();
            }

            //Values will be a dict with squares as keys.
            //The value of each key will be the possible digits for that square:
            //- A single digit if it was given as part of the puzzle definition
            //- Or if we have figured out what it must be, and a collection of several digits if we are still uncertain.
            public static void Display(SortedList<string, string> values)
            {
                var width = 1 + (Squares.Select(s => values[s].Length)).Max();

                foreach (var row in rows)
                {
                    string gridLine = "";
                    foreach (var col in columns)
                    {
                        gridLine += (values["" + row + col]);
                    } 
                     
                    Console.WriteLine(gridLine);
                    SolveResultAI += gridLine + "\n";
                }
            }

            public static SortedList<string, string> ParseGrid(string grid)
            {
                var values = new List<string>();
                string alldigits = "123456789";

                foreach (var d in grid)
                {
                    if (d == '.')
                    {
                        values.Add(alldigits);
                    }
                    else if (alldigits.Contains(d))
                    {
                        values.Add("" + d);
                    } 
                }
                if ((grid.Length != 81))
                {
                    return null;
                }  
                var dict = new SortedList<string, string>();
                foreach (var item in Squares.Zip(values, (X, Y) => new { Box = X, Grid = Y }))
                    dict.Add(item.Box, item.Grid);

                return dict;
            }

            // Constraint Propagation
            // - If a square has only one possible value, then eliminate that value from the square's peers.
            // - If a unit has only one possible place for a value, then put the value there

            public static SortedList<string, string> RuleOut(SortedList<string, string> values)
            {
                var solvedValues = values.Keys.Where(box => values[box].Length == 1).ToList();

                foreach (var box in solvedValues)
                {
                    var digit = values[box];
                    foreach (var peer in peers[box])
                    {
                        if (digit != "")
                        {
                            values[peer] = values[peer].Replace(digit, "");
                        } 
                            
                    }
                }
                return values;
            }

            public static SortedList<string, string> SingularChoice(SortedList<string, string> values)
            {
                foreach (var u in unitList)
                {
                    foreach (var digit in "123456789")
                    {
                        var dp = u.Where(box => values[box].Contains(digit)).ToList();

                        if (dp.Count == 1)
                        {
                            values[dp[0]] = digit.ToString();
                        }                             
                    }
                }
                return values;
            }

            public static SortedList<string, string> SimplifyPuzzle(SortedList<string, string> values)
            {
                bool halted = false;

                while (!halted)
                {
                    var solvedValuesBefore = values.Keys.Count(box => values[box].Length == 1);

                    if (!values.Values.Any(x => x.Length > 1))
                    {
                        halted = true;
                        continue;
                    }
                    values = RuleOut(values);
                    if (!values.Values.Any(x => x.Length > 1))
                    {
                        halted = true;
                        continue;
                    }
                    values = SingularChoice(values);
                    var solvedValuesAfter = values.Keys.Count(box => values[box].Length == 1);
                    halted = solvedValuesBefore == solvedValuesAfter;
                    if ((values.Keys.Count(box => values[box].Length == 0) > 0))
                    {
                        return null;
                    }                         
                }
                return values;
            }


            // Choose one unfilled square and consider all its possible values.
            // One at a time, tries assigning the square each value, and searching from the resulting position.
            // Depth First Search - *Recurrsive* Search 
            // Creates a new copy of values for each recursive call to search.
            // This way each branch of the search tree is independent, and doesn't confuse another branch.
            public static SortedList<string, string> Search(SortedList<string, string> values, DateTime  dateEnd)
            {
                bool prematureEND = false;
                while (DateTime.Now < dateEnd && prematureEND == false)
                {
                    values = SimplifyPuzzle(values);

                    if (values == null)
                    {
                        return null;
                    } 
                        
                    if (Squares.Select(s => values[s].Length == 1).All(x => x))
                    {
                        UpdateValidity = (values);
                        prematureEND = true;
                        return values;                       
                    }

                    var pairs = new SortedList<string, int>();
                    foreach (var s in Squares)
                    {
                        if (values[s].Length > 1)
                            pairs.Add(s, values[s].Length);
                    }

                    string boxContLeastPos = null;
                    int minPos = 0;
                    foreach (var pair in pairs)
                    {
                        if (boxContLeastPos == null)
                        {
                            boxContLeastPos = pair.Key;
                            minPos = pair.Value;
                        }
                        else
                        {
                            if (pair.Value < minPos)
                            {
                                boxContLeastPos = pair.Key;
                                minPos = pair.Value;
                            }
                        }
                    }

                    foreach (var value in values[boxContLeastPos ?? throw new InvalidOperationException()])
                    {
                        var newSudoku = new SortedList<string, string>();

                        foreach (var x in values)
                        {
                            newSudoku.Add(x.Key, x.Value);
                        }   

                        newSudoku[boxContLeastPos] = "" + value;
                        var attempt = Search(newSudoku, dateEnd);

                        if (attempt != null)
                        {
                            return attempt;
                        }                           
                    }
                    return null;
                }
                return null;
            }              
        } 
    }
}

