using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform[] monsterSpawnPos;
    Monster tmpMonster;
    Vector3 vec = Vector3.zero; //몬스터 위치 벡터
    int monsterType;

    public GameObject[] bulletPrefab;
    public Transform[] bulletSpawnPos;

    int numberOfBullets;
    float angleStep;
    float bulletRadius;
    float angle;






    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetMonsterSpawnPos() // 몬스터의 스폰위치와 스프라이트 설정
    {
        monsterType = Random.Range(0, 2); // 0이 그라운드, 1이 fly
        tmpMonster = MonsterManager.Instance.GetMonsterFromPool(monsterType);
        int pos = Random.Range(0, monsterSpawnPos.Length);
        vec.x = monsterSpawnPos[pos].position.x;

        if (monsterType == 1)
        {
            tmpMonster.SetMonsterSprite(MonsterManager.Instance.AllFlyMonsterSprites[DungeonManager.Instance.dungeonNum]);
            vec.y = monsterSpawnPos[pos].position.y + 1;
        }
        else if (monsterType == 0)
        {
            tmpMonster.SetMonsterSprite(MonsterManager.Instance.AllGroundMonsterSprites[DungeonManager.Instance.dungeonNum]);
            vec.y = monsterSpawnPos[pos].position.y;
        }
        tmpMonster.transform.position = vec;
        tmpMonster.gameObject.name = "이거"+val;
        val++;
        tmpMonster.gameObject.SetActive(true);
    }
    static int val=0;

    public IEnumerator SetBulletSpawnPos()
    {
        for (int i = 0; i < bulletSpawnPos.Length; i++)
        {
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, 0f);
            Vector3 bulletPosition = bulletSpawnPos[i].transform.position;
            GameObject monsterBullet = Instantiate(bulletPrefab[Random.Range(0, bulletPrefab.Length)], bulletPosition, bulletRotation);
            yield return new WaitForSeconds(5f);
        }
    }
}
