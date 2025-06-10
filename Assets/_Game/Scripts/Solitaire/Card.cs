using System;

namespace Game.Solitaire
{
  public class Card
  {
    public CardRank Rank { get; }
    public CardSuit Suit { get; }

    public CardColor Color =>
      Suit is CardSuit.Hearts or CardSuit.Diamonds
        ? CardColor.Red
        : CardColor.Black;

    public Card(CardRank rank, CardSuit suit)
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
