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

    public void ChangePanelImage() // ���� �Ǻ� ��ȣ �޾Ƽ� �̹��� ����
    {
        for (int i = 0; i < tileMap.Length; i++)
        {
            // �ش� �ε����� tileMap Ȱ��ȭ ���θ� �Ǻ��Ͽ� ����
            tileMap[i].SetActive(i == DungeonManager.Instance.dungeonNum); // ���߿� ���� �Ŵ��� ���ؼ� �� �ε��Ҷ� �߰��ؾ���
        }

        // �������� �г� �̹����� ����
        panelImage.sprite = panelSprites[DungeonManager.Instance.dungeonNum];

    }
}
