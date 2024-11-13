using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private Score score;

    [Header("player Health Statistics")]
    [SerializeField] private float health;
    [SerializeField] private float maximumHealth;
    [SerializeField] private float regenarationRate;
    [SerializeField] private float timeSinceLastDamaged;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI pointsEarned;
    [SerializeField] private GameObject deathPanel;

    public Slider hpBar;
    private void Awake()
    {
        hpBar.maxValue = maximumHealth;
        hpBar.value = health;
    }
    void Start()
    {
        health = maximumHealth;
    }
    private void Update()
    {
        timeSinceLastDamaged += Time.deltaTime;
        PlayerHealthRegeneration();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        hpBar.value = health;
        timeSinceLastDamaged = 0;
        if (health < 1)
        {
            Die();
        }
        print("123");
    }
    private void PlayerHealthRegeneration()
    {
        if (timeSinceLastDamaged > 5 && health < maximumHealth)
        {
            health += regenarationRate * Time.deltaTime;
        }
    }
    private void Die()
    {
        deathPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pointsEarned.text = ("" + Score.points).ToString();
    }
}
