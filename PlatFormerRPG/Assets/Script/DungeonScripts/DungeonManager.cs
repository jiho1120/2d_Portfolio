using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonManager : Singleton<DungeonManager>
{
    public Fplayer fplayer;
    public Image panelImage;
    public Sprite[] panelSprites;
    public GameObject[] tileMap;

    public int dungeonNum { get; private set; }

    // 몬스터 프리팹 생성 후 큐랑 리스트에 담기 몬스터 스탯 저장 몬스터 생성 시간 설정 후 스폰 몬스터의 스폰위치와 스프라이트 설정


    private void Start()
    {
        checkDungeonNum(30);
        ChangePanelImage();
        CheckGenerateMonsterCoroutine();
        MonsterManager.instance.SetMonsterInfo();
    }

    void CheckGenerateMonsterCoroutine()
    {
        if (dungeonNum <= 3)
        {
            StartCoroutine(MonsterManager.instance.GenerateMonster());
        }
        else
        {
            return;
        }
    }

    public void checkDungeonNum(int playerLevel)
    {
        if (playerLevel < 10)
        {
            dungeonNum = 0;
        }
        else if (playerLevel < 20)
        {
            dungeonNum = 1;
        }
        else if (playerLevel < 30)
        {
            dungeonNum = 2;
        }
        else if (playerLevel < 40)
        {
            dungeonNum = 3;
        } else
        {
            dungeonNum = 4;
        }
    }

    public void ChangePanelImage() // 레벨 판별 번호 받아서 이미지 변경
    {
        for (int i = 0; i < tileMap.Length; i++)
        {
            // 해당 인덱스의 tileMap 활성화 여부를 판별하여 설정
            tileMap[i].SetActive(i == DungeonManager.Instance.dungeonNum); // 나중에 게임 매니저 통해서 씬 로드할때 추가해야함
        }

        // 마지막에 패널 이미지를 설정
        panelImage.sprite = panelSprites[DungeonManager.Instance.dungeonNum];

    }



}
