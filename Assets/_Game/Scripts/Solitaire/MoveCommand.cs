namespace Game.Solitaire
{
  public class MoveCommand : ICommand
  {
    private readonly Card _card;
    private readonly Pile _targetPile;
    private readonly Pile _sourcePile;
    private readonly Card? _sourceParent;
    private readonly SolitaireMover _mover;

    public MoveCommand(Card card, Pile targetPile, SolitaireMover mover)
    {
      _card = card;
      _targetPile = targetPile;
      _sourcePile = card.Pile!;
      _sourceParent = card.Parent;
      _mover = mover;
    }

    public void Execute()
    {
      _mover.ExecuteMove(_card, _targetPile);
    }

    public void Undo()
    {
      _mover.RemoveCardFromPile(_card);

      if (_sourceParent != null) {
        _sourceParent.SetChild(_card);
      } else {
        _sourcePile.Append(_card);
      }
    }
  }
}
