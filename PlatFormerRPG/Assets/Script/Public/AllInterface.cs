using UnityEngine;

public interface IAtt
{
    float Attak();      //�⺻ ����
    float Skill();      //��ų ����

    void GetHit(float damage, Vector3 dir);     //���� ����
}

public interface IHit
{
    void Hit(float damage, Vector3 dir);

    float GetAtt();
}
