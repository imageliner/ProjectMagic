using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    public void StartGame()
    {
        SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Confirm);
        //SceneManager.LoadScene(1);
        LoadingScreenManager.singleton.LoadScene(1);
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
    }

    public void MainMenuQuit()
    {
        Application.Quit();
    }
}
