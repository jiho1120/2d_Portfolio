using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    //public LayerMask isLayer;

    public float speed = 0;
    public float distance = 0;

    Vector3 scaleVec = Vector3.one;
    Vector2 vec = Vector2.right;

    private void Start()
    {
        Invoke("DestroyBullet", 2f);
    }

    void Update()
    {
        //RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        //if (ray.collider != null)
        //{
        //    if (ray.collider.tag == "GroundEnemy" && ray.collider.tag == "FlyEnemy")
        //    {
        //        Debug.Log("���Ϳ� ����");       //Ȯ�ο�
        //    }
        //    else if (ray.collider.tag == "Ground")
        //    {
        //        Debug.Log("���� ����");           //Ȯ�ο�
        //    }
        //    DestroyBullet();
        //}

        //scaleVec.x = PlayerManager.Instance.player.transform.localScale.x;

        //if (scaleVec.x == 1)
        //{
        //    transform.Translate(vec * speed * Time.deltaTime);
        //}
        //else
        //{
        //    transform.Translate(vec * -1 * speed * Time.deltaTime);
        //}
        //transform.localScale = scaleVec;

        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    //���̾ ����(=>���� �� Ȱ��ȭ, ��Ȱ��ȭ�� ���� ����)
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        //���Ϳ� ����� �� �ǰ�
        if(collision.gameObject.CompareTag("GroundEnemy") || collision.gameObject.CompareTag("FlyEnemy") || collision.gameObject.CompareTag("Boss"))
        {
            IHit hit = collision.GetComponent<IHit>();
            if (hit !=null)
            {
                hit.Hit(PlayerManager.Instance.player.Attak(), transform.position);
            }
            
            Debug.Log("���Ϳ� ����");       //Ȯ�ο�
            DestroyBullet();
        }
        //���� ����� �� ����
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("���� ����");           //Ȯ�ο�
            DestroyBullet();
        }
    }
}
