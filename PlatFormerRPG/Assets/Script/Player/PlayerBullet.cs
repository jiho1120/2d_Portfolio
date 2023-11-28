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
                //if(Vector2.Distance(transform.position, /*Ÿ��������*/))
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

    //bullet ����
    public void SetInfo(Vector2 myPosition, Vector2 targetPos)
    {
        transform.position = myPosition;
        //this.targetPos = targetPos;       //���� Ÿ��
    }

    private void FixedUpdate()
    {
        //���� ������
        if (PlayerManager.Instance.player.isWar == true)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        //������� => �÷��̾� ����ٴϴ� ���� ����..�Ф�
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

    //���̾ ����(=>���� �� Ȱ��ȭ, ��Ȱ��ȭ�� ���� ����)
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
                    hit.Hit(PlayerManager.Instance.player.Attak(), transform.position);
                }
                Debug.Log("���Ϳ� ����");       //Ȯ�ο�
            }
            //������ ����X
            else
            {
                if (hit != null)
                {
                    hit.Hit(PlayerManager.Instance.player.Attak(), transform.position);
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
