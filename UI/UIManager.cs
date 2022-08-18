using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class UIManager : MonoBehaviour
{


    public TMP_Text  CointTextPause, CointWinText,LevelText;

    //public GameObject WinGamePanel;

    


    public void SetCointPauseText(string txt)
    {
        if (CointTextPause)
        {
            CointTextPause.text = txt;
        }
    }
    public void SetCointWinText(string txt)
    {
        if (CointWinText)
        {
            CointWinText.text = txt;
        }
    }

    public void SetLevelText(string txt)
    {
        if (LevelText)
        {
            LevelText.text = txt;
        }
    }


    //public void ShowWinGame(bool isShow)
    //{
    //    if (WinGamePanel)
    //        WinGamePanel.SetActive(isShow);
    //}
  
}
