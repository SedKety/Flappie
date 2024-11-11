using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAI : MonoBehaviour
{
    [SerializeField] private LineOfSight lineOfSight;
    [SerializeField] private WeaponManager weaponManager;

    [Header("Scripteble Objects")]

    [SerializeField] private AIStatistics aiStatistics;
    [SerializeField] private WeaponStatistics weaponStatistics;


    [SerializeField] private Transform target;

    private int weaponAmmunition;

    private void Start()
    {
        weaponManager.canShoot = true;
    }


    private void Update()
    {

        if (target != null && lineOfSight.playerIsViseble == true)
        {
            LookAtPlayer();


            MoveTowardsPlayer();
        }


        if (lineOfSight.playerIsViseble == true && weaponManager.canShoot == true && weaponStatistics.amunition > 0)
        {
            ShootAtPlayer();
        }
    }


    private void LookAtPlayer()
    {
        Vector3 directionToTarget = target.position - transform.position;


        directionToTarget.y = 0;


        if (directionToTarget != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);


            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, aiStatistics.rotationSpeed * Time.deltaTime);
        }
    }


    private void MoveTowardsPlayer()
    {
        if (Vector3.Distance(transform.position, target.position) > 3)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, aiStatistics.movementSpeed * Time.deltaTime);
        }
    }


    private void ShootAtPlayer()
    {
        weaponManager.Shoot();


        if (weaponAmmunition < 1)
        {
            weaponManager.canShoot = false;


            StartCoroutine(weaponManager.ReloadProces());
        }
    }
}
