using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : Singleton<GameManager>
{
    GameObject Warrior;
    GameObject Wizald;

    public GameObject WarriorPlayer;
    public GameObject WizaldPlayer;
    
    Vector3 vec = Vector3.zero;

    public void NewCharacter(AllEnum.Type characterType)
    {
        if (characterType == AllEnum.Type.Warrior)
        {
            Warrior = Instantiate(WarriorPlayer, vec, Quaternion.identity,transform);
        }
        
        else if (characterType == AllEnum.Type.Dragon)
        {
            Wizald = Instantiate(WizaldPlayer, vec, Quaternion.identity,transform);
        }
    }

    
    
}
