using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : Singleton<PlayerManager>
{
    public bool InPotal = false;
    public Player player;

    //public PlayerSkill skill;



    public Vector3 GetPlayerPosition()
    {
        return player.transform.position  - Vector3.up * 1.5f;
    }


}
