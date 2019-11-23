using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject[] lootPool = null;
    [SerializeField] GameObject[] doors = null;
    [SerializeField] float openDoorDelay = 0.3f;
    [Tooltip("This is used to calculate camera bounds, also implies that the room is centered around its origin.")]
    [SerializeField] Vector2 cameraBounds = Vector2.zero;

    List<Enemy> enemiesInThisRoom = new List<Enemy>();

    Vector3 lastEnemyPos;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(cameraBounds.x, 0, cameraBounds.y));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EnterRoom();
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        enemiesInThisRoom.Add(enemy);
        CancelInvoke("OnRoomClear");
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemiesInThisRoom.Remove(enemy);
        if (enemiesInThisRoom.Count == 0)
        {
            lastEnemyPos = enemy.transform.position;
            Invoke("OnRoomClear", openDoorDelay);
        }
    }

    void OnRoomClear()
    {
        // Open doors
        foreach (GameObject door in doors)
        {
            if (door != null)
            {
                door.SetActive(false);
            }
        }
        // Spawn powerup
        if (lootPool.Length > 0)
        {
            GameObject loot = lootPool[Random.Range(0, lootPool.Length)];
            Instantiate(loot, lastEnemyPos, loot.transform.rotation);
        }
    }

    void EnterRoom()
    {
        Debug.Log("Entered " + name);
        Debug.Log(enemiesInThisRoom.Count);
        CameraControl.Instance.CalculateBounds(new Bounds(transform.position, new Vector3(cameraBounds.x, 0, cameraBounds.y)));
        foreach (Enemy enemy in enemiesInThisRoom)
        {
            enemy.enabled = true;
        }
        if (enemiesInThisRoom.Count > 0)
        {
            foreach (GameObject door in doors)
            {
                door.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject door in doors)
            {
                door.SetActive(false);
            }
        }
    }
}
