using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public Transform[] monsterSpawnPos;
    Monster tmpMonster;

    Vector3 vec = Vector3.zero; //몬스터 위치 벡터

    float generateTime = 0;
    [SerializeField]
    float minGenerateTime = 0;
    [SerializeField]
    float maxGenerateTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMonsterSpawnPos()
    {
        tmpMonster = MonsterManager.instance.GetMonsterFromPool();
        int pos = Random.Range(0, monsterSpawnPos.Length);
        vec.x = monsterSpawnPos[pos].position.x;
        if (tmpMonster.transform.CompareTag("FlyEnemy"))
        {
            tmpMonster.SetMonsterSprite(MonsterManager.instance.AllFlyMonsterSprites[DungeonManager.instance.dungeonNum]);
            vec.y = monsterSpawnPos[pos].position.y + 1;
        }
        else if (tmpMonster.transform.CompareTag("GroundEnemy"))
        {
            tmpMonster.SetMonsterSprite(MonsterManager.instance.AllGroundMonsterSprites[DungeonManager.instance.dungeonNum]);
            vec.y = monsterSpawnPos[pos].position.y;
        }

        tmpMonster.transform.position = vec;
        tmpMonster.gameObject.SetActive(true);

    }
    public IEnumerator GenerateMonster()
    {
        while (true)
        {
            generateTime = Random.Range(minGenerateTime, maxGenerateTime);
            yield return new WaitForSeconds(generateTime);
            SetMonsterSpawnPos();
        }
    }
}
