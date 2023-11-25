using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Constructure.MonsterStat bossStat;

    float speed = 3;
    bool isMove = true;
    bool IsLeft = true;
    bool isAttack = false;
    bool boundary = false;
    int bossPhase = 1;

    Vector3 sclaeVec = new Vector3(0.5f,0.5f,0.5f);
    Vector3 telpoVec = new Vector3(2, 0, 0);
    Vector3 vec = Vector3.right;

    Rigidbody2D rigid;
    Animator anim;
    Coroutine bossCor = null;

    

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bossStat = new Constructure.MonsterStat(100); // DungeonManager.Instance.dungeonNum 으로 세팅하면 맵열때 숫자가 바뀜
        bossCor = StartCoroutine(Bossmove());
        InvokeRepeating("Teleport", 1f, 10f);
        //if (bossCor!=null)        


        //StopCoroutine(bossCor);
    }

    // Update is called once per frame
    void Update()
    {
        chcekBossPhase();
        LimitArea();
        Boundary();

        CloseAttack();
        FarAttack();

    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            transform.Translate(vec * speed * (IsLeft ? -1 : 1) * Time.fixedDeltaTime);
        }
    }

    //기본 능력
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
        if (PlayerManager.Instance.GetPlayerPosition().x > this.transform.position.x - 2 || PlayerManager.Instance.GetPlayerPosition().x < this.transform.position.x + 2
            || PlayerManager.Instance.GetPlayerPosition().y > this.transform.position.y - 2 || PlayerManager.Instance.GetPlayerPosition().y < this.transform.position.y + 2)
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
        if (transform.position.x <= -14)
        {
            IsLeft = false;
        }
        else if (transform.position.x >= 14)
        {
            IsLeft = true;
        }
    }



    //이동관련함수 

    

    IEnumerator Bossmove()
    {
        while (true)
        {
            isMove = true;
            anim.SetBool("isMove", isMove);
            IsLeft = Random.Range(0, 2) == 0 ? true : false;
            sclaeVec.x = (IsLeft ? -0.5f : 0.5f);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            isMove = false;
            anim.SetBool("isMove", isMove);
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        }
    }
    void Teleport()
    {
        if (boundary)
        {
            WatchPlayer();
            if (PlayerManager.Instance.GetPlayerPosition().x <= 0) // 보스가 맵안쪽에 들어오게
            {
                this.transform.position = (PlayerManager.Instance.GetPlayerPosition() + telpoVec);
                IsLeft = true;
            }
            else
            {
                this.transform.position = (PlayerManager.Instance.GetPlayerPosition() - telpoVec);
                IsLeft = false;

            }
        }
    }
    void WatchPlayer() // 순간이동시 플레이어 보기위한 함수
    {
        if (PlayerManager.Instance.GetPlayerPosition().x > this.transform.position.x)
        {
            IsLeft = false;
        }
        else
        {
            IsLeft = true;
        }
    }


    //공격 함수


    void CloseAttack()
    {
        if (boundary == true && isAttack == true)
        {
            anim.SetBool("closeAttack", isAttack);
        }
        isAttack = false;
    }

    void FarAttack()
    {
        if (isAttack == true)
        {
            anim.SetBool("farAttack", isAttack);
        }
        isAttack = false;
    }

    //죽음
    

    public void isDead()
    {
        if (bossStat.hP <= 0)
        {
            PlayerManager.Instance.player.myStat.ExpVal += bossStat.giveExp;
            //PlayerManager.Instance.player.myStat.money += bossStat.giveMoney;
            this.gameObject.SetActive(false);
        }
    }

    

}
