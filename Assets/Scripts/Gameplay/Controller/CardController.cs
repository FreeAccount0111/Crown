using System;
using System.Collections.Generic;
using DataSO;
using DG.Tweening;
using Gameplay.Controller;
using Gameplay.Queen;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Card
{
    public class CardController : MonoBehaviour
    {
        private ColumnController _columnController;
        [SerializeField] private List<CardController> cardMoving = new List<CardController>();
        [FormerlySerializedAs("cellMask")] [SerializeField] private LayerMask columnMask;
        [SerializeField] private LayerMask queenMask;
        [SerializeField] private CardDataSo cardDataSo;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public CardDataSo Data => cardDataSo;
        private Vector3 _originalPosition;
        private Vector3 _amountOrigin;

        private bool isMoving;

        public void Initialized(CardDataSo cardData)
        {
            cardDataSo = cardData;
            spriteRenderer.sprite = cardData.icon;
        }

        public void SetColumn(ColumnController columnController)
        {
            _columnController = columnController;
            transform.SetParent(_columnController.transform);
        }

        private void OnMouseDown()
        {
            if (_columnController.CanSelectCard(this,cardMoving))
            {
                isMoving = true;
                foreach (var card in cardMoving)
                    card.transform.SetParent(transform);
                
                var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _originalPosition = transform.position;
                _amountOrigin = transform.position - point;
            }
        }

        private void OnMouseDrag()
        {
            if (isMoving)
            {
                var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = _amountOrigin + point;
            }
        }

        private void OnMouseUp()
        {
            if (isMoving)
            {
               CheckEnd();
            }
        }

        private void CheckEnd()
        {
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitQueen = Physics2D.OverlapPoint(point, queenMask);
            if (hitQueen != null)
                CheckQueen(hitQueen);
            else
            {
                Collider2D hit = Physics2D.OverlapPoint(point, columnMask);
                CheckColumn(hit);
            }
        }

        private void CheckQueen(Collider2D hit)
        {
            if (hit != null)
            {
                var newQueen = hit.GetComponent<QueenController>();
                if (newQueen.CanAddCardToCrown(cardMoving))
                {
                    var oldColumn = _columnController;
                    for (int i = cardMoving.Count - 1; i >= 0; i--)
                        oldColumn.RemoveCard(cardMoving[i]);
                    
                    newQueen.AddCardToCrown(cardMoving);
                }
                else
                {
                    transform.DOMove(_originalPosition, 0.05f).SetEase(Ease.Linear);
                    isMoving = false;
                }
            }
            else
            {
                transform.DOMove(_originalPosition, 0.05f).SetEase(Ease.Linear);
                isMoving = false;
            }
        }

        private void CheckColumn(Collider2D hit)
        {
            if (hit != null)
            {
                var newColumn = hit.GetComponent<ColumnController>();
                if (newColumn.CanAddCard(this))
                {
                    var oldColumn = _columnController;
                    for (int i = cardMoving.Count - 1; i >= 0; i--)
                    {
                        oldColumn.RemoveCard(cardMoving[i]);
                        newColumn.AddCard(cardMoving[i]);
                    }
                }
                else
                {
                    transform.DOMove(_originalPosition, 0.05f).SetEase(Ease.Linear);
                    isMoving = false;
                }
            }
            else
            {
                transform.DOMove(_originalPosition, 0.05f).SetEase(Ease.Linear);
                isMoving = false;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            Initialized(cardDataSo);
        }
        #endif
    }
}
