using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class MapCon : MonoBehaviour
{
    public Image panelImage;
    public Sprite[] panelSprites;
    public GameObject[] tileMap;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChangePanelImage();
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
