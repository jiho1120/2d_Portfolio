using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object : MonoBehaviour, IHit
{
    protected Vector3 scale = Vector3.one;
    protected Vector3 vec = Vector3.right;
    protected Vector3 dir = Vector3.zero;

    public float realAttack; // 나중에 공격력에다 추가 공격력 더해서 반환하는 최종 플레이어가 입을 데미지
    protected float addAtt;
    protected float limitAreaPos;
    protected float xDifference;
    protected float yDifference;
    protected float errorMargin;

    protected float speed;
    protected bool isMove = false;
    protected bool IsLeft = true;
    protected bool boundary = false;
    protected float timeAfterAttack;
    protected float attackRate; // 공격주기

    protected Transform target;
    protected Rigidbody2D rigid;
    protected Animator anim;
    public Constructure.MonsterStat objectStat;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LimitArea(float limitAreaPos)
    {
        if (transform.position.x <= -limitAreaPos)
        {
            IsLeft = false;
        }
        else if (transform.position.x >= limitAreaPos)
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

    public virtual void Hit(float damage, Vector3 dir)
    {
        if (objectStat.hP <= 0)
        {
            return;
        }

        this.objectStat.hP = Mathf.Clamp(this.objectStat.hP - damage, 0, this.objectStat.maxHP);
        anim.SetTrigger("hit");
    }
    public float GetAtt()
    {
        return realAttack = objectStat.att * addAtt;
    }

    public void isDead()
    {
        if (this.objectStat.hP <= 0)
        {
            //PlayerManager.Instance.player.myStat.ExpVal += MonsterManager.Instance.objectStat.giveExp;
            // PlayerManager.Instance.player.myStat.money += MonsterManager.Instance.objectStat.giveMoney;
            this.gameObject.SetActive(false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dir = (this.transform.position - collision.transform.position).normalized;
            Hit(PlayerManager.Instance.player.Attak(), dir);
            Debug.Log(this.objectStat.hP);
        }
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            IsLeft = true ? false : true;
        }
    }
}
