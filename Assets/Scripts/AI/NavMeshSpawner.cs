using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class NavMeshSpawner : MonoBehaviour
{
    public NavMeshSurface surface;
   
    private void Start()
    {
        StartCoroutine(WaitForSpawning());
    }

    private IEnumerator WaitForSpawning()
    {
        yield return new WaitForSeconds(0.4f);
        surface.BuildNavMesh();
    }
}
