using UnityEngine;

public interface IAtt
{
    float Attak();      //기본 공격
    float Skill();      //스킬 공격

    void GetHit(float damage, Vector3 dir);     //입은 피해
}

public interface IHit
{
    void Hit(float damage, Vector3 dir);

    float GetAtt();
}
