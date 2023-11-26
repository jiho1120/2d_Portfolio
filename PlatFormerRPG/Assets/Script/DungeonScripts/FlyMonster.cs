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
        realAttack = monsterStat.att;
        errorMargin = 3;
    }

    // Update is called once per frame
    void Update()
    {
        LimitArea();
        Boundary();
        timeAfterAttack += Time.deltaTime;
        Attack();
        isDead();

    }

    private void FixedUpdate()
    {
        basicMove();
    }

    public override void Attack()
    {
        base.Attack();
        if (boundary)
        {
            if (timeAfterAttack >= attackRate)
            {
                anim.SetTrigger("attack");
                timeAfterAttack = 0f;
                GameObject monsterBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            }
        }
    }

}
