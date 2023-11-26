using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attak : MonoBehaviour
{
    public Transform swordParent;           //���� ��ų object prefab �θ�
    public GameObject swordPrefab;          //���� ��ų object prefab
    public GameObject swordPos;             //���� ��ų object ���� ��ġ
    public Transform fireBallParent;        //������ object prefab �θ�
    public GameObject fireBallPrefab;       //������ �⺻ ���� object prefab
    public GameObject bigFireBallPrefab;    //������ ��ų ���� object prefab
    public GameObject fireBallPos;          //������ ���� object ���� ��ġ

    Rigidbody2D rigid;
    Animator anim;
    GameObject tmpObj;              //�ӽú���

    public float speed;
    public float bulletCoolTime;        //�߻�ü ��Ÿ��
    private float curTime;              //��Ÿ�� ��

    void Start()
    {
        rigid = transform.GetComponent<Rigidbody2D>();
        anim = transform.GetComponent<Animator>();
    }

    void Update()
    {
        //�⺻ ����
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //player�� ����
            //if (/*enum �ҷ��ͼ� ��*/)
            //{
            //    WarriorAtt();
            //}
            ////player�� ������
            //else
            //{
            //    WizardAtt();
            //}
            anim.SetTrigger("IsAtt");
        }

        //��ų ����
        if (Input.GetKeyDown(KeyCode.X))
        {
            //player�� ����
            //if (/*enum �ҷ��ͼ� ��*/)
            //{
            //    WarriorSkill();
            //}
            ////player�� ������
            //else
            //{
            //    WizardSkill();
            //}
            anim.SetTrigger("IsSkill");
        }
    }

    //���� �⺻ ���� ����
    public void WarriorAtt()
    {

    }

    //���� ��ų ����
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

    //������ �⺻ ���� ����
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

    //������ ��ų ����
    public void WizardSkill()
    {

    }
}
