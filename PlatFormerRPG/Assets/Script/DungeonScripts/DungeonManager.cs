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

    // ���� ������ ���� �� ť�� ����Ʈ�� ��� ���� ���� ���� ���� ���� �ð� ���� �� ���� ������ ������ġ�� ��������Ʈ ����


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
