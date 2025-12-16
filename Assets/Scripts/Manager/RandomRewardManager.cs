using Unity.VisualScripting;
using UnityEngine;

public class RandomRewardManager : MonoBehaviour
{
    public static RandomRewardManager singleton;

    [SerializeField] private GameObject canvas;
    [SerializeField] private UIRandomReward[] rewardContainerPrefab;
    [SerializeField] private RandomReward randomReward;

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
        DisableUI();
    }

    public RewardEntry GetRandomReward()
    {
        RewardEntry entry = new RewardEntry();

        float ranValue = Random.Range(0, 100);

        if (ranValue >= 50)
        {
            entry.isAbility = true;
            entry.ability = randomReward.randomAbility[Random.Range(0, randomReward.randomAbility.Length)];
        }
        else
        {
            entry.isAbility = false;
            entry.item = randomReward.randomItem[Random.Range(0, randomReward.randomItem.Length)];
        }

        return entry;
    }

    public void EnableUI()
    {
        canvas.SetActive(true);

        foreach (var reward in rewardContainerPrefab)
            reward.gameObject.SetActive(false);

        int count = Random.Range(1, rewardContainerPrefab.Length + 1);

        for (int i = 0; i < count; i++)
        {
            rewardContainerPrefab[i].gameObject.SetActive(true);
        }

        SoundManager.singleton.SlowMusicPitch();
        Time.timeScale = 0.0f;
    }

    public void DisableUI()
    {
        Time.timeScale = 1.0f;
        SoundManager.singleton.ReturnMusicPitch();
        canvas.SetActive(false);
    }
}
public class RewardEntry
{
    public bool isAbility;
    public CharacterAbility ability;
    public InventoryItem item;
}
