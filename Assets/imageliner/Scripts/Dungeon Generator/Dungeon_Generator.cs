using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon_Generator : MonoBehaviour
{
    public Room_Generator startRoomPrefab;
    public Room_Generator[] roomPrefabs;
    public Room_Generator bossRoomPrefab;
    public int maxRooms = 10;

    private List<Room_Generator> spawnedRooms = new();
    private List<DoorPoint> openDoors = new();

    public static Action<float> OnDungeonProgress;
    public static Action OnDungeonGenerated;

    private void Start()
    {
        Room_Generator startRoom = Instantiate(startRoomPrefab, Vector3.zero, Quaternion.identity);
        spawnedRooms.Add(startRoom);

        foreach (var point in startRoom._points)
        {
            if (point != null)
                openDoors.Add(point);
        }

        StartCoroutine(StartGenerationAsync());
    }

    private Room_Generator PickRoomPrefab()
    {
        if (spawnedRooms.Count == maxRooms - 1)
        {
            return bossRoomPrefab;
        }
        return roomPrefabs[UnityEngine.Random.Range(0, roomPrefabs.Length)];
    }

    Vector2Int Opposite(Vector2Int dir)
    {
        return new Vector2Int(-dir.x, -dir.y);
    }

    private DoorPoint FindOppositeDoor(Room_Generator room, Vector2Int originDir)
    {
        Vector2Int needed = Opposite(originDir);

        foreach (DoorPoint point in room._points)
        {
            if (point == null)
                continue;

            if (point.direction == needed)
                return point;
        }

        return null;
    }

    void AlignRooms(DoorPoint from, DoorPoint to)
    {
        Vector3 offset = from.transform.position - to.transform.position;
        to.transform.root.position += offset;
    }

    private bool Overlaps(Room_Generator candidate)
    {
        Bounds candidateBounds = candidate.GetComponent<BoxCollider>().bounds;

    foreach (Room_Generator room in spawnedRooms)
    {
        if (room == candidate)
            continue;

        Bounds otherBounds = room.GetComponent<BoxCollider>().bounds;

        if (candidateBounds.Intersects(otherBounds))
            return true;
    }

    return false;
    }

    public IEnumerator StartGenerationAsync()
    {
        const int MAX_ATTEMPTS = 20;

        while (spawnedRooms.Count < maxRooms && openDoors.Count > 0)
        {
            DoorPoint originDoor = openDoors[UnityEngine.Random.Range(0, openDoors.Count)];
            openDoors.Remove(originDoor);

            bool placed = false;
            int attempts = 0;

            while (!placed && attempts < MAX_ATTEMPTS)
            {
                attempts++;

                Room_Generator newRoom = Instantiate(PickRoomPrefab());

                DoorPoint matchingDoor = FindOppositeDoor(newRoom, originDoor.direction);

                if (matchingDoor == null)
                {
                    Destroy(newRoom.gameObject);
                    continue;
                }

                AlignRooms(originDoor, matchingDoor);

                yield return null;
                //yield return new WaitForSeconds(0.1f);

                if (Overlaps(newRoom))
                {
                    Destroy(newRoom.gameObject);
                    
                    continue; 
                }

                originDoor.occupied = true;
                matchingDoor.occupied = true;

                originDoor.SetDoor(true);
                matchingDoor.SetDoor(true);

                int randomEnemyCount = Mathf.Clamp(UnityEngine.Random.Range(1, 6), 1, 50);
                if (newRoom.isBossRoom)
                    randomEnemyCount = 1;

                int newRoomLevel = Mathf.Clamp(spawnedRooms.Count, 1, 6);
                newRoom.roomLevel = newRoomLevel;
                newRoom.SetRoomSpawner(newRoomLevel, randomEnemyCount);
                spawnedRooms.Add(newRoom);

                foreach (DoorPoint point in newRoom._points)
                {
                    if (point != null && !point.occupied)
                        openDoors.Add(point);
                }

                placed = true;

                newRoom.SetActive();
                
            }
            if (!placed)
            {
                originDoor.SetDoor(false);
            }
            // debug yield to see generation
            //yield return new WaitForSeconds(0.1f);
            yield return null;
            OnDungeonProgress?.Invoke((float)spawnedRooms.Count / maxRooms);
        }

        foreach (Room_Generator room in spawnedRooms)
        {
            room.SetUnusedToWall();
        }

        OnDungeonGenerated?.Invoke();
    }
}
