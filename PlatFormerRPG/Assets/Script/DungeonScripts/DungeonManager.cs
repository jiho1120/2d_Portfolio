using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonManager : Singleton<DungeonManager>
{
    public Image panelImage;
    public Sprite[] panelSprites;
    public GameObject[] tileMap;

    public int dungeonNum { get; private set; }

    

    private void Start()
    {
        checkDungeonNum(30);
        ChangePanelImage();

        StartCoroutine(SpawnManager.instance.GenerateMonster());
        MonsterManager.instance.SetMonsterInfo();
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
        else
        {
            dungeonNum = 3;
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
