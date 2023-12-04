using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonManager : Singleton<DungeonManager>
{
    Image panelImage;
    public Sprite[] panelSprites;
    

    public GameObject bossPrefab;
    public Boss boss;


    public int dungeonNum { get; private set; }
    DungeonScript dgscript;

    public void StartDungeon()
    {
        dgscript = GameObject.Find("DungeonScript").GetComponent<DungeonScript>();
        MonsterManager.instance.InitSpawn();

        panelImage = UIManager.instance.panelImage;
        checkDungeonNum(PlayerManager.Instance.player.GetLevel());                

        ChangePanelImage();
        MonsterManager.instance.SetMonsterInfo(); //������ �ֵ�Ǯ�� ����� ?
        dgscript.CheckGenerateCoroutine();//��ġ����

        if (boss == null)
        {
            boss = Instantiate(bossPrefab, Vector3.zero, Quaternion.identity).GetComponent<Boss>();
        }

        if (dungeonNum >3)
        {

            boss.gameObject.SetActive(true);
        }
        else
        {
            boss.gameObject.SetActive(false);

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
        Debug.Log("dungeonNum : "+dungeonNum);
    }

    public void ChangePanelImage() // ���� �Ǻ� ��ȣ �޾Ƽ� �̹��� ����
    {
        if (dgscript !=null)
        {
            dgscript.ChangePanelImage();
        }       
        // �������� �г� �̹����� ����
        panelImage.sprite = panelSprites[dungeonNum];

    }
}
