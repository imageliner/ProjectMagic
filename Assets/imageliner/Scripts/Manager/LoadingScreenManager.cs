using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager singleton;

    public UILoadingBar loadingBar;

    private List<InventoryItem> itemsToTransfer = new();
    private List<AbilityClass> abilitiesToTransfer = new();

    private bool genFinished;

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

        loadingBar = GetComponentInChildren<UILoadingBar>();
    }

    private void Start()
    {
        loadingBar.gameObject.SetActive(false);
        
    }

    private void OnEnable()
    {
        Dungeon_Generator.OnDungeonProgress += FillBar;
        Dungeon_Generator.OnDungeonGenerated += DungeonLoaded;
    }

    private void OnDisable()
    {
        Dungeon_Generator.OnDungeonProgress -= FillBar;
        Dungeon_Generator.OnDungeonGenerated -= DungeonLoaded;
    }

    private void DungeonLoaded()
    {
        genFinished = true;
    }

    public void FillBar(float fill)
    {
        loadingBar.SetLoadFill(fill);
    }

    public void LoadScene(int index, bool generate = false)
    {
        itemsToTransfer.Clear();
        abilitiesToTransfer.Clear();

        if (index == 1)
        {
            if (GameManager.singleton != null)
                GameManager.singleton.ResetPlayer();
        }

        if (index > 1)
        {
            SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Teleport);
            //list of inventory objects and abilities, carry over into next scene
            var inventory = FindAnyObjectByType<Inventory>();
            foreach (InventoryItem item in inventory.allItems)
            {
                if (item != null)
                {
                    itemsToTransfer.Add(item);
                    itemsToTransfer.Add(inventory.equippedWeapon);
                    itemsToTransfer.Add(inventory.equippedHelmet);
                }
            }

            var player = FindAnyObjectByType<PlayerCharacter>();
            foreach (AbilityClass ability in player.abilities)
            {
                if (ability != null)
                    abilitiesToTransfer.Add(ability);
            }
        }
        

        if (!generate)
        {
            StartCoroutine(LoadSceneAsync(index, false, true));
            return;
        }
        StartCoroutine(LoadSceneAsync(index, true));
    }

    IEnumerator LoadSceneAsync(int index, bool generate = false, bool cutscene = false)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        loadingBar.gameObject.SetActive(true);

        while (!operation.isDone)
        {
            float barvalue = Mathf.Clamp01(operation.progress / 0.9f);

            FillBar(barvalue);

            yield return null;
        }

        if (generate)
        {
            genFinished = false;
            FillBar(0);

            while (!genFinished)
            {
                yield return null;
            }
            //loading bar code here wait until gen is finished
        }

        // 1 frame delay to bug fix
        yield return null;

        GameManager.singleton.CheckReferences();
        GameManager.singleton.player = FindAnyObjectByType<PlayerCharacter>();
        if (GameManager.singleton.player != null)
            GameManager.singleton.RegisterPlayer(GameManager.singleton.player);

        GameManager.singleton.menuObject.SetActive(false);

        var inventory = FindAnyObjectByType<Inventory>();
        foreach (InventoryItem item in itemsToTransfer)
        {
            if (item != null)
                inventory.AddItem(item);
        }

        var player = FindAnyObjectByType<PlayerCharacter>();
        int count = Mathf.Min(player.abilities.Length, abilitiesToTransfer.Count);

        for (int i = 0; i < count; i++)
            player.abilities[i] = abilitiesToTransfer[i];

        yield return new WaitForSeconds(0.5f);


        loadingBar.gameObject.SetActive(false);

        if (cutscene)
        {
            if (!SaveManager.singleton.data.hasSeenIntro)
            {
                var director = FindAnyObjectByType<PlayableDirector>(FindObjectsInactive.Include);
                if (director != null)
                {
                    director.gameObject.SetActive(true);
                    director.Play();
                    SaveManager.singleton.data.hasSeenIntro = true;
                    SaveManager.singleton.SaveData();
                }
            }
            else
            {
                var director = FindAnyObjectByType<PlayableDirector>(FindObjectsInactive.Include);
                director?.gameObject.SetActive(false);
            }
                
        }

    }
}
