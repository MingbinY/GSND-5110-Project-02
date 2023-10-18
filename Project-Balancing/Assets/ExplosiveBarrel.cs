using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public GameObject explosionVFX;
    public GameObject explosionSFX;
    public float explosionRadius;
    public int explosionDamage;

    public bool onlyDamageAI = false;

    public void Explode()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider coll in colls)
        {
            if (coll.gameObject == gameObject)
                continue;
            BasicHealthManager health = onlyDamageAI ? coll.GetComponent<EnemyHealthManager>() : coll.GetComponent<BasicHealthManager>();
            if (health != null) health.TakeDamage(explosionDamage);
        }

        if (explosionVFX) Instantiate(explosionSFX, transform.position, Quaternion.identity);
        if (explosionSFX) Instantiate(explosionVFX, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
