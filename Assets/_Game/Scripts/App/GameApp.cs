using System.Collections.Generic;
using Game.Solitaire;
using Game.View;
using UnityEngine;

namespace Game.App
{
  public class GameApp : MonoBehaviour
  {
    private GameView? _gameView;

    private void Start()
    {
      InitializeGame();
    }

    private void InitializeGame()
    {
      if (_gameView == null) {
        _gameView = FindAnyObjectByType<GameView>(FindObjectsInactive.Include);
      }

      var board = CreateBoard();
      _gameView?.Initialize(board);
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
  }
}
