using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TempFixScript : MonoBehaviour 
{ 
    public WeaponStatistics weaponStatistics;
    public Transform weaponTransform;


    private int startAmunition;
    private int weaponAmmunition;

    public bool canShoot = true;

    [Header("Weapon Info")]

    private float damage;
    private int amunition;
    private float realodTime;
    //public float range;
    private float fireRate;
    private float bulletSpeed;

    public float BulletRemovalLifetime;

    public void Awake()
    {
        damage = weaponStatistics.damage;
        startAmunition = weaponStatistics.amunition;
        weaponAmmunition = weaponStatistics.amunition;
        realodTime = weaponStatistics.realodTime;
        fireRate = weaponStatistics.fireRate;
        bulletSpeed = weaponStatistics.bulletSpeed;
        BulletRemovalLifetime = weaponStatistics.BulletRemovalLifetime;
    }


    public void OnReload()
    {
        weaponAmmunition = startAmunition;
    }


    public void Shoot()
    {
        GameObject bullet = Instantiate(weaponStatistics.bulletProjectile, weaponTransform.position, weaponTransform.rotation);

        bullet.GetComponent<Rigidbody>().velocity = transform.right * bulletSpeed;


        weaponAmmunition--;


        if (weaponAmmunition < 1)
        {
            StartCoroutine(ReloadProces());
        }


        canShoot = false;


        StartCoroutine(FireCooldDown());
    }


    public IEnumerator ReloadProces()
    {
        yield return new WaitForSeconds(realodTime);


        OnReload();
    }

    private IEnumerator FireCooldDown()
    {
        yield return new WaitForSeconds(1 / fireRate);

        canShoot = true;
    }
}
