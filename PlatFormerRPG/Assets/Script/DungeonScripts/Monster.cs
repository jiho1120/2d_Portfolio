using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static Constructure;
using static UnityEngine.GraphicsBuffer;

public class Monster : MonoBehaviour, IHit
{
    Vector3 scale = Vector3.one;
    Vector3 vec = Vector3.right;
    Vector3 dir = Vector3.zero;

    float speedMin = 1;
    float speedMax = 2;
    float speed;
    float followspeed = 1f;
    float knockBack = 1;
    bool isMove = false;
    bool IsLeft = true;
    bool boundary = false;

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
        MonsterAct();
        Boundary();
        isDead();
        AttackPlayer();

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

    void Boundary()
    {
        if (PlayerManager.Instance.GetPlayerPosition().x > this.transform.position.x - 1 || PlayerManager.Instance.GetPlayerPosition().x < this.transform.position.x + 1
            || PlayerManager.Instance.GetPlayerPosition().y > this.transform.position.y - 1 || PlayerManager.Instance.GetPlayerPosition().y < this.transform.position.y + 1)
        {
            boundary = true;
        }
        else
        {
            boundary = false;
        }
    }
    public void AttackPlayer()
    {
        if (this.gameObject.CompareTag("GroundEnemy"))
        {
            CloseAttack();
        }
        else if (this.gameObject.CompareTag("FlyEnemy"))
        {
            FarAttack();
        }
    }
    public void CloseAttack()
    {
        //if (PlayerManager.Instance.player.myStat.HP <= 0)
        //{
        //    return;
        //}
        if (boundary)
        {
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followspeed * Time.deltaTime);
            Debug.Log("돌진");
        }
    }

    public void FarAttack()
    {

        if (boundary == true)
        {
            Debug.Log("퉤");
        }
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
            this.gameObject.SetActive(false);
        }
        //플레이어 경험치 += MonsterManager.Instance.monsterStat.giveExp;
        // 플레이어 돈 += MonsterManager.Instance.monsterStat.giveMoney;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dir = (this.transform.position - collision.transform.position).normalized;
            Hit(20, dir);
            Debug.Log(this.monsterStat.hP);
        }
    }

}
