using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public HitstopManager hitstopManager;

    public PlayerCharacter player;

    public PlayerStats playerStats;
    public LevelSystem playerLevel;

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
    }
}
