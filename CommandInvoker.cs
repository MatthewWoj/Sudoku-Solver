using System;
using System.Linq;
using System.Collections.Generic;

namespace Sudoku
{
	
	public static class CommandInvoker
	{
		// Undo/Redo 
		// *Stacks* x2
		static Stack<Command> undoStack = new Stack<Command>();
		static Stack<Command> redoStack = new Stack<Command>();

		public static void DoCommand(Command command)
		{
			command.Execute();
			undoStack.Push(command);
            redoStack.Clear();
        }

		//Undo Stack
		public static void Undo()
		{
            if (undoStack.Count == 0)
			{
				return;
			} 			
			Command command = undoStack.Pop();
			command.Undo();
			redoStack.Push(command);
		}

		//Redo Stack
		public static void Redo()
		{
            if (redoStack.Count == 0)
			{
				return;
			} 			
			Command command = redoStack.Pop();
			command.Execute();
			undoStack.Push(command);
		}
	}
}
