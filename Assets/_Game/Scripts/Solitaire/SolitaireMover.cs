namespace Game.Solitaire
{
  public class SolitaireMover
  {
    private readonly SolitaireBoard _board;
    private readonly CommandHistory _commandHistory;

    public SolitaireMover(SolitaireBoard board)
    {
      _board = board;
      _commandHistory = new();
    }

    public bool CanUndo => _commandHistory.CanUndo;

    public bool ValidateMove(Card card, Pile targetPile)
    {
      return targetPile.PileKind switch {
        PileKind.Foundation => ValidateFoundationMove(card, targetPile),
        PileKind.Tableau => ValidateTableauMove(card, targetPile),
        PileKind.WastePile => ValidateWastePileMove(card, targetPile),
        _ => false,
      };
    }

    public bool MakeMove(Card card, Pile targetPile)
    {
      if (!ValidateMove(card, targetPile)) {
        return false;
      }

      var command = new MoveCommand(card, targetPile, this);
      _commandHistory.ExecuteCommand(command);

      return true;
    }

    public void ExecuteMove(Card card, Pile targetPile)
    {
      RemoveCardFromCurrentPile(card);
      targetPile.Append(card);
    }

    private bool ValidateFoundationMove(Card card, Pile foundationPile)
    {
      var topCard = foundationPile.LastChild;

      if (topCard == null) {
        return card.Rank == CardRank.Ace;
      }

      return card.Suit == topCard.Suit &&
             (int)card.Rank == (int)topCard.Rank + 1;
    }

    private bool ValidateTableauMove(Card card, Pile tableauPile)
    {
      var topCard = tableauPile.LastChild;

      if (topCard == null) {
        return card.Rank == CardRank.King;
      }

      return card.Color != topCard.Color &&
             (int)card.Rank == (int)topCard.Rank - 1;
    }

    private bool ValidateWastePileMove(Card card, Pile wastePile)
    {
      return card.Pile?.PileKind == PileKind.Deck;
    }

    public void ResetDeck(Pile wastePile)
    {
      if (wastePile.PileKind != PileKind.WastePile) {
        return;
      }

      if (wastePile.FirstChild == null) {
        return;
      }

      var command = new ResetDeckCommand(wastePile, _board.Deck, this);
      _commandHistory.ExecuteCommand(command);
    }

    public void ExecuteResetDeck(Pile wastePile)
    {
      var cards = wastePile.FirstChild;
      if (cards == null) {
        return;
      }

      Card? reversedHead = null;
      var current = cards;

      while (current != null) {
        var next = current.Child;
        current.SetChild(reversedHead);
        reversedHead = current;
        current = next;
      }

      wastePile.SetEmpty();
      _board.Deck.Append(reversedHead);
    }

    public void Undo()
    {
      _commandHistory.Undo();
    }

    public void RemoveCardFromPile(Card card)
    {
      RemoveCardFromCurrentPile(card);
    }

    private void RemoveCardFromCurrentPile(Card card)
    {
      var currentPile = card.Pile;
      if (currentPile == null) {
        return;
      }

      if (currentPile.FirstChild == card) {
        currentPile.SetEmpty();
        if (card.Child != null) {
          currentPile.Append(card.Child);
        }
      } else {
        var parent = card.Parent;
        parent?.SetChild(null);
      }

      card.SetParent(null);
      card.SetChild(null);
    }
  }
}
