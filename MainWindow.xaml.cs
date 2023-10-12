using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Sudoku
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		bool loadingGame;
		bool alwaysEnableSolveButtons = true;
		List<BaseGroupSolver> solvers = new List<BaseGroupSolver>();
		public static string availableChars;

		public MainWindow()
		{
			InitializeComponent();
			InitializeSquares();
			InitializeSolvers();
			HookEvents();

			SudokuBoard.SelectFirstSquare();
		}

		private void InitializeSquares()
		{
			SudokuBoard.AddSquare(0, 0, tbx0_0);
			SudokuBoard.AddSquare(0, 1, tbx0_1);
			SudokuBoard.AddSquare(0, 2, tbx0_2);
			SudokuBoard.AddSquare(0, 3, tbx0_3);
			SudokuBoard.AddSquare(0, 4, tbx0_4);
			SudokuBoard.AddSquare(0, 5, tbx0_5);
			SudokuBoard.AddSquare(0, 6, tbx0_6);
			SudokuBoard.AddSquare(0, 7, tbx0_7);
			SudokuBoard.AddSquare(0, 8, tbx0_8);
			SudokuBoard.AddSquare(1, 0, tbx1_0);
			SudokuBoard.AddSquare(1, 1, tbx1_1);
			SudokuBoard.AddSquare(1, 2, tbx1_2);
			SudokuBoard.AddSquare(1, 3, tbx1_3);
			SudokuBoard.AddSquare(1, 4, tbx1_4);
			SudokuBoard.AddSquare(1, 5, tbx1_5);
			SudokuBoard.AddSquare(1, 6, tbx1_6);
			SudokuBoard.AddSquare(1, 7, tbx1_7);
			SudokuBoard.AddSquare(1, 8, tbx1_8);
			SudokuBoard.AddSquare(2, 0, tbx2_0);
			SudokuBoard.AddSquare(2, 1, tbx2_1);
			SudokuBoard.AddSquare(2, 2, tbx2_2);
			SudokuBoard.AddSquare(2, 3, tbx2_3);
			SudokuBoard.AddSquare(2, 4, tbx2_4);
			SudokuBoard.AddSquare(2, 5, tbx2_5);
			SudokuBoard.AddSquare(2, 6, tbx2_6);
			SudokuBoard.AddSquare(2, 7, tbx2_7);
			SudokuBoard.AddSquare(2, 8, tbx2_8);
			SudokuBoard.AddSquare(3, 0, tbx3_0);
			SudokuBoard.AddSquare(3, 1, tbx3_1);
			SudokuBoard.AddSquare(3, 2, tbx3_2);
			SudokuBoard.AddSquare(3, 3, tbx3_3);
			SudokuBoard.AddSquare(3, 4, tbx3_4);
			SudokuBoard.AddSquare(3, 5, tbx3_5);
			SudokuBoard.AddSquare(3, 6, tbx3_6);
			SudokuBoard.AddSquare(3, 7, tbx3_7);
			SudokuBoard.AddSquare(3, 8, tbx3_8);
			SudokuBoard.AddSquare(4, 0, tbx4_0);
			SudokuBoard.AddSquare(4, 1, tbx4_1);
			SudokuBoard.AddSquare(4, 2, tbx4_2);
			SudokuBoard.AddSquare(4, 3, tbx4_3);
			SudokuBoard.AddSquare(4, 4, tbx4_4);
			SudokuBoard.AddSquare(4, 5, tbx4_5);
			SudokuBoard.AddSquare(4, 6, tbx4_6);
			SudokuBoard.AddSquare(4, 7, tbx4_7);
			SudokuBoard.AddSquare(4, 8, tbx4_8);
			SudokuBoard.AddSquare(5, 0, tbx5_0);
			SudokuBoard.AddSquare(5, 1, tbx5_1);
			SudokuBoard.AddSquare(5, 2, tbx5_2);
			SudokuBoard.AddSquare(5, 3, tbx5_3);
			SudokuBoard.AddSquare(5, 4, tbx5_4);
			SudokuBoard.AddSquare(5, 5, tbx5_5);
			SudokuBoard.AddSquare(5, 6, tbx5_6);
			SudokuBoard.AddSquare(5, 7, tbx5_7);
			SudokuBoard.AddSquare(5, 8, tbx5_8);
			SudokuBoard.AddSquare(6, 0, tbx6_0);
			SudokuBoard.AddSquare(6, 1, tbx6_1);
			SudokuBoard.AddSquare(6, 2, tbx6_2);
			SudokuBoard.AddSquare(6, 3, tbx6_3);
			SudokuBoard.AddSquare(6, 4, tbx6_4);
			SudokuBoard.AddSquare(6, 5, tbx6_5);
			SudokuBoard.AddSquare(6, 6, tbx6_6);
			SudokuBoard.AddSquare(6, 7, tbx6_7);
			SudokuBoard.AddSquare(6, 8, tbx6_8);
			SudokuBoard.AddSquare(7, 0, tbx7_0);
			SudokuBoard.AddSquare(7, 1, tbx7_1);
			SudokuBoard.AddSquare(7, 2, tbx7_2);
			SudokuBoard.AddSquare(7, 3, tbx7_3);
			SudokuBoard.AddSquare(7, 4, tbx7_4);
			SudokuBoard.AddSquare(7, 5, tbx7_5);
			SudokuBoard.AddSquare(7, 6, tbx7_6);
			SudokuBoard.AddSquare(7, 7, tbx7_7);
			SudokuBoard.AddSquare(7, 8, tbx7_8);
			SudokuBoard.AddSquare(8, 0, tbx8_0);
			SudokuBoard.AddSquare(8, 1, tbx8_1);
			SudokuBoard.AddSquare(8, 2, tbx8_2);
			SudokuBoard.AddSquare(8, 3, tbx8_3);
			SudokuBoard.AddSquare(8, 4, tbx8_4);
			SudokuBoard.AddSquare(8, 5, tbx8_5);
			SudokuBoard.AddSquare(8, 6, tbx8_6);
			SudokuBoard.AddSquare(8, 7, tbx8_7);
			SudokuBoard.AddSquare(8, 8, tbx8_8);

			SudokuBoard.Initialize();
		}

		private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Right)
			{
				GetSquarePosition(SudokuBoard.SelectedSquare, out int row, out int column);
				SelectSquare(row, column + 1);
			}

			if (e.Key == Key.Left)
			{
				GetSquarePosition(SudokuBoard.SelectedSquare, out int row, out int column);
				SelectSquare(row, column - 1);
			}

			if (e.Key == Key.Down)
			{
				GetSquarePosition(SudokuBoard.SelectedSquare, out int row, out int column);
				SelectSquare(row + 1, column);
			}

			if (e.Key == Key.Up)
			{
				GetSquarePosition(SudokuBoard.SelectedSquare, out int row, out int column);
				SelectSquare(row - 1, column);
			}

			if (e.Key == Key.Z && Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && !Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
			{
				CommandInvoker.Undo();
				e.Handled = true;
			}

			bool ctrlShiftZPressed = e.Key == Key.Z && Keyboard.Modifiers.HasFlag(ModifierKeys.Control | ModifierKeys.Shift);
			bool ctrlYPressed = e.Key == Key.Y && Keyboard.Modifiers.HasFlag(ModifierKeys.Control);

			if (ctrlShiftZPressed || ctrlYPressed)
			{
				CommandInvoker.Redo();
				e.Handled = true;
			}
		}

		private void SelectSquare(int row, int column)
		{
			SudokuBoard.SelectSquare(row, column);
		}

		private void GetSquarePosition(ISudokuSquare square, out int row, out int column)
		{
			row = square.Row;
			column = square.Column;
		}

		void SetAvailableCharacters()
		{
			availableChars = tbxAvailableCharacter.Text.Trim();
            if (availableChars.Length != 9)
			{
				Background = new SolidColorBrush(Colors.Red);
			}
            else
            {
				Background = new SolidColorBrush(Colors.White);
			}			
		}

		private void TbxAvailableCharacter_TextChanged(object sender, TextChangedEventArgs e)
		{
			SetAvailableCharacters();
		}

		private string easyGame =
			@"53  7
6  195
 98    6
8   6   3
4  8 3  1
7   2   6
 6    28 
   419  5
    8  79
";



		private string hardGame = @"629   53
     27
5 7 96 18
  6  1 8
 9867
    2
      9
   2   43
31   9 62";

		private	string expertGame = @"         
     3 85
  1 2    
   5 7   
  4   1  
 9       
5      73
  2 1    
    4   9";


		private	string mediumGame = @" 4 8    6
  1  6  3
  63 98
25 6 3

 87    4
    9 7
     4 1
     2  5";

		void ClearGame()
		{
            for (int c = 0; c < 9; c++)
			{
                for (int r = 0; r < 9; r++)
				{
					SudokuBoard.squares[r, c].Clear();
				} 		
			} 	
		}


		void LoadValuesForLine(int row, string line, bool UserSave)
		{
			bool userSave = UserSave;
			for (int column = 0; column < line.Length; column++)
			{
				char chr = line[column];
				if (chr != ' ' && userSave == false)
				{
					SudokuBoard.squares[row, column].SetText(chr.ToString());
					SudokuBoard.squares[row, column].Locked = true;
				}
				else
                {
					SudokuBoard.squares[row, column].SetText(chr.ToString());
					SudokuBoard.squares[row, column].Locked = false;
				}
			}
		}	

		public void LoadGame(string gameStr, string message, bool UserSave)
		{
			Title = message;
			loadingGame = true;
			XamlSudokuSquare.Updating = true;
			btnBruteForce.IsEnabled = false;
			btnHint.IsEnabled = false;
			try
			{
				ClearGame();
				string[] lines = gameStr.Split('\n');
				int row = 0;
				foreach (string line in lines)
				{
					LoadValuesForLine(row, line.TrimEnd(), UserSave);
					row++;
				}
			}
			finally
			{
				XamlSudokuSquare.Updating = false;
				loadingGame = false;
				ShowConflicts();
			}
		}

		private void BtnTest_Click(object sender, RoutedEventArgs e)
		{
			if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
				if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
					if (Keyboard.Modifiers.HasFlag(ModifierKeys.Alt))
						LoadGame(expertGame, "Expert Game Loaded", false);
					else
						LoadGame(hardGame, "Hard Game Loaded", false);
				else
					LoadGame(mediumGame, "Medium Game Loaded", false);
			else
				LoadGame(easyGame, "Easy Game Loaded", false);
		}

		private void btnRandomPuzzle_Click(object sender, RoutedEventArgs e)
		{
			Generator.RandomSudokuString = null;
			if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
				if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
					if (Keyboard.Modifiers.HasFlag(ModifierKeys.Alt))
					{ PuzzleCreation(50); LoadGame(Generator.RandomSudokuString, "Random Expert Game Loaded", false); }
					else
					{ PuzzleCreation(40); LoadGame(Generator.RandomSudokuString, "Random Hard Game Loaded", false); }
				else
				{ PuzzleCreation(30); LoadGame(Generator.RandomSudokuString, "Random Medium Game Loaded", false); }
			else
			{ PuzzleCreation(20); LoadGame(Generator.RandomSudokuString, "Random Easy Game Loaded", false); }


		}

		// Starts Random Puzzle generation
		public static void PuzzleCreation(int NumberOfEmptySquares)
		{
			int N = 9, K = NumberOfEmptySquares/*K = 60*/;
			Generator generator = new Generator(N, K);
			generator.FillValuesForBoard();
			generator.ConvertSudoku();

		}

		void RemoveCharactersFromGroup(List<char> availableChars, ISudokuSquare[] group)
		{
			for (int i = 0; i < 9; i++)
			{
				char thisChar = group[i].Value;
                if (availableChars.Contains(thisChar))
				{
					availableChars.Remove(thisChar);
				} 			
			}
		}

		private void btnFill_Click(object sender, RoutedEventArgs e)
		{
			FillFromAllNotes();
		}

		private void FillFromAllNotes()
		{
            for (int c = 0; c < 9; c++)
			{
                for (int r = 0; r < 9; r++)
				{
					SudokuBoard.squares[r, c].FillFromNotesIfPossible();
				} 			
			} 			
		}

		private void ShowNotesForSquare(ISudokuSquare square)
		{
            if (square.GetText().Trim().Length > 0)
			{
				return;
			} 
			ShowNotesForSquareAt(square);
		}

		private void ShowNotesForSquareAt(ISudokuSquare square)
		{
			ISudokuSquare[] column = SudokuBoard.GetColumn(square.Column);
			ISudokuSquare[] row = SudokuBoard.GetRow(square.Row);
			ISudokuSquare[] block = SudokuBoard.GetBlock(square.Row, square.Column);

			List<char> availableChars = new List<char>();
            foreach (char item in tbxAvailableCharacter.Text)
			{
				availableChars.Add(item);
			} 
	
			RemoveCharactersFromGroup(availableChars, row);
			RemoveCharactersFromGroup(availableChars, column);
			RemoveCharactersFromGroup(availableChars, block);

			square.SetNotes(string.Join(", ", availableChars));
		}

		private void btnSolve_Click(object sender, RoutedEventArgs e)
		{
			FillFromAllNotes();
			RefreshAllNotes();
			
			ApplySolvers();
			UpdateCombinationsRemaining();
		}

		private bool ApplySolvers()
		{
			bool squaresSolved = false;
			foreach (BaseGroupSolver sudokuSolver in solvers)
			{
                // Calling solve 27 times for all the rows, columns, and blocks.
                for (int c = 0; c < 9; c++)
				{
                    if (sudokuSolver.Solve(SudokuBoard.GetColumn(c), TypeOfGroup.Column) == SolveResult.SquaresSolved)
					{
						squaresSolved = true;
					} 
						
				}

                for (int r = 0; r < 9; r++)
				{
                    if (sudokuSolver.Solve(SudokuBoard.GetRow(r), TypeOfGroup.Row) == SolveResult.SquaresSolved)
					{
						squaresSolved = true;
					} 
						
				}

                for (int r = 0; r < 3; r++)
				{
                    for (int c = 0; c < 3; c++)
					{
                        if (sudokuSolver.Solve(SudokuBoard.GetBlock(r * 3, c * 3), TypeOfGroup.Block) == SolveResult.SquaresSolved)
						{
							squaresSolved = true;
						} 					
					} 					
				} 			
			}
			return squaresSolved;
		}

		private void RefreshAllNotes()
		{
            for (int c = 0; c < 9; c++)
			{
                for (int r = 0; r < 9; r++)
				{
					ShowNotesForSquare(SudokuBoard.squares[r, c]);
				} 			
			} 			
		}

		private void btnClearNotes_Click(object sender, RoutedEventArgs e)
		{
            for (int c = 0; c < 9; c++)
			{
                for (int r = 0; r < 9; r++)
				{
					SudokuBoard.squares[r, c].ClearNotes();
				} 			
			} 		
		}

		private void btnConflictToggle_Click(object sender, RoutedEventArgs e)
		{
			SudokuBoard.SelectedSquare.ToggleConflicts();
		}

		void HookEvents()
		{
            for (int row = 0; row < 9; row++)
			{
				for (int column = 0; column < 9; column++)
				{
					if (SudokuBoard.squares[row, column] is XamlSudokuSquare sudokuSquare)
					{
						sudokuSquare.ValueChanged += SudokuSquare_ValueChanged;
						sudokuSquare.SquareReceivedFocus += MainWindow_SquareReceivedFocus;
					}
				}
			} 		
		}

		private void MainWindow_SquareReceivedFocus(object sender, EventArgs e)
		{
            if (sender is XamlSudokuSquare sudokuSquare)
			{
				SudokuBoard.SelectedSquare = sudokuSquare;
			} 		
		}

		bool HasConflicts(int r, int c)
		{
			return CheckForConflicts(r, c, false);
		}

		bool CheckForConflicts(int r, int c, bool setHasConflictedProperty = true)
		{
			ISudokuSquare thisSquare = SudokuBoard.squares[r, c];
			string text = thisSquare.GetText();
            if (string.IsNullOrWhiteSpace(text))
			{
				return false;
			} 
				
			ISudokuSquare[] column = SudokuBoard.GetColumn(c);
			ISudokuSquare[] row = SudokuBoard.GetRow(r);
			ISudokuSquare[] block = SudokuBoard.GetBlock(r, c);

			bool isConflicted = false;
            for (int rowIndex = 0; rowIndex < 9; rowIndex++)
			{
				if (rowIndex != r && column[rowIndex].GetText() == text)
				{
					if (setHasConflictedProperty)
					{
						thisSquare.HasConflict = true;
						column[rowIndex].HasConflict = true;
					}
					isConflicted = true;
				}
			}

            for (int colIndex = 0; colIndex < 9; colIndex++)
			{
				if (colIndex != c && row[colIndex].GetText() == text)
				{
					if (setHasConflictedProperty)
					{
						thisSquare.HasConflict = true;
						row[colIndex].HasConflict = true;
					}
					isConflicted = true;
				}
			} 
				
			for (int squareIndex = 0; squareIndex < 9; squareIndex++)
			{
				GetSquarePosition(block[squareIndex], out int blockRow, out int blockColumn);
                if (blockRow == r && blockColumn == c)
				{
					continue;
				} 
					
				if (block[squareIndex].GetText() == text)
				{
					if (setHasConflictedProperty)
					{
						thisSquare.HasConflict = true;
						block[squareIndex].HasConflict = true;
					}
					isConflicted = true;
				}
			}
			return isConflicted;
		}

		void ShowConflicts()
		{
			ClearAllConflicts();

            for (int r = 0; r < 9; r++)
			{
				for (int c = 0; c < 9; c++)
				{
					CheckForConflicts(r, c);
				}
			} 		
		}

		private void ClearAllConflicts()
		{
            for (int r = 0; r < 9; r++)
			{
                for (int c = 0; c < 9; c++)
				{
					SudokuBoard.squares[r, c].HasConflict = false;
				} 		
			} 			
		}

		private void SudokuSquare_ValueChanged(object sender, EventArgs e)
		{
			if (loadingGame)
			{
				return;
			}
			else
			{
				ShowConflicts();
			}
		}

		void InitializeSolvers()
		{
			solvers.Add(new Logical2x2Solver());
			solvers.Add(new Logical3x3Solver());
			solvers.Add(new Logical4x4Solver());
			solvers.Add(new Logical5x5Solver());
			solvers.Add(new OnlyOneSolver());
		}

		bool AllSquaresAreFilled()
		{
            for (int row = 0; row < 9; row++)
			{
				for (int column = 0; column < 9; column++)
				{
					ISudokuSquare sudokuSquare = SudokuBoard.squares[row, column];
                    if (sudokuSquare.IsEmpty)
					{
						return false;
					} 				
				}
			} 
			return true;
		}

		int numCombinationsTried;
		bool BruteForceAttack(DateTime dateEnd, int startingRow = 0, int startingColumn = 0)
		{
			// *Recurssive Algo*
			bool prematureEND = false;
            while (DateTime.Now < dateEnd && prematureEND == false)
			{
				if (startingColumn >= 9)
				{
					startingColumn = 0;
					startingRow++;
				}
				for (int row = startingRow; row < 9; row++)
				{
					for (int column = startingColumn; column < 9; column++)
					{
						startingColumn = 0;
						ISudokuSquare sudokuSquare = SudokuBoard.squares[row, column];
						if (!sudokuSquare.IsEmpty)
						{
							continue;
						}
						//Square with no notes
						if (sudokuSquare.Notes == "")
						{
							ShowNotesForSquareAt(sudokuSquare);
							if (sudokuSquare.Notes == "")
							{
								return BruteForceAttack(dateEnd ,row, column + 1);
							}
						}

						//Gets candidates for square
						List<int> notes = BaseGroupSolver.GetNumbers(sudokuSquare.Notes);

						foreach (int number in notes)
						{
							
							sudokuSquare.Value = number.ToString()[0];
							sudokuSquare.HasTestValue = true;
							numCombinationsTried++;
							if (HasConflicts(row, column))
							{

								sudokuSquare.Value = Char.MinValue;
								sudokuSquare.HasTestValue = false;
							}
							else if (BruteForceAttack(dateEnd, row, column + 1))
							{
								// increment to the next square							
								sudokuSquare.Notes = "";
								return true;
							}
							else
							{
								sudokuSquare.Value = Char.MinValue;
								sudokuSquare.HasTestValue = false;
							}
						}					
						return false;
					}
				}
				prematureEND = true;
			}
			if (DateTime.Now > dateEnd)
			{
				Title = "15s Have Passed - No Solution Was Found - Please Double Check The Puzzle OR Try the Super Solver";
			}
			return AllSquaresAreFilled();
		}

		private void btnBruteForce_Click(object sender, RoutedEventArgs e)
		{
			RefreshAllNotes();
			StartBruteForceAttack();
		}

		private void StartBruteForceAttack()
		{
			XamlSudokuSquare.Updating = true;
			try
			{
				numCombinationsTried = 0;
				BruteForceAttack(DateTime.Now.AddSeconds(15));
			}
			finally
			{
				XamlSudokuSquare.Updating = false;
			}
		}

		private void btnHint_Click(object sender, RoutedEventArgs e)
		{
			RefreshAllNotes();
			StartBruteForceAttack();
			bool foundOneYet = false;
			for (int row = 0; row < 9; row++)
				for (int column = 0; column < 9; column++)
				{
					if (SudokuBoard.squares[row, column].HasTestValue)
					{
						if (!foundOneYet)
						{
							foundOneYet = true;
                            if (SudokuBoard.squares[row, column] is XamlSudokuSquare sudokuSquare)
							{
								sudokuSquare.Background = new SolidColorBrush(Color.FromRgb(163, 247, 176)); // UI change here.
								Title = "Hint Given!";
							} 				
						}
						else
							SudokuBoard.squares[row, column].Value = char.MinValue;

						SudokuBoard.squares[row, column].HasTestValue = false;
					}
				}	
		}

		void UpdateCombinationsRemaining()
		{
			BigInteger numCombos = 1;
            for (int row = 0; row < 9; row++)
			{
				for (int column = 0; column < 9; column++)
				{
					List<int> numbers = BaseGroupSolver.GetNumbers(SudokuBoard.squares[row, column].Notes);
					if (numbers.Count > 0)
					{
						numCombos *= numbers.Count;
					}
				}
			} 
				
            if (numCombos == 1)
			{
				numCombos = 0;
			} 		
			bool weHaveAReasonableNumberOfCombosWeCanSolve = numCombos < 200_000_000_000_000;
			bool shouldEnableSolveButtons = alwaysEnableSolveButtons || weHaveAReasonableNumberOfCombosWeCanSolve;
			btnBruteForce.IsEnabled = shouldEnableSolveButtons;
			btnHint.IsEnabled = shouldEnableSolveButtons;
			Title = $"The number of combinations is {numCombos}!";
		}

		void GenerateGameSave()
		{
			string gameSaveString = "";
			int startingRow = 0;
			int startingColumn = 0;		

			for (int Row = startingRow; Row < 9; Row++)
			{
				if (Row != 0)
				{
					gameSaveString += '\n';
				}
				for (int Column = startingColumn; Column < 9; Column++)
				{
					startingColumn = 0;
					ISudokuSquare sudokuSquare = SudokuBoard.squares[Row, Column];
					if (sudokuSquare.IsEmpty)
					{
						gameSaveString += " ";
					}
					else
					{
						gameSaveString += sudokuSquare.Value;
					}
				}

			}
			Console.WriteLine(gameSaveString);
			StoreGameSave(gameSaveString);
		}

		void StoreGameSave(string newGameSaveString)
		{
			// *Writing File*
			string UserGameSave = "UserGameSave.txt";

				if (File.Exists(UserGameSave))
                {
					File.Delete(UserGameSave);
                }
			File.WriteAllText(UserGameSave, newGameSaveString);
		}

		private void btnClearAll_Click(object sender, RoutedEventArgs e)
		{
			ClearGame();
		}

        private void btnSaveGame_Click(object sender, RoutedEventArgs e)
        {
			GenerateGameSave();
		}

        private void btnLoadSavedGame_Click(object sender, RoutedEventArgs e)
        {
			// *Reading File*
			string savedGameLoadedMsg = "User Save Loaded";
            try
            {
				string savedGame = System.IO.File.ReadAllText("UserGameSave.txt");
				LoadGame(savedGame, savedGameLoadedMsg, true);
			}
            catch (Exception)
            {

				return;
            }			
        }

        private void btnShowNotes_Click(object sender, RoutedEventArgs e)
        {
			RefreshAllNotes();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

		private void Ai_Click(object sender, RoutedEventArgs e)
        {
            GenerateAIstring();
        }
        void GenerateAIstring()
        {
			// Converts current board for Super Solver
            string AIstring = "";
            int startingRow = 0;
            int startingColumn = 0;           

            for (int Row = startingRow; Row < 9; Row++)
            {
                for (int Column = startingColumn; Column < 9; Column++)
                {
                    startingColumn = 0;
                    ISudokuSquare sudokuSquare = SudokuBoard.squares[Row, Column];
                    if (sudokuSquare.IsEmpty)
                    {
                        AIstring += ".";
                    }
                    else
                    {
                        AIstring += sudokuSquare.Value;
                    }
                }

            }
            Console.WriteLine(AIstring);
            SendAIString(AIstring);
        }
        void SendAIString(string AIstring)
        {
			// Sends string to Super Solver
			Solving.SolverV2.SolveResultAI = "";
			Solving.SolverV2.UserString = AIstring;
            Solving.SolverV2.InitiateSolverV2();
            if (Solving.SolverV2.SolveResultAI == "")
            {
				Title = "10s Have Passed - No Solution Was Found - Please Double Check The Puzzle";
				return;
            }
            else
            {
				LoadGame(Solving.SolverV2.SolveResultAI, "AI solution", false);
			}
		}
    }
}
