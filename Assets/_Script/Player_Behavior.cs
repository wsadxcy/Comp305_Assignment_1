using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Behavior : MonoBehaviour
{
    // Movement modifier applied to directional movement.
    public float playerSpeed = 4.0f;
    // The laser we will be shooting
    public Transform laser;
    // How far from the center of the ship should the laser be
    public float laserDistance = .2f;
    // The buttons that we can use to shoot lasers
    public List<KeyCode> shootButton;
    // How much time (in seconds) we should wait before
    // we can fire again
    public float timeBetweenFires = 0.5f;
    // If value is less than or equal 0, we can fire
    private float timeTilNextFire = 0.0f;




    public float Speed;
    public float xMin, xMax, yMin, yMax;
    public bool isMoving = false;

    private Rigidbody2D rBody;

    // Use this for initialization
    void Start ()
    {
        rBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Movement();

        foreach (KeyCode element in shootButton)
        {
            if (Input.GetKey(element) && timeTilNextFire < 0)
            {
                timeTilNextFire = timeBetweenFires;
                ShootLaser();
                break;
            }
        }
        timeTilNextFire -= Time.deltaTime;
    }

    public void Movement ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        rBody.velocity = movement * Speed;
        rBody.position = new Vector3(Mathf.Clamp(rBody.position.x, xMin, xMax), Mathf.Clamp(rBody.position.y, yMin, yMax), 0.0f);
    }

    void ShootLaser()
    {
        // Calculate the position right in front of the ship's
        // position laserDistance units away
        Instantiate(laser, new Vector3(this.transform.position.x + laserDistance, this.transform.position.y, this.transform.position.z) , this.transform.rotation);
    }
}
