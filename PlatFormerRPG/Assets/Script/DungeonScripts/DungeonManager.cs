using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonManager : Singleton<DungeonManager>
{
    public Image panelImage;
    public Sprite[] panelSprites;
    public GameObject[] tileMap;
    public GameObject[] Walls;

    public int dungeonNum { get; private set; }

    Coroutine monCor = null;

    private void Start()
    {
        checkDungeonNum(30);
        ChangePanelImage();
        CheckGenerateCoroutine();
        MonsterManager.instance.SetMonsterInfo();
    }

    void CheckGenerateCoroutine()
    {
        if (dungeonNum <= 3)
        {
            monCor = StartCoroutine(MonsterManager.instance.GenerateMonster());
            Walls[0].gameObject.SetActive(true);
            Walls[1].gameObject.SetActive(true);
            Walls[2].gameObject.SetActive(false);
        }
        else
        {
            Walls[0].gameObject.SetActive(false);
            Walls[1].gameObject.SetActive(false);
            Walls[2].gameObject.SetActive(true);
            if (monCor != null)
            {
                StopCoroutine(monCor);
            }
            StartCoroutine(MonsterManager.instance.GenerateBullet());
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
