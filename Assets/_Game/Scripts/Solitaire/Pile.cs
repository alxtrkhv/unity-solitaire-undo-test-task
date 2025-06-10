namespace Game.Solitaire
{
  public class Pile
  {
    public PileKind PileKind { get; }
    public Card? FirstChild { get; private set; }

    public Card? LastChild
    {
      get
      {
        if (FirstChild == null) {
          return null;
        }

        var current = FirstChild;
        while (current.Child != null) {
          current = current.Child;
        }

        return current;
      }
    }

    public Pile(PileKind pileKind)
    {
      PileKind = pileKind;
      FirstChild = null;
    }

    public void Append(Card? card)
    {
      if (card == null) {
        return;
      }

      card.SetPile(this);

      if (FirstChild == null) {
        FirstChild = card;
        return;
      }

      var current = FirstChild;
      while (current.Child != null) {
        current = current.Child;
      }
      
      current.SetChild(card);
    }

    public void SetEmpty()
    {
      FirstChild = null;
    }
  }
}
