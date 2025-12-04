using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public GameObject menuObject;

    public HitstopManager hitstopManager;

    public PlayerCharacter player;

    public PlayerStats playerStats;
    public LevelSystem playerLevel;

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

        hitstopManager = gameObject.GetComponent<HitstopManager>();
        toggleMenu += ToggleMenu;
    }

    private void Start()
    {
        menuObject.SetActive(false);
        inMenu = false;
    }

    private void ToggleMenu()
    {
        inMenu = !inMenu;

        menuObject.SetActive(inMenu);
        
    }
}
