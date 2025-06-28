using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Gameplay.Card;
using UnityEngine;

namespace Gameplay.Queen
{
    public class QueenController : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> cardRenderer = new List<SpriteRenderer>();
        [SerializeField] private CardController[] cardInCrown = new CardController[6];
        [SerializeField] private Transform content;

        public bool CanAddCardToCrown(List<CardController> cards)
        {
            foreach (var card in cards)
                if (cardInCrown[card.Data.indexCard] != null)
                    return false;
            
            var indexNext = cards[^1].Data.indexCard + 1 < 6 ? cards[^1].Data.indexCard + 1 : 0;
            var indexPrev = cards[0].Data.indexCard - 1 > 0 ? cards[0].Data.indexCard - 1 : 5;

            /*if (cardInCrown[indexPrev] != null 
                && cardInCrown[indexPrev].Data.colorType == cards[0].Data.colorType)
                return false;
                
            if (cardInCrown[indexNext] != null 
                && cardInCrown[indexNext].Data.colorType == cards[^1].Data.colorType)
                return false;*/
         
            return true;
        }

        public void AddCardToCrown(List<CardController> cards)
        {
            foreach (var card in cards)
            {
                cardRenderer[card.Data.indexCard].sprite = card.Data.icon;
                cardInCrown[card.Data.indexCard] = card;
                ObjectPool.Instance.Return(card.gameObject, true); 
            }
            
            foreach (var card in cardInCrown)
                if (card == null)
                    return;
            
            CheckComplete();
        }

        private void CheckComplete()
        {
            StartCoroutine(WinCoroutine());
        }

        IEnumerator WinCoroutine()
        {
            yield return new WaitForSeconds(1);
            content.DOLocalMoveY(5, 1.5f).SetEase(Ease.Linear);
          
            yield return new WaitForSeconds(1.5f);
            foreach (var card in cardRenderer)
                card.sprite = null;
            for (int i = 0; i < cardInCrown.Length; i++)
                cardInCrown[i] = null;

            content.DOLocalMoveY(3.75f, 1.5f).OnComplete(() =>
            {
                GameManager.Instance.CheckGame();
            });
        }
    }
}
