using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //���� ����� ��
        if (collider.gameObject.CompareTag("Ground"))
        {

            PlayerManager.Instance.player.jumpCount = 0;
            PlayerManager.Instance.player.rigid.velocity = Vector2.zero;      //�̲�������
        }
    }
}
