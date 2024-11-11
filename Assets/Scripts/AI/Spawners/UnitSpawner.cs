using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("Spawner Type")]

    [SerializeField] private bool hunterSpawner;
    [SerializeField] private bool civilianSpawner;


    [Header("Unit Type")]

    [SerializeField] private GameObject hunterUnit;
    [SerializeField] private GameObject[] civilianUnits;


    [Header("Unit Amounts")]

    [SerializeField] private int minimalUnitsToSpawn;
    [SerializeField] private int maximalUnitsToSpawn;


    [Header("Spawn Radius")]

    [SerializeField] private float spawnRange;

    [SerializeField] private float overlapSphereRadius;


    [Header("Layermask")]

    [SerializeField] private LayerMask groundLayer;


    private void Start()
    {
        if (hunterSpawner == true)
        {
            HunterUnitSpawner();
        }


        if (civilianSpawner)
        {
            CivilianUnitSpawner();
        }
    }


    private void HunterUnitSpawner()
    {

        int unitsToSpawn = Random.Range(minimalUnitsToSpawn, maximalUnitsToSpawn + 1);


        for (int i = 0; i < unitsToSpawn; i++)
        {
            Vector3 randomPosition = new Vector3(transform.position.x + Random.Range(-spawnRange, spawnRange), transform.position.y, transform.position.z + Random.Range(-spawnRange, spawnRange));


            if (positionIsClear(randomPosition) == true)
            {
                Instantiate(hunterUnit, randomPosition, Quaternion.identity);
            }
        }
    }


    private void CivilianUnitSpawner()
    {
        int unitsToSpawn = Random.Range(minimalUnitsToSpawn, maximalUnitsToSpawn + 1);


        for (int i = 0; i < unitsToSpawn; i++)
        {
            Vector3 randomPosition = new Vector3(transform.position.x + Random.Range(-spawnRange, spawnRange), transform.position.y, transform.position.z + Random.Range(-spawnRange, spawnRange));


            if (positionIsClear(randomPosition) == true)
            {
                Instantiate(civilianUnits[Random.Range(0, civilianUnits.Length)], randomPosition, Quaternion.identity);
            }
        }
    }


    private bool positionIsClear(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);

        foreach (Collider collider in colliders)
        {
            if (groundLayer == (groundLayer | (1 << GetComponent<Collider>().gameObject.layer)))
            {
                continue;
            }


            return false;
        }


        return true;
    }
}
