using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlocker : MonoBehaviour
{
    Door doorToUnlock;

    [Tooltip("This door will unlock if all the enemies in this list are killed")]
    public List<EnemyHealthManager> enemyHealthManagerList;

    private void Awake()
    {
        doorToUnlock = GetComponent<Door>();
    }

    private void Update()
    {
        if (CheckUnlock())
            doorToUnlock.isLocked = false;
    }

    bool CheckUnlock()
    {
        if (enemyHealthManagerList.Count == 0)
            return true;

        foreach(EnemyHealthManager enemy in enemyHealthManagerList)
        {
            if (!enemy.isDead)
                return false;
            else
                enemyHealthManagerList.Remove(enemy);
        }
        return true;
    }
}
