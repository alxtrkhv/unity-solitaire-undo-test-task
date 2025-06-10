using System.Collections.Generic;
using UnityEngine;
using Game.Solitaire;

namespace Game.View
{
  public class SolitaireBoardView : MonoBehaviour
  {
    [Header("Settings")]
    [SerializeField]
    private float _cardInStackOffset;

    [Header("Prefabs")]
    [SerializeField]
    private CardView _cardViewPrefab = null!;

    [Header("Pile Containers")]
    [SerializeField]
    private List<PileView> _tableauViews = null!;

    [SerializeField]
    private List<PileView> _foundationViews = null!;

    [SerializeField]
    private PileView _deckView = null!;

    [SerializeField]
    private PileView _wasteView = null!;

    public SolitaireBoard Board { get; private set; } = null!;

    private readonly Dictionary<Card, CardView> _cardViews = new();

    public void Initialize(SolitaireBoard board)
    {
      Board = board;
      CreatePileViews();
      CreateCardViews();
      RefreshAllViews();
    }

    private void CreatePileViews()
    {
      for (var i = 0; i < _tableauViews.Count; i++) {
        _tableauViews[i].SetPile(Board.Tableau[i]);
      }

      for (var i = 0; i < _foundationViews.Count; i++) {
        _foundationViews[i].SetPile(Board.Foundations[i]);
      }

      _deckView.SetPile(Board.Deck);
      _wasteView.SetPile(Board.WastePile);
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

    public void RefreshAllViews()
    {
      UpdateCardViewParents();
      UpdateCardViewPositions();
    }

    private void UpdateCardViewParents()
    {
      UpdateCardViewParentsForPiles(Board.Tableau, GetTableauView);
      UpdateCardViewParentsForPiles(Board.Foundations, GetFoundationView);
      UpdateCardViewParentsForPile(Board.Deck, GetDeckView());
      UpdateCardViewParentsForPile(Board.WastePile, GetWasteView());
    }

    private void UpdateCardViewPositions()
    {
      UpdateCardViewPositionsForPiles(Board.Tableau, GetTableauView);
      UpdateCardViewPositionsForPiles(Board.Foundations, GetFoundationView);
      UpdateCardViewPositionsForPile(Board.Deck, GetDeckView());
      UpdateCardViewPositionsForPile(Board.WastePile, GetWasteView());
    }

    private void UpdateCardViewParentsForPiles(Pile[] piles, System.Func<int, PileView> getPileView)
    {
      for (var i = 0; i < piles.Length; i++) {
        UpdateCardViewParentsForPile(piles[i], getPileView(i));
      }
    }

    private void UpdateCardViewPositionsForPiles(Pile[] piles, System.Func<int, PileView> getPileView)
    {
      for (var i = 0; i < piles.Length; i++) {
        UpdateCardViewPositionsForPile(piles[i], getPileView(i));
      }
    }

    private void UpdateCardViewParentsForPile(Pile pile, PileView pileView)
    {
      var current = pile.FirstChild;
      CardView? previousCardView = null;

      while (current != null) {
        if (_cardViews.TryGetValue(current, out var cardView)) {
          if (previousCardView == null) {
            cardView.transform.SetParent(pileView.transform);
          } else {
            cardView.transform.SetParent(previousCardView.transform);
          }
          previousCardView = cardView;
        }

        current = current.Child;
      }
    }

    private void UpdateCardViewPositionsForPile(Pile pile, PileView pileView)
    {
      var current = pile.FirstChild;
      var cardIndex = 0;

      while (current != null) {
        if (_cardViews.TryGetValue(current, out var cardView)) {
          PositionCardInPile(cardView, cardIndex, pile.PileKind, cardIndex > 0);
        }

        current = current.Child;
        cardIndex++;
      }
    }

    private void PositionCardInPile(CardView cardView, int index, PileKind pileKind, bool isChildCard)
    {
      var position = pileKind switch {
        PileKind.Tableau when isChildCard => new(0, -_cardInStackOffset, -0.01f),
        PileKind.Tableau => new(0, -index * _cardInStackOffset, -index * 0.01f),
        PileKind.Foundation or PileKind.Deck or PileKind.WastePile when isChildCard => new(0, 0, -0.01f),
        PileKind.Foundation or PileKind.Deck or PileKind.WastePile => new(0, 0, -index * 0.01f),
        _ => Vector3.zero,
      };

      cardView.transform.localPosition = position;
    }

    public PileView GetTableauView(int index)
    {
      return _tableauViews[index];
    }

    public PileView GetFoundationView(int index)
    {
      return _foundationViews[index];
    }

    public PileView GetDeckView()
    {
      return _deckView;
    }

    public PileView GetWasteView()
    {
      return _wasteView;
    }
  }
}
