using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterManager : Singleton<MonsterManager>
{
    public GameObject[] monsterPrefabs; // 0이 그라운드 1이 fly
    public Sprite[] AllGroundMonsterSprites;
    public Sprite[] AllFlyMonsterSprites;

    public GameObject tmpobj { get; private set; } //임시변수
    Queue<Monster> objectPool = new Queue<Monster>();
    List<GameObject> allMonsterList = new List<GameObject>();


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
        }

    }


    public Monster GetMonsterFromPool()
    {
        if (objectPool.Count > 0) //오브젝트 풀에 내용물이 있다면~
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
