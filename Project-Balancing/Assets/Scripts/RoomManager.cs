using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<Door> doors;
    public List<SpawnPoint> spawnPoints;
    [SerializeField] List<EnemyHealthManager> enemyHealths;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();
        doors = GetComponentsInChildren<Door>().ToList();
        foreach (SpawnPoint p in spawnPoints)
        {
            GameObject go = Instantiate(p.gameObjectToSpawn[p.objectIndex], p.transform.position, Quaternion.identity);
            if (go.GetComponent<EnemyHealthManager>() != null )
            {
                enemyHealths.Add(go.GetComponent<EnemyHealthManager>());
            }
        }
    }

    private void Update()
    {
        if (CheckUnlock())
            UnlockDoors();
    }

    void UnlockDoors()
    {
        foreach (Door door in doors)
        {
            door.isLocked = false;
        }
    }

    bool CheckUnlock()
    {
        if (enemyHealths.Count == 0)
            return true;

        foreach (EnemyHealthManager enemy in enemyHealths)
        {
            if (!enemy.isDead)
                return false;
            else
                enemyHealths.Remove(enemy);
        }
        return true;
    }
}
