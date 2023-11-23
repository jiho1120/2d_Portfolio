using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IAtt
{
    //public Camera cam;
    //public Image[] coolImg;       //쿨타임 이미지
    //public Slider hpSlider;         //HP Bar
    //public Slider expSlider;        //EXP Bar
    //public Text levelTxt;           //Level Txt
    Rigidbody2D rigid;
    Animator anim;

    Constructure.Stat myStat;       //플레이어 스탯

    Vector3 vec = Vector3.zero;
    Vector3 scaleVec = Vector3.one;
    Vector3 direction = Vector3.zero;

    float x = 0;
    public float speed = 5;
    int jumpCount = 0;
    float knockBack = 1;
    bool isHit = false;

    void Start()
    {
        StatSetting();

        rigid = transform.GetComponent<Rigidbody2D>();
        anim = transform.GetComponent<Animator>();
    }

    //스탯 초기 세팅
    void StatSetting()
    {
        myStat = new Constructure.Stat(100, 10, 20, 0, 100, 0);
        //hpSlider.maxValue = myStat.MaxHP;
        //hpSlider.value = myStat.MaxHP;
        //expSlider.maxValue = myStat.MaxExpVal;
        //expSlider.value = myStat.ExpVal;
        //levelTxt.text = $"{0}";
    }

    void Update()
    {
        //조작
        x = Input.GetAxisRaw("Horizontal");
        vec.x = x;
        transform.Translate(vec.normalized * Time.deltaTime * speed);

        //캐릭터 스프라이트 반전
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
            if(jumpCount < 2)
            {
                jumpCount++;

                if(jumpCount == 2)
                {
                    speed = 2.5f;
                }
            }
            else
            {
                return;
            }
            rigid.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
            anim.SetTrigger("IsJump");
            speed = 5;
        }

        //공격(임시)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("IsAtt");
        }

        //스킬(임시)
        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayerManager.Instance.skill.SkillSetting();
            anim.SetTrigger("IsSkill");
        }

        //HP회복(임시)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            myStat.HP += 10;
            //hpSlider.value = myStat.HP;
        }

        //경험치(임시)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            myStat.ExpVal += 10;
            //expSlider.value = myStat.ExpVal;

            if(myStat.ExpVal == myStat.MaxExpVal)
            {
                myStat.Level += 1;
                //levelTxt.text = $"{myStat.Level}";
                myStat.ExpVal = 0;
                //expSlider.value = myStat.ExpVal;
                myStat.MaxExpVal += 100;
                //expSlider.maxValue = myStat.MaxExpVal;
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
        //hpSlider.value = this.myStat.HP;
        anim.SetTrigger("IsHit");
        rigid.AddForce(dir, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //땅에 닿았을 때
        if (collision.gameObject.CompareTag("Ground"))
        {
            isHit = false;
            jumpCount = 0;      //점프 초기화
            rigid.velocity = Vector2.zero;      //미끄럼방지
        }

        //몬스터랑 닿았을 때
        else if (collision.gameObject.CompareTag("Monster"))
        {
            isHit = true;
            direction = (collision.transform.position - transform.position).normalized;
            direction.y += 1;
            direction *= knockBack;

            GetHit(collision.transform.GetComponent<IAtt>().Attak(), direction);
        }
    }
}
