using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    

    public static float fireRate;
    public static int damage;

    [Header("PlayerBased")]
    public static int healthRegenRate;
    public static int maxHealth;
    public static int jumpForce;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            Instance = null;
        }
        Instance = this;
    }

    public void ResetStats()
    {
        maxHealth = 100;
        healthRegenRate = 10;
        fireRate = 3;
        damage = 1;
        jumpForce = 1;
    }
}
