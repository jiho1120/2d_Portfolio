using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 0;
    float distance = 0;

    Vector3 scaleVec = Vector3.one;
    Vector2 vecR = Vector2.right;

    void Start()
    {
        if(PlayerManager.Instance.player.isWar == true)
        {
            Invoke("DestroyBullet", 1.2f);
        }
        else
        {
            if(PlayerManager.Instance.player.useSkill == true)
            {
                //if(Vector2.Distance(transform.position, /*타겟포지션*/))
                //{
                //    Invoke("DestroyBullet", 1f);
                //}
            }
            else
            {
                Invoke("DestroyBullet", 0.8f);
            }
        }
    }

    //bullet 세팅
    public void SetInfo(Vector2 myPosition, Vector2 targetPos)
    {
        transform.position = myPosition;
        //this.targetPos = targetPos;       //몬스터 타겟
    }

    private void FixedUpdate()
    {
        //만약 전사라면
        if (PlayerManager.Instance.player.isWar == true)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        //마법사면 => 플레이어 따라다니는 문제 있음..ㅠㅜ
        else
        {
            scaleVec.x = PlayerManager.Instance.player.transform.localScale.x;
            if (scaleVec.x == 1)
            {
                transform.Translate(vecR * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(vecR * -1 * speed * Time.deltaTime);
            }
            transform.localScale = scaleVec;
        }
    }

    //파이어볼 삭제(=>생성 후 활성화, 비활성화로 변경 예정)
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {        
        //몬스터와 닿았을 때 피격
        if(collision.gameObject.CompareTag("GroundEnemy") || collision.gameObject.CompareTag("FlyEnemy") || collision.gameObject.CompareTag("Boss"))
        {
            IHit hit = collision.GetComponent<IHit>();

            //전사 스킬 관통
            if (PlayerManager.Instance.player.isWar == true)
            {
                if (hit != null)
                {
                    hit.Hit(PlayerManager.Instance.player.Attak(), transform.position);
                }
                Debug.Log("몬스터에 맞음");       //확인용
            }
            //마법사 관통X
            else
            {
                if (hit != null)
                {
                    hit.Hit(PlayerManager.Instance.player.Attak(), transform.position);
                }
                Debug.Log("몬스터에 맞음");       //확인용
                DestroyBullet();
            }
        }
        //땅에 닿았을 때 삭제
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("땅에 맞음");           //확인용
            DestroyBullet();
        }
    }
}
