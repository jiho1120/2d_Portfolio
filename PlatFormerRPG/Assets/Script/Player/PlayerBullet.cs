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
                Debug.Log("몬스터에 맞음");       //확인용
            }
            else if(ray.collider.tag == "Ground")
            {
                Debug.Log("땅에 맞음");           //확인용
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

    //파이어볼 삭제(=>생성 후 활성화, 비활성화로 변경 예정)
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
