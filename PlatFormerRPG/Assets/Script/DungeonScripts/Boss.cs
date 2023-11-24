using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Constructure.MonsterStat bossStat;
    Rigidbody2D rigid;
    Animator anim;
    Coroutine enemyCor = null;
    Vector3 sclaeVec = new Vector3(0.5f,0.5f,0.5f);
    Vector3 telpoVec = new Vector3(2, 0, 0);


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
        Invoke("Teleport", 1f);
        StartCoroutine(Bossmove());
    }

    // Update is called once per frame
    void Update()
    {
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
            sclaeVec.x = 0.5f;
        }
        else
        {
            sclaeVec.x = -0.5f;
        }
    }

    void Teleport()
    {
        Boundary();
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

    IEnumerator Bossmove()
    {
        while (true)
        {
            isMove = true;
            anim.SetBool("isMove", isMove);
            IsLeft = Random.Range(0, 2) == 0 ? true : false;
            if (IsLeft)
            {
                sclaeVec.x = -0.5f;
            }
            if (!IsLeft)
            {
                sclaeVec.x = 0.5f;
            }
            this.transform.localScale = sclaeVec;
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            isMove = false;
            anim.SetBool("isMove", isMove);
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        }
    }

}
