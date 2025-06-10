using UnityEngine;

namespace Game.App
{
  public class GameApp : MonoBehaviour
  {
    private void Start()
    {
      InitializeGame();
    }

    private void InitializeGame()
    {
      Debug.Log("GameApp: Initializing game systems...");
    }
  }
}
