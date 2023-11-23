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

    public GameObject[] monsterPrefabs; // 0�� �׶��� 1�� fly
    public Sprite[] AllGroundMonsterSprites;
    public Sprite[] AllFlyMonsterSprites;

    Spawn spawn;
    GameObject tmpobj;//�ӽú���
    Queue<Monster> objectPool = new Queue<Monster>();
    List<GameObject> allMonsterList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        spawn = GetComponent<Spawn>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMonsterInfo() // ���� ������ ���� �� ť�� ����Ʈ�� ��� ���� ���� ����
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
     
    public IEnumerator GenerateMonster() // ���� ���� �ð� ���� �� ����
    {
        while (true)
        {
            generateTime = Random.Range(minGenerateTime, maxGenerateTime);
            yield return new WaitForSeconds(generateTime);
            spawn.SetMonsterSpawnPos();
        }
    }
}
