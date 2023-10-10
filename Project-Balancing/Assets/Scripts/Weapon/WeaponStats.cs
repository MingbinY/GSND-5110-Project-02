using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="stats/weapon Stat")]
public class WeaponStats : ScriptableObject
{
    public int damage = 10;
    public int clipSize = 30;
    public float fireInterval = 0.2f;
    public float reloadTime = 3f;
    public Vector3 recoil = Vector3.zero;

    public List<ParticleSystem> muzzleFlash;

    public TrailRenderer tracerEffect;
    public bool addBulletSpread = true;

    public Vector3 weaponOffset = Vector3.zero;
}
