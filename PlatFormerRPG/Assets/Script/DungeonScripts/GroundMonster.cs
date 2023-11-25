using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GroundMonster : Monster
{
    // Start is called before the first frame update
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
        if (boundary)
        {
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followspeed * Time.deltaTime);
            Debug.Log("µ¹Áø");
        }
    }
}
