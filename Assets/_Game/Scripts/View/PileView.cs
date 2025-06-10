using UnityEngine;
using UnityEngine.UI;
using Game.Solitaire;

namespace Game.View
{
  public class PileView : MonoBehaviour
  {
    public Pile Pile { get; private set; } = null!;

    public void SetPile(Pile pile)
    {
      Pile = pile;
    }
  }
}
