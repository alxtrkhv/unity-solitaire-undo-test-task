using System.Collections.Generic;
using Game.Solitaire;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.View
{
  public class SolitaireInput : MonoBehaviour
  {
    [Header("References")]
    [SerializeField]
    private GameView _gameView = null!;

    [SerializeField]
    private Transform _movingCardTransform = null!;

    private Card? _selectedCard;
    private CardView? _selectedCardView;
    private bool _isDragging;
    private Vector3 _dragOffset;
    private Canvas _canvas = null!;
    private Camera _camera = null!;

    private readonly List<RaycastResult> _results = new();

    private void Start()
    {
      _canvas = GetComponentInParent<Canvas>();
      _camera = Camera.main!;
    }

    private void Update()
    {
      HandleInput();
    }

    private void HandleInput()
    {
      if (Input.GetMouseButtonDown(0)) {
        HandleMouseDown();
      } else if (Input.GetMouseButton(0) && _isDragging) {
        HandleMouseDrag();
      } else if (Input.GetMouseButtonUp(0)) {
        HandleMouseUp();
      }

      if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.U)) {
        _gameView.UndoLastMove();
      }
    }

    private void HandleMouseDown()
    {
      var cardView = GetCardViewUnderMouse();
      if (cardView == null) {
        return;
      }

      var card = cardView.Card;

      if (!CanMoveCard(card)) {
        return;
      }

      _selectedCard = card;
      _selectedCardView = cardView;
      _isDragging = true;

      var mouseWorldPos = GetMouseWorldPosition();
      _dragOffset = cardView.transform.position - mouseWorldPos;

      cardView.transform.SetParent(_movingCardTransform, true);
    }

    private void HandleMouseDrag()
    {
      if (_selectedCardView == null) return;

      var mouseWorldPos = GetMouseWorldPosition();
      _selectedCardView.transform.position = mouseWorldPos + _dragOffset;
    }

    private void HandleMouseUp()
    {
      if (_selectedCard == null || _selectedCardView == null) {
        ResetSelection();
        return;
      }

      var targetPile = GetPileUnderMouse();
      if (targetPile != null) {
        var success = _gameView.TryMoveCard(_selectedCard, targetPile);
        if (!success) {
          ReturnCardToOriginalPosition();
        }
      } else {
        ReturnCardToOriginalPosition();
      }

      ResetSelection();
    }

    private CardView? GetCardViewUnderMouse()
    {
      var pointerEventData = new PointerEventData(EventSystem.current) {
        position = Input.mousePosition
      };

      EventSystem.current.RaycastAll(pointerEventData, _results);

      foreach (var result in _results) {
        var cardView = result.gameObject.transform.parent.GetComponent<CardView>();
        if (cardView != null) {
          return cardView;
        }
      }

      return null;
    }

    private Pile? GetPileUnderMouse()
    {
      var pointerEventData = new PointerEventData(EventSystem.current) {
        position = Input.mousePosition
      };

      EventSystem.current.RaycastAll(pointerEventData, _results);

      foreach (var result in _results) {
        var parent = result.gameObject.transform.parent;
        var pileView = parent.GetComponent<PileView>();
        if (pileView != null) {
          return pileView.Pile;
        }

        var cardView = parent.GetComponent<CardView>();
        if (cardView != null && cardView != _selectedCardView) {
          return cardView.Card.Pile;
        }
      }

      return null;
    }

    private bool CanMoveCard(Card card)
    {
      if (card.Child == null) {
        return true;
      }

      if (card.Pile?.PileKind == PileKind.Tableau) {
        return IsValidTableauSequence(card);
      }

      return false;
    }

    private bool IsValidTableauSequence(Card startCard)
    {
      var current = startCard;
      while (current.Child != null) {
        var next = current.Child;

        if ((int)current.Rank != (int)next.Rank + 1 || current.Color == next.Color) {
          return false;
        }

        current = next;
      }

      return true;
    }

    private Vector3 GetMouseWorldPosition()
    {
      var mouseScreenPos = Input.mousePosition;

      if (_canvas.renderMode == RenderMode.ScreenSpaceOverlay) {
        return mouseScreenPos;
      }

      if (_canvas.renderMode == RenderMode.ScreenSpaceCamera) {
        return _camera.ScreenToWorldPoint(new(mouseScreenPos.x, mouseScreenPos.y, _canvas.planeDistance));
      }

      return _camera.ScreenToWorldPoint(mouseScreenPos);
    }

    private void ReturnCardToOriginalPosition()
    {
      if (_selectedCardView == null) return;

      _gameView.Refresh();
    }

    private void ResetSelection()
    {
      _selectedCard = null;
      _selectedCardView = null;
      _isDragging = false;
      _dragOffset = Vector3.zero;
    }
  }
}
