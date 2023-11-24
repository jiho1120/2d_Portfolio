using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : Singleton<PlayerManager>
{
    public Player player;
    public PlayerSkill skill;

    #region ΩÃ±€≈Ê
    private static PlayerManager instance = null;
    public static PlayerManager Instance => instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion ΩÃ±€≈Ê
}
