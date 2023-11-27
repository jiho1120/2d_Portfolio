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
        dir = playerPos.position - transform.position; // �̷��� ���� ����


    }
    private void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(dir.normalized  * bulletSpeed * Time.deltaTime);
        if (gameObject != null)
        {
            Destroy(gameObject, 5f);
        }
    }

    public float GetBulletDamage() // �÷��̾�� ���
    {
        return bulletDamage;
    }
    private void FixedUpdate()
    {
        // dir = playerPos.position - transform.position; // �̷��� ����ź
    }
    //public void SetDirection(Vector3 newDirection)  // ������
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
