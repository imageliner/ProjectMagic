using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager singleton;

    [SerializeField] private AudioSource _audio;

    public AudioMixer mixer;

    [Header("General Audio")]
    public AudioClip sfx_Pickup;
    public AudioClip sfx_Confirm;
    public AudioClip sfx_Deny;
    public AudioClip sfx_Equip;
    public AudioClip sfx_Unequip;
    public AudioClip sfx_Click;
    public AudioClip sfx_UseItem;
    public AudioClip sfx_Death;
    public AudioClip sfx_Teleport;
    public AudioClip sfx_Cast;
    public AudioClip sfx_Dash;

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
    }

    private void Start()
    {
        float master = SaveManager.singleton.data.volume_Master;
        float music = SaveManager.singleton.data.volume_Music;
        float sfx = SaveManager.singleton.data.volume_SFX;

        mixer.SetFloat("volume_Master", Mathf.Log10(master) * 20f);
        mixer.SetFloat("volume_Music", Mathf.Log10(music) * 20f);
        mixer.SetFloat("volume_SFX", Mathf.Log10(sfx) * 20f);
    }

    public void PlayAudio(AudioClip clip)
    {
        AudioSource src = Instantiate(_audio);
        src.clip = clip;
        float clipLength = src.clip.length;
        src.Play();
        Destroy(src.gameObject, clipLength);
    }

    public void SlowMusicPitch()
    {
        StartCoroutine(LerpMusicPitch(0.1f, 1.0f));
    }

    public void ReturnMusicPitch()
    {
        StartCoroutine(LerpMusicPitch(1.0f, 1.0f));
    }

    public IEnumerator LerpMusicPitch(float target, float duration)
    {
        mixer.GetFloat("pitch_Music", out float start);

        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float pitch = Mathf.Lerp(start, target, t/duration);
            mixer.SetFloat("pitch_Music", pitch);
            yield return null;
        }

        mixer.SetFloat("pitch_Music", target);
    }
}
