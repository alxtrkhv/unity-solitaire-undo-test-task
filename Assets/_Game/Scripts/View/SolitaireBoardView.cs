using UnityEngine;
using Game.Solitaire;

namespace Game.View
{
  public class SolitaireBoardView : MonoBehaviour
  {
    [Header("Pile View Prefabs")]
    [SerializeField]
    private PileView _pileViewPrefab = null!;

    [Header("Pile Containers")]
    [SerializeField]
    private Transform _tableauContainer = null!;

    [SerializeField]
    private Transform _foundationContainer = null!;

    [SerializeField]
    private Transform _deckContainer = null!;

    [SerializeField]
    private Transform _wasteContainer = null!;

    private PileView[] _tableauViews = null!;
    private PileView[] _foundationViews = null!;
    private PileView _deckView = null!;
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
      _tableauViews = new PileView[Board.Settings.TableauColumns];
      for (var i = 0; i < _tableauViews.Length; i++) {
        var pileView = Instantiate(_pileViewPrefab, _tableauContainer);
        pileView.SetPile(Board.Tableau[i]);
        _tableauViews[i] = pileView;
      }

      _foundationViews = new PileView[Board.Settings.FoundationPiles];
      for (var i = 0; i < _foundationViews.Length; i++) {
        var pileView = Instantiate(_pileViewPrefab, _foundationContainer);
        pileView.SetPile(Board.Foundations[i]);
        _foundationViews[i] = pileView;
      }

      _deckView = Instantiate(_pileViewPrefab, _deckContainer);
      _deckView.SetPile(Board.Deck);

      _wasteView = Instantiate(_pileViewPrefab, _wasteContainer);
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
