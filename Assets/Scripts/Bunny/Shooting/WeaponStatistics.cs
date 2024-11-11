using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "WeaponStatistics")]


public class WeaponStatistics : ScriptableObject
{
    [Header("Weapon Info")]

    public float damage;
    public int amunition;
    public float realodTime;
    //public float range;
    public float fireRate;
    public float bulletSpeed;

    public float BulletRemovalLifetime;

    public GameObject bulletProjectile;
}
