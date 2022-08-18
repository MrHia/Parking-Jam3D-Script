using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using DG.Tweening;

public class GameController : MonoBehaviour
{

    int LevelGame;
    public int m_Coint = 0;
    public int CarInGame = 0;
    public RectTransform PauseMenu, WingameMenu, MainMenu;
    int Coint_db;
    UIManager m_ui;

    Data m_db;

    public string statusGame = "";
    public void incrementCoint()
    {
        m_Coint++;
    }
    private void Awake()
    {
        
    }
    private void Start()
    {
        m_ui = FindObjectOfType<UIManager>();
        m_db = FindObjectOfType<Data>();
        Instantiate(Resources.Load<GameObject>("Level/Level-" + m_db.GetLevel()));

        m_ui.SetLevelText("Level: "+ m_db.GetLevel());
        m_ui.SetCointWinText("" + m_db.GetCoint());
        CountCarInGame();


    }
    void CountCarInGame()
    {
        CarInGame = 1;
        CarInGame += GameObject.FindGameObjectsWithTag("CarHorizontal-left").Length;
        

        CarInGame += GameObject.FindGameObjectsWithTag("CarVertical-top").Length;
        

        CarInGame += GameObject.FindGameObjectsWithTag("CarVertical-bottom").Length;
        

        CarInGame += GameObject.FindGameObjectsWithTag("CarHorizontal-right").Length;
        CarInGame -= 1;

    }
    public void ReduceCar()
    {
        CarInGame--;
    }
    public bool IsWingame = true;
    void CheckWinGame()
    {
        int CarFinish = GameObject.FindGameObjectsWithTag("Finish").Length;
        if (CarInGame == 0 && IsWingame && CarFinish ==0)
        {
            MainMenu.DOAnchorPos(new Vector2(0, 2000), 0.25f);
            WingameMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);

            IsWingame = false;
        }
    }

    private void Update()
    {
        CountCarInGame();
        CheckWinGame();

        m_ui.SetCointPauseText(""+m_db.GetCoint());
        
    }



    public void RePlay()
    {
        
        IsWingame = true;
        Destroy(GameObject.FindWithTag("Map"));
        Instantiate(Resources.Load<GameObject>("Level/Level-"+m_db.GetLevel()));
        m_Coint = 0;
        CountCarInGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;


    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void PauseButton()
    {


        MainMenu.DOAnchorPos(new Vector2(0, 2000), 0.1f);
        PauseMenu.DOAnchorPos(new Vector2(0, 0), 0.1f).OnComplete(() => { PauseGame(); });

    }
    public void ResumeButton()
    {
        ResumeGame();
        MainMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
        PauseMenu.DOAnchorPos(new Vector2(1300, 0), 0.25f);
    }
    public void Nextlevel()
    {   

        MainMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
        WingameMenu.DOAnchorPos(new Vector2(-1300, 0), 0.25f);
        m_db.SetCoint(m_Coint);

        m_db.SetLevel();
        Destroy(GameObject.FindWithTag("Map"));
        Instantiate(Resources.Load<GameObject>("Level/Level-" + m_db.GetLevel()));
        m_ui.SetLevelText("Level: " + m_db.GetLevel());
        m_ui.SetCointPauseText(""+m_db.GetCoint());
        m_ui.SetCointWinText("" + m_db.GetCoint());
        IsWingame = true;
        m_Coint = 0;
        CountCarInGame();
        m_db.SaveData();

    }

   
}
