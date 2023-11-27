using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Constructure.MonsterStat bossStat;
    Rigidbody2D rigid;
    Animator anim;
    Vector3 sclaeVec = new Vector3(0.5f,0.5f,0.5f);
    Vector3 telpoVec = new Vector3(2, 0, 0);
    Vector3 vec = Vector3.right;

    Coroutine enemyCor = null;

    float speed = 10;
    bool isMove = false;
    bool IsLeft = true;
    bool boundary = true;
    int bossPhase = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bossStat = new Constructure.MonsterStat(100); // DungeonManager.Instance.dungeonNum 으로 세팅하면 맵열때 숫자가 바뀜
        //Invoke("Teleport", 1f);
        StartCoroutine(Bossmove());
    }

    // Update is called once per frame
    void Update()
    {
        Boundary();
        LimitArea();
        chcekBossPhase();
    }

    // 움직임은 이정하게 움직이지만 1페이즈 들어가면 순간이동 하게



    void chcekBossPhase()
    {
        if (bossStat.hP < (bossStat.hP * 0.5))
        {
            bossPhase++;
        }
    }

    void CloseAttack()
    {

    }

    void FarAttack()
    {

    }

    void WatchPlayer()
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

    void Teleport()
    {
        if (boundary)
        {
            if (PlayerManager.Instance.GetPlayerPosition().x <= 0) // 보스가 맵안쪽에 들어오게
            {
                this.transform.position = (PlayerManager.Instance.GetPlayerPosition() + telpoVec);
            }
            else
            {
                this.transform.position = (PlayerManager.Instance.GetPlayerPosition() - telpoVec);
            }
            WatchPlayer();
        }

        Debug.Log($"{PlayerManager.Instance.GetPlayerPosition() } \n {this.transform.position}");
    }

    void Boundary()
    {
        if(PlayerManager.Instance.GetPlayerPosition().x > this.transform.position.x - 1 || PlayerManager.Instance.GetPlayerPosition().x < this.transform.position.x + 1 
            || PlayerManager.Instance.GetPlayerPosition().y > this.transform.position.y - 1 || PlayerManager.Instance.GetPlayerPosition().y < this.transform.position.y + 1)
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
    

    IEnumerator Bossmove()
    {
        while (true)
        {
            isMove = true;
            anim.SetBool("isMove", isMove);
            IsLeft = Random.Range(0, 2) == 0 ? true : false;
            sclaeVec.x = (IsLeft ? -0.5f : 0.5f);
            transform.Translate(vec * speed * (IsLeft ? -1 : 1) * Time.deltaTime);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            isMove = false;
            anim.SetBool("isMove", isMove);
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        }
    }

}
