using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fplayer : MonoBehaviour, IHit
{
    public Constructure.MonsterStat PlayerStat;
    Rigidbody2D rigid;

    public void Hit(float damage, Vector3 dir)
    {
        if (PlayerStat.hP <= 0)
        {
            return;
        }

        this.PlayerStat.hP = Mathf.Clamp(this.PlayerStat.hP - damage, 0, this.PlayerStat.maxHP);
        //slider.value = this.monsterStat.hP;
        //anim.SetTrigger("Hit");
        rigid.AddForce(dir, ForceMode2D.Impulse);
    }
    public float GetAtt()
    {
        return PlayerStat.att;
    }



    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
