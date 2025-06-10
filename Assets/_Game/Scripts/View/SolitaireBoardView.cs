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

    public void Initialize(SolitaireBoard board)
    {
      Board = board;
      CreatePileViews();
      UpdateAllViews();
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

    public void UpdateAllViews()
    {
      foreach (var tableauView in _tableauViews) {
        tableauView.RefreshCardPositions();
      }

      foreach (var foundationView in _foundationViews) {
        foundationView.RefreshCardPositions();
      }

      _deckView.RefreshCardPositions();
      _wasteView.RefreshCardPositions();
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
