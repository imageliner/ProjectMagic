using UnityEngine;
using UnityEngine.UI;

public class UIOptionsMenu : MonoBehaviour
{
    public Slider masterVolSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (SoundManager.singleton == null)
            return;

        SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Confirm);

        LoadSliderValues();

        masterVolSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void OnDisable()
    {
        masterVolSlider.onValueChanged.RemoveAllListeners();
        musicSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();
    }

    private void LoadSliderValues()
    {
        float value;

        if (SoundManager.singleton.mixer.GetFloat("volume_Master", out value))
            masterVolSlider.value = Mathf.Pow(10, value / 20f);

        if (SoundManager.singleton.mixer.GetFloat("volume_Music", out value))
            musicSlider.value = Mathf.Pow(10, value / 20f);

        if (SoundManager.singleton.mixer.GetFloat("volume_SFX", out value))
            sfxSlider.value = Mathf.Pow(10, value / 20f);
    }

    public void SetMasterVolume(float volume)
    {
        if (masterVolSlider.value < 0.01f)
        {
            volume = -100;
        }
        SoundManager.singleton.mixer.SetFloat("volume_Master", Mathf.Log10(volume) * 20);
        SaveManager.singleton.data.volume_Master = volume;
        SaveManager.singleton.SaveData();
    }

    public void SetMusicVolume(float volume)
    {
        if (musicSlider.value < 0.01f)
        {
            volume = -100;
        }
        SoundManager.singleton.mixer.SetFloat("volume_Music", Mathf.Log10(volume) * 20);
        SaveManager.singleton.data.volume_Music = volume;
        SaveManager.singleton.SaveData();
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxSlider.value < 0.01f)
        {
            volume = -100;
        }
        SoundManager.singleton.mixer.SetFloat("volume_SFX", Mathf.Log10(volume) * 20);
        SaveManager.singleton.data.volume_SFX = volume;
        SaveManager.singleton.SaveData();
    }

    public void CloseOptions()
    {
        SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Deny);
        gameObject.SetActive(false);
    }
}
