using UnityEngine;

public interface IAtt
{
    float Attak();      //�⺻ ����
    float Skill();      //��ų ����

    void GetHit(float damage, Vector3 dir);     //���� ����
}
