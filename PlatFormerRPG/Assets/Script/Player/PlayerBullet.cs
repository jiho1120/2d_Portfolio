using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public LayerMask isLayer;

    public float speed = 10f;
    public float distance = 0.5f;

    private void Start()
    {
        Invoke("DestroyBullet", 2);
    }

    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if(ray.collider != null)
        {
            if(ray.collider.tag == "GroundEnemy" && ray.collider.tag == "FlyEnemy")
            {
                Debug.Log("���Ϳ� ����");       //Ȯ�ο�
            }
            else if(ray.collider.tag == "Ground")
            {
                Debug.Log("���� ����");           //Ȯ�ο�
            }
            DestroyBullet();
        }

        if(transform.rotation.y == 0)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }
    }

    //���̾ ����(=>���� �� Ȱ��ȭ, ��Ȱ��ȭ�� ���� ����)
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
