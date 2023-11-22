using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform[] monsterSpawnPos;
    Monster tmpMonster;

    Vector3 vec = Vector3.zero; //���� ��ġ ����

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMonsterSpawnPos() // ������ ������ġ�� ��������Ʈ ����
    {
        tmpMonster = MonsterManager.Instance.GetMonsterFromPool();
        int pos = Random.Range(0, monsterSpawnPos.Length);
        vec.x = monsterSpawnPos[pos].position.x;
        if (tmpMonster.transform.CompareTag("FlyEnemy"))
        {
            tmpMonster.SetMonsterSprite(MonsterManager.Instance.AllFlyMonsterSprites[DungeonManager.Instance.dungeonNum]);
            vec.y = monsterSpawnPos[pos].position.y + 1;
        }
        else if (tmpMonster.transform.CompareTag("GroundEnemy"))
        {
            tmpMonster.SetMonsterSprite(MonsterManager.Instance.AllGroundMonsterSprites[DungeonManager.Instance.dungeonNum]);
            vec.y = monsterSpawnPos[pos].position.y;
        }

        tmpMonster.transform.position = vec;
        tmpMonster.gameObject.SetActive(true);

    }
    
}
