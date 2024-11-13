using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivilianUnit : MonoBehaviour
{
    private NavMeshAgent agent;

    public bool isRunningFromPlayer;

    private LineOfSight lineOfSight;


    [Header("Scriptable Objects")]

    [SerializeField] private AIStatistics aiStatistics;


    [SerializeField] private Transform targetObject;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float playerGoneRadius;

    [SerializeField] private float avoidDistance;

    [SerializeField] private float walkRadius;
    

    private Vector3 originalPosition = Vector3.zero;

    private float thisSpeed;

    private Vector3 movePosition;

    [SerializeField]private LayerMask ground; 
    

    [Header("Animations")]
    public Animator civillianAnimator;

    public GameObject canvas;

    private bool hasFoundPoint;

    private void Awake()
    {
        lineOfSight = GetComponent<LineOfSight>();


        thisSpeed = aiStatistics.movementSpeed;
        civillianAnimator.SetTrigger("Idle");

        targetObject = GameObject.Find("PlayerHolder").transform;
        agent = GetComponent<NavMeshAgent>();

        canvas = GetComponentInChildren<Canvas>().gameObject;
        canvas.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(WaitForSurface());
        
    }

    private IEnumerator WaitForSurface()
    {
        yield return new WaitForSeconds(0.4f);
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, ground))
        {
            transform.position = hit.point;
        }

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hitNav, Mathf.Infinity, NavMesh.AllAreas)) 
        {

            transform.position = hitNav.position;
            
        }

        agent.enabled = true;
        originalPosition = transform.position;

        SearchPoint();
        agent.SetDestination(movePosition);
    }

    private void CheckForPlayer()
    {
        if (lineOfSight.playerIsViseble == true)
        {
            isRunningFromPlayer = true;
            RunningFromPlayer();
            Ticker.Instance.OnTick += RunningFromPlayer;
            StartCoroutine(ShowTheMark());
            StartCoroutine(MakeTheMarkLook());
        }
        else
        {
            civillianAnimator.SetTrigger("Idle");
        }
    }

    private IEnumerator ShowTheMark()
    {
        canvas.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        canvas.SetActive(false);
    }

    private IEnumerator MakeTheMarkLook()
    {
        while (canvas.activeInHierarchy)
        {
            canvas.transform.LookAt(targetObject.position);
            yield return null;
        }
        
    }
         
    private void Update()
    {  
        if (isRunningFromPlayer)
        {
            CheckIfPlayerIsGone();

        }

        else
        {
            CheckForPlayer();
            WalkAround();
        }

    }

    private void RunningFromPlayer()
    {
        // Calculate the direction away from the target object
        Vector3 directionAway = (transform.position - targetObject.position).normalized;
        directionAway.y = 0;

        Vector3 newDestination = agent.transform.position + directionAway * avoidDistance;

        civillianAnimator.SetTrigger("Run");
        if (NavMesh.SamplePosition(newDestination, out NavMeshHit hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
    private void CheckIfPlayerIsGone()
    {
        if (Vector3.Distance(transform.position, targetObject.position) > playerGoneRadius)
        {
            Ticker.Instance.OnTick -= RunningFromPlayer;
            isRunningFromPlayer = false;
            agent.SetDestination(movePosition);
        }
    }

    private void WalkAround()
    {
        if (Vector3.Distance(transform.position, movePosition) < 0.1f && !hasFoundPoint)
        {
            StartCoroutine(WaitForThisGuyToBeSmart());
        }
    }

    private IEnumerator WaitForThisGuyToBeSmart()
    {
        hasFoundPoint = true;
        yield return new WaitForSeconds(2f);
        SearchPoint();
        agent.SetDestination(movePosition);
        hasFoundPoint = false;

    }

    private void SearchPoint()
    {
        float xRadius = Random.Range(transform.position.x - walkRadius, transform.position.x + walkRadius);
        float zRadius = Random.Range(transform.position.z - walkRadius, transform.position.z + walkRadius);

        Vector3 randomPos = new Vector3(xRadius, transform.position.y, zRadius);

        if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            movePosition = hit.position;
        }
    }

    private void OnDestroy()
    {
        Ticker.Instance.OnTick -= RunningFromPlayer;
    }


}
