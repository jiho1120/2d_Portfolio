using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FlyMonster : Monster
{
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        monsterStat = new Constructure.MonsterStat(DungeonManager.Instance.dungeonNum);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(MonsterMove());

    }

    // Update is called once per frame
    void Update()
    {
        basicMove();
        Boundary();
        Attack();
        isDead();
    }


    public override void Attack()
    {
        base.Attack();
        if (boundary == true)
        {
            Debug.Log("¤¼¤¼");
        }
    }
}
