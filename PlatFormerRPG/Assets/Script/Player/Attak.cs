using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attak : MonoBehaviour
{
    public Transform swordParent;           //전사 스킬 object prefab 부모
    public GameObject swordPrefab;          //전사 스킬 object prefab
    public GameObject swordPos;             //전사 스킬 object 생성 위치
    public Transform fireBallParent;        //마법사 object prefab 부모
    public GameObject fireBallPrefab;       //마법사 기본 공격 object prefab
    public GameObject bigFireBallPrefab;    //마법사 스킬 공격 object prefab
    public GameObject fireBallPos;          //마법사 공격 object 생성 위치

    Rigidbody2D rigid;
    Animator anim;
    GameObject tmpObj;              //임시변수

    public float speed;
    public float bulletCoolTime;        //발사체 쿨타임
    private float curTime;              //쿨타임 끝

    void Start()
    {
        rigid = transform.GetComponent<Rigidbody2D>();
        anim = transform.GetComponent<Animator>();
    }

    void Update()
    {
        //기본 공격
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //player가 전사
            //if (/*enum 불러와서 비교*/)
            //{
            //    WarriorAtt();
            //}
            ////player가 마법사
            //else
            //{
            //    WizardAtt();
            //}
            anim.SetTrigger("IsAtt");
        }

        //스킬 공격
        if (Input.GetKeyDown(KeyCode.X))
        {
            //player가 전사
            //if (/*enum 불러와서 비교*/)
            //{
            //    WarriorSkill();
            //}
            ////player가 마법사
            //else
            //{
            //    WizardSkill();
            //}
            anim.SetTrigger("IsSkill");
        }
    }

    //전사 기본 공격 구현
    public void WarriorAtt()
    {

    }

    //전사 스킬 구현
    public void WarriorSkill()
    {
    //    if (curTime <= 0)
    //    {
    //        tmpObj = Instantiate(swordPrefab, swordParent, swordPos);
    //        tmpObj.SetActive(false);

    //        curTime = bulletCoolTime;
    //    }
    //    curTime -= Time.deltaTime;
    }

    //마법사 기본 공격 구현
    public void WizardAtt()
    {
        if (curTime <= 0)
        {
            tmpObj = Instantiate(swordPrefab, swordParent, swordPos);
            //tmpObj.SetActive(false);

            curTime = bulletCoolTime;
        }
        curTime -= Time.deltaTime;
    }

    //마법사 스킬 구현
    public void WizardSkill()
    {

    }
}
