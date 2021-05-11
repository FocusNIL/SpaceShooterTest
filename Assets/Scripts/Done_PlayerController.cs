using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Done_Boundary boundary;

    // Track what to spawn
    public GameObject specialBullet;

    // Track where to spawn it 
    public Transform specialSpawnLocation;

    // The wait time after a special shot
    public float specialFireRate;

    // The text to display the cooldown time in
    public Text cooldownText;

    private float nextFire;

    // All the weapons attached to this ship
    private Weapon[] weapons;

    // Grab all weapons on start
    private void Start()
    {
        weapons = GetComponentsInChildren<Weapon>();
    }
 
    void Update ()
	{
        
        if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
            // Fire each weapon on the ship
            foreach(Weapon _weapon in weapons)
            {
                _weapon.fire();
            }
            // Only play the shoot sound once so we don't overwhelm the player with too much noise
			GetComponent<AudioSource>().Play();
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
		
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);

        // Display the time remaining on the special shot cooldown
        float remainingTime = nextFire - Time.time;
        if(remainingTime > 0)
        {
            cooldownText.text = remainingTime.ToString("F1");
        }
        else
        {
            cooldownText.text = "";
        }
        

    }

    // Fire the special bullet
    public void spawnSpecial()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + specialFireRate;
            Instantiate(specialBullet, specialSpawnLocation.position, specialSpawnLocation.rotation);
        }
    }
}
