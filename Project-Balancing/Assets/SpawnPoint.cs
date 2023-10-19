using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public List<GameObject> enemies;
    public int enemyIndex = 0;

    private void Start()
    {
        if (enemies.Count > 0)
            Instantiate(enemies[enemyIndex], transform.position, Quaternion.identity);
    }
}
