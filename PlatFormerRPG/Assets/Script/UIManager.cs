using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /*
     * 사용 이유 : UI의 버튼과 캐릭터 바를 관리한다.
     * 싱글톤을 사용함으로서 맵이동이 되어도 현재 상태가 씬이 바뀌어도 지속되도록 하려고 한다.
     */
    #region Ui싱글톤
    private static UIManager instance = null;
    public static UIManager Instance => instance;
    #endregion
    #region 기본 싱글톤
    /*
     * UI 설명
     * 일시정지 버튼 : 현재 상태에서 일시정지.  pause 기능
     * 일시정지 해제 버튼 : 일시정지를 푸는 기능
     * 메시지 버튼 : 퀘스트 기능
     * 플레이 버튼 : 던전으로 가는 기능
     * 하트 버튼 : 현재 자신의 상태를 볼 수 있는 기능
     * 딸라 버튼 : 상점을 열 수 있는 버튼
     *
     * 초록색 동그라미 : 캐릭터 얼굴
     * 주황색 동그라미 : 레벨
     * 빨간색 슬라이더 : 채력
     * 노란색 슬라이더 : 경험치
     */
    
    // 버튼 리스트
    public GameObject PauseCanvas;
    public Button PauseButton;
    public GameObject PauseObj;
    public GameObject ResumeCanvas;
    public GameObject QuestCanvas;
    public GameObject QuestOpenCanvas;
    public GameObject DunGeonCanvas;
    public GameObject InfoCanvas;
    public GameObject HpExpCanvas;
    public GameObject InfoBtnCanvas;
    public GameObject StateInfoCanvas;
    public GameObject StateInfoOffCanvas;
    public Button InfoButton;
    public GameObject ShopCanvas;
    public GameObject OnShopCanvas;
    public GameObject OffShopCanvas;
    public Button ShopButton;
    public GameObject Potal;
    
    // 캐릭터 채력 및 레벨 리스트
    public GameObject PlayerDisPlay;
    // public Text LevelInfo;
    // public Slider HPbar;
    // public Slider Expbar;
    
    // 게임이 현재 일시정지 중인지 여부를 나타내는 변수
    public static bool isPaused = false;
    
    // 퀘스트 오브젝트
    public GameObject Quest1;
    public GameObject StateInfoDialog;
    public GameObject ShupUi;
    
    //public Camera cam;
    //public Image[] coolImg;       //쿨타임 이미지
    public Slider hpSlider;         //HP Bar
    public Slider expSlider;        //EXP Bar
    public Text levelTxt;           //Level Txt
    
    
    
    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance !=this)
            {
                Destroy(this.gameObject);
            }
        }
    }
    #endregion
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            PauseObj.SetActive(true);
            PauseCanvas.SetActive(false);
            ResumeCanvas.SetActive(true);
            PauseGame();
            Debug.Log("성공적으로 일시정지 되었습니다. 함수명 : OnPause");
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            PauseObj.SetActive(false);
            ResumeCanvas.SetActive(false);
            PauseCanvas.SetActive(true);
            ResumeGame();
            Debug.Log("성공적으로 일시정지가 해제 되었습니다. 함수명 : OnResume");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (PlayerManager.Instance.InPotal == true)
            {
                Debug.Log("이동");
                SceneManager.LoadScene("Dungeon");
            }
            else if (PlayerManager.Instance.InPotal == false)
            {
                Debug.Log("펄스 입니다");
            }
        }
    }

    public void State(Constructure.Stat myStat)
    {
        hpSlider.maxValue = myStat.MaxHP;
        hpSlider.value = myStat.HP;
        expSlider.maxValue = myStat.MaxExpVal;
        expSlider.value = myStat.ExpVal;
        levelTxt.text = myStat.Level.ToString();
    }

    public void SetHpSlider(float HP)
    {
        hpSlider.value = HP;
    }

    

    #region 버튼 함수
    public void OnPause(bool pauseStatus)
    {
        PauseObj.SetActive(true);
        PauseCanvas.SetActive(false);
        ResumeCanvas.SetActive(true);
        PauseGame(); 
        Debug.Log("성공적으로 일시정지 되었습니다. 함수명 : OnPause");
    }

    public void OnResume(bool pauseStatus)
    {
        PauseObj.SetActive(false);
        ResumeCanvas.SetActive(false);
        PauseCanvas.SetActive(true);
        ResumeGame();
        Debug.Log("성공적으로 일시정지가 해제 되었습니다. 함수명 : OnResume");
    }

    public void OnQuest()
    {
        Quest1.SetActive(true);
        QuestCanvas.SetActive(false);
        QuestOpenCanvas.SetActive(true);
    }

    public void CloseQuest()
    {
        Quest1.SetActive(false);
        QuestOpenCanvas.SetActive(false);
        QuestCanvas.SetActive(true);
    }

    public void OnDungeon()
    {
        SceneManager.LoadScene("Dungeon");
        Debug.Log("성공적으로 던전으로 이동하였습니다.  =  테스트 던전 이동");
        Debug.Log("UI매니져가 무사히 이동이 되었는지 확인을 하여야 합니다");
        QuestCanvas.SetActive(false);
        QuestOpenCanvas.SetActive(false);
        DunGeonCanvas.SetActive(false);
        InfoBtnCanvas.SetActive(false);
        OnShopCanvas.SetActive(false);
        OffShopCanvas.SetActive(false);
        
    }
    public void OnInfo()
    {
        StateInfoCanvas.SetActive(false);
        StateInfoOffCanvas.SetActive(true);
        StateInfoDialog.SetActive(true);
    }

    public void OffInfo()
    {
        StateInfoOffCanvas.SetActive(false);
        StateInfoCanvas.SetActive(true); 
        StateInfoDialog.SetActive(false);
    }

    public void OnShop()
    {
       OnShopCanvas.SetActive(false);
       OffShopCanvas.SetActive(true);
       ShupUi.SetActive(true);
    }

    public void OffShop()
    {
        OnShopCanvas.SetActive(true);
        OffShopCanvas.SetActive(false);
        ShupUi.SetActive(false);
    }

    #endregion

    #region OnPause관련 함수

    // 게임을 일시정지하는 함수
    private void PauseGame()
    {
        Time.timeScale = 0; // 게임 시간을 정지시킴
        isPaused = true;
        
    }

    // 게임을 재개하는 함수
    private void ResumeGame()
    {
        Time.timeScale = 1; // 게임 시간을 다시 시작함
        isPaused = false;
        
    }

    #endregion
    
    
    
    
}
