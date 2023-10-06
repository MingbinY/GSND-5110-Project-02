using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public int damage = 10;
    public Camera cam;

    public bool addBulletSpread = true;
    public Vector3 bulletSpreadVariance = Vector3.zero;

    public float fireInterval = 0.5f;

    public Transform muzzle;

    public ParticleSystem impactEffect;
    public ParticleSystem[] muzzleFlash;

    public TrailRenderer tracerEffect;

    private float lastShootTime = 0;

    public virtual void Fire()
    {
        if (Time.time < lastShootTime + fireInterval) return;

        lastShootTime = Time.time;
        foreach (var particle in muzzleFlash) particle.Emit(1);
        var tracer = Instantiate(tracerEffect, muzzle.position, Quaternion.identity);
        tracer.AddPosition(muzzle.position);

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit)) // player aim at something
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


}
