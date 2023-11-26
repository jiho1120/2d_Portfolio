using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IAtt
{
    public GameObject swordPrefab;          //전사 스킬 object prefab
    public Transform swordPos;              //전사 스킬 object 생성 위치
    public GameObject fireBallPrefab;       //마법사 기본 공격 object prefab
    public GameObject bigFireBallPrefab;    //마법사 스킬 공격 object prefab
    public Transform fireBallPos;           //마법사 공격 object 생성 위치

    Rigidbody2D rigid;
    Animator anim;
    GameObject tmpObj;              //임시변수

    public Constructure.Stat myStat;       //스탯 정보

    Vector3 vec = Vector3.zero;
    Vector3 scaleVec = Vector3.one;
    Vector3 direction = Vector3.zero;

    float x = 0;
    public float speed = 6;
    public float jumpPower = 8;
    int jumpCount = 0;
    float knockBack = 1;
    bool isHit = false;
    bool isStart = false;

    void Start()
    {
        rigid = transform.GetComponent<Rigidbody2D>();
        anim = transform.GetComponent<Animator>();
        StatSetting();
        isStart = true;
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
        PlayerAttSkill();       //player 공격, 스킬

        //HP 확인용 Key(임시)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            myStat.HP += 10;
            //hpSlider.value = myStat.HP;
            UIManager.Instance.SetHpSlider(myStat.HP);
        }

        //경험치 확인용 Key(임시)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            myStat.ExpVal += 10;
            // expSlider.value = myStat.ExpVal;

            if(myStat.ExpVal == myStat.MaxExpVal)
            {
                myStat.Level += 1;
                // levelTxt.text = $"{myStat.Level}";
                myStat.ExpVal = 0;
                // expSlider.value = myStat.ExpVal;
                myStat.MaxExpVal += 100;
                // expSlider.maxValue = myStat.MaxExpVal;
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
            //if (/*enum 불러와서 비교*/)
            //{
            //    
            //}
            ////player가 마법사
            //else
            //{
            Instantiate(fireBallPrefab, fireBallPos.position, transform.rotation);
            //tmpObj.SetActive(false);

            //Attak();      //공격력

            Debug.Log("공격 발사됨");
            //}
            anim.SetTrigger("IsAtt");
        }

        //스킬 공격
        if (Input.GetKeyDown(KeyCode.X))
        {
            //player가 전사
            //if (/*enum 불러와서 비교*/)
            //{
            //Instantiate(swordPrefab, swordPos.position, transform.rotation);
            //}
            ////player가 마법사
            //else
            //{
            Instantiate(bigFireBallPrefab, fireBallPos.position, transform.rotation);
            //tmpObj.SetActive(false);

            //Skill();      //공격력

            Debug.Log("스킬 발사됨");
            //}
            anim.SetTrigger("IsSkill");
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
        // hpSlider.value = this.myStat.HP;
        anim.SetTrigger("IsHit");
        rigid.AddForce(dir, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //땅과 닿았을 때
        if (collision.gameObject.CompareTag("Ground"))
        {
            isHit = false;
            jumpCount = 0;
            rigid.velocity = Vector2.zero;      //미끄럼방지
        }

        //몬스터와 닿았을 때
        else if (collision.gameObject.CompareTag("GroundEnemy") && collision.gameObject.CompareTag("FlyEnemy"))
        {
            isHit = true;
            direction = (collision.transform.position - transform.position).normalized;
            direction.y += 1;
            direction *= knockBack;

            GetHit(collision.transform.GetComponent<IHit>().GetAtt(), direction);
        }
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
}
