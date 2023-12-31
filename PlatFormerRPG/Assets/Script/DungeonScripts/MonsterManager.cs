using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;
using static Constructure;

public class MonsterManager : Singleton<MonsterManager>
{
    float generateTime = 0;
    [SerializeField]
    float minGenerateTime = 0;
    [SerializeField]
    float maxGenerateTime = 0;

    public GameObject[] monsterPrefabs; // 0이 그라운드 1이 fly
    public Sprite[] AllGroundMonsterSprites;
    public Sprite[] AllFlyMonsterSprites;

    Spawn spawn;
    GameObject tmpobj;//임시변수
    //Queue<Monster> objectPool = new Queue<Monster>();
    Dictionary<int, Queue<Monster>> monsterObjectPool = new Dictionary<int, Queue<Monster>>();

    List<GameObject> allMonsterList = new List<GameObject>();

    Coroutine cor = null;
    // Start is called before the first frame update
    //void Start()
    //{
    //    //spawn = GetComponent<Spawn>();        
    //}

    // Update is called once per frame
    public void InitSpawn()
    {
        if (spawn == null)
        {
            spawn = GameObject.Find("DungeonScript").GetComponent<Spawn>();
        }
    }

    public void SetMonsterInfo()
    {
        if (monsterObjectPool.Count == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                monsterObjectPool.Add(i, new Queue<Monster>());
                for (int j = 0; j < 10; j++)
                {
                    tmpobj = Instantiate(monsterPrefabs[i], this.transform.GetChild(0));
                    monsterObjectPool[i].Enqueue(tmpobj.GetComponent<Monster>());// Monster a = new flymonster();
                    tmpobj.SetActive(false);
                    allMonsterList.Add(tmpobj);
                }
            }
        }

    }


    public Monster GetMonsterFromPool(int monsterNum)
    {
        if (monsterObjectPool[monsterNum].Count > 0)
        {
            return monsterObjectPool[monsterNum].Dequeue();

        }
        else
        {
            tmpobj = Instantiate(monsterPrefabs[monsterNum], this.transform.GetChild(0));
            allMonsterList.Add(tmpobj);
            return tmpobj.GetComponent<Monster>();
        }
    }

    public void StartGenerateMonster(bool on)
    {
        if (on)
        {            
            if (cor == null)
            {
                cor = StartCoroutine(GenerateMonster());
            }
        }
        else
        {
            if (cor != null)
            {
                StopCoroutine(cor);
                cor = null;
            }
        }
    }
    public void AllkillMonster()
    {
        for (int i = 0; i < allMonsterList.Count; i++)
        {
            allMonsterList[i].GetComponent<Object>().kill();
            allMonsterList[i].GetComponent<Object>().isDead();
        }
    }
    IEnumerator GenerateMonster() // 몬스터 생성 시간 설정 후 스폰
    {       
        while (true)
        {
            generateTime = Random.Range(minGenerateTime, maxGenerateTime);
            yield return new WaitForSeconds(generateTime);
            spawn.SetMonsterSpawnPos();
        }
    }
    public IEnumerator GenerateBullet() // 총알 생성 시간 설정 후 스폰
    {
        while (true)
        {
            StartCoroutine(spawn.SetBulletSpawnPos());
            //spawn.SetBulletSpawnPos();
            generateTime = 40f;
            yield return new WaitForSeconds(generateTime);
        }
    }
}
