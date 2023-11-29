using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScript : MonoBehaviour
{
    public GameObject[] Tilemaps;    
    public GameObject[] Walls;    

    public void CheckGenerateCoroutine()
    {
        if (DungeonManager.Instance.dungeonNum <= 3)
        {
            MonsterManager.Instance.StartGenerateMonster(true);
            Walls[0].gameObject.SetActive(true);
            Walls[1].gameObject.SetActive(true);
            Walls[2].gameObject.SetActive(true);            
            UIManager.Instance.BossHpSlider.gameObject.SetActive(false);

        }
        else
        {
            Walls[0].gameObject.SetActive(false);
            Walls[1].gameObject.SetActive(false);
            Walls[2].gameObject.SetActive(false);
            Walls[3].gameObject.SetActive(true);
            UIManager.Instance.BossHpSlider.gameObject.SetActive(true);
            MonsterManager.Instance.StartGenerateMonster(false);
            StartCoroutine(MonsterManager.Instance.GenerateBullet());
        }
    }

    public void ChangePanelImage() // ���� �Ǻ� ��ȣ �޾Ƽ� �̹��� ����
    {
        for (int i = 0; i < Tilemaps.Length; i++)
        {
            // �ش� �ε����� tileMap Ȱ��ȭ ���θ� �Ǻ��Ͽ� ����
            Tilemaps[i].SetActive(i == DungeonManager.Instance.dungeonNum); // ���߿� ���� �Ŵ��� ���ؼ� �� �ε��Ҷ� �߰��ؾ���
        }             
    }
}
