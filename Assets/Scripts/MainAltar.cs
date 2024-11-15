using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainAltar : MonoBehaviour
{
    private int started = Animator.StringToHash("Started");
    public int currentAmount;
    public int neededAmount;
    public Animator animator;
    public GameObject boss;
    private Vector3 bossPos;

    private void Start()
    {
        bossPos = transform.GetChild(1).position;
    }

    private void Update()
    {
        if (currentAmount == neededAmount)
        {
            StartCoroutine(Sacrifice());
        
        }
    }

    IEnumerator Sacrifice()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool(started, true);

        yield return new WaitForSeconds(5);
        Instantiate(boss, bossPos, Quaternion.identity);
        Destroy(transform.parent.gameObject);

    }
}
