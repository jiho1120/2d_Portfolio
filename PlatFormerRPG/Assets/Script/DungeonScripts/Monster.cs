using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constructure;

public class Monster : MonoBehaviour, IHit
{
    public Player player;  // 게임 매니저의 플레이어로 교체해야함

    Vector3 scale = Vector3.one;
    Vector3 vec = Vector3.right;
    Vector3 dir = Vector3.zero;

    float speedMin = 1;
    float speedMax = 2;
    float speed;
    bool isMove = false;
    bool IsLeft = true;

    Rigidbody2D rigid;
    SpriteRenderer spren;
    Animator anim;
    Coroutine enemyCor = null;


    public Constructure.MonsterStat monsterStat;


    public void SetMonsterSprite(Sprite _spr) // 몬스터 매니저로 옮기고 싶은데 힘들다
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
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        monsterStat = new Constructure.MonsterStat(DungeonManager.Instance.dungeonNum);

        Invoke("FindPlayer", 2f);
        MonsterStartCoroutine();
    }

    // Update is called once per frame
    void Update()
    {
        MonsterAct();
        isDead();
    }

    public void MonsterStartCoroutine()
    {
        enemyCor = StartCoroutine(MonsterMove());
    }

    protected virtual void MonsterAct()
    {
        basicMove();

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

    IEnumerator MonsterMove()
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
    public void FindPlayer()
    {
        //    //GameManager.Instance.player.transform.position;
        //    //anim.SetBool("PlyerFind", playerfind);
        //    if (Vector3.Distance(player.transform.position, this.transform.position) < 5)
        //    {
        //        Debug.Log("발견");
        //        findPlayer = true;
        //    }

        if (Vector3.Distance(player.transform.position, this.transform.position) < 5)
        {
            Debug.Log("발견");
            AttackPlayer();
        }
    }

   

    public void AttackPlayer()
    {
        //if (PlayerStat.hP <= 0)
        //{
        //    return;
        //}

        //anim.SetBool("AttackPlayer", playerfind);
        Debug.Log("공격");

    }

    public void Hit(float damage, Vector3 dir)
    {
        if (monsterStat.hP <= 0)
        {
            return;
        }

        this.monsterStat.hP = Mathf.Clamp(this.monsterStat.hP - damage, 0, this.monsterStat.maxHP);
        //slider.value = this.monsterStat.hP;
        //anim.SetTrigger("Hit");
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
            //플레이어 경험치 += MonsterManager.Instance.monsterStat.giveExp;
            // 플레이어 돈 += MonsterManager.Instance.monsterStat.giveMoney;

            this.gameObject.SetActive(false);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player")) // 플레이어 무기로 바꿔야함
        {
            dir = this.transform.position - player.transform.position;
            anim.SetTrigger("hit");
            Hit(20, dir);
            Debug.Log(this.monsterStat.hP);
        }
    }



}
