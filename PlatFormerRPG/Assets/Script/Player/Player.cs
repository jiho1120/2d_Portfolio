using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IAtt
{

    public Rigidbody2D rigid;

    Animator anim;

    Constructure.Stat myStat;       //�÷��̾� ����
    //Collider2D col;    
    //Collider2D footCol;
    Collider2D[] cols;
    Collider2D groundCol;

    //Allenum

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
    bool ignoreCollision = false;
    void Start()
    {
        rigid = transform.GetComponent<Rigidbody2D>();
        anim = transform.GetComponent<Animator>();
        //col = transform.GetComponent<Collider2D>();
        //footCol = transform.GetComponentInChildren<Collider2D>();
        cols = transform.GetComponentsInChildren<Collider2D>();
        StatSetting();
        isStart = true;
    }

    //���� �ʱ� ����
    void StatSetting()
    {
        myStat = new Constructure.Stat(100, 10, 20, 0, 100, 0);
        if (UIManager.Instance != null)
        {
            UIManager.Instance.State(myStat);
        }
    }

    void Update()
    {
        if (isStart == false)
        {
            return;
        }
        //Key����
        x = Input.GetAxisRaw("Horizontal");
        vec.x = x;
        transform.Translate(vec.normalized * Time.deltaTime * speed);

        //ĳ���� ��������Ʈ ����
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

        //����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount < 2)
            {
                rigid.velocity = Vector2.zero;      //velocity �ʱ�ȭ, �����ϰ� �ٰ� ��
                jumpCount++;
            }
            else
            {
                return;
            }
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetTrigger("IsJump");
        }

        //����(�ӽ�)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("IsAtt");
        }

        //��ų(�ӽ�)
        if (Input.GetKeyDown(KeyCode.X))
        {
            //PlayerManager.Instance.skill.SkillSetting();
            anim.SetTrigger("IsSkill");
        }

        //HPȸ��(�ӽ�)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            myStat.HP += 10;
            //hpSlider.value = myStat.HP;
            UIManager.Instance.SetHpSlider(myStat.HP);
        }

        //����ġ(�ӽ�)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            myStat.ExpVal += 10;
            UIManager.Instance.SetExpSlider(myStat.ExpVal);


            if (myStat.ExpVal == myStat.MaxExpVal)
            {
                myStat.Level += 1;
                UIManager.Instance.levelTxt.text = $"{myStat.Level}";
                myStat.ExpVal = 0;
                UIManager.Instance.expSlider.value = myStat.ExpVal;
                myStat.MaxExpVal += 100;
                UIManager.Instance.expSlider.maxValue = myStat.MaxExpVal;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {            
            if (ignoreCollision == false && groundCol!=null)
            {
                Debug.Log("다운키 실행");
                ignoreCollision = true;
                Physics2D.IgnoreCollision(cols[0], groundCol, true);
                Physics2D.IgnoreCollision(cols[1], groundCol, true);
                StartCoroutine(CollisionForSeconds(4f));
            }            
        }

    }
    IEnumerator CollisionForSeconds(float seconds)
    {        
        yield return new WaitForSeconds(seconds);

        Physics2D.IgnoreCollision(cols[0], groundCol, false);
        Physics2D.IgnoreCollision(cols[1], groundCol, false);
        ignoreCollision = false;
        groundCol = null;        

        Debug.Log("코 실행");
    }

    //�⺻ ����
    public float Attak()
    {
        return myStat.Att;
    }

    //��ų ����
    public float Skill()
    {
        return myStat.Skill;
    }

    //���� ����
    public void GetHit(float damage, Vector3 dir)
    {
        if (myStat.HP <= 0)
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
        //몬스터와 닿았을 때
        if (collision.gameObject.CompareTag("GroundEnemy") && collision.gameObject.CompareTag("FlyEnemy"))
        {
            isHit = true;
            direction = (collision.transform.position - transform.position).normalized;
            direction.y += 1;
            direction *= knockBack;

            GetHit(collision.transform.GetComponent<IAtt>().Attak(), direction);        //����
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("이게 실행되면 돼야함");            
            groundCol = collision.collider;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Ʈ���ſ���");
        if (other.gameObject.CompareTag("Potal"))
        {
            PlayerManager.Instance.InPotal = true;
            Debug.Log("��Ż����");
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Potal"))
        {
            PlayerManager.Instance.InPotal = false;
            Debug.Log("��Ż���");
        }
    }
}
