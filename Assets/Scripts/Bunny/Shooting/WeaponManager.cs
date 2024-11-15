using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


/// this script 
public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    [Header("Weapon Information")]
    public WeaponStatistics weaponStatistics;
    public Transform weaponTransform;

    private BunnyInput bunnyInput;
    private InputAction shoot;
    private GameObject parent;

    private int startAmunition;
    private int thisAmmunition;

    public bool canShoot = true;


    [Header("Animations")]

    [SerializeField] private Animator animator;

    [Header("Particles")]
    public ParticleSystem muzzleFlash;

    [Header("Sounds")]

    [SerializeField] private AudioSource gunShotSound;

    private float damage;
    private int amunition;
    private float realodTime;
    private float fireRate;
    private float bulletSpeed;

    public float BulletRemovalLifetime;

    public void Awake()
    {
        damage = weaponStatistics.damage;
        startAmunition = weaponStatistics.amunition;
        thisAmmunition = weaponStatistics.amunition;
        realodTime = weaponStatistics.realodTime;
        fireRate = weaponStatistics.fireRate;
        bulletSpeed = weaponStatistics.bulletSpeed;
        BulletRemovalLifetime = weaponStatistics.BulletRemovalLifetime;
        bunnyInput = new BunnyInput();
    }

    private void Start()
    {
        parent = weaponTransform.root.gameObject;
    }

    private void OnEnable()
    {
        shoot = bunnyInput.Bunny.Shoot;


        shoot.Enable();
    }


    private void OnDisable()
    {
        shoot.Disable();
    }



    /// Shoot is called whenever the input action is called.
    public void OnShoot(InputAction.CallbackContext context)
    {
        if(context.performed && weaponStatistics.amunition > 0 && canShoot == true)
        {
            Shoot();
        }
    }


    public void OnReload()
    {
        thisAmmunition = startAmunition;
    }


    public void Shoot()
    {
        animator.Play("shoot");


        GameObject bullet = Instantiate(weaponStatistics.bulletProjectile, weaponTransform.position, weaponTransform.rotation);

        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        bullet.GetComponent<BulletManager>().shooterGameObject = parent;
        thisAmmunition --;


        if(thisAmmunition < 1)
        {
            StartCoroutine(ReloadProces());
        }

        
        gunShotSound.Play();
        muzzleFlash.Play();

        canShoot = false;


        StartCoroutine(FireCooldDown());
    }


    public IEnumerator ReloadProces()
    {
        animator.SetTrigger("reload");


        yield return new WaitForSeconds(realodTime);


        OnReload();
    }

    private IEnumerator FireCooldDown()
    {
        yield return new WaitForSeconds(1 / fireRate);


        canShoot = true;
    }
}
