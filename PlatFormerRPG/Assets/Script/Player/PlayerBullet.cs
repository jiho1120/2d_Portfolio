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
        //        Debug.Log("몬스터에 맞음");       //확인용
        //    }
        //    else if(ray.collider.tag == "Ground")
        //    {
        //        Debug.Log("땅에 맞음");           //확인용
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

    //파이어볼 삭제(=>생성 후 활성화, 비활성화로 변경 예정)
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("GroundEnemy") && collision.gameObject.CompareTag("FlyEnemy"))
        {
            Debug.Log("몬스터에 맞음");       //확인용
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("땅에 맞음");           //확인용
        }
    }
}
