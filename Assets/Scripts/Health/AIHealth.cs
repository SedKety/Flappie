using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


/// This script will manage all things related to the health of the AI.
public class AIHealth : MonoBehaviour
{
    [Header("Scripts")]

    [SerializeField] private Score score;


    [Header("Scriptable Object")]

    [SerializeField] private AIStatistics aiStatistics;


    public float aiHealth;
    bool isDead;
    [Header("Particles")]

    [SerializeField] ParticleSystem impactParticle ,deathParticle;


    /// Start is called before the first frame update
    void Start()
    {
        aiHealth = aiStatistics.health;


        score = GameObject.Find("GameManager").GetComponent<Score>();
    }


    /// Update is called every frame
    private void Update()
    {
        
    }


    /// TakeDamage is called everytime the AI takes damage.
    public void TakeDamage(float damage)
    {
        aiHealth -= damage;
        // Checks if the "AIHealth" float is under 1.
        if (aiHealth < 1 && !isDead)
        {
            // Executes void DIe.
            StartCoroutine("Die");
            return;
        }
        impactParticle.Play();


    }

    /// Die is called everytime a Unit dies.
    private IEnumerator Die()
    {
        isDead = true;
        deathParticle.Play();
        GetFirstChildWithLayer(transform, 8).gameObject.SetActive(false);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Score.points += aiStatistics.points;
        score.AddPoints();
        // Adds the amount of points the enemy gave to the player to the players point tally.
        //score.AddPoints(aiStatistics.points);
        yield return new WaitForSeconds(1);
        // Destroyes the gameobejct this script is on.
        Destroy(gameObject);
    }

    public static Transform GetFirstChildWithLayer(Transform t, int layer)
    {
        Transform[] array = new Transform[t.childCount];
        for (int i = 0; i < t.childCount; i++)
        {
            array[i] = t.GetChild(i);
        }
        return array.First(x => x.gameObject.layer == layer);
    }
}
