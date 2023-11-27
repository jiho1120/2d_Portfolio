using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GroundMonster : Monster
{
    public float ownSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        objectStat = new Constructure.MonsterStat(DungeonManager.Instance.dungeonNum);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(MonsterMove());
        attackRate = 3f;
        timeAfterAttack = 0;
        errorMargin = 4;
        realAttack = objectStat.att;
        ownSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        LimitArea(14f);
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
                StartCoroutine(ChangeSpeed());
                timeAfterAttack = 0f;
                Debug.Log("µ¹Áø");
            }
        }
    }

    IEnumerator ChangeSpeed()
    {
        ownSpeed = speed;
        speed = 5f;
        yield return new WaitForSeconds(2f);
        speed = ownSpeed;

    }

}
