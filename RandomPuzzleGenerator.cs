using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Generator
    {
        // *Advanced matrix operations*

        public static string RandomSudokuString;
        private readonly int[,] matrix;
        private readonly int Nmb_XY; // number of columns/rows
        private readonly int SQRT_Nmb_XY; // SQRT of number of columns/rows
        readonly int NumberOfMissingDigits;

        public Generator(int N, int K)
        {
            Nmb_XY = N;
            NumberOfMissingDigits = K;

            double SRNd = Math.Sqrt(N);
            SQRT_Nmb_XY = (int)SRNd;

            matrix = new int[N, N];
        }

        public void FillValuesForBoard()
        {
            FillDiagonal();
            FillRemaining(0, SQRT_Nmb_XY);
            RemoveXDigits();
        }

        void FillDiagonal()
        {
            for (int i = 0; i < Nmb_XY; i += SQRT_Nmb_XY)
            {
                Fill3x3Box(i, i);
            }                
        }

        bool NotUsedInBox(int rowStart, int colStart, int num)
        {
            for (int i = 0; i < SQRT_Nmb_XY; i++)
            {
                for (int j = 0; j < SQRT_Nmb_XY; j++)
                {
                    if (matrix[rowStart + i, colStart + j] == num)
                    {
                        return false;
                    }                         
                }     
            } 
            return true;
        }

        void Fill3x3Box(int row, int col)
        {
            int num;
            for (int i = 0; i < SQRT_Nmb_XY; i++)
            {
                for (int j = 0; j < SQRT_Nmb_XY; j++)
                {
                    do
                    {
                        num = RandomGenerator(Nmb_XY);
                    }
                    while (!NotUsedInBox(row, col, num));

                    matrix[row + i, col + j] = num;
                }
            }
        }

        int RandomGenerator(int num)
        {
            Random rand = new Random();
            return (int)Math.Floor((double)(rand.NextDouble() * num + 1));
        }

        bool CheckForSafety(int i, int j, int num)
        {
            return (NotUsedInRow(i, num) &&
                    NotUsedInCol(j, num) &&
                    NotUsedInBox(i - i % SQRT_Nmb_XY, j - j % SQRT_Nmb_XY, num));
        }

        bool NotUsedInRow(int i, int num)
        {
            for (int j = 0; j < Nmb_XY; j++)
            {
                if (matrix[i, j] == num)
                {
                    return false;
                }                  
            } 
            return true;
        }

        bool NotUsedInCol(int j, int num)
        {
            for (int i = 0; i < Nmb_XY; i++)
            {
                if (matrix[i, j] == num)
                {
                    return false;
                }         
            }           
            return true;
        }

        // *Recursive algorithim* to fill the rest of the matrix
        bool FillRemaining(int i, int j)
        {

            if (j >= Nmb_XY && i < Nmb_XY - 1)
            {
                i += 1;
                j = 0;
            }
            if (i >= Nmb_XY && j >= Nmb_XY)
            {
                return true;
            } 

            if (i < SQRT_Nmb_XY)
            {
                if (j < SQRT_Nmb_XY)
                {
                    j = SQRT_Nmb_XY;
                }            
            }
            else if (i < Nmb_XY - SQRT_Nmb_XY)
            {
                if (j == (int)(i / SQRT_Nmb_XY) * SQRT_Nmb_XY)
                {
                    j += SQRT_Nmb_XY;
                }          
            }
            else
            {
                if (j == Nmb_XY - SQRT_Nmb_XY)
                {
                    i += 1;
                    j = 0;
                    if (i >= Nmb_XY)
                    {
                        return true;
                    }                     
                }
            }

            for (int num = 1; num <= Nmb_XY; num++)
            {
                if (CheckForSafety(i, j, num))
                {
                    matrix[i, j] = num;
                    if (FillRemaining(i, j + 1))
                    {
                        return true;
                    } 
                       
                    matrix[i, j] = 0;
                }
            }
            return false;
        }

        // Depends on difficulty chosen - user defined
        public void RemoveXDigits()
        {
            int count = NumberOfMissingDigits;
            while (count != 0)
            {
                int cellId = RandomGenerator(Nmb_XY * Nmb_XY) - 1;

                int i = (cellId / Nmb_XY);
                int j = cellId % 9;
                if (j != 0)
                {
                    j -= 1;
                }                   
             
                if (matrix[i, j] != 0)
                {
                    count--;
                    matrix[i, j] = 0;
                }
            }
        }
        public void ConvertSudoku()
        {
            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                string str = string.Empty;
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    str = String.Concat(str, matrix[j, i].ToString());
                }                   
                if (j == 0)
                {
                    RandomSudokuString += str;
                }
                else
                {
                    RandomSudokuString = RandomSudokuString + '\n' + str;
                    RandomSudokuString = RandomSudokuString.Replace("0", " ");
                }
            } 
        }        
    }
}

