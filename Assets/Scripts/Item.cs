using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject player;
    private void Start()
    {
        player = FindObjectOfType<Movement>().gameObject;
    }

    public void PickUpItem()
    {
        player.GetComponent<Movement>().amountOfHead++;
        Destroy(gameObject);
    }
}
