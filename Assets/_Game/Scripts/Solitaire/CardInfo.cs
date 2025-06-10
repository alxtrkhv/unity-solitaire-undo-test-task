using System;

namespace Game.Solitaire
{
  public struct CardInfo
  {
    public CardRank Rank { get; }
    public CardSuit Suit { get; }

    public CardColor Color =>
      Suit is CardSuit.Hearts or CardSuit.Diamonds
        ? CardColor.Red
        : CardColor.Black;

    public CardInfo(CardRank rank, CardSuit suit)
    {
      Rank = rank;
      Suit = suit;
    }

    public override string ToString()
    {
      return $"{Rank} of {Suit}";
    }
  }
}
