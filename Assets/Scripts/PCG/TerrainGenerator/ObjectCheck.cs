using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ObjectCheck : MonoBehaviour
{
    RaycastHit hit;

    private void Start()
    {
        Invoke("MoveObjects", 0.2f);
    }

    void MoveObjects()
    {
        Collider collider = gameObject.GetComponent<Collider>();
        LayerMask mask = LayerMask.GetMask("Ground");
        Vector3 lowestPoint = collider.bounds.min;
        if (Physics.Raycast(lowestPoint + Vector3.up * 1.2f, Vector3.down, out hit, 10000, mask))
        {
            float distanceToMoveDown = Vector3.Distance(lowestPoint, hit.point);
            gameObject.transform.position -= Vector3.up * distanceToMoveDown;
        }
        else
        {
            Destroy(this.gameObject );
        }
    }
}
