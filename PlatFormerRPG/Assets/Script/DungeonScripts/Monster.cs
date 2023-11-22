using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Vector3 scale = Vector3.one;
    Vector3 vec = Vector3.right;

    float speedMin = 1;
    float speedMax = 2;
    float speed;
    bool isMove = false;
    bool IsLeft = true;

    Rigidbody2D rigid;
    SpriteRenderer spren;
    Animator anim;
    Coroutine enemyCor = null;

    

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        MonsterStartCoroutine();
    }

    


    // Update is called once per frame
    void Update()
    {
        MonsterAct();
    }

    public void MonsterStartCoroutine()
    {
        enemyCor = StartCoroutine(EnemyMove());
    }

    protected virtual void MonsterAct()
    {
        basicMove();
    }

    public void SetMonsterSprite(Sprite _spr)
    {
        if (spren == null)
        {
            spren = this.transform.GetComponent<SpriteRenderer>();
        }
        spren.sprite = _spr;
        speed = Random.Range(speedMin, speedMax);
    }

    void basicMove()
    {
        if (isMove)
        {
            scale.x = (IsLeft ? -1 : 1);
            transform.localScale = scale;
            transform.Translate(vec * speed * (IsLeft ? -1 : 1) * Time.deltaTime);

            if (transform.position.x <= -14)
            {
                IsLeft = false;
            }
            else if (transform.position.x >= 14)
            {
                IsLeft = true;
            }
        }
    }

    IEnumerator EnemyMove()
    {
        while (true)
        {
            isMove = true;
            anim.SetBool("isMove", isMove);
            anim.SetBool("isLeft", IsLeft);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            isMove = false;
            anim.SetBool("isMove", isMove);
            IsLeft = Random.Range(0, 2) == 0 ? true : false;
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        }
    }
    

}
