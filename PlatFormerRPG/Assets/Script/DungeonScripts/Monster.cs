using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constructure;

public class Monster : MonoBehaviour, IHit
{
    Vector3 scale = Vector3.one;
    Vector3 vec = Vector3.right;
    Vector3 direction = Vector3.zero;

    float speedMin = 1;
    float speedMax = 2;
    float speed;
    bool isMove = false;
    bool IsLeft = true;
    bool isAttack = false;

    Rigidbody2D rigid;
    SpriteRenderer spren;
    Animator anim;
    Coroutine enemyCor = null;


    public Constructure.MonsterStat monsterStat;


    public void SetMonsterSprite(Sprite _spr) // ���� �Ŵ����� �ű�� ������ �����
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
        //Debug.Log($"{monsterStat.hP}");


        MonsterStartCoroutine();
    }

    // Update is called once per frame
    void Update()
    {
        MonsterAct();
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
    public void findPlayer()
    {
        if (Vector2.Distance (transform.position, DungeonManager.Instance.fplayer.transform.position) < 15f)
        {
            //GameManager.Instance.player.transform.position;
            //anim.SetBool("PlyerFind", playerfind);
            isAttack = true;

        }

    }
    public void AttackPlayer()
    {
        //if (PlayerStat.hP <= 0)
        //{
        //    return;
        //}

        if (isAttack)
        {
            direction = (DungeonManager.Instance.fplayer.transform.position - this.gameObject.transform.position).normalized;
            //anim.SetBool("AttackPlayer", playerfind);
            DungeonManager.Instance.fplayer.Hit(monsterStat.att, direction);
        }
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
        //�÷��̾� ����ġ += MonsterManager.Instance.monsterStat.giveExp;
        // �÷��̾� �� += MonsterManager.Instance.monsterStat.giveMoney;


        this.gameObject.SetActive(false);
    }

    
}
