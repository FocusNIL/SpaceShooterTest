using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject shot;       // The prefab to spawn
    public float rateOfFire;      // The minimum delay between shots in seconds
    private float nextFire = 0;   // The next time that the gun can fire

    // Fires the weapon
    public void fire()
    {
        if(Time.time >= nextFire)
        {
            // Set the next fire time
            nextFire = Time.time + rateOfFire;
            // Spawns the shot 
            Instantiate(shot, gameObject.transform.position, gameObject.transform.rotation);
            // Play the soundeffect if any
            if (GetComponent<AudioSource>())
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
