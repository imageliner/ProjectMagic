using UnityEngine;

public class DoorPoint : MonoBehaviour
{
    [SerializeField] private GameObject prefab_Wall;
    [SerializeField] private GameObject prefab_Door;

    public Vector2Int direction; // (0,1)=north, (1,0)=east
    public bool occupied;

    [SerializeField] private bool door;

    public GameObject spawned;

    private void Start_notUsing()
    {
        if (door)
        {
            spawned = Instantiate(prefab_Door, transform);
            return;
        }

        spawned  = Instantiate(prefab_Wall, transform);
    }

    public void SetDoor(bool value)
    {
        door = value;
        Refresh();
    }

    private void Refresh()
    {
        if (spawned != null)
            Destroy(spawned);

        spawned = Instantiate(
            door ? prefab_Door : prefab_Wall,
            transform
        );

        spawned.transform.localPosition = Vector3.zero;
        spawned.transform.localRotation = Quaternion.identity;
    }
}
