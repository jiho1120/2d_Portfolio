using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    /*
     * 사용 이유 : UI의 버튼과 캐릭터 바를 관리한다.
     * 싱글톤을 사용함으로서 맵이동이 되어도 현재 상태가 씬이 바뀌어도 지속되도록 하려고 한다.
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
    public GameObject InPotalBtn;
    public GameObject InPotalVillBtn;
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

    #region GameObject
    //public Camera cam;
    //public Image[] coolImg;       //쿨타임 이미지
    public Slider hpSlider;         //HP Bar
    public Slider expSlider;        //EXP Bar
    public Text StateBtn_levelTxt;  //Level Txt
    public GameObject TypeW;
    public GameObject TypeD;
    public GameObject UiScript;
    public Slider BossHpSlider;
    public int PotionCount = 0;
    public Text PotionCountText;
    public Text GoldText;
    public int TotalGold = 0;
    public int UseGold = 5000;
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
    public FixedJoystick joystick;
    public GameObject WinPopup_black;
    public GameObject WinPopup_White;
    int scene = 2;
    public GameObject DeadPopup;
    public Constructure.MonsterStat objectStat;
    public float delayInSeconds = 2f;
    //Scene dungeon;
    public GameObject SkillBtn;
    public Text Name;
    public Text MyGold;
    #endregion

    #region Start

    private void Start()
    {
        // 초기 경험치는 0이다.
        PlayerManager.Instance.player.myStat.ExpVal = 0;
        //dungeon = SceneManager.GetSceneByName("Dungeon");
        // 최대채력을 슬라이더바에 설정
        hpSlider.maxValue = PlayerManager.Instance.player.myStat.MaxHP;
        // 레벨업에 필요한 경험치를 슬라이더바에 설정
        expSlider.maxValue = PlayerManager.Instance.player.myStat.MaxExpVal;
        // 포션 갯수
        PotionCount += 3;
        // 문자열 전환
        PotionCountText.text = PotionCount.ToString();
        // 정보창에 보일 현재 레벨
        PopupLevelText.text = PlayerManager.Instance.player.myStat.Level.ToString();
        // 정보창에 보일 현재 공격력
        PopupAttText.text = PlayerManager.Instance.player.myStat.Att.ToString();
        // 현재 경험치
        PopupExpText.text = PlayerManager.Instance.player.myStat.ExpVal.ToString();
        // 정보창에 보일 현재 경험치
        PopupMaxExpText.text = PlayerManager.Instance.player.myStat.MaxExpVal.ToString();
        // 정보창에 보일 이름
        PopupNameText.text = PopupNameText_string;
        // 정보창에 보일 직업
        PopupTypeText.text = PopupTypeText_string;
        // 메인창 레벨
        StateBtn_levelTxt.text = PlayerManager.Instance.player.myStat.Level.ToString();
        // 보유골드
        MyGold.text = PlayerManager.Instance.player.myStat.Money.ToString();
        // 이름
        Name.text = PopupNameText_string;
        // 사용가능한 능력치 카운트
        // AddStatCount.text = PlayerManager.Instance.AddStatCount.ToString();
        InPotalBtn.SetActive(false);
        InPotalVillBtn.SetActive(false);
        WinPopup_black.SetActive(false);
        WinPopup_White.SetActive(false);
        DeadPopup.SetActive(false);
        objectStat.hP = objectStat.maxHP * 0.4f;
        
    }

    #endregion

    #region Update

    private void Update()
    {
        expSlider.value = PlayerManager.Instance.player.myStat.ExpVal;
        hpSlider.value = PlayerManager.Instance.player.myStat.HP;
        MyGold.text = PlayerManager.Instance.player.myStat.Money.ToString();


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
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.W)))
        {
            if (PlayerManager.Instance.InPotal == true)
            {
                if (scene == 2)
                {
                    OnDungeon();
                }
                else if(scene == 3) 
                {
                    OnVillagePotal();
                }
                Debug.Log("이동");
                
            }
            else if (PlayerManager.Instance.InPotal == false)
            {
                Debug.Log("이동 할 수 없는 상태 입니다");
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            objectStat.hP = 0;
            Debug.Log("보스의 채력을 강제로 채력 0");
        }
        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            PlayerManager.Instance.player.myStat.HP = 0;
            Debug.Log("플레이어의 채력을 강제로 채력 0");
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            TotalGold += 10000;
            Debug.Log("강제로 보유금액 10000으로 증가");
        }


        if (Input.GetKeyDown(KeyCode.F4))
        {
            PlayerManager.Instance.player.myStat.ExpVal += 50;
            Debug.Log("경험치를 강제로 50%씩 증가시킴");
            if (PlayerManager.Instance.player.myStat.ExpVal == 100)
            {
                Debug.Log("레벨업!");
                expSlider.value = 100;
                LevelUp();
            }
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            objectStat.hP = 0;
            Debug.Log("보스의 채력을 강제로 채력 0");
        }

        if (PlayerManager.Instance.player.myStat.MaxExpVal <= PlayerManager.Instance.player.myStat.ExpVal)
        {
            LevelUp();
        }

        if (PlayerManager.Instance.InPotal == true)
        {
            InPotalBtn.SetActive(true);
        }
        else
        {
            InPotalBtn.SetActive(false);
        }

        if (PlayerManager.Instance.player.myStat.HP == 0)
        {
            DeadPopup.SetActive(true);
            PlayerManager.Instance.player.myStat.HP += 100;
            BossHpSlider.gameObject.SetActive(false);
            Invoke("DeadAfterDelay", delayInSeconds);
            OnVillPotal();
            
        }

        if (BossHpSlider.value == 0)
        {
            BossHpSlider.gameObject.SetActive(false);
            StartCoroutine(AlternatePopups());
            
        }
    }

    #endregion
    
    
    IEnumerator AlternatePopups()
    {
        while (true)
        {
            // 첫 번째 팝업을 활성화하고, 두 번째는 비활성화
            WinPopup_black.SetActive(true);
            WinPopup_White.SetActive(false);

            // 2초 기다린 후
            yield return new WaitForSeconds(4f);

            // 두 번째 팝업을 활성화하고, 첫 번째는 비활성화
            WinPopup_black.SetActive(false);
            WinPopup_White.SetActive(true);

            // 2초 기다린 후
            yield return new WaitForSeconds(4f);
            QuestCanvas.SetActive(false);
            QuestOpenCanvas.SetActive(false);
            DunGeonCanvas.SetActive(false);
            InfoBtnCanvas.SetActive(false);
            OnShopCanvas.SetActive(false);
            OffShopCanvas.SetActive(false);
            OffUiScript();
            SceneManager.LoadScene("FirstScene");
            
            
        }
    }

    #region 레벨업을 했을경우

    public void LevelUp()
    {
        PlayerManager.Instance.player.myStat.Level += 1;
        PlayerManager.Instance.AddStatCount += 2;
        PlayerManager.Instance.player.myStat.Att += 5;
        PlayerManager.Instance.player.myStat.MaxHP += 100;
        PlayerManager.Instance.player.myStat.HP += 100;
        PlayerManager.Instance.player.myStat.Skill += 10;
        Debug.Log("레벨 : "+PlayerManager.Instance.player.myStat.Level);
        Debug.Log("습득 능력치 카운트 : "+PlayerManager.Instance.AddStatCount);
        PotionCountText.text = PotionCount.ToString();
        PopupLevelText.text = PlayerManager.Instance.player.myStat.Level.ToString();
        PopupAttText.text = PlayerManager.Instance.player.myStat.Att.ToString();
        PopupExpText.text = PlayerManager.Instance.player.myStat.ExpVal.ToString();
        PopupMaxExpText.text = PlayerManager.Instance.player.myStat.MaxExpVal.ToString();
        PopupNameText.text = PopupNameText_string;
        PopupTypeText.text = PopupTypeText_string;
        StateBtn_levelTxt.text = PlayerManager.Instance.player.myStat.Level.ToString();
        PlayerManager.Instance.player.myStat.ExpVal = 0;
        PlayerManager.Instance.player.myStat.MaxExpVal += 10;
        Debug.Log(PlayerManager.Instance.player.myStat.MaxExpVal);
        // AddStatCount.text = PlayerManager.Instance.AddStatCount.ToString();
    }

    #endregion
    
    
    void DeadAfterDelay()
    {
        DeadPopup.SetActive(false);
        
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
        InPotalVillBtn.SetActive(true);
        InPotalBtn.SetActive(false);
        scene = 3;
        // DungeonManager.Instance.gameObject.SetActive(true);
        SceneManager.LoadScene("Dungeon");
        QuestCanvas.SetActive(false);
        QuestOpenCanvas.SetActive(false);
        DunGeonCanvas.SetActive(false);
        InfoBtnCanvas.SetActive(false);
        OnShopCanvas.SetActive(false);
        OffShopCanvas.SetActive(false);
        PlayerManager.Instance.WarriorPlayer.transform.Translate(-640, -355,0);
        PlayerManager.Instance.WizaldPlayer.transform.Translate(-640, -355,0);
        
        StartCoroutine(CheckDungeon());
    }

    IEnumerator CheckDungeon()
    {        
        yield return new WaitUntil(()=> SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Dungeon"));        

        while (DungeonManager.instance ==null)
        {
            yield return null;
        }

        DungeonManager.instance.StartDungeon();
    }

    public void OnVillagePotal()
    {
        scene = 2;
        InPotalBtn.SetActive(true); 
        InPotalVillBtn.SetActive(false);
        MonsterManager.instance.StartGenerateMonster(false);
        MonsterManager.instance.AllkillMonster();
        SceneManager.LoadScene("VillageScene");
        QuestCanvas.SetActive(true);
        QuestOpenCanvas.SetActive(true);
        DunGeonCanvas.SetActive(true);
        InfoBtnCanvas.SetActive(true);
        OnShopCanvas.SetActive(true);
        OffShopCanvas.SetActive(true);
        PlayerManager.Instance.WarriorPlayer.transform.Translate(-640, -355,0);
        PlayerManager.Instance.WizaldPlayer.transform.Translate(-640, -355,0);
        //StopCoroutine(DungeonManager.instance.monCor);
        //StopCoroutine(MonsterManager.instance.GenerateMonster());        
        //DungeonManager.instance.monCor = null;
        
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

    public void OnPotal()
    {
        
        InPotalVillBtn.SetActive(true);
        InPotalBtn.SetActive(false);
        OnDungeon();
    }

    public void OnVillPotal()
    {
        InPotalBtn.SetActive(true); 
        InPotalVillBtn.SetActive(false);
        OnVillagePotal();
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
        
        if (PotionCount <= 0 || PlayerManager.Instance.player.myStat.HP == 100)
        {
            PotionCount -= 0;
            PotionCountText.text = PotionCount.ToString();
            hpSlider.value -= 0;
        }
        else
        {
            PlayerManager.Instance.player.myStat.HP = 100;
            PotionCount -= 1;
            PotionCountText.text = PotionCount.ToString();
        }
    }
    public void GetPotion()
    {
        if (PlayerManager.Instance.player.myStat.Money < UseGold)
        {
            PotionCount += 0;
            PotionCountText.text = PotionCount.ToString();
        }
        else
        {
            PlayerManager.Instance.player.myStat.Money -= UseGold;
            PotionCount += 1;
            PotionCountText.text = PotionCount.ToString();
        }
    }
    #endregion

    #region 조이스틱버튼 함수

    public void A_Btn()
    {
        PlayerManager.instance.player.PlayerAtt();
    }
    
    public void P_Btn()
    {
        UsePotion();
    }
    
    public void J_Btn()
    {
        PlayerManager.instance.player.JumpMove();
        PlayerManager.Instance.player.JumpSound.PlayOneShot(PlayerManager.Instance.player.JumpClip);
    }
    
    private void StartCooldown(float cooldownDuration)
    {
        // 쿨다운 중임을 표시
        isCooldown = true;

        // 쿨다운 타이머 설정
        cooldownTimer = cooldownDuration;

        // 쿨다운 타이머를 감소시키는 코루틴 시작
        StartCoroutine(CooldownTimer());
    }
    
    public void S_Btn()
    {
        if (!isCooldown)
        {
            // 기능을 실행
            PlayerManager.instance.player.PlayerSkill();
            SkillBtn.SetActive(false);
            

            // 쿨다운 타이머 시작 (3초)
            StartCooldown(3f);
        }
        else
        {
            Debug.Log("cooldown.");
        }
        
    }
    
    private float cooldownTimer = 0f;
    private bool isCooldown = false;
    
    private IEnumerator CooldownTimer()
    {
        while (cooldownTimer > 0)
        {
            // 1초씩 감소
            cooldownTimer -= Time.deltaTime;

            // 대기
            yield return null;
        }

        // 쿨다운이 종료되면 상태 초기화
        isCooldown = false;
        SkillBtn.SetActive(true);
    }

    #endregion
    
    
}
