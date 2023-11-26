using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GroundMonster : Monster
{
    protected float followspeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        monsterStat = new Constructure.MonsterStat(DungeonManager.Instance.dungeonNum);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(MonsterMove());
        attackRate = 3f;
        timeAfterAttack = 0;
        errorMargin = 4;


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

                Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, followspeed * Time.deltaTime);
                Debug.Log("µ¹Áø");

            }
        }
    }
}
