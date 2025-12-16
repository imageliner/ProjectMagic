using UnityEngine;

public class UIResultScreens : MonoBehaviour
{
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject WinScreen;

    private void Start()
    {
        DisableAll();
    }

    public void DisableAll()
    {
        loseScreen.SetActive(false);
        WinScreen.SetActive(false);
    }

    public void GameLoseScreen()
    {
        SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Confirm); // win sfx?
        WinScreen.SetActive(false);
        loseScreen.SetActive(true);
    }

    public void GameWinScreen()
    {
        SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Confirm); //lose sfx?
        loseScreen.SetActive(false);
        WinScreen.SetActive(true);
    }

    public void LeaveDungeon()
    {
        SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Confirm);
        DisableAll();
        LoadingScreenManager.singleton.LoadScene(1);
    }
}
