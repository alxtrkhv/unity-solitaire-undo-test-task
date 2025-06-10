using UnityEngine;
using Game.Solitaire;

namespace Game.View
{
  public class PileView : MonoBehaviour
  {
    [SerializeField]
    private Transform _cardContainer = null!;

    public Pile Pile { get; private set; } = null!;

    private void Start()
    {
      if (_cardContainer == null) {
        _cardContainer = transform;
      }

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
