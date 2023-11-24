using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : Singleton<PlayerManager>
{
    public Player Warrior;      //전사
    public Player Wizard;       //마법사(드래곤)
    //public PlayerSkill skill;
}
