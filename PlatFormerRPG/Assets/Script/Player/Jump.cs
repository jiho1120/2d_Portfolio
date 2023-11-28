using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //¶¥°ú ´ê¾ÒÀ» ¶§
        if (collider.gameObject.CompareTag("Ground"))
        {

            PlayerManager.Instance.player.jumpCount = 0;
            PlayerManager.Instance.player.rigid.velocity = Vector2.zero;      //¹Ì²ô·³¹æÁö
        }
    }
}
