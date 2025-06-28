using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private LevelCtrl _currentLevelCtrl;
    public int indexCurrentLevel;
    
    public static Func<bool> GameComplete;
    
    private void Awake()
    {
        Instance = this;
    }

    public void LoadGame(int indexLevel)
    {
        indexCurrentLevel = indexLevel;
    }

    public void ResetLevel()
    {

    }

    public void CheckGame()
    {
        if (GameComplete())
        {
            PopupCtrl.Instance.GetPopupByType<PopupWin>().UpdateSore(UnityEngine.Random.Range(40, 100));
            PopupCtrl.Instance.GetPopupByType<PopupWin>().ShowImmediately(false);
        }
    }
}
