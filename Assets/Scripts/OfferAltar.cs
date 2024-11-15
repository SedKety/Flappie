using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class OfferAltar : MonoBehaviour
{
    public bool hasAdded;
    public Vector3 sacrificePosition;
    public GameObject sacrifice;
    public GameObject mainAltar;

    public void Start()
    {
        sacrificePosition = transform.GetChild(0).position;
    }

    public void AddSacrifice()
    {
        GameObject head = Instantiate(sacrifice, sacrificePosition, Quaternion.identity);
        hasAdded = true;
        mainAltar.GetComponent<MainAltar>().currentAmount++;
    }
}
