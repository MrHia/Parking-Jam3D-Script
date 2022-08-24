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
    public AudioSource m_MoveAudio;

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
    public int CountCarFinish()
    {


        return GameObject.FindGameObjectsWithTag("Finish").Length;
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
            m_ui.SetCointWinText("" + (m_db.GetCoint() + m_Coint));
            IsWingame = false;
        }
    }
    float x = 25f;
    private void Update()
    {
        CountCarInGame();
        CheckWinGame();
        CountCarFinish();
        m_ui.SetCointPauseText(""+m_db.GetCoint());
        
        if (!isPausedMoveAudio)
        {
            if (CountCarFinish() == 0 && m_MoveAudio.isPlaying)
            {
                m_MoveAudio.Stop();
            }
            if (CountCarFinish() > 0 && !m_MoveAudio.isPlaying)
            {
                m_MoveAudio.Play();
            }
        }
        if(CarInGame ==0 && CountCarFinish() > 0)
        {
            x -= Time.fixedDeltaTime;
            if (x <= 0 )
            {
                GameObject[] FinishCar = GameObject.FindGameObjectsWithTag("Finish");
                for(int i=0;  i<FinishCar.Length; i++)
                {
                    CarController CarfinishCtrl = FinishCar[i].GetComponent<CarController>();
                    CarfinishCtrl.isMoveTowards = true;
                    //Debug.LogError("hi");
                    
                }
                //GameObject.FindGameObjectsWithTag("Finish").[]
            }
        }

    }



    public void RePlay()
    {
        
        IsWingame = true;
        Destroy(GameObject.FindWithTag("Map"));
        Instantiate(Resources.Load<GameObject>("Level/Level-"+m_db.GetLevel()));
        m_Coint = 0;
        CountCarInGame();
        m_ui.SetLevelText("Level: " + m_db.GetLevel());
    }
    public bool isPausedMoveAudio = false;
    //bool isPausedDrifAudio = false;
    public void PauseGame()
    {
        Time.timeScale = 0;

        if (m_MoveAudio.isPlaying)
        {
            m_MoveAudio.Pause();
            isPausedMoveAudio = true;
        }
        //if(CarController)

    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        if (isPausedMoveAudio)
        {
            m_MoveAudio.Play();
            isPausedMoveAudio = false;
        }

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
