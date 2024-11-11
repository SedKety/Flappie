using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHitSound : MonoBehaviour
{
    [Header("Audio")]

    [SerializeField] private AudioSource hitSound;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            hitSound.Play();
        }
    }
}
