using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : Singleton<PlayerManager>
{
    public bool InPotal = false;
    public PlayerBullet[] playerbullet;       //플레이어 원거리 공격

    public Constructure.Stat defaultStats;

    // 밑에 플레이어는 지우지 말아주세요 테스트용입니다.
    public Player player;
    public GameObject WarriorPlayer;
    public GameObject WizaldPlayer;

    public void NewCharacter(AllEnum.Type characterType)
    {
        if (characterType == AllEnum.Type.Warrior)
        {
            player = Instantiate(WarriorPlayer, Vector3.zero, Quaternion.identity, transform).GetComponent<Player>();
        }
        else if (characterType == AllEnum.Type.Dragon)
        {
            player = Instantiate(WizaldPlayer, Vector3.zero, Quaternion.identity, transform).GetComponent<Player>();
        }

        // 스탯 초기화 레벨 1기준
        defaultStats.Level = 1;
        defaultStats.MaxExpVal = 5; // 레벨 1에서 2되는 5 이후로 레벨x 5의 조건으로
        defaultStats.Att = 5;
        defaultStats.HP = 50;
        defaultStats.MaxHP = 50;
        defaultStats.ExpVal = 0;
        defaultStats.Skill = 10;
    }

    public Vector3 GetPlayerPosition()
    {
        return player.transform.position - Vector3.up * 1.5f;
    }
}
