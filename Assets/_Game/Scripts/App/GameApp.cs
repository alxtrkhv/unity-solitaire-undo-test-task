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

      _gameView?.Initialize();
    }
  }
}
