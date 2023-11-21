using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public MapCon dungeonSecene { get; private set; }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetDungeonInfo()
    {
        dungeonSecene = GameObject.FindObjectOfType<MapCon> ();
        DungeonManager.instance.checkDungeonNum(30);
    }
}
