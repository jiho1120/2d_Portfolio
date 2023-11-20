using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Application = UnityEngine.Device.Application;

public class YesAndNoEvent : MonoBehaviour
{
    /*
     * 용도 : 최종적 종료 유무에서 선택을 받으면 실행을 하기 위함
     */
    

    public GameObject ExitMain;

    public void Yes()
    {
        Debug.Log("게임을 종료합니다.");
        Application.Quit();
    }

    public void No()
    {
        ExitMain.SetActive(false);
    }
}
