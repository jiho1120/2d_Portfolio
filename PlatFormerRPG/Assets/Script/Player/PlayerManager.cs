using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : Singleton<PlayerManager>
{

    public Player Warrior;
    public Player Wizard;
    // 밑에 플레이어는 지우지 말아주세요 테스트용입니다.
    public Player player;
    //public PlayerSkill skill;



    public Vector3 GetPlayerPosition()
    {
        return player.transform.position  - Vector3.up * 1.5f;
    }
    

}
