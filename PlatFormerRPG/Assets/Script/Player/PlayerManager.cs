using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : Singleton<PlayerManager>
{
    public bool InPotal = false;

    public AllEnum.Type CharacterType { get; private set; } = AllEnum.Type.Warrior;
    public PlayerBullet playerbullet;       //플레이어 원거리 공격
    // 밑에 플레이어는 지우지 말아주세요 테스트용입니다.
    public Player player;
    public GameObject warriorPrefab; // Reference to the Warrior prefab
    public GameObject dragonPrefab; // Reference to the Dragon prefab

    //private void Start()
    //{
    //    instance = this;
    //    warriorPrefab.SetActive(false);
    //    dragonPrefab.SetActive(false);
    //    dragonPrefab.transform.Translate(0, 0, 0);
    //    warriorPrefab.transform.Translate(0, 0, 0);
    //}

    public void SetType(AllEnum.Type type)
    {
        Debug.Log(type + " 직업이 선택되었습니다. = 플레이어 매니져");
       
        CharacterType = type;
        if (type == AllEnum.Type.Warrior)
        {
            warriorPrefab.SetActive(true);
            dragonPrefab.SetActive(false);
            // // Instantiate the Warrior prefab in the village
            // Instantiate(warriorPrefab, new Vector3(0f,1f,0f), Quaternion.identity);
        }
        //        warriorPrefab.SetActive(false);
        else if (type == AllEnum.Type.Dragon)
        {
            dragonPrefab.SetActive(true);
            warriorPrefab.SetActive(false);

            // // Instantiate the Dragon prefab in the village
            // Instantiate(dragonPrefab, new Vector3(0f,1f,0f), Quaternion.identity);

        }
    }
    public void NewCharacter(AllEnum.Type characterType)
    {
        if (characterType == AllEnum.Type.Warrior)
        {
            player = Instantiate(WarriorPlayer, Vector3.zero, Quaternion.identity, transform).GetComponent<Player>();
        }
        else if (characterType == AllEnum.Type.Dragon)
        {
            player = Instantiate(WizaldPlayer, Vector3.zero, Quaternion.identity, transform).GetComponent<Player>();

        }
    }

    public Vector3 GetPlayerPosition()
    {
        return player.transform.position - Vector3.up * 1.5f;
    }

    //public void GetName(string name)
    //{
    //    Debug.Log(name + " 라는 이름이 입력이 되었습니다. = 플레이어 매니져");
    //    UIManager.Instance.SetName(name);
    //}
}
