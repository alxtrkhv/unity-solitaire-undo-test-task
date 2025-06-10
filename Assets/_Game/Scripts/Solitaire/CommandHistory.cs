using System.Collections.Generic;

namespace Game.Solitaire
{
  public class CommandHistory
  {
    private readonly Stack<ICommand> _history = new();

    public bool CanUndo => _history.Count > 0;

    public void ExecuteCommand(ICommand command)
    {
      command.Execute();
      _history.Push(command);
    }

    public void Undo()
    {
      if (_history.Count <= 0) {
        return;
      }

      var command = _history.Pop();
      command.Undo();
    }

    public void Clear()
    {
      _history.Clear();
    }
  }
}
