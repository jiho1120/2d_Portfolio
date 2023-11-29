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
        //���� ������
        if (PlayerManager.Instance.player.isWar == true)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        //�������
        else
        {
            transform.Translate(vecR * speed * Time.deltaTime);
        }
    }

    //���̾ ����
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {        
        //���Ϳ� ����� �� �ǰ�
        if(collision.gameObject.CompareTag("GroundEnemy") || collision.gameObject.CompareTag("FlyEnemy") || collision.gameObject.CompareTag("Boss"))
        {
            IHit hit = collision.GetComponent<IHit>();

            //���� ��ų ����
            if (PlayerManager.Instance.player.isWar == true)
            {
                if (hit != null)
                {
                    //��ų
                    if (PlayerManager.Instance.player.useSkill == true)
                    {
                        hit.Hit(PlayerManager.Instance.player.Skill(), transform.position);
                    }
                    else
                    {
                        hit.Hit(PlayerManager.Instance.player.Attak(), transform.position);
                    }
                }
                Debug.Log("���Ϳ� ����");       //Ȯ�ο�
            }
            //������ ����X
            else
            {
                if (hit != null)
                {
                    //��ų(ȭ�� ���� ������)
                    if (PlayerManager.Instance.player.useSkill == true)
                    {
                        hit.Hit(PlayerManager.Instance.player.WizSkill(), transform.position);
                    }
                    //�Ϲ� ����
                    else
                    {
                        hit.Hit(PlayerManager.Instance.player.Attak(), transform.position);
                    }
                }
                Debug.Log("���Ϳ� ����");       //Ȯ�ο�
                DestroyBullet();
            }
        }
        //���� ����� �� ����
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("���� ����");           //Ȯ�ο�
            DestroyBullet();
        }
    }
}
