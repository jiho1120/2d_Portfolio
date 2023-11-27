using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bulletSpeed = 1000f;
    public float bulletDamage = 5;

    Transform playerPos;
    Vector3 dir;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        dir = playerPos.position - transform.position; // 이러면 직선 공격


    }
    private void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(dir.normalized  * bulletSpeed * Time.deltaTime);
        if (gameObject != null)
        {
            Destroy(gameObject, 5f);
        }
    }

    public float GetBulletDamage() // 플레이어에서 사용
    {
        return bulletDamage;
    }
    private void FixedUpdate()
    {
        // dir = playerPos.position - transform.position; // 이러면 유도탄
    }
    //public void SetDirection(Vector3 newDirection)  // 사용안함
    //{
    //    dir = newDirection.normalized;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
