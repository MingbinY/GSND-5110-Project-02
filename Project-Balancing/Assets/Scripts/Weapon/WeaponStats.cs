using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : ScriptableObject
{
    public int damage = 10;
    public int clipSize = 30;
    public float fireInterval = 0.2f;
    public Vector3 bulletSpreadVariance = Vector3.zero;

    public List<ParticleSystem> muzzleFlash;

    public TrailRenderer tracerEffect;
    public bool addBulletSpread = true;
}
