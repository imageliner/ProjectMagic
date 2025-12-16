using UnityEngine;

public class Room_Generator : MonoBehaviour
{
    private BoxCollider box;
    public Bounds Bounds => box.bounds;

    public int doorAmount;

    public DoorPoint[] _points;

    [SerializeField] private DoorPoint socket_north;
    [SerializeField] private DoorPoint socket_south;
    [SerializeField] private DoorPoint socket_east;
    [SerializeField] private DoorPoint socket_west;

    public bool isStartRoom;

    [SerializeField] private EnemyGroupManager spawner;

    public bool isBossRoom;
    public int roomLevel;
    public int roomSpawnAmount;

    [Header("Decorations")]
    [SerializeField] private Transform[] floorTiles;
    [SerializeField] private GameObject[] groundDeco;


    private void Awake()
    {
        _points = new DoorPoint[] { socket_north, socket_south, socket_east, socket_west };
        box = GetComponent<BoxCollider>();
    }

    public void SetActive()
    {
        spawner.ActivateRoom();
        GenerateDeco();
    }

    public void SetUnusedToWall()
    {
        foreach (DoorPoint point in _points)
        {
            if (point != null && !point.occupied)
                point.SetDoor(false);
        }
    }

    public void SetRoomSpawner(int level, int amount)
    {
        if (amount == 1 && !isBossRoom)
        {
            spawner.hasMiniboss = true;
        }
        spawner.groupLevel = level;
        spawner.amount = amount;
    }

    private void GenerateDeco()
    {
        foreach (Transform tile in floorTiles)
        {
            Vector3 decoPos = new Vector3(Random.Range(tile.position.x - 0.5f, tile.position.x + 0.5f), 0.05f,
                Random.Range(tile.position.z - 0.5f, tile.position.z + 0.5f));

            Quaternion decoRot = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            float chanceToSpawn = Random.Range(0, 10);
            if (chanceToSpawn > 5)
            {
                Instantiate(groundDeco[Random.Range(0, groundDeco.Length)], decoPos, decoRot, tile);
            }
        }
    }
}
