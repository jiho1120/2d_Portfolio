using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCon : MonoBehaviour
{
    public int num = 1;
    public Image panelImage;
    public Sprite[] panelSprites;
    public GameObject[] tileMap;



    // Start is called before the first frame update
    void Start()
    {
        ChangePanelImage(num);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangePanelImage(int num) // 레벨 판별 번호 받아서 이미지 변경
    {
        for (int i = 0; i < tileMap.Length; i++)
        {
            // 해당 인덱스의 tileMap 활성화 여부를 판별하여 설정
            tileMap[i].SetActive(i == num);
        }

        // 마지막에 패널 이미지를 설정
        panelImage.sprite = panelSprites[num];
    }
}
