using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Solitaire
{
  public class SolitaireBoard
  {
    public BoardSettings Settings { get; }

    public Pile[] Tableau { get; }

    public Pile[] Foundations { get; }

    public Pile Deck { get; }

    public Pile WastePile { get; }

    public SolitaireBoard(BoardSettings? settings = null)
    {
      Settings = settings ?? BoardSettings.Default;

      Tableau = new Pile[Settings.TableauColumns];
      for (var i = 0; i < Tableau.Length; i++) {
        Tableau[i] = new(PileKind.Tableau);
      }

      Foundations = new Pile[Settings.FoundationPiles];
      for (var i = 0; i < Foundations.Length; i++) {
        Foundations[i] = new(PileKind.Foundation);
      }

      Deck = new(PileKind.Deck);
      WastePile = new(PileKind.WastePile);
    }

    public void Populate(IEnumerable<Card> cards)
    {
      var shuffledCards = cards.ToList();
      var random = new Random();
      for (var i = shuffledCards.Count - 1; i > 0; i--) {
        var j = random.Next(i + 1);
        (shuffledCards[i], shuffledCards[j]) = (shuffledCards[j], shuffledCards[i]);
      }

      var cardIndex = 0;

      for (var column = 0; column < Settings.TableauColumns; column++) {
        Card? columnHead = null;
        Card? currentCard = null;

        for (var row = 0; row <= column; row++) {
          if (cardIndex >= shuffledCards.Count) {
            continue;
          }

          var card = shuffledCards[cardIndex];
          if (columnHead == null) {
            columnHead = card;
          } else {
            currentCard?.SetChild(card);
          }

          currentCard = card;
          cardIndex++;
        }

        Tableau[column].Append(columnHead);
      }

      Card? deckHead = null;
      Card? currentDeckCard = null;

      while (cardIndex < shuffledCards.Count) {
        var card = shuffledCards[cardIndex];
        if (deckHead == null) {
          deckHead = card;
        } else {
          currentDeckCard!.SetChild(card);
        }

        currentDeckCard = card;
        cardIndex++;
      }

      Deck.Append(deckHead);
    }
  }
}
