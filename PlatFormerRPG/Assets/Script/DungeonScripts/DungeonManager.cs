using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonManager : Singleton<DungeonManager>
{
    Image panelImage;
    public Sprite[] panelSprites;
    public GameObject[] tileMap;
    public GameObject[] Walls;
    public Object boss;

    public int dungeonNum { get; private set; }
    

    private void Start()
    {
        panelImage = UIManager.instance.panelImage;
        checkDungeonNum(PlayerManager.Instance.player.GetLevel());
        Debug.Log(PlayerManager.Instance.player.GetLevel()); 
        ChangePanelImage();        
        MonsterManager.instance.SetMonsterInfo(); //������ �ֵ�Ǯ�� ����� ?
        CheckGenerateCoroutine();//��ġ����
    }

    void CheckGenerateCoroutine()
    {
        if (dungeonNum <= 3)
        {
            //if (monCor == null)
            //{
            //    monCor = StartCoroutine(MonsterManager.instance.GenerateMonster());
            //}
            MonsterManager.instance.StartGenerateMonster(true);
            Walls[0].gameObject.SetActive(true);
            Walls[1].gameObject.SetActive(true);
            Walls[2].gameObject.SetActive(false);
            boss.gameObject.SetActive(false);
            UIManager.instance.BossHpSlider.gameObject.SetActive(false);

        }
        else
        {
            Walls[0].gameObject.SetActive(false);
            Walls[1].gameObject.SetActive(false);
            Walls[2].gameObject.SetActive(true);
            boss.gameObject.SetActive(true);
            UIManager.instance.BossHpSlider.gameObject.SetActive(true);

            //if (monCor != null)
            //{
            //    StopCoroutine(monCor);
            //}
            MonsterManager.instance.StartGenerateMonster(false);
            StartCoroutine(MonsterManager.instance.GenerateBullet());
        }
    }

    //public void StopCor()
    //{
    //    monCor = null;
    //}

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
