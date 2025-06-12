using System.Collections;
using System.Collections.Generic;
using DataSO;
using DG.Tweening;
using Gameplay.Card;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Controller
{
    public class SpawnCardController : MonoBehaviour
    {
        private List<CardDataSo> dataSpawn = new List<CardDataSo>();
        [SerializeField] private ColumnController[] columns = new ColumnController[4];
        [SerializeField] private float amountBaseHeight, amountNearHeight;
        
        private Coroutine _coroutine;

        public void StartSpawn(List<CardDataSo> data)
        {
            dataSpawn = data;
            SpawnCard();
        }

        private void SpawnCard()
        {
            for (int i = 0; i < 4; i++)
                columns[i].ClearColumn();
            
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var newCard = ObjectPool.Instance.Get(ObjectPool.Instance.card).GetComponent<CardController>();
                    newCard.Initialized(GetRandomData());
                    columns[j].AddCard(newCard);
                }
            }
            
            for (int i = 2; i < 4; i++)
            {
                var newCard = ObjectPool.Instance.Get(ObjectPool.Instance.card).GetComponent<CardController>();
                newCard.Initialized(GetRandomData());
        
                columns[i].AddCard(newCard);
            }
        }

        private CardDataSo GetRandomData()
        {
            var randomData = dataSpawn[UnityEngine.Random.Range(0, dataSpawn.Count)];
            dataSpawn.Remove(randomData);
            return randomData;
        }
    }
}
