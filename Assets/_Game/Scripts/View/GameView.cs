using System.Collections.Generic;
using UnityEngine;
using Game.Solitaire;

namespace Game.View
{
  public class GameView : MonoBehaviour
  {
    [Header("Prefabs")]
    [SerializeField]
    private CardView _cardViewPrefab = null!;

    private readonly Dictionary<Card, CardView> _cardViews = new();

    public SolitaireBoard Board { get; private set; } = null!;
    public SolitaireBoardView BoardView { get; private set; } = null!;
    public SolitaireMover Mover { get; private set; } = null!;

    public void Initialize(SolitaireBoard board)
    {
      Board = board;
      Mover = new(Board);

      CreateBoardView();
      CreateCardViews();
      RefreshAllViews();
    }

    private void CreateBoardView()
    {
      BoardView.Initialize(Board);
    }

    private void CreateCardViews()
    {
      CreateCardViewsForPiles(Board.Tableau);
      CreateCardViewsForPiles(Board.Foundations);
      CreateCardViewsForPile(Board.Deck);
      CreateCardViewsForPile(Board.WastePile);
    }

    private void CreateCardViewsForPiles(Pile[] piles)
    {
      foreach (var pile in piles) {
        CreateCardViewsForPile(pile);
      }
    }

    private void CreateCardViewsForPile(Pile pile)
    {
      var current = pile.FirstChild;
      while (current != null) {
        CreateCardView(current);
        current = current.Child;
      }
    }

    private void CreateCardView(Card card)
    {
      if (_cardViews.ContainsKey(card)) {
        return;
      }

      var cardView = Instantiate(_cardViewPrefab);
      cardView.SetCard(card);
      _cardViews[card] = cardView;
    }

    public CardView GetCardView(Card card)
    {
      return _cardViews.TryGetValue(card, out var cardView) ? cardView : null!;
    }

    public void RefreshAllViews()
    {
      BoardView.UpdateAllViews();
      UpdateCardViewPositions();
    }

    private void UpdateCardViewPositions()
    {
      UpdateCardViewPositionsForPiles(Board.Tableau, BoardView.GetTableauView);
      UpdateCardViewPositionsForPiles(Board.Foundations, BoardView.GetFoundationView);
      UpdateCardViewPositionsForPile(Board.Deck, BoardView.GetDeckView());
      UpdateCardViewPositionsForPile(Board.WastePile, BoardView.GetWasteView());
    }

    private void UpdateCardViewPositionsForPiles(Pile[] piles, System.Func<int, PileView> getPileView)
    {
      for (int i = 0; i < piles.Length; i++) {
        UpdateCardViewPositionsForPile(piles[i], getPileView(i));
      }
    }

    private void UpdateCardViewPositionsForPile(Pile pile, PileView pileView)
    {
      var current = pile.FirstChild;
      var cardIndex = 0;

      while (current != null) {
        if (_cardViews.TryGetValue(current, out var cardView)) {
          cardView.transform.SetParent(pileView.transform);
          PositionCardInPile(cardView, cardIndex, pile.PileKind);
        }

        current = current.Child;
        cardIndex++;
      }
    }

    private void PositionCardInPile(CardView cardView, int index, PileKind pileKind)
    {
      var position = pileKind switch {
        PileKind.Tableau => new(0, -index * 0.3f, -index * 0.01f),
        PileKind.Foundation or PileKind.Deck or PileKind.WastePile => new Vector3(0, 0, -index * 0.01f),
        _ => Vector3.zero
      };

      cardView.transform.localPosition = position;
    }

    public bool TryMoveCard(Card card, Pile targetPile)
    {
      var success = Mover.MakeMove(card, targetPile);
      if (success) {
        RefreshAllViews();
      }

      return success;
    }

    public void UndoLastMove()
    {
      if (!Mover.CanUndo) {
        return;
      }

      Mover.Undo();
      RefreshAllViews();
    }

    public void ResetDeck()
    {
      Mover.ResetDeck(Board.WastePile);
      RefreshAllViews();
    }
  }
}
