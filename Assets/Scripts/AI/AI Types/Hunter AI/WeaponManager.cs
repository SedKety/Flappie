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


    /// Start is called 
    private void Start()
    {
        startAmunition = weaponStatistics.amunition;

        weaponAmmunition = weaponStatistics.amunition;
    }


    public void OnReload()
    {
        weaponAmmunition = startAmunition;
    }


    public void Shoot()
    {
        GameObject bullet = Instantiate(weaponStatistics.bulletProjectile, weaponTransform.position, weaponTransform.rotation);

        bullet.GetComponent<Rigidbody>().velocity = transform.right * weaponStatistics.bulletSpeed;


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
        yield return new WaitForSeconds(weaponStatistics.realodTime);


        OnReload();
    }

    private IEnumerator FireCooldDown()
    {
        yield return new WaitForSeconds(1 / weaponStatistics.fireRate);

        canShoot = true;
    }
}
