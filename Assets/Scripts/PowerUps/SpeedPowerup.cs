using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
    public float strength;


    public void OnTriggerEnter(Collider other)
    {
            Movement.Instance.moveSpeed += strength;
            Movement.Instance.sprintspeed += strength * 2;
            Destroy(transform.parent.gameObject);
 
    }
}
