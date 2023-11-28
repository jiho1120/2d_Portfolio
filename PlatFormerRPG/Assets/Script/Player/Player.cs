using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IAtt
{
    //public GameObject swordPrefab;          //전사 기본 공격 object prefab
    public GameObject swordSkillPrefab;     //전사 스킬 object prefab
    public Transform swordPos;              //전사 스킬 object 생성 위치
    public GameObject fireBallPrefab;       //마법사 기본 공격 object prefab
    public GameObject bigFireBallPrefab;    //마법사 스킬 공격 object prefab
    public Transform fireBallPos;           //마법사 공격 object 생성 위치

    public Rigidbody2D rigid;
    Animator anim;
    GameObject tmpObj;              //임시변수
    PlatformEffector2D effector2D = null;       //이펙터

    //public GameObject[] AttSkillPrefabs;        //관리할 원본 공격, 스킬 prefab들
    //Dictionary<int, Queue<GameObject>> skillObjects = new Dictionary<int, Queue<GameObject>>();     //전사 스킬, 마법사 공격, 스킬 관리

    public Constructure.Stat myStat;       //스탯 정보

    Vector3 vec = Vector3.zero;
    Vector3 scaleVec = Vector3.one;
    Vector3 direction = Vector3.zero;

    float x = 0;
    public float speed = 6;
    public float jumpPower = 8;
    public int jumpCount = 0;
    float knockBack = 1;
    public bool isHit = false;
    bool isStart = false;
    int layermask = 0;

    Coroutine cor = null;

    void Start()
    {
        rigid = transform.GetComponent<Rigidbody2D>();
        anim = transform.GetComponent<Animator>();
        StatSetting();
        isStart = true;

        layermask = 1 << LayerMask.NameToLayer("Enemy")/* | 1 << LayerMask.NameToLayer("Boss")*/;


    }

    //Player 스탯 세팅
    void StatSetting()
    {
        myStat = new Constructure.Stat(100, 10, 20, 0, 100, 0, 0);
        if (UIManager.Instance !=null)
        {
            UIManager.Instance.State(myStat);
        }        
    }

    void Update()
    {
        if (isStart==false)
        {
            return;
        }

        PlayerMove();       //player 조작
        PlayerAttSkill();   //Player 공격, 스킬

        //HP 확인용 Key(임시)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            myStat.HP += 10;
            UIManager.Instance.SetHpSlider(myStat.HP);
        }

        //경험치 확인용 Key(임시)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            myStat.ExpVal += 10;

            if(myStat.ExpVal == myStat.MaxExpVal)
            {
                myStat.Level += 1;
                myStat.ExpVal = 0;
                myStat.MaxExpVal += 100;
            }
        }
    }

    //Player 조작
    void PlayerMove()
    {
        //Key조작(자후 조이스틱으로 변경)
        x = Input.GetAxisRaw("Horizontal");
        vec.x = x;
        transform.Translate(vec.normalized * Time.deltaTime * speed);

        //Player 이동 반전
        if (vec.x != 0)
        {
            scaleVec.x = vec.x;
            anim.SetBool("IsMove", true);
        }
        else
        {
            anim.SetBool("IsMove", false);
        }
        transform.localScale = scaleVec;

        //점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount < 2)
            {
                rigid.velocity = Vector2.zero;      //velocity 초기화(일정한 점프 유지)
                jumpCount++;
            }
            else
            {
                return;
            }
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetTrigger("IsJump");
        }
    }

    //플레이어 공격, 스킬
    void PlayerAttSkill()
    {
        //기본 공격
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //player가 전사
            if (PlayerManager.Instance.CharacterType == AllEnum.Type.Warrior)
            {
                Attack_Warrior();
                anim.SetTrigger("IsAtt");
                //for (int i = 0; i < 1; i++)
                //{
                //    tmpObj = Instantiate(swordPrefab, swordPos);
                //}
            }
            //player가 마법사
            else if (PlayerManager.Instance.CharacterType == AllEnum.Type.Dragon)
            {
                tmpObj = Instantiate(fireBallPrefab, fireBallPos.position, transform.rotation);
            }
            Attak();      //공격력
            Debug.Log("공격함");
            anim.SetTrigger("IsAtt");
        }

        //스킬 공격
        if (Input.GetKeyDown(KeyCode.X))
        {
            //player가 전사
            if (PlayerManager.Instance.CharacterType == AllEnum.Type.Warrior)
            {
                Instantiate(swordSkillPrefab, swordPos.position, transform.rotation);
            }
            //player가 마법사
            else if (PlayerManager.Instance.CharacterType == AllEnum.Type.Dragon)
            {
                Instantiate(bigFireBallPrefab, fireBallPos.position, transform.rotation);
            }
            
            Skill();      //공격력
            Debug.Log("스킬씀");
            anim.SetTrigger("IsSkill");
        }

        tmpObj.SetActive(false);
    }

    //전사 기본 공격
    public void Attack_Warrior()
    {
        Collider2D[] allcols = Physics2D.OverlapCircleAll(swordPos.position, 2, layermask);
        float neardist = Mathf.Infinity;
        int index = -1;

        for (int i = 0; i < allcols.Length; i++)
        {
            if (Vector2.Distance(allcols[i].transform.position, transform.position) < neardist)
            {
                allcols[i].GetComponent<IAtt>().GetHit(Attak(), transform.right /*내 데미지, 방향*/);
                index = i;
            }
        }
    }

    //기본 공격
    public float Attak()
    {
        return myStat.Att;
    }

    //스킬 공격
    public float Skill()
    {
        return myStat.Skill;
    }

    //입은 피해
    public void GetHit(float damage, Vector3 dir)
    {
        if(myStat.HP <= 0)
        {
            return;
        }

        this.myStat.HP = Mathf.Clamp(this.myStat.HP - damage, 0, this.myStat.MaxHP);
        UIManager.Instance.hpSlider.value = this.myStat.HP;
        anim.SetTrigger("IsHit");
        rigid.AddForce(dir, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //땅과 닿았을 때
        if (collision.gameObject.CompareTag("Ground"))
        {
            //isHit = false;

            //땅 통과_땅을 통과할 때 점프 카운트 리셋되면 안됨
            if(effector2D != null)
            {
                effector2D.rotationalOffset = 180;
            }
            else
            {
                jumpCount = 0;
                rigid.velocity = Vector2.zero;      //미끄럼방지
            }
        }

        //몬스터와 닿았을 때
        else if (collision.gameObject.CompareTag("GroundEnemy") && collision.gameObject.CompareTag("FlyEnemy"))
        {
            isHit = true;
            //부딪혔을 때 내가 몬스터보다 위에 있으면
            if(transform.position.y > collision.transform.position.y + 0.3f)
            {
                direction = (transform.position - collision.transform.position).normalized;
                direction.y += 2;
                direction *= knockBack;
            }
            GetHit(collision.transform.GetComponent<IHit>().GetAtt(), direction);
            Debug.Log("몬스터랑 닿았음");        //확인용
        }

        //내가 점프 그라운드 위에 있으면
        //else if (collision.gameObject.CompareTag("JumpGround"))
        //{
        //    effector2D = collision.transform.GetComponent<PlatformEffector2D>();
        //}
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Potal"))
        {
            PlayerManager.Instance.InPotal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Potal"))
        {
            PlayerManager.Instance.InPotal = false;
        }
    }

    IEnumerator AttSkillDelay()
    {
        while(true)
        {

        }
    }
}
