using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    float bulletSpeed;

    Transform playerPos;
    Vector3 dir;

    private void Start()
    {
        bulletSpeed = 50000f;
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        dir = playerPos.position - transform.position;
        GetComponent<Rigidbody2D>().AddForce(dir.normalized * Time.deltaTime* bulletSpeed);
        if (gameObject != null)
        {
            Destroy(gameObject, 5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
