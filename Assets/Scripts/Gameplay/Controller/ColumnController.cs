using System.Collections.Generic;
using Gameplay.Card;
using UnityEngine;

namespace Gameplay.Controller
{
    public class ColumnController : MonoBehaviour
    {
        [SerializeField] private Transform posStart;
        [SerializeField] private List<CardController> cards = new List<CardController>();
        
        public bool hasCards => cards.Count > 0;

        public bool CanSelectCard(CardController card,List<CardController> cardMoving)
        {
            cardMoving.Clear();
            for (int i = cards.Count - 1; i >= 0; i--)
            {
                cardMoving.Add(cards[i]);
                if (i > 0 && i < cards.Count - 1)
                {
                    if (!CanConnectCard(cards[i + 1],cards[i]))
                    {
                        return false;
                    }
                }
                
                if (cards[i] == card)
                    return true;
            }
            
            return false;
        }

        public bool CanAddCard(CardController newCard)
        {
            if (cards.Count == 0)
                return true;
            
            if (CanConnectCard(newCard,cards[^1]))
                return true;
            else
                return false;
        }

        public void AddCard(CardController newCard)
        {
            newCard.SetColumn(this);
            
            if (cards.Count == 0)
                newCard.transform.position= posStart.position;
            else
            {
                if (CanConnectCard(newCard,cards[^1]))
                    newCard.transform.localPosition = cards[^1].transform.localPosition - new Vector3(0, 0.84f, 0.01f);
                else
                    newCard.transform.localPosition = cards[^1].transform.localPosition - new Vector3(0, 1.15f, 0.01f);
            }
                    
            cards.Add(newCard);
        }

        public void RemoveCard(CardController card)
        {
            cards.Remove(card);
        }

        public void ClearColumn()
        {
            foreach (var card in cards)
                ObjectPool.Instance.Return(card.gameObject,true);
            
            cards.Clear();
        }

        private bool CanConnectCard(CardController card1,CardController card2)
        {
            if ((card1.Data.indexCard == card2.Data.indexCard + 1
                 || card2.Data.indexCard - card1.Data.indexCard == 5)
                /*&& card1.Data.colorType != card2.Data.colorType*/)
                return true;
            else
                return false;
        }
    }
}
