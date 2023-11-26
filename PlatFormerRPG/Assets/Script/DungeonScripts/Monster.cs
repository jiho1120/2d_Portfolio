using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static Constructure;
using static UnityEngine.GraphicsBuffer;

public class Monster : MonoBehaviour, IHit
{
    protected Vector3 scale = Vector3.one;
    Vector3 vec = Vector3.right;
    Vector3 dir = Vector3.zero;

    public float knockBack = 1;
    public float realAttack; // 나중에 공격력에다 추가 공격력 더해서 반환하는 최종 플레이어가 입을 데미지
    float speedMin = 1;
    float speedMax = 2;
    float xDifference;
    float yDifference;

    protected float speed;
    protected bool isMove = false;
    protected bool IsLeft = true;
    protected bool boundary = false;
    protected float errorMargin;
    protected float timeAfterAttack;
    protected float attackRate; // 공격주기

    protected Transform target;
    protected Rigidbody2D rigid;
    protected SpriteRenderer spren;
    protected Animator anim;
    protected Coroutine enemyCor = null;
    public Constructure.MonsterStat monsterStat;

    public void SetMonsterSprite(Sprite _spr)
    {
        if (spren == null)
        {
            spren = this.transform.GetComponent<SpriteRenderer>();
        }
        spren.sprite = _spr;
        speed = Random.Range(speedMin, speedMax);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

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
            anim.SetBool("isLeft", isMove);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            isMove = false;
            anim.SetBool("isMove", isMove);
            anim.SetBool("isLeft", isMove);
            IsLeft = Random.Range(0, 2) == 0 ? true : false;
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        }
    }

    public void LimitArea()
    {
        if (transform.position.x <= -14)
        {
            IsLeft = false;
        }
        else if (transform.position.x >= 14)
        {
            IsLeft = true;
        }
    }

    public void Boundary()
    {
        xDifference = Mathf.Abs(PlayerManager.Instance.GetPlayerPosition().x - this.transform.position.x);
        yDifference = Mathf.Abs(PlayerManager.Instance.GetPlayerPosition().y - this.transform.position.y);

        if (xDifference < errorMargin && yDifference < errorMargin)
        {
            boundary = true;
        }
        else
        {
            boundary = false;
        }
    }


    public virtual void Attack() 
    {
    }
    public void Hit(float damage, Vector3 dir)
    {
        if (monsterStat.hP <= 0)
        {
            return;
        }

        this.monsterStat.hP = Mathf.Clamp(this.monsterStat.hP - damage, 0, this.monsterStat.maxHP);
        anim.SetTrigger("hit");
        rigid.AddForce(dir, ForceMode2D.Impulse);
    }
    public float GetAtt()
    {
        return monsterStat.att;
    }

    public void isDead()
    {
        if (this.monsterStat.hP <= 0)
        {
            //PlayerManager.Instance.player.myStat.ExpVal += MonsterManager.Instance.monsterStat.giveExp;
            // PlayerManager.Instance.player.myStat.money += MonsterManager.Instance.monsterStat.giveMoney;
            this.gameObject.SetActive(false);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dir = (this.transform.position - collision.transform.position).normalized;
            Hit(20, dir);
            Debug.Log(this.monsterStat.hP);
        }
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            IsLeft = true ? false : true; 
        }
    }
}
