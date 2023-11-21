using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class DungeonManager : Singleton<DungeonManager>
{
    public Transform[] spawnPos;
    public GameObject[] monsterPrefabs; // 0이 그라운드 1이 fly
    public Sprite[] AllGroundMonsterSprites;
    public Sprite[] AllFlyMonsterSprites;

    public int dungeonNum { get; private set; }

    float generateTime = 0;
    [SerializeField]
    float minGenerateTime = 0;
    [SerializeField]
    float maxGenerateTime = 0;

    GameObject tmpobj; //임시변수
    Monster tmpMonster;
    Queue<Monster> objectPool = new Queue<Monster>();
    List<GameObject> allMonsterList = new List<GameObject>();
    Vector3 vec = Vector3.zero;


    private void Start()
    {
        //for (int i = 0; i < 10; i++)
        //{
        //    tmpobj = Instantiate(monsterPrefabs[Random.Range(0, monsterPrefabs.Length)], this.transform.GetChild(0));
        //    objectPool.Enqueue(tmpobj.GetComponent<Monster>());
        //    tmpobj.SetActive(false);
        //    allMonsterList.Add(tmpobj);
        //}

        //StartCoroutine(GenerateMonster());
        SetDungeonInfo();
    }

    public void SetDungeonInfo()
    {
        checkDungeonNum(30);
        for (int i = 0; i < 10; i++)
        {
            tmpobj = Instantiate(monsterPrefabs[Random.Range(0, monsterPrefabs.Length)], this.transform.GetChild(0));
            objectPool.Enqueue(tmpobj.GetComponent<Monster>());
            tmpobj.SetActive(false);
            allMonsterList.Add(tmpobj);
        }
        StartCoroutine(GenerateMonster());

    }

    public void checkDungeonNum(int playerLevel)
    {
        if (playerLevel < 10)
        {
            dungeonNum = 0;
        }
        else if (playerLevel < 20)
        {
            dungeonNum = 1;
        }
        else if (playerLevel < 30)
        {
            dungeonNum = 2;
        }
        else
        {
            dungeonNum = 3;
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

    public void SetMonster()
    {
        tmpMonster = GetMonsterFromPool();
        int pos = Random.Range(0, spawnPos.Length);
        vec.x = spawnPos[pos].position.x;
        if (tmpMonster.transform.CompareTag("FlyEnemy"))
        {
            tmpMonster.SetInfo(AllFlyMonsterSprites[dungeonNum]);
            vec.y = spawnPos[pos].position.y + 1;
        }
        else if (tmpMonster.transform.CompareTag("GroundEnemy"))
        {
            tmpMonster.SetInfo(AllGroundMonsterSprites[dungeonNum]);
            vec.y = spawnPos[pos].position.y;
        }

        tmpMonster.transform.position = vec;
        tmpMonster.gameObject.SetActive(true);

    }
    IEnumerator GenerateMonster()
    {
        while (true)
        {
            generateTime = Random.Range(minGenerateTime, maxGenerateTime);
            yield return new WaitForSeconds(generateTime);
            SetMonster();
        }
    }

}
