using UnityEngine;

public interface IHit
{
    void Hit(float damage, Vector3 dir);

    float GetAtt();
}
