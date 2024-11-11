using UnityEngine;


/// This script manages everything the bullet needs to do or needs to take into consideration.
public class BulletManager : MonoBehaviour
{
    [Header("Scripteble Objects")]

    public WeaponStatistics weaponStatistics;


    [Header("Bullet Position")]

    private Vector3 startPosition;
    private float distanceTraveled;
    /// this void is called before the first frame.
    private void Start()
    {
        // Sets the start position to the cureent position of the bullet.
        startPosition = transform.position;
    }


    ///this void is called every frame
    private void Update()
    {
        // Executes the void Range
        Range();
    }


    /// This void is called everytime a collision is called.
    private void OnCollisionEnter(Collision collision)
    {
        // Checks the collision layer of the object the bullet colided with.
        if (collision.gameObject.layer == 6)
        {
            // Gets the component "PlayerHealth" script from the object it has collided with after witch it executes the void take damage. 
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(weaponStatistics.damage);


            // Destroys the bullet after impact.
            Destroy(gameObject);
        }


        // Checks the collision layer of the object the bullet colided with.
        // And checks if the gameobject the bullet has collided with has the tag "Enemy".
        if (collision.gameObject.layer == 8 && collision.gameObject.tag == "Enemy")
        {
            // Gets the component "AIHealth" script from the object it has collided with after witch it executes the void take damage.
            collision.gameObject.GetComponent<AIHealth>().TakeDamage(weaponStatistics.damage);

            // Destroys the bullet after impact.
            Destroy(gameObject);
        }


        // Checks if the bullet hits something other than the player layer or the Entety Layer.
        if (collision.gameObject.layer != 6 || collision.gameObject.layer != 8)
        {
            // Destroys the bullet after impact.
            Destroy(gameObject);
        }
    }


    /// Controlls the range of the bullet
    private void Range()
    {
        // checks the difference between the start position and its current position and then equals the difference to the "distanceTraveled" float.
        distanceTraveled = Vector3.Distance(transform.position, startPosition);

        // Checks if the "distanceTraveled" float is greater than the "range" float.
        if (distanceTraveled > weaponStatistics.range)
        {
            //destroyes the bullet if the "distanceTraveled" float is greater than the "range" float.
            Destroy(gameObject);
        }
    }
}
