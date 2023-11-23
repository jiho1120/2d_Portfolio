using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public Transform swordParent;   //���� ��ų object prefab �θ�
    public GameObject swordPrefab;  //���� ��ų object prefab
    public GameObject swordPos;     //���� ��ų object ���� ��ġ
    GameObject tmpObj;              //�ӽú���

    public float bulletCoolTime;        //�߻�ü ��Ÿ��
    private float curTime;              //

    //���� ��ų ����
    public void SkillSetting()
    {
        if(curTime <= 0)
        {
            tmpObj = Instantiate(swordPrefab, swordParent, swordPos);
            tmpObj.SetActive(false);

            curTime = bulletCoolTime;
        }
        curTime -= Time.deltaTime;
    }

    //������ ��ų ����
}
