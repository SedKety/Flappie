using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// This script will manage all things related to the health of the player.
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



    /// Start is called before the first frame update
    void Start()
    {
        // Sets the health to the maximum allowed health points at the start of the game.
        health = maximumHealth;
    }


    /// Update is called every frame
    private void Update()
    {
        // Adds time to the float "timeSinceLastDamaged".
        timeSinceLastDamaged += Time.deltaTime;


        // Executes the void "PlayerHealthRegeneration"
        PlayerHealthRegeneration();
    }


    /// TakeDamage is called everytime something takes damage.
    public void TakeDamage(float damage)
    {
        // Removes the damage amount to the players health.
        health -= damage;


        // Resets the "timeSinceLastDamaged" float to 0.
        timeSinceLastDamaged = 0;


        // Checks if the health is under 1.
        if (health < 1)
        {
            // Executes void DIe.
            Die();
        }


        print("123");
    }


    /// PlayerHealthRegeneration is called when the players health regenarates.
    private void PlayerHealthRegeneration()
    {
        // Checks wheter there have passed 5 seconds since the player was last damaged.
        // Also checks if the players health isn't over the maximum health points amount.
        if (timeSinceLastDamaged > 5 && health < maximumHealth)
        {
            // Adds new health points to the players health.
            health += regenarationRate * Time.deltaTime;
        }
    }


    /// Die is called everytime something dies.
    private void Die()
    {
        // shows the player the death screen.
        deathPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Makes it so that the amount opf points scored are shown on the win screen.
       // pointsEarned.text = Score.points.ToString();
        pointsEarned.text = ("" + Score.points).ToString();
    }
}
