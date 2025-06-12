using System;
using System.Collections;
using System.Collections.Generic;
using DataSO;
using Gameplay.Controller;
using UnityEngine;

namespace Gameplay.Card
{
    public class CardManager : MonoBehaviour
    {
        private const string BLUE_PATH = "GameDatas/Card/Blue";
        private const string GREEN_PATH = "GameDatas/Card/Green";
        private const string ORANGE_PATH = "GameDatas/Card/Orange";
        private const string PURPLE_PATH = "GameDatas/Card/Purple";
        private const string RED_PATH = "GameDatas/Card/Red";
        private const string YELLOW_PATH = "GameDatas/Card/Yellow";

        [SerializeField] private CardDataSo[] bluesData;
        [SerializeField] private CardDataSo[] greensData;
        [SerializeField] private CardDataSo[] orangesData;
        [SerializeField] private CardDataSo[] purplesData;
        [SerializeField] private CardDataSo[] redsData;
        [SerializeField] private CardDataSo[] yellowsData;

        [SerializeField] private SpawnCardController spawnCardController;
        [SerializeField] private List<CardDataSo> dataSpawn = new List<CardDataSo>();

        private Coroutine _coroutine;

        private void Awake()
        {
            Initialized();
        }

        private void Initialized()
        {
            bluesData = Resources.LoadAll<CardDataSo>(BLUE_PATH);
            greensData = Resources.LoadAll<CardDataSo>(GREEN_PATH);
            orangesData = Resources.LoadAll<CardDataSo>(ORANGE_PATH);
            purplesData = Resources.LoadAll<CardDataSo>(PURPLE_PATH);
            redsData = Resources.LoadAll<CardDataSo>(RED_PATH);
            yellowsData = Resources.LoadAll<CardDataSo>(YELLOW_PATH);
        }

        [ContextMenu("Start Spawn")]
        public void SpawnCard()
        {
           CreateDataSpawn();
           spawnCardController.StartSpawn(dataSpawn);
        }

        private void CreateDataSpawn()
        {
            dataSpawn.Clear();
            
            for (int i = 0; i < 3; i++)
            {
                List<int> colors = new List<int>() { 0, 1, 2, 3, 4, 5 };
                for (int j = 0; j < 6; j++)
                {
                    var indexColor = colors[UnityEngine.Random.Range(0, colors.Count)];
                    colors.Remove(indexColor);
                    
                    var randomData = GetDataSo(j, indexColor);
                    dataSpawn.Add(randomData);
                }
            }
            
            Shuffle(dataSpawn);
        }

        private static void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            for (int i = 0; i < n - 1; i++)
            {
                int r = UnityEngine.Random.Range(i, n);
                (list[i], list[r]) = (list[r], list[i]);
            }
        }

        private CardDataSo GetDataSo(int indexCard, int indexColor)
        {
            CardDataSo[] cardData = new CardDataSo[0];
            switch (indexColor)
            {
                case 0:
                    cardData = bluesData;
                    break;
                case 1:
                    cardData = greensData;
                    break;
                case 2:
                    cardData = orangesData;
                    break;
                case 3:
                    cardData = purplesData;
                    break;
                case 4:
                    cardData = redsData;
                    break;
                case 5:
                    cardData = yellowsData;
                    break;
            }

            return cardData[indexCard];
        }
    }
}
