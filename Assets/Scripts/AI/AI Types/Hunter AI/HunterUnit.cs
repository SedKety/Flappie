using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HunterUnit : MonoBehaviour
{
    [SerializeField] private Transform target; 
    
    
    private Vector3[] path;
    private int targetIndex;

    [Header("Script references")]

    [SerializeField] private HunterWeaponManager hunterWeaponManager;
    private LineOfSight lineOfSight;


    [Header("Scriptable Objects")]

    [SerializeField] private AIStatistics aiStatistics;
    [SerializeField] private WeaponStatistics weaponStatistics;


    private float thisSpeed;
    private int thisAmmunition;


    [Header("Animation")]
    
    [SerializeField] private Animator hunterAnimator;


    [Header("Audio")]

    [SerializeField] private AudioSource walkAudioSource;
    [SerializeField] private AudioSource ShootAudio;


    private void Awake()
    {
        target = GameObject.Find("PlayerHolder").transform;
        
        
        lineOfSight = GetComponent<LineOfSight>();


        thisAmmunition = weaponStatistics.amunition;

        thisSpeed = aiStatistics.movementSpeed;

        hunterAnimator.SetTrigger("Idle");
    }


    private void Start()
    {
        UpdatePath();
    }


    void Update()
    {
        if (target != null && lineOfSight.playerIsViseble == true)
        {
            LookAtPlayer();


            // Continuously check if the target's position has changed
            if (Vector3.Distance(transform.position, target.position) > 0f)
            {
                UpdatePath(); // Update the path if the target has moved

                hunterAnimator.SetTrigger("Walk");
            }
        }

        
        if (lineOfSight.playerIsViseble == false)
        {
            StopCoroutine("FollowPath");
            hunterAnimator.SetTrigger("Idle");
        }


        if (lineOfSight.playerIsViseble == true && hunterWeaponManager.canShoot == true && thisAmmunition > 0)
        {
            ShootAtPlayer();
        }


        else if (lineOfSight.playerIsViseble == true && hunterWeaponManager.canShoot == true && thisAmmunition <= 0)
        {
            StartCoroutine(hunterWeaponManager.ReloadProces());
        }
    }
   

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful == true && lineOfSight.playerIsViseble == true)
        {
            path = newPath;


            StopCoroutine("FollowPath");

            StartCoroutine("FollowPath");


            walkAudioSource.Play();
        }
    }


    IEnumerator FollowPath()
    {
        if(targetIndex < path.Length) 
        {
            Vector3 currentWaypoint = path[targetIndex];

            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break; // Reached the end of the path
                    }
                    currentWaypoint = path[targetIndex];
                }


                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, thisSpeed * Time.deltaTime);


                yield return null; // Wait until the next frame
            }
        }
    }


    public void UpdatePath()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
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


    private void ShootAtPlayer()
    {
        hunterWeaponManager.Shoot(target.position);
        hunterAnimator.SetTrigger("Shoot");

        if (thisAmmunition < 1)
        {
            hunterWeaponManager.canShoot = false;

            StartCoroutine(hunterWeaponManager.ReloadProces());
        }


        ShootAudio.Play();

    }


    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);


                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }

                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
