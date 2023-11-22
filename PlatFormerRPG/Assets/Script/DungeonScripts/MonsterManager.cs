using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;
using static Constructure;

public class MonsterManager : Singleton<MonsterManager>
{
    public GameObject[] monsterPrefabs; // 0�� �׶��� 1�� fly
    public Sprite[] AllGroundMonsterSprites;
    public Sprite[] AllFlyMonsterSprites;

    GameObject tmpobj;//�ӽú���
    Queue<Monster> objectPool = new Queue<Monster>();
    List<GameObject> allMonsterList = new List<GameObject>();

    public Constructure.MonsterStat monsterStat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMonsterInfo()
    {
        for (int i = 0; i < 10; i++)
        {
            tmpobj = Instantiate(monsterPrefabs[Random.Range(0, monsterPrefabs.Length)], this.transform.GetChild(0));
            objectPool.Enqueue(tmpobj.GetComponent<Monster>());
            tmpobj.SetActive(false);
            allMonsterList.Add(tmpobj);
            SetMonsterStat(DungeonManager.instance.dungeonNum);
            Debug.Log($"{monsterStat.hP}, {monsterStat.maxHP}, {monsterStat.att}, {monsterStat.giveExp}, {monsterStat.giveMoney}");
        }

    }
    public void SetMonsterStat(int level)
    {
        monsterStat = new Constructure.MonsterStat(level);
    }

    public Monster GetMonsterFromPool()
    {
        if (objectPool.Count > 0) //������Ʈ Ǯ�� ���빰�� �ִٸ�~
        {
            return objectPool.Dequeue();
        }
        else
        {
            tmpobj = Instantiate(monsterPrefabs[Random.Range(0, monsterPrefabs.Length)]);
            allMonsterList.Add(tmpobj);
            return tmpobj.GetComponent<Monster>();
        }
    }

    
}
