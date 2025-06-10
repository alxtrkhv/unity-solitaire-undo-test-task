using UnityEngine;
using Game.Solitaire;

namespace Game.View
{
  public class PileView : MonoBehaviour
  {
    public Pile Pile { get; private set; } = null!;

    private void Start()
    {
      UpdateVisuals();
    }

    private void UpdateVisuals() { }

    public void SetPile(Pile pile)
    {
      Pile = pile;
      UpdateVisuals();
    }

    public void RefreshCardPositions()
    {
      UpdateVisuals();
    }
  }
}
