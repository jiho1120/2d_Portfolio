using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FlyMonster : Monster
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        monsterStat = new Constructure.MonsterStat(DungeonManager.Instance.dungeonNum);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(MonsterMove());
        attackRate = 3f;
        timeAfterAttack = 0;
        errorMargin = 3;


    }

    // Update is called once per frame
    void Update()
    {
        basicMove();
        Boundary();
        timeAfterAttack += Time.deltaTime;
        Attack();
        isDead();

    }


    public override void Attack()
    {
        base.Attack();
        if (boundary)
        {
            if (timeAfterAttack >= attackRate)
            {
                timeAfterAttack = 0f;

                GameObject monsterBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

            }
        }
    }

}
