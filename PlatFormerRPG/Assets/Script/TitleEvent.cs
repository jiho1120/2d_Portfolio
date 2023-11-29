using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleEvent : MonoBehaviour
{
    // 이유 : 번갈아가면서 보이도록 하는 애니메이션
    // 스프라이트가 아닌 이미지로 하는것이 좋다. = 로딩 화면
    // public GameObject TitleImage1;
    public GameObject TitleImage2;
    public GameObject LodingDisPlay;
    public float switchDelay = 0.5f;
    public float LodingTime = 3.0f;
    private SpriteRenderer sp;
    private Color color = Color.white;
    private string secenName = "NewUserLife"; // 이동시킬 씬 이름
    
    private void Start()
    {
        // UIManager.Instance.OffUiScript();
        LodingDisPlay.SetActive(false);
        sp = LodingDisPlay.transform.GetComponent<SpriteRenderer>();
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

    

    public void GameStart()
    {
        StartCoroutine(OKDialogDisappear());
        Debug.Log("정상적으로 씬이동을 완료하였습니다. : GameStart()");
    }
    
    IEnumerator OKDialogDisappear()
    {
        LodingDisPlay.SetActive(true);
        color.a = 0;
        sp.color = color;
        while (color.a < 1)
        {
            color.a += 0.1f;
            sp.color = color;
            yield return new WaitForSeconds(0.5f);
        }
        
        SceneManager.LoadScene(secenName);
    }

}
