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

    public void ChangePanelImage() // 레벨 판별 번호 받아서 이미지 변경
    {
        for (int i = 0; i < Tilemaps.Length; i++)
        {
            // 해당 인덱스의 tileMap 활성화 여부를 판별하여 설정
            Tilemaps[i].SetActive(i == DungeonManager.Instance.dungeonNum); // 나중에 게임 매니저 통해서 씬 로드할때 추가해야함
        }             
    }
}
