using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TitleEvent : MonoBehaviour
{
    // 이유 : 번갈아가면서 보이도록 하는 애니메이션
    // public GameObject TitleImage1;
    public GameObject TitleImage2;
    public float switchDelay = 0.5f; 
    
    private void Start()
    {
        // Start the coroutine when the script is first initialized
        StartCoroutine(SwitchObjects());
    }

    private IEnumerator SwitchObjects()
    {
        while (true)
        {
            // TitleImage1.SetActive(false);
            TitleImage2.SetActive(true);
            yield return new WaitForSeconds(switchDelay);
            // TitleImage1.SetActive(true);
            TitleImage2.SetActive(false);
            yield return new WaitForSeconds(switchDelay);
        }
    }

}
