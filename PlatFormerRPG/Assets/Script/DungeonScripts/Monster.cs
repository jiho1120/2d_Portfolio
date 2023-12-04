using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static Constructure;
using static UnityEngine.GraphicsBuffer;

public class Monster : Object
{
    public float knockBack = 1;
    float speedMin = 1;
    float speedMax = 2;
    protected SpriteRenderer spren;
    protected Coroutine enemyCor = null;

    public void SetMonsterSprite(Sprite _spr)
    {
        if (spren == null)
        {
            spren = this.transform.GetComponent<SpriteRenderer>();
        }
        spren.sprite = _spr;
        speed = Random.Range(speedMin, speedMax);
    }

    // void Start()
    // {
    // }
    //
    // // Update is called once per frame
    // void Update()
    // {
    // }

    public void basicMove()
    {
        if (isMove)
        {
            scale.x = (IsLeft ? -1f : 1f);
            transform.localScale = scale;
            transform.Translate(vec * speed * (IsLeft ? -1 : 1) * Time.fixedDeltaTime);
        }
    }


    public IEnumerator MonsterMove()
    {
        while (true)
        {
            isMove = true;
            anim.SetBool("isMove", isMove);
            anim.SetBool("isLeft", IsLeft);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            isMove = false;
            IsLeft = Random.Range(0, 2) == 0 ? true : false;
            anim.SetBool("isMove", isMove);
            anim.SetBool("isLeft", IsLeft);
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        }
    }




    public virtual void Attack() 
    {
    }
    public override void Hit(float damage, Vector3 dir)
    {
        base.Hit(damage, dir);
        rigid.AddForce(dir, ForceMode2D.Impulse);
    }

    
}
