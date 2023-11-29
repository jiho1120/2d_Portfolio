using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    /*
     * 사용 이유 : UI의 버튼과 캐릭터 바를 관리한다.
     * 싱글톤을 사용함으로서 맵이동이 되어도 현재 상태가 씬이 바뀌어도 지속되도록 하려고 한다.
     */
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

    #region 버튼 리스트
    // 버튼 리스트
    public GameObject PauseCanvas;
    public GameObject PauseObj;
    public GameObject ResumeCanvas;
    public GameObject QuestCanvas;
    public GameObject QuestOpenCanvas;
    public GameObject DunGeonCanvas;
    public GameObject InfoBtnCanvas;
    public GameObject StateInfoCanvas;
    public GameObject StateInfoOffCanvas;
    public GameObject OnShopCanvas;
    public GameObject OffShopCanvas;
    #endregion

    #region 게임이 현재 일시정지 중인지 여부를 나타내는 변수
    // 게임이 현재 일시정지 중인지 여부를 나타내는 변수
    public static bool isPaused = false;
    #endregion

    #region 퀘스트 오브젝트
    // 퀘스트 오브젝트
    public GameObject Quest1;
    public GameObject StateInfoDialog;
    public GameObject ShupUi;
    #endregion
    
    
    //public Camera cam;
    //public Image[] coolImg;       //쿨타임 이미지
    public Slider hpSlider;         //HP Bar
    public Slider expSlider;        //EXP Bar
    public Text StateBtn_levelTxt;           //Level Txt
    public GameObject TypeW;
    public GameObject TypeD;
    public GameObject UiScript;
    public Slider BossHpSlider;
    public int PotionCount = 0;
    public Text PotionCountText;
    public Text GoldText;
    public int TotalGold = 0;
    public int UseGold = 100;
    public Constructure.Stat stat;
    public Text PopupLevelText;
    public Text PopupAttText;
    public Text PopupDefText;
    public Text PopupExpText;
    public Text PopupMaxExpText;
    public Text PopupNameText;
    public Text PopupTypeText;
    public Text AddStatCount;
    public Image panelImage;
    private string PopupNameText_string;
    private string PopupTypeText_string;

    private void Start()
    {
        hpSlider.value = 0;
        expSlider.value = 100;
        BossHpSlider.maxValue = 0;
        PotionCount += 3;
        PotionCountText.text = PotionCount.ToString();
        PopupLevelText.text = PlayerManager.Instance.defaultStats.Level.ToString();
        PopupAttText.text = PlayerManager.Instance.defaultStats.Att.ToString();
        PopupExpText.text = PlayerManager.Instance.defaultStats.ExpVal.ToString();
        PopupMaxExpText.text = PlayerManager.Instance.defaultStats.MaxExpVal.ToString();
        PopupNameText.text = PopupNameText_string;
        PopupTypeText.text = PopupTypeText_string;
        StateBtn_levelTxt.text = PlayerManager.Instance.defaultStats.Level.ToString();
        // AddStatCount.text = PlayerManager.Instance.AddStatCount.ToString();


    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.P)) // Pause의 P
        {
            PauseObj.SetActive(true);
            PauseCanvas.SetActive(false);
            ResumeCanvas.SetActive(true);
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.O)) // On의 O
        {
            PauseObj.SetActive(false);
            ResumeCanvas.SetActive(false);
            PauseCanvas.SetActive(true);
            ResumeGame();
            Debug.Log("성공적으로 일시정지가 해제 되었습니다. 함수명 : OnResume");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.W)))
        {
            if (PlayerManager.Instance.InPotal == true)
            {
                Debug.Log("이동");
                OnDungeon();
            }
            else if (PlayerManager.Instance.InPotal == false)
            {
                Debug.Log("이동 할 수 없는 상태 입니다");
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            BossHpSlider.value += 100;
            Debug.Log("보스의 채력을 강제로 채력 0");
        }
        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            hpSlider.value += 100;
            Debug.Log("플레이어의 채력을 강제로 채력 0");
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            TotalGold += 10000;
            Debug.Log("강제로 보유금액 10000으로 증가");
        }


        if (Input.GetKeyDown(KeyCode.F4))
        {
            expSlider.value -= 50;
            Debug.Log("경험치를 강제로 50%씩 증가시킴");
            if (expSlider.value == 0)
            {
                Debug.Log("레벨업!");
                expSlider.value = 100;
                LevelUp();
            }
        }

        if (Input.GetKeyDown(KeyCode.Insert))
        {
            UsePotion();
        }
    }

    public void LevelUp()
    {
        PlayerManager.Instance.defaultStats.Level += 1;
        PlayerManager.Instance.AddStatCount += 2;
        PlayerManager.Instance.defaultStats.Att += 1;
        Debug.Log("레벨 : "+PlayerManager.Instance.defaultStats.Level);
        Debug.Log("습득 능력치 카운트 : "+PlayerManager.Instance.AddStatCount);
        PotionCountText.text = PotionCount.ToString();
        PopupLevelText.text = PlayerManager.Instance.defaultStats.Level.ToString();
        PopupAttText.text = PlayerManager.Instance.defaultStats.Att.ToString();
        PopupExpText.text = PlayerManager.Instance.defaultStats.ExpVal.ToString();
        PopupMaxExpText.text = PlayerManager.Instance.defaultStats.MaxExpVal.ToString();
        PopupNameText.text = PopupNameText_string;
        PopupTypeText.text = PopupTypeText_string;
        StateBtn_levelTxt.text = PlayerManager.Instance.defaultStats.Level.ToString();
        // AddStatCount.text = PlayerManager.Instance.AddStatCount.ToString();
        
    }

    public void State(Constructure.Stat myStat)
    {
        if (hpSlider !=null)
        {
            hpSlider.maxValue = myStat.MaxHP;
            hpSlider.value = myStat.HP;
        }
        if (expSlider != null)
        {
            expSlider.maxValue = myStat.MaxExpVal;
            expSlider.value = myStat.ExpVal;
        }
        if (StateBtn_levelTxt != null)        
            StateBtn_levelTxt.text = myStat.Level.ToString();
    }

    public void SetHpSlider(float HP)
    {
        hpSlider.value = HP;
    }

    public void SetExpSlider(float Exp)
    {
        expSlider.value = Exp;
    }

    public void SetName(string name)
    {
        Debug.Log(name+" 라는 이름이 정상적으로 입력이 되었습니다.");
        PopupNameText_string = name;

    }

    public void SetType(AllEnum.Type type)
    {
        if (type == AllEnum.Type.Warrior)
        {
            TypeW.SetActive(true);
            TypeD.SetActive(false);
        }

        else if (type == AllEnum.Type.Dragon)
        {
            TypeD.SetActive(true);
            TypeW.SetActive(false); 
        }
        PopupTypeText_string = type.ToString();
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

    public void OnVillagePotal()
    {
        SceneManager.LoadScene("VillageScene");
        QuestCanvas.SetActive(true);
        QuestOpenCanvas.SetActive(true);
        DunGeonCanvas.SetActive(true);
        InfoBtnCanvas.SetActive(true);
        OnShopCanvas.SetActive(true);
        OffShopCanvas.SetActive(true);
        // 코루틴 끄기
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

    public void OffUiScript()
    {
        UiScript.SetActive(false);
    }

    public void OnUiScript()
    {
        UiScript.SetActive(true);
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

    #region Potion

    public void UsePotion()
    {
        
        if (PotionCount <= 0 || hpSlider.value == 0)
        {
            PotionCount -= 0;
            PotionCountText.text = PotionCount.ToString();
            hpSlider.value -= 0;
        }
        else
        {
            hpSlider.value -= 100;
            PotionCount -= 1;
            PotionCountText.text = PotionCount.ToString();
        }
    }

    public void GetPotion()
    {
        
        if (TotalGold < UseGold)
        {
            Debug.Log("보유금액이 구매하고자 하는 금액보다 적음");
        }
        else
        {
            Debug.Log("구매완료");
            
            TotalGold -= UseGold;
            PotionCount += 1;
            PotionCountText.text = PotionCount.ToString();
        }
    }

    #endregion

    #region 조이스틱버튼 함수

    public void A_Btn()
    {
        
    }
    
    public void P_Btn()
    {
        UsePotion();
    }
    
    public void J_Btn()
    {
        
    }
    
    public void S_Btn()
    {
        
    }

    #endregion
    
    
}
