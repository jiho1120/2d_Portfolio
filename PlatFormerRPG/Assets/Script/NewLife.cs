using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * 용도 : 캐릭터를 새로 생성하여 싱글톤을 통해서 보냄
 */
public class NewLife : MonoBehaviour
{
    public GameObject Character_Human;
    public GameObject Character_Ryu;
    public Text InputText;
    public GameObject OkDialog;
    public GameObject NotDialog;
    public GameObject SeletedWarrior;
    public GameObject SeletedDragon;
    private string OkName;
    public Text testText;
    
    //private Dictionary<AllEnum.JobType, int> jobTypeValues = new Dictionary<AllEnum.JobType, int>();
    public static AllEnum.Type jobtype;

    private void Start()
    {
        UIManager.Instance.OffUiScript();
    }

    public void SeleteCharacterHuman()
    {
        Character_Human.SetActive(true);
        if (Character_Ryu.activeSelf)
        {
            Character_Ryu.SetActive(false);
        }

        jobtype = AllEnum.Type.Warrior;
        SeletedWarrior.SetActive(true);
        Invoke("SeletedWarriorActive",1f);  
        
    }
    public void SeletedWarriorActive()
    {
        SeletedWarrior.SetActive(false);
        
    }

    
    public void SeleteCharacterRyu()
    {
        Character_Ryu.SetActive(true);
        if (Character_Human.activeSelf)
        {
            Character_Human.SetActive(false);
        }

        jobtype = AllEnum.Type.Dragon;
        SeletedDragon.SetActive(true);
        Invoke("SeletedDragonActive",0.3f);  
        
    }
    
    public void SeletedDragonActive()
    {
        SeletedDragon.SetActive(false);
        
        
    }

    

    public void OkButton()
    {
        if (String.IsNullOrEmpty(InputText.text))
        {
            NotDialog.SetActive(true);
            Invoke("NotOkDialogDisappear",2f);  
            
        }
        else
        {
            OkDialog.SetActive(true);
            OkName = InputText.text;
            UIManager.Instance.SetName(OkName);
            UIManager.Instance.SetType(jobtype);
            PlayerManager.Instance.NewCharacter(jobtype); // 생성시킴
            Invoke("OKDialogDisappear",2f);
            UIManager.Instance.OnUiScript();
            SceneManager.LoadScene("VillageScene");
        }
    }

    public void OKDialogDisappear()
    {
        OkDialog.SetActive(false);
    }

    public void NotOkDialogDisappear()
    {
        NotDialog.SetActive(false); 
    }
    
}
