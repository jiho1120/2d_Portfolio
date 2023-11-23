using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public Transform swordParent;   //전사 스킬 object prefab 부모
    public GameObject swordPrefab;  //전사 스킬 object prefab
    public GameObject swordPos;     //전사 스킬 object 생성 위치
    GameObject tmpObj;              //임시변수

    public float bulletCoolTime;        //발사체 쿨타임
    private float curTime;              //

    //전사 스킬 구현
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

    //마법사 스킬 구현
}
