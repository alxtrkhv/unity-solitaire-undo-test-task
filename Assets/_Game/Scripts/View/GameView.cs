using System.Collections.Generic;
using UnityEngine;
using Game.Solitaire;

namespace Game.View
{
  public class GameView : MonoBehaviour
  {
    [Header("References")]
    [field: SerializeField]
    public SolitaireBoardView BoardView { get; private set; } = null!;

    public SolitaireBoard Board { get; private set; } = null!;

    public SolitaireMover Mover { get; private set; } = null!;

    public void Initialize()
    {
      Board = CreateBoard();
      Mover = new(Board);

      CreateBoardView();
    }

    private SolitaireBoard CreateBoard()
    {
      var board = new SolitaireBoard();
      var cards = CreateDeck();
      board.Populate(cards);
      return board;
    }

    private List<Card> CreateDeck()
    {
      var cards = new List<Card>();
      var cardId = 0;

      foreach (var suit in System.Enum.GetValues(typeof(CardSuit))) {
        foreach (var rank in System.Enum.GetValues(typeof(CardRank))) {
          cards.Add(new((CardRank)rank, (CardSuit)suit, cardId++));
        }
      }

      return cards;
    }

    private void CreateBoardView()
    {
      BoardView.Initialize(Board);
    }

    public bool TryMoveCard(Card card, Pile targetPile)
    {
      var success = Mover.MakeMove(card, targetPile);
      if (success) {
        BoardView.RefreshAllViews();
      }

      return success;
    }

    public void UndoLastMove()
    {
      if (!Mover.CanUndo) {
        return;
      }

      Mover.Undo();
      BoardView.RefreshAllViews();
    }

    public void Refresh()
    {
      BoardView.RefreshAllViews();
    }
  }
}
