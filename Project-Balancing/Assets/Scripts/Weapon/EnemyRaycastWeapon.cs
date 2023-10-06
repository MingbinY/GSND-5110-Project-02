using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaycastWeapon : MonoBehaviour
{
    public int damage = 10;
    public GameObject agent;

    public bool addBulletSpread = true;
    public Vector3 bulletSpreadVariance = Vector3.zero;

    public float fireInterval = 0.5f;

    public Transform muzzle;

    public ParticleSystem impactEffect;
    public ParticleSystem[] muzzleFlash;

    public TrailRenderer tracerEffect;

    private float lastShootTime = 0;

    private void Awake()
    {
        agent = GetComponentInParent<AiAgent>().gameObject;
    }

    public virtual void Fire()
    {
        if (Time.time < lastShootTime + fireInterval) return;

        lastShootTime = Time.time;
        foreach (var particle in muzzleFlash) particle.Emit(1);
        var tracer = Instantiate(tracerEffect, muzzle.position, Quaternion.identity);
        tracer.AddPosition(muzzle.position);

        RaycastHit hit;
        if (Physics.Raycast(agent.transform.position, agent.transform.forward, out hit)) // player aim at something
        {
            GameObject objectHit = hit.collider.gameObject;
            //Generate tracer effect
            BasicHealthManager healthManager = objectHit.GetComponent<BasicHealthManager>();
            if (healthManager != null)
            {
                // hit someone with health
                healthManager.TakeDamage(damage);
            }
            if (impactEffect)
            {
                impactEffect.transform.position = hit.transform.position;
                impactEffect.transform.forward = hit.normal;
                impactEffect.Emit(1);
            }
            tracer.transform.position = hit.point;
        }
    }
}
