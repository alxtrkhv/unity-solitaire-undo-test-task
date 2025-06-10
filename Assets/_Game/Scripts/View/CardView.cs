using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Solitaire;

namespace Game.View
{
  public class CardView : MonoBehaviour
  {
    [Header("Text Components")]
    [SerializeField]
    private TMP_Text _rankText = null!;

    [SerializeField]
    private TMP_Text _suitText = null!;

    [Header("Colors")]
    [SerializeField]
    private Color _redColor = Color.red;

    [SerializeField]
    private Color _blackColor = Color.black;

    public Card Card { get; private set; } = null!;

    private void Start()
    {
      UpdateVisuals();
    }

    private void UpdateVisuals()
    {
      UpdateRankText();
      UpdateSuitText();
      UpdateColors();
    }

    private void UpdateRankText()
    {
      if (_rankText == null) {
        return;
      }

      _rankText.text = Card.Rank switch {
        CardRank.Ace => "A",
        CardRank.Jack => "J",
        CardRank.Queen => "Q",
        CardRank.King => "K",
        _ => ((int)Card.Rank).ToString(),
      };
    }

    private void UpdateSuitText()
    {
      if (_suitText == null) {
        return;
      }

      _suitText.text = Card.Suit switch {
        CardSuit.Hearts => "♥",
        CardSuit.Diamonds => "♦",
        CardSuit.Clubs => "♣",
        CardSuit.Spades => "♠",
        _ => "",
      };
    }

    private void UpdateColors()
    {
      var color = Card.Color == CardColor.Red ? _redColor : _blackColor;

      if (_rankText != null) {
        _rankText.color = color;
      }

      if (_suitText != null) {
        _suitText.color = color;
      }
    }

    public void SetCard(Card card)
    {
      Card = card;
      UpdateVisuals();
    }
  }
}
