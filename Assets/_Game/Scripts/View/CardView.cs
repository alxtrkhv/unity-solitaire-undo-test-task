using UnityEngine;
using Game.Solitaire;

namespace Game.View
{
  public class CardView : MonoBehaviour
  {
    public Card Card { get; private set; } = null!;

    private void Start()
    {
      UpdateVisuals();
    }

    private void UpdateVisuals() { }

    public void SetCard(Card card)
    {
      Card = card;
      UpdateVisuals();
    }
  }
}
