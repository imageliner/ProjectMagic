using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public GameObject menuObject;

    public HitstopManager hitstopManager;

    public PlayerCharacter player;

    public PlayerStats playerStats;
    public LevelSystem playerLevel;

    [SerializeField] private UILoadingBar loadingBar;

    public UIAbility abilityUI;

    public bool inMenu;
    public Action toggleMenu;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = 60;

        CheckReferences();

        toggleMenu += ToggleMenu;
    }

    private void Start()
    {
        abilityUI = FindAnyObjectByType<UIAbility>();
        //loadingBar.gameObject.SetActive(false);
        menuObject.SetActive(false);
        inMenu = false;
    }

    public void QuitGame()
    {
        SaveManager.singleton.SaveData();
        Application.Quit();
    }

    public void ShowTutorial()
    {
        FindAnyObjectByType<UIPlayerHUD>().EnableTutorial();
    }

    public void RegisterPlayer(PlayerCharacter p)
    {
        if (player != null)
        {
            player = p;

            abilityUI.Bind(p);
        }
    }

    public void CheckReferences()
    {
        if (hitstopManager == null)
            hitstopManager = FindAnyObjectByType<HitstopManager>();

        if (menuObject == null)
            menuObject = FindAnyObjectByType<UIInventory>().gameObject;

        if (player == null)
            player = FindAnyObjectByType<PlayerCharacter>();

        if (playerStats == null)
            playerStats = FindAnyObjectByType<PlayerStats>();

        if (playerLevel == null)
            playerLevel = FindAnyObjectByType<LevelSystem>();

    }


    public void GameFailed()
    {
        FindAnyObjectByType<UIResultScreens>().GameLoseScreen();
    }


    public void GameWin()
    {
        FindAnyObjectByType<UIResultScreens>().GameWinScreen();
    }
    
    public void ResetPlayer()
    {
        if (playerLevel != null)
        {
            playerLevel.ResetLevel();
            playerStats.ResetStats();
            var inv = FindAnyObjectByType<Inventory>();
            inv.ResetInventory();
            FindAnyObjectByType<PlayerCharacter>().CheckLowHP();
        }   
    }

    private void ToggleMenu()
    {
        if (menuObject != null)
        {
            inMenu = !inMenu;

            menuObject.SetActive(inMenu);

            if (inMenu)
                SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Equip);
            if (!inMenu)
                SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Unequip);
        }        
    }
}
