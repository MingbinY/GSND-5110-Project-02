using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaycastWeapon : MonoBehaviour
{
    public GameObject agent;

    public WeaponStats weaponStats;
    public Transform muzzle;

    private ParticleSystem impactEffect;
    [SerializeField] private List<ParticleSystem> muzzleFlash;
    private TrailRenderer tracerEffect;

    private float lastShootTime = 0;
    private int damage;
    private float fireInterval = 0.5f;
    private Vector3 bulletSpreadVariance = Vector3.zero;
    private bool addBulletSpread = true;
    bool reloading = false;

    private void Awake()
    {
        agent = GetComponentInParent<AiAgent>().gameObject;

        damage = weaponStats.damage;
        fireInterval = weaponStats.fireInterval;
        bulletSpreadVariance = weaponStats.bulletSpreadVariance;
        addBulletSpread = weaponStats.addBulletSpread;

        foreach (ParticleSystem particle in weaponStats.muzzleFlash)
        {
            ParticleSystem flash = Instantiate(particle, muzzle.position, muzzle.rotation, muzzle);
            muzzleFlash.Add(flash);
        }

        tracerEffect = weaponStats.tracerEffect;
    }

    public virtual void Fire()
    {
        if (Time.time < lastShootTime + fireInterval) return;

        lastShootTime = Time.time;
        foreach (var particle in muzzleFlash) particle.Emit(1);
        var tracer = Instantiate(tracerEffect, muzzle.position, Quaternion.identity);
        tracer.AddPosition(muzzle.position);

        RaycastHit hit;
        Vector3 fwd = agent.transform.forward;
        fwd = fwd + agent.transform.TransformDirection(new Vector3(Random.Range(-bulletSpreadVariance.x, bulletSpreadVariance.x), Random.Range(-bulletSpreadVariance.y, bulletSpreadVariance.y), Random.Range(-bulletSpreadVariance.z, bulletSpreadVariance.z)));
        if (Physics.Raycast(agent.transform.position, fwd, out hit)) // player aim at something
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
