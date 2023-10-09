using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public WeaponStats weaponStats;
    public Camera cam;
    public Transform muzzle;

    private ParticleSystem impactEffect;
    [SerializeField] private List<ParticleSystem> muzzleFlash;
    private TrailRenderer tracerEffect;

    private float lastShootTime = 0;
    private int damage;
    private float fireInterval = 0.5f;
    private Vector3 bulletSpreadVariance = Vector3.zero;
    private bool addBulletSpread = true;
    private int currentClip = 0;
    bool reloading = false;

    private void Awake()
    {
        //assign stats
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

        currentClip = weaponStats.clipSize;
    }

    public virtual void Fire()
    {
        if (currentClip == 0) Reload();
        if (Time.time < lastShootTime + fireInterval) return;
        if (currentClip < 1 || reloading) return;
        lastShootTime = Time.time;
        currentClip--;
        foreach (var particle in muzzleFlash) particle.Emit(1);
        var tracer = Instantiate(tracerEffect, muzzle.position, Quaternion.identity);
        tracer.AddPosition(muzzle.position);

        RaycastHit hit;
        Vector3 fwd = cam.transform.forward;
        fwd = fwd + cam.transform.TransformDirection(new Vector3(Random.Range(-bulletSpreadVariance.x, bulletSpreadVariance.x), Random.Range(-bulletSpreadVariance.y, bulletSpreadVariance.y), Random.Range(-bulletSpreadVariance.z, bulletSpreadVariance.z)));
        if (!addBulletSpread)
            fwd = cam.transform.forward;
        if (Physics.Raycast(cam.transform.position, fwd, out hit)) // player aim at something
        {
            GameObject objectHit = hit.collider.gameObject;
            //Generate tracer effect

            tracer.transform.position = hit.point;
            if (Physics.Raycast(muzzle.transform.position, objectHit.transform.position - muzzle.transform.position, out hit))
            {
                //Actual object hit by weapon
                objectHit = hit.collider.gameObject;
                BasicHealthManager healthManager = objectHit.GetComponent<BasicHealthManager>();
                if (healthManager != null)
                {
                    // hit someone with health
                    healthManager.TakeDamage(damage);
                }

                //Generate hit effect
                if (impactEffect)
                {
                    impactEffect.transform.position = hit.transform.position;
                    impactEffect.transform.forward = hit.normal;
                    impactEffect.Emit(1);
                }
            }
        }
    }

    void Reload()
    {
        if (currentClip == weaponStats.clipSize) return;
        if (reloading) return;
        else
        {
            reloading = true;
            StartCoroutine(ReloadSequence());
        }
    }

    IEnumerator ReloadSequence()
    {
        yield return new WaitForSeconds(weaponStats.reloadTime);
        currentClip = weaponStats.clipSize;
        reloading = false;
    }
}
