using System;
using System.Linq;

namespace Sudoku
{
	public interface ISudokuSquare
	{

		//Implementation of Interfaces == Light weight
		//Reduced dependancies that would be needed if working with a heavier class

		char Value { get; set; }
		string Notes { get; set; }
		bool IsEmpty { get; }

		int Row { get; set; }
		int Column { get; set; }
		int Block { get; set; }
		bool HasTestValue { get; set; }
		bool HasConflict { get; set; }
		bool Locked { get; set; }
		void SetNotes(string notes);

		void FillFromNotesIfPossible();

		
		void SetTextNoInternalEvents(string whatChanged);
		void Clear();
		void ClearNotes();

		
		string GetText();
		void SetText(string str);
	}
}
