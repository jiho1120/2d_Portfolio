using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMonster : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        MonsterStartCoroutine();
    }

    // Update is called once per frame
    void Update()
    {
        MonsterAct();
    }

    protected override void MonsterAct()
    {
        base.MonsterAct();
    }
}
