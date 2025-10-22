using UnityEngine;

public class MinmapController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 playerPos;

    void Start()
    {
    }

    void Update()
    {
        playerPos = player.transform.position;
        playerPos.y = 50;
        transform.position = playerPos;
    }
}
