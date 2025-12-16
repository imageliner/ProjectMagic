using UnityEngine;

public class UIDungeonMenu : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;

    private void Start()
    {
        DisableUI();
    }

    public void OnLevelWasLoaded(int level)
    {
        DisableUI();
    }

    public void OnEnterClicked()
    {
        //DisableUI();
        SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Confirm);

        LoadingScreenManager.singleton.LoadScene(2, true);
    }

    public void EnableUI()
    {
        uiPanel.SetActive(true);
    }

    public void DisableUI()
    {
        uiPanel.SetActive(false);
    }
}
