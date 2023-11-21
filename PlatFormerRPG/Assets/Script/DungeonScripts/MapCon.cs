using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCon : MonoBehaviour
{
    public Image panelImage;
    public Sprite[] panelSprites;
    public GameObject[] tileMap;

    

    // Start is called before the first frame update
    void Start()
    {
        ChangePanelImage();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangePanelImage() // ���� �Ǻ� ��ȣ �޾Ƽ� �̹��� ����
    {
        for (int i = 0; i < tileMap.Length; i++)
        {
            // �ش� �ε����� tileMap Ȱ��ȭ ���θ� �Ǻ��Ͽ� ����
            tileMap[i].SetActive(i == DungeonManager.Instance.dungeonNum);
        }

        // �������� �г� �̹����� ����
        panelImage.sprite = panelSprites[DungeonManager.Instance.dungeonNum];
        Debug.Log(DungeonManager.Instance.dungeonNum);

    }
}
