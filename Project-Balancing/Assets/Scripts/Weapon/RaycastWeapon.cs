using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RaycastWeapon : MonoBehaviour
{
    public WeaponStats weaponStats;
    public Camera cam;
    public Transform muzzle;
    public Recoil recoilScript;

    private ParticleSystem impactEffect;
    [SerializeField] private List<ParticleSystem> muzzleFlash;
    private TrailRenderer tracerEffect;
    WeaponClipUI weaponUI;
    AudioSource weaponAudioSource;

    private float lastShootTime = 0;
    private int damage;
    private float fireInterval = 0.5f;
    private Vector3 recoil = Vector3.zero;
    private bool hasRecoil = true;
    public int currentClip = 0;
    bool reloading = false;

    [Header("Shotgun")]
    public bool isShotgun = false;

    private void Awake()
    {
        //assign stats
        cam = Camera.main;
        damage = weaponStats.damage;
        fireInterval = weaponStats.fireInterval;
        recoil = weaponStats.recoil;
        hasRecoil = weaponStats.addBulletSpread;
        recoilScript = GetComponentInParent<Recoil>();
        weaponAudioSource = GetComponent<AudioSource>();

        foreach (ParticleSystem particle in weaponStats.muzzleFlash)
        {
            ParticleSystem flash = Instantiate(particle, muzzle.position, muzzle.rotation, muzzle);
            muzzleFlash.Add(flash);
        }

        tracerEffect = weaponStats.tracerEffect;

        currentClip = weaponStats.startClipSize;
        weaponUI = GetComponentInChildren<WeaponClipUI>();
    }

    public virtual void Fire()
    {
        //if (currentClip == 0) Reload();
        
        if (Time.time < lastShootTime + fireInterval) return;
        if (currentClip < 1 || reloading) return;
        lastShootTime = Time.time;
        currentClip--;
        foreach (var particle in muzzleFlash) particle.Emit(1);
        if (isShotgun) { ShotgunFire(); return; }
        var tracer = Instantiate(tracerEffect, muzzle.position, Quaternion.identity);
        tracer.AddPosition(muzzle.position);

        //Gun SFX
        if (weaponAudioSource)
            weaponAudioSource.PlayOneShot(weaponStats.weaponSound);

        RaycastHit hit;
        Vector3 fwd = cam.transform.forward;
        if (hasRecoil) recoilScript.RecoilFire(recoil);
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
    void ShotgunFire()
    {
        int pillets = weaponStats.pilletCount;
        Vector3 spread = weaponStats.bulletSpread;
        Vector3 fwd = cam.transform.forward;
        if (weaponAudioSource)
            weaponAudioSource.PlayOneShot(weaponStats.weaponSound);

        List<Vector3> directions = new List<Vector3>();
        List<TrailRenderer> tracers = new List<TrailRenderer>();
        Vector3 lastDir = Vector3.zero;
        for (int i = 0; i < pillets; i=i+2)
        {
            Vector3 newDir = new Vector3(lastDir.x + spread.x, lastDir.y + spread.y, lastDir.z + spread.z);
            directions.Add(newDir);
            var tracer = Instantiate(tracerEffect, muzzle.position, Quaternion.identity);
            tracer.AddPosition(muzzle.position);
            tracers.Add(tracer);
            newDir = newDir * -1;
            directions.Add(newDir);
            tracer = Instantiate(tracerEffect, muzzle.position, Quaternion.identity);
            tracer.AddPosition(muzzle.position);
            tracers.Add(tracer);
        }

        for (int i = 0; i < pillets; i++)
        {
            Vector3 thisDir = directions[i];
            thisDir += fwd;
            
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, thisDir, out hit)) // check objects inside range
            {
                GameObject objectHit = hit.collider.gameObject;
                //Generate tracer effect
                var tracer = tracers[i];
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
}
