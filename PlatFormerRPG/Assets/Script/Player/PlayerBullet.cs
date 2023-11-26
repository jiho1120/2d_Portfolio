using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    //public LayerMask isLayer;

    public float speed = 10f;
    public float distance = 0.5f;

    Vector3 scaleVec = Vector3.one;

    private void Start()
    {
        Invoke("DestroyBullet", 0.5f);
    }

    void Update()
    {
        //RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        //if(ray.collider != null)
        //{
        //    if(ray.collider.tag == "GroundEnemy" && ray.collider.tag == "FlyEnemy")
        //    {
        //        Debug.Log("���Ϳ� ����");       //Ȯ�ο�
        //    }
        //    else if(ray.collider.tag == "Ground")
        //    {
        //        Debug.Log("���� ����");           //Ȯ�ο�
        //    }
        //    DestroyBullet();
        //}
        scaleVec.x = transform.localScale.x;

        if(scaleVec.x == 1)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }
        transform.localScale = scaleVec;
    }

    //���̾ ����(=>���� �� Ȱ��ȭ, ��Ȱ��ȭ�� ���� ����)
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("GroundEnemy") && collision.gameObject.CompareTag("FlyEnemy"))
        {
            Debug.Log("���Ϳ� ����");       //Ȯ�ο�
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("���� ����");           //Ȯ�ο�
        }
    }
}
