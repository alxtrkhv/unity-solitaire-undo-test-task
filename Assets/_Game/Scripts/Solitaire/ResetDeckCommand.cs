using System.Collections.Generic;

namespace Game.Solitaire
{
  public class ResetDeckCommand : ICommand
  {
    private readonly Pile _wastePile;
    private readonly Pile _deck;
    private readonly List<Card> _wasteCards;
    private readonly SolitaireMover _mover;

    public ResetDeckCommand(Pile wastePile, Pile deck, SolitaireMover mover)
    {
      _wastePile = wastePile;
      _deck = deck;
      _mover = mover;
      _wasteCards = new();

      var current = wastePile.FirstChild;
      while (current != null) {
        _wasteCards.Add(current);
        current = current.Child;
      }
    }

    public void Execute()
    {
      _mover.ExecuteResetDeck(_wastePile);
    }

    public void Undo()
    {
      _deck.SetEmpty();

      if (_wasteCards.Count <= 0) {
        return;
      }

      Card? head = null;
      Card? current = null;

      foreach (var card in _wasteCards) {
        if (head == null) {
          head = card;
        } else {
          current!.SetChild(card);
        }

        current = card;

        card.SetChild(null);
      }

      _wastePile.Append(head);
    }
  }
}
