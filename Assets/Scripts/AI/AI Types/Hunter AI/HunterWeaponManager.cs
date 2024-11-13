using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HunterWeaponManager : MonoBehaviour 
{ 
    public WeaponStatistics weaponStatistics;
    private Transform weaponTransform;


    private int thisAmunition;
    private int thisAmmunition;


    public bool canShoot = true;


    private void Awake()
    {
        weaponTransform = GetComponent<Transform>();


        thisAmunition = weaponStatistics.amunition;

        thisAmmunition = weaponStatistics.amunition;
    }


    public void OnReload()
    {
        thisAmmunition = thisAmunition;
    }


    public void Shoot(Vector3 playerPosition)
    {
        Vector3 directionOfPlayer = playerPosition - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(directionOfPlayer);
        GameObject bullet = Instantiate(weaponStatistics.bulletProjectile, weaponTransform.position, lookRotation);

        bullet.GetComponent<Rigidbody>().velocity = transform.forward * weaponStatistics.bulletSpeed;


        thisAmmunition--;


        if (thisAmmunition < 1)
        {
            StartCoroutine(ReloadProces());
        }


        canShoot = false;


        StartCoroutine(FireCooldDown());
    }


    public IEnumerator ReloadProces()
    {
        yield return new WaitForSeconds(weaponStatistics.realodTime);


        OnReload();
    }

    private IEnumerator FireCooldDown()
    {
        yield return new WaitForSeconds(1 / weaponStatistics.fireRate);

        canShoot = true;
    }
}
