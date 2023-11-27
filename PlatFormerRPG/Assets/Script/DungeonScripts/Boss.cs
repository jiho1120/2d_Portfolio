﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Constructure;

// 탄막 구현 제대로하기
// 페이즈 넘어갈때 지정한 위치로 안가고 멈추는 이유
// 벽뚫기 구현
// 몬스터 와리가리 
// 프로덕트 만들고 몬스터 랑 보스 겹치는거 상속


public class Boss : Object
{
    
    Vector3 telpoVec = new Vector3(2, 0, 0);
    Vector3 middlePos = new Vector3(0, -5, 0);

    int bossPhase = 1;
    int attackCount = 0;
    bool checkPhase = true;

    public GameObject bulletPrefab;
    public GameObject bulletSpawnPos;
    public Slider hpSlider;

    Coroutine bossMoveCor = null;
    Coroutine bossAttCor = null;


    // Start is called before the first frame update
    void Start()
    {
        scale = new Vector3(0.5f, 0.5f, 0.5f);
        speed = 3;
        isMove = true;
        errorMargin = 10f;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hpSlider = GetComponent<Slider>();
        objectStat = new Constructure.MonsterStat(10); // DungeonManager.Instance.dungeonNum 으로 세팅하면 맵열때 숫자가 바뀜
        //hpSlider.maxValue = ObjectStat.maxHP;
        bossMoveCor = StartCoroutine(Bossmove());
        bossAttCor = StartCoroutine(AttackCor());
        InvokeRepeating("Teleport", 1f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            objectStat.hP = objectStat.maxHP * 0.4f;
            Debug.Log("objectStat.hP    " + objectStat.hP);
        }
        scale.x = (IsLeft ? 0.5f : -0.5f);
        chcekBossPhase();
        LimitArea(12f);
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
            transform.localScale = scale;
            transform.Translate(vec * speed * (IsLeft ? -1 : 1) * Time.fixedDeltaTime);
        }
    }

    //기본 능력
    void chcekBossPhase()
    {
        if (checkPhase)
        {
            if (objectStat.hP <= (objectStat.maxHP * 0.5))
            {
                bossPhase++;
                checkPhase = false;
            }
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
        }

        if (bossPhase == 2)
        {
            StopCoroutine(bossMoveCor);
            StartCoroutine(SetMiddlePosition());
            objectStat.hP = objectStat.maxHP * 0.5f;
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
        scale.x = (IsLeft ? 0.5f : -0.5f);
    }

    IEnumerator SetMiddlePosition()
    {
        Debug.Log("도착 위치 : " + middlePos);
        while (Vector2.Distance(transform.position, middlePos) > 0.1f)
        {
            Debug.Log("플레이어 : " + transform.position);
            transform.position = Vector3.MoveTowards(transform.position, middlePos, Time.deltaTime * 10);
            yield return null;
        }
        transform.position = middlePos;
        //transform.Translate(middlePos);
    }

    //공격 함수
    IEnumerator AttackCor()
    {
        while (true)
        {
            WatchPlayer();
            if (bossPhase == 1)
            {
                if (attackCount >= 4) // 5번째에 스킬
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
                if (attackCount >= 4)
                {
                    FarSkill();
                }
                else
                {
                    FarAttack();
                }
            }
            realAttack = objectStat.att * addAtt;
            yield return new WaitForSeconds(3f);
        }
    }

    void CloseAttack()
    {
        if (boundary)
        {
            anim.SetTrigger("closeAttack");
            addAtt = 1;
            Debug.Log("closeAttack");
        }
        attackCount++;
    }

    void CloseSkill()
    {
        if (boundary)
        {
            anim.SetTrigger("closeSkill");
            addAtt = 10;
            Debug.Log("closeSkill");
        }
        attackCount = 0;
    }

    void FarAttack()
    {
        GameObject monsterBullet = Instantiate(bulletPrefab, bulletSpawnPos.transform.position, bulletSpawnPos.transform.rotation);
        anim.SetTrigger("farAttack");
        addAtt = 5;
        Debug.Log("farAttack");
        attackCount++;
    }

    void FarSkill()
    {
        GameObject monsterBullet = Instantiate(bulletPrefab, bulletSpawnPos.transform.position, bulletSpawnPos.transform.rotation);
        anim.SetTrigger("farSkill");
        addAtt = 15;
        Debug.Log("farSkill");
        attackCount = 0;
    }

    //피격
    public override void Hit(float damage, Vector3 dir)
    {
        base.Hit(damage, dir);
        //hpSlider.value = objectStat.hP;
    }

    //트리거

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 무기로바꿔야함
        {
            dir = (this.transform.position - collision.transform.position).normalized;
            Hit(20, dir); //PlayerManager.Instance.player.myStat.Att;
            Debug.Log(objectStat.hP);
        }
    }

}
