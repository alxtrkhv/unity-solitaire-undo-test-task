using System;

namespace Game.Solitaire
{
  public class Card
  {
    public CardInfo Info { get; }
    public Card? Child { get; private set; }

    public CardRank Rank => Info.Rank;
    public CardSuit Suit => Info.Suit;
    public CardColor Color => Info.Color;

    public Card(CardRank rank, CardSuit suit)
    {
      Info = new CardInfo(rank, suit);
    }

    public Card(CardInfo info)
    {
      Info = info;
    }

    public void SetChild(Card? child)
    {
      Child = child;
    }

    public override string ToString()
    {
      return Info.ToString();
    }
  }
}
