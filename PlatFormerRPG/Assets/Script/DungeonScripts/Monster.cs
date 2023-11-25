using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Constructure;
using static UnityEngine.GraphicsBuffer;

public class Monster : MonoBehaviour, IHit
{
    Vector3 scale = Vector3.one;
    Vector3 vec = Vector3.right;

    float speedMin = 1;
    float speedMax = 2;
    float speed;
    float followspeed = 1f;
    bool isMove = false;
    bool IsLeft = true;
    bool findPlayer = false;

    Transform target;

    Rigidbody2D rigid;
    SpriteRenderer spren;
    Animator anim;
    Coroutine enemyCor = null;

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
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        monsterStat = new Constructure.MonsterStat(DungeonManager.Instance.dungeonNum);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


        MonsterStartCoroutine();
    }

    // Update is called once per frame
    void Update()
    {
        AttackPlayer();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            findPlayer = true;
            Debug.Log("찾음");
        }
        else
        {
            findPlayer = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            findPlayer = false;
            Debug.Log("놓침");

        }
        else
        {
            findPlayer = true;
        }

    }
    
    public void AttackPlayer()
    {
        //if (PlayerManager.Instance.player.myStat.HP <= 0)
        //{
        //    return;
        //}

        if (findPlayer)
        {
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followspeed * Time.deltaTime);
            Debug.Log("돌진");
        }
        //anim.SetBool("AttackPlayer", playerfind);


    }

    public void Hit(float damage, Vector3 dir)
    {
        if (monsterStat.hP <= 0)
        {
            return;
        }

        this.monsterStat.hP = Mathf.Clamp(this.monsterStat.hP - damage, 0, this.monsterStat.maxHP);
        //slider.value = this.monsterStat.hP;
        anim.SetTrigger("Hit");
        rigid.AddForce(dir, ForceMode2D.Impulse);
    }
    public float GetAtt()
    {
        return monsterStat.att;
    }

    public void isDead()
    {
        //플레이어 경험치 += MonsterManager.Instance.monsterStat.giveExp;
        // 플레이어 돈 += MonsterManager.Instance.monsterStat.giveMoney;


        this.gameObject.SetActive(false);
    }

    
}
