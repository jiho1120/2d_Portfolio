using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitEvent : MonoBehaviour
{
    public GameObject RealBtn;
    
   /*
    * 설명 : 나가기 버튼 클릭시 알림창으로 정말 종료할것인지 판별을 요청을 시킨다.
    */
  

   public void FirstMainExitButtonClick()
   {
       RealBtn.SetActive(true);
   }

   private void Update()
   {
       if (!RealBtn.activeSelf && Input.GetKeyDown(KeyCode.Escape))
       {
           RealBtn.SetActive(true);
       }
       if (Input.GetKeyDown(KeyCode.Escape))
       {
           RealBtn.SetActive(false);
       }

       
   }
   
   
}
