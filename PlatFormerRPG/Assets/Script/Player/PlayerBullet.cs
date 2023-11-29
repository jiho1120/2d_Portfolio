using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 0;

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
            Invoke("DestroyBullet", 0.8f);
        }
    }

    public void SetDir(Vector3 vec)
    {
        transform.localScale = vec;
        vecR.x = vec.x;
    }

    private void FixedUpdate()
    {
        //만약 전사라면
        if (PlayerManager.Instance.player.isWar == true)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        //마법사면
        else
        {
            transform.Translate(vecR * speed * Time.deltaTime);
        }
    }

    //파이어볼 삭제
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
                    //스킬
                    if (PlayerManager.Instance.player.useSkill == true)
                    {
                        hit.Hit(PlayerManager.Instance.player.Skill(), transform.position);
                    }
                    else
                    {
                        hit.Hit(PlayerManager.Instance.player.Attak(), transform.position);
                    }
                }
                Debug.Log("몬스터에 맞음");       //확인용
            }
            //마법사 관통X
            else
            {
                if (hit != null)
                {
                    //스킬(화염 지속 데미지)
                    if (PlayerManager.Instance.player.useSkill == true)
                    {
                        hit.Hit(PlayerManager.Instance.player.WizSkill(), transform.position);
                    }
                    //일반 공격
                    else
                    {
                        hit.Hit(PlayerManager.Instance.player.Attak(), transform.position);
                    }
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
