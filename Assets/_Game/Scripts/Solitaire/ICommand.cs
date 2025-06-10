namespace Game.Solitaire
{
  public interface ICommand
  {
    void Execute();
    void Undo();
  }
}
