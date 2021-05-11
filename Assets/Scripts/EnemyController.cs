using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    // Track the current and max health
    public float health = 1;
    private float maxHealth;

    private Done_GameController gameController;

    // Track the health bar and its max width
    public GameObject healthBar;
    private float heathBarInitialWidth;

    // Track any attached objects that are not children of this object so they can be destroyed with this object
    public List<GameObject> attachedObjects;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<Done_GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        // Get the maximum health of the enemy
        maxHealth = health;

        // Also get the maximum width of the health bar 
        heathBarInitialWidth = healthBar.transform.localScale.x;
    }

    // Set the rotation of the health bar
    private void Update()
    {
        healthBar.transform.rotation = Quaternion.identity;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag == "Enemy")
        {
            return;
        }

        // If the other is an indestructable object, destroy this unit
        if (other.tag == "Indestructable")
        {
            health = 0;
        }
        // Otherwise, subtract 1 health
        else
        {
            health--;
        }
        

        // Update the health bar to the percentage remaining
        Vector3 _newScale = healthBar.transform.localScale;
        _newScale.x = heathBarInitialWidth * (health / maxHealth);
        healthBar.transform.localScale = _newScale;

        // If the enemy is out of health, blow it up
        if (health <= 0)
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }

            if (other.tag == "Player")
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver(gameObject.name);
            }

            gameController.AddScore(scoreValue);

            destroySelf();
            Destroy(healthBar);
        }

        // Prevent indestructable objects from being destroyed
        if (other.tag != "Indestructable")
        {
            Destroy(other.gameObject);
        }
    }

    // Destroys the ship and all attached objects
    public void destroySelf()
    {
        Destroy(gameObject);
        foreach(GameObject _object in attachedObjects)
        {
            Destroy(_object);
        }
    }
}
