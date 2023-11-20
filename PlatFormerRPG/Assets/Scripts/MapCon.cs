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

    public void ChangePanelImage(int num) // ���� �Ǻ� ��ȣ �޾Ƽ� �̹��� ����
    {
        for (int i = 0; i < tileMap.Length; i++)
        {
            // �ش� �ε����� tileMap Ȱ��ȭ ���θ� �Ǻ��Ͽ� ����
            tileMap[i].SetActive(i == num);
        }

        // �������� �г� �̹����� ����
        panelImage.sprite = panelSprites[num];
    }
}
