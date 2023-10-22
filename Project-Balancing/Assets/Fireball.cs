using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float moveSpeed = 5f;

    public int damage = 5;

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyHealthManager>() || other.GetComponent<Pickup>())
        {
            return;
        }

        PlayerHealthManager playerHealth = other.GetComponent<PlayerHealthManager>();
        if (playerHealth != null )
        {
            playerHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
