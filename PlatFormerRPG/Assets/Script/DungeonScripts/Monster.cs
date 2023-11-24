using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constructure;

public class Monster : MonoBehaviour, IHit
{
    public Player player;  // ���� �Ŵ����� �÷��̾�� ��ü�ؾ���

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
        //        Debug.Log("�߰�");
        //        findPlayer = true;
        //    }

        if (Vector3.Distance(player.transform.position, this.transform.position) < 5)
        {
            Debug.Log("�߰�");
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
        Debug.Log("����");

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
            //�÷��̾� ����ġ += MonsterManager.Instance.monsterStat.giveExp;
            // �÷��̾� �� += MonsterManager.Instance.monsterStat.giveMoney;

            this.gameObject.SetActive(false);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player")) // �÷��̾� ����� �ٲ����
        {
            dir = this.transform.position - player.transform.position;
            anim.SetTrigger("hit");
            Hit(20, dir);
            Debug.Log(this.monsterStat.hP);
        }
    }



}
