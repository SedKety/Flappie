using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [Header("Line OF Sight Erea")]

    public float radius;

    [Range(0, 360)]
    public float viewAngle; 

    private Collider[] rangeChecks;

    private Vector3 directionToTarget;


    public GameObject playerReference;


    public RaycastHit hitInfo;


    public bool playerIsViseble;


    [Header("Layers")]

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;


    private void Start()
    {
        playerReference = GameObject.Find("PlayerHolder");


        StartCoroutine(InSightCheck());
    }


    private void CheckForPlayerInLineOfSight()
    {
        rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);


        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;

            directionToTarget = (target.position - transform.position).normalized;
            

            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);


                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    playerIsViseble = true;
                }

                else
                {
                    playerIsViseble = false;
                }
            }

            else
            {
                playerIsViseble = false;
            }
        }

        else if (playerIsViseble == true)
        {
            playerIsViseble = false;
        }
    }


    private IEnumerator InSightCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);


        while (true)
        {
            yield return wait;


            CheckForPlayerInLineOfSight();
        }
    }

}
