using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NewLife : MonoBehaviour
{
    public GameObject Character_Human;
    public GameObject Character_Ryu;
    public Text InputText;
    public GameObject OkDialog;
    public GameObject NotDialog;
    private string OkName;
    
    private Dictionary<AllEnum.JobType, int> jobTypeValues = new Dictionary<AllEnum.JobType, int>();
    
    public NewLife()
    {
        foreach (AllEnum.JobType jobType in Enum.GetValues(typeof(AllEnum.JobType)))
        {
            jobTypeValues.Add(jobType, 0);
        }
    }
    
    public void SeleteCharacterHuman()
    {
        Character_Human.SetActive(true);
        if (Character_Ryu.activeSelf)
        {
            Character_Ryu.SetActive(false);
        }
        jobTypeValues[AllEnum.JobType.Ryu] = 0;
        jobTypeValues[AllEnum.JobType.Human] += 1;
    }

    public void SeleteCharacterRyu()
    {
        Character_Ryu.SetActive(true);
        if (Character_Human.activeSelf)
        {
            Character_Human.SetActive(false);
        }
        jobTypeValues[AllEnum.JobType.Human] = 0;
        jobTypeValues[AllEnum.JobType.Ryu] += 1;
    }

    public void OkButton()
    {
        if (String.IsNullOrEmpty(InputText.text))
        {
            Debug.Log("내용확인 = "+InputText.text);
            NotDialog.SetActive(true);
            Debug.Log("캐릭터 생성 실패");
            Invoke("NotOkDialogDisappear",2f);  
            
        }
        else
        {
            Debug.Log("내용확인 = "+InputText.text);
            OkDialog.SetActive(true);
            Debug.Log("캐릭터 생성 성공");
            OkName = InputText.text;
            Invoke("OKDialogDisappear",2f);
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
