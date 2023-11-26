using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Constructure;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Boss : MonoBehaviour, IHit
{
    public Constructure.MonsterStat bossStat;

    float speed = 3;
    int bossPhase = 1;
    int attackCount = 0;
    bool isMove = true;
    bool IsLeft = true;
    bool isAttack = false;
    bool boundary = false;
    float xDifference;
    float yDifference;
    float errorMargin;

    Vector3 sclaeVec = new Vector3(0.5f,0.5f,0.5f);
    Vector3 telpoVec = new Vector3(2, 0, 0);
    Vector3 vec = Vector3.right;
    Vector3 dir = Vector3.zero;


    Rigidbody2D rigid;
    Animator anim;
    Coroutine bossCor = null;
    Coroutine bossAttCor = null;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bossStat = new Constructure.MonsterStat(100); // DungeonManager.Instance.dungeonNum ���� �����ϸ� �ʿ��� ���ڰ� �ٲ�
        bossCor = StartCoroutine(Bossmove());
        bossAttCor = StartCoroutine(AttackCor());
        InvokeRepeating("Teleport", 1f, 10f);
        //if (bossCor!=null)        


        //StopCoroutine(bossCor);
    }

    // Update is called once per frame
    void Update()
    {
        sclaeVec.x = (IsLeft ? 0.5f : -0.5f);
        chcekBossPhase();
        LimitArea();

        if (bossPhase == 1)
        {
            Boundary();
            CloseAttack();
            CloseSkill();

        }
        else if (bossPhase == 2)
        {
            StopCoroutine(bossCor);
            CancelInvoke("Teleport");
            FarAttack();
            FarSkill();
        }
        else
        {
            return;
        }
    }

    IEnumerator AttackCor()
    {
        while (true)
        {
            isAttack = true;
            if (bossPhase == 1)
            {
                if (attackCount == 5)
                {
                    CloseSkill();
                }
                else
                {
                    CloseAttack();
                }
            }
            else if (bossPhase == 2)
            {
                if(attackCount == 5)
                {
                    FarSkill();
                }
                else
                {
                    FarAttack();
                }
            }
            
            yield return new WaitForSeconds(3f);
        }
    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            transform.localScale = sclaeVec;
            transform.Translate(vec * speed * (IsLeft ? -1 : 1) * Time.fixedDeltaTime);
        }
    }

    //�⺻ �ɷ�
    void chcekBossPhase()
    {
        if (bossStat.hP <= (bossStat.maxHP * 0.5))
        {
            bossStat.hP = bossStat.maxHP * 0.5f;
            bossPhase++;
        }
    }

    void Boundary()
    {
        xDifference = Mathf.Abs(PlayerManager.Instance.GetPlayerPosition().x - this.transform.position.x); //����
        yDifference = Mathf.Abs(PlayerManager.Instance.GetPlayerPosition().y - this.transform.position.y);
        errorMargin = 2f;

        if (xDifference < errorMargin && yDifference < errorMargin)
        {
            boundary = true;
        }
        else
        {
            boundary = false;
        }
    }

    void LimitArea()
    {
        if (transform.position.x <= -12)
        {
            IsLeft = false;
        }
        else if (transform.position.x >= 12)
        {
            IsLeft = true;
        }
    }
    

//�̵������Լ� 
IEnumerator Bossmove()
    {
        while (true)
        {
            isMove = true;
            anim.SetBool("isMove", isMove);
            //IsLeft = Random.Range(0, 2) == 0 ? true : false;
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            isMove = false;
            anim.SetBool("isMove", isMove);
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        }
    }

    void Teleport()
    {
        if (!boundary)
        {
            if (PlayerManager.Instance.GetPlayerPosition().x <= 0) // ������ �ʾ��ʿ� ������
            {
                this.transform.position = (PlayerManager.Instance.GetPlayerPosition() + telpoVec);
                //IsLeft = true;
            }
            else
            {
                this.transform.position = (PlayerManager.Instance.GetPlayerPosition() - telpoVec);
                //IsLeft = false;
            }
            WatchPlayer();
        }
    } 

    void WatchPlayer() // �����̵��̳� ���ݽ� �÷��̾� �������� �Լ�
    {
        if (PlayerManager.Instance.GetPlayerPosition().x < this.transform.position.x)
        {
            IsLeft = true;
        }
        else
        {
            IsLeft = false;
        }
        sclaeVec.x = (IsLeft ? 0.5f : -0.5f);
    }

    //���� �Լ�
    void CloseAttack()
    {
        if (boundary == true && isAttack == true)
        {
            WatchPlayer();
            anim.SetBool("closeAttack", isAttack);
            Debug.Log("closeAttack");
        }
        
        isAttack = false;
        attackCount++;
        anim.SetBool("closeAttack", isAttack);
    }

    void CloseSkill()
    {
        if (boundary == true && isAttack == true)
        {
            WatchPlayer();
            anim.SetBool("closeSkill", isAttack);
            Debug.Log("closeSkill");
        }
        isAttack = false;
        attackCount = 0;
        anim.SetBool("closeSkill", isAttack);
    }

    void FarAttack()
    {
        if (isAttack == true)
        {
            WatchPlayer();
            anim.SetBool("farAttack", isAttack);
            Debug.Log("farAttack");

        }
        isAttack = false;
        attackCount++;
        anim.SetBool("farAttack", isAttack);
    }

    void FarSkill()
    {
        if (isAttack == true)
        {
            anim.SetBool("farSkill", isAttack);
            Debug.Log("farSkill");
        }
        isAttack = false;
        attackCount = 0;
        anim.SetBool("farSkill", isAttack);
    }

    //�ǰ�
    public void Hit(float damage, Vector3 dir)
    {
        if (bossStat.hP <= 0)
        {
            return;
        }

        this.bossStat.hP = Mathf.Clamp(this.bossStat.hP - damage, 0, this.bossStat.maxHP);
        anim.SetTrigger("hit");
        //rigid.AddForce(dir, ForceMode2D.Impulse); �˹���ٰ���
    }
    public float GetAtt()
    {
        return bossStat.att;
    }

    //����
    public void isDead()
    {
        if (bossStat.hP <= 0)
        {
            PlayerManager.Instance.player.myStat.ExpVal += bossStat.giveExp;
            //PlayerManager.Instance.player.myStat.money += bossStat.giveMoney;
            this.gameObject.SetActive(false);
        }
    }

    //Ʈ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ����ιٲ����
        {
            dir = (this.transform.position - collision.transform.position).normalized;
            Hit(20, dir);
            Debug.Log(bossStat.hP);
        }
    }

}
