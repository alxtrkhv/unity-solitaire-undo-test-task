namespace Game.Solitaire
{
  public class Card
  {
    public CardInfo Info { get; }
    public int Id { get; }

    public Card? Child { get; private set; }
    public Card? Parent { get; private set; }
    public Pile? Pile { get; private set; }

    public CardRank Rank => Info.Rank;
    public CardSuit Suit => Info.Suit;
    public CardColor Color => Info.Color;

    public Card(CardRank rank, CardSuit suit, int id)
    {
      Info = new(rank, suit);
      Id = id;
    }

    public Card(CardInfo info, int id)
    {
      Info = info;
      Id = id;
    }

    public void SetChild(Card? child)
    {
      Child = child;
      child?.SetParent(this);
    }

    public void SetParent(Card? parent)
    {
      Parent = parent;
      SetPile(Parent?.Pile);
    }

    public void SetPile(Pile? pile)
    {
      Pile = pile;
      Child?.SetPile(pile);
    }

    public override string ToString()
    {
      return Info.ToString();
    }
  }
}
