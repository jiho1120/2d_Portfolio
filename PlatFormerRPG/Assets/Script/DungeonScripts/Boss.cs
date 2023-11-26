using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using static Constructure;

public class Boss : MonoBehaviour, IHit
{

    // 애니메이터 작동 오류 (시간되면 탄막 구현 제대로하기)
    // 페이즈 넘어갈때 지정한 위치로 안가고 멈추는 이유
    // 보스가 맵에 가려짐
    public Constructure.MonsterStat bossStat;

    float speed = 3;
    int bossPhase;
    int attackCount = 0;
    bool isMove = true;
    bool IsLeft = true;
    bool isAttack = false;
    bool boundary = false;
    public float realAttack;
    float addAttack;
    float xDifference;
    float yDifference;
    float errorMargin;

    Vector3 sclaeVec = new Vector3(0.5f, 0.5f, 0.5f);
    Vector3 telpoVec = new Vector3(2, 0, 0);
    Vector3 vec = Vector3.right;
    Vector3 dir = Vector3.zero;
    Vector3 middlePos = new Vector3(0, -5, 0);


    public GameObject bulletPrefab;
    public GameObject bulletSpawnPos;
    public Slider hpSlider;

    Rigidbody2D rigid;
    Animator anim;
    Coroutine bossMoveCor = null;
    Coroutine bossAttCor = null;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hpSlider = GetComponent<Slider>();
        //테스트하려고 능력치 줄여놈
        bossStat = new Constructure.MonsterStat(10); // DungeonManager.Instance.dungeonNum 으로 세팅하면 맵열때 숫자가 바뀜
        hpSlider.maxValue = bossStat.maxHP;
        bossPhase = 1;
        bossMoveCor = StartCoroutine(Bossmove());
        bossAttCor = StartCoroutine(AttackCor());
        InvokeRepeating("Teleport", 1f, 10f);
        //if (bossMoveCor!=null)        
        //StopCoroutine(bossMoveCor);
    }

    // Update is called once per frame
    void Update()
    {
        sclaeVec.x = (IsLeft ? 0.5f : -0.5f);
        chcekBossPhase();
        LimitArea();
        isDead();

        if (bossPhase == 1)
        {
            Boundary();
        }
        else if (bossPhase == 2)
        {
            CancelInvoke("Teleport");
        }
        else
        {
            return;
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

    //기본 능력
    void chcekBossPhase()
    {
        if (bossStat.hP <= (bossStat.maxHP * 0.5))
        {
            bossPhase++;
        }
    }

    void Boundary()
    {
        xDifference = Mathf.Abs(PlayerManager.Instance.GetPlayerPosition().x - this.transform.position.x); //절댓값
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


    //이동관련함수 
    IEnumerator Bossmove()
    {
        while (bossPhase == 1)
        {
            isMove = true;
            anim.SetBool("isMove", isMove);
            //IsLeft = Random.Range(0, 2) == 0 ? true : false;
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            isMove = false;
            anim.SetBool("isMove", isMove);
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
            if (bossPhase == 2)
            {
                SetMiddlePosition();
                StopCoroutine(bossMoveCor);
                bossStat.hP = bossStat.maxHP * 0.5f;
            }
        }
    }

    void Teleport()
    {
        if (!boundary)
        {
            if (PlayerManager.Instance.GetPlayerPosition().x <= 0) // 보스가 맵안쪽에 들어오게
            {
                this.transform.position = (PlayerManager.Instance.GetPlayerPosition() + telpoVec);
                //IsLeft = true;
            }
            else
            {
                this.transform.position = (PlayerManager.Instance.GetPlayerPosition() - telpoVec);
                //IsLeft = false;
            }
            isMove = false;
            anim.SetBool("isMove", isMove);
            WatchPlayer();
        }
    }

    void WatchPlayer() // 순간이동이나 공격시 플레이어 보기위한 함수
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

    void SetMiddlePosition()
    {
        Vector3.MoveTowards(transform.position, middlePos, Time.deltaTime * speed);
    }

    //공격 함수
    IEnumerator AttackCor()
    {
        while (true)
        {
            isAttack = true;
            WatchPlayer();
            if (bossPhase == 1)
            {
                if (attackCount >= 4) // 5번째에 스킬
                {
                    CloseSkill();
                    anim.SetBool("closeSkill", isAttack);
                }
                else
                {
                    CloseAttack();
                    anim.SetBool("closeAttack", isAttack);
                }
            }
            else if (bossPhase == 2)
            {
                if (attackCount >= 4)
                {
                    FarSkill();
                    anim.SetBool("farSkill", isAttack);
                }
                else
                {
                    FarAttack();
                    anim.SetBool("farAttack", isAttack);
                }
            }
            realAttack = bossStat.att * addAttack;
            yield return new WaitForSeconds(3f);
        }
    }

    void CloseAttack()
    {
        if (boundary)
        {
            anim.SetBool("closeAttack", isAttack);
            addAttack = 1;
            isAttack = false;
            Debug.Log("closeAttack");
        }
        attackCount++;
    }

    void CloseSkill()
    {
        if (boundary)
        {
            anim.SetBool("closeSkill", isAttack);
            addAttack = 10;
            isAttack = false;
            Debug.Log("closeSkill");
        }
        attackCount = 0;
    }

    void FarAttack()
    {
        GameObject monsterBullet = Instantiate(bulletPrefab, bulletSpawnPos.transform.position, bulletSpawnPos.transform.rotation);
        anim.SetBool("farAttack", isAttack);
        addAttack = 5;
        isAttack = false;
        Debug.Log("farAttack");
        attackCount++;
    }

    void FarSkill()
    {
        anim.SetBool("farSkill", isAttack);
        addAttack = 15;
        isAttack = false;
        Debug.Log("farSkill");
        attackCount = 0;
    }

    //피격
    public void Hit(float damage, Vector3 dir)
    {
        if (bossStat.hP <= 0)
        {
            return;
        }

        this.bossStat.hP = Mathf.Clamp(this.bossStat.hP - damage, 0, this.bossStat.maxHP);
        anim.SetTrigger("hit");
        hpSlider.value = bossStat.hP;
        //rigid.AddForce(dir, ForceMode2D.Impulse); 넉백안줄거임
    }
    public float GetAtt()
    {
        return realAttack;
    }

    //죽음
    public void isDead()
    {
        if (bossStat.hP <= 0)
        {
            PlayerManager.Instance.player.myStat.ExpVal += bossStat.giveExp;
            //PlayerManager.Instance.player.myStat.money += bossStat.giveMoney;
            gameObject.SetActive(false);
        }
    }

    //트리거

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 무기로바꿔야함
        {
            dir = (this.transform.position - collision.transform.position).normalized;
            Hit(20, dir); //PlayerManager.Instance.player.myStat.Att;
            Debug.Log(bossStat.hP);
        }
    }

}
