using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NewLife : MonoBehaviour
{
    public GameObject Character_Human;
    public GameObject Character_Ryu;
    public Text InputText;
    public GameObject OkDialog;
    public GameObject NotDialog;
    private string OkName;
    
    //private Dictionary<AllEnum.JobType, int> jobTypeValues = new Dictionary<AllEnum.JobType, int>();
    //private AllEnum.JobType jobtype;
    public void SeleteCharacterHuman()
    {
        Character_Human.SetActive(true);
        if (Character_Ryu.activeSelf)
        {
            Character_Ryu.SetActive(false);
        }

        //jobtype = AllEnum.JobType.Human;
    }

    public void SeleteCharacterRyu()
    {
        Character_Ryu.SetActive(true);
        if (Character_Human.activeSelf)
        {
            Character_Human.SetActive(false);
        }

        //jobtype = AllEnum.JobType.Ryu;
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
            // 플레이어 매니져에 데이터를 보낼 코드위치
            // 이름, 직업만 보냄
            // 매개변수로 이름 직업 보내야됨
            Invoke("OKDialogDisappear",2f);
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
