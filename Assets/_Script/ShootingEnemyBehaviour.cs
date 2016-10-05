using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootingEnemyBehaviour : MonoBehaviour
{
    public Transform laser;
    // How far from the center of the ship should the laser be
    public float laserDistance = 45f;
    // How many times should I be hit before I die
    public int health = 1;

    // When the enemy dies, we play an explosion
    public Transform explosion;

    private float InstantiationTimer = 2.0f;
    private Transform player;
    public float speed = 500.0f;

    private GameController controller;


    void Rotation()
    {
        // We need to tell where the mouse is relative to the
        // player
        Vector3 worldPos = player.position - transform.position;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);
        /*
		* Get the differences from each axis (stands for
		* deltaX and deltaY)
		*/
        float dx = this.transform.position.x - worldPos.x;
        float dy = this.transform.position.y - worldPos.y;
        // Get the angle between the two objects
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        /*
		* The transform's rotation property uses a Quaternion,
		* so we need to convert the angle in a Vector
		* (The Z axis is for rotation for 2D).
		*/
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        // Assign the ship's rotation
        this.transform.rotation = rot;
    }

    // Use this for initialization
    void Start ()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = GameObject.Find("Player_ship").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Rotation();
        
        transform.Translate(Vector3.left * Time.deltaTime * speed);

        InstantiationTimer -= Time.deltaTime;
        if (InstantiationTimer <= 0)
        {
            ShootLaser();
            InstantiationTimer = 2f;
        }

    }

    void OnCollisionEnter2D(Collision2D theCollision)
    {
        // Uncomment this line to check for collision
        Debug.Log("Hit" + theCollision.gameObject.name);
        // this line looks for "bullet" in the names of
        // anything collided.
        if (theCollision.gameObject.name.Contains("bullet"))
        {
            BulletBehaviour bullet =
                theCollision.gameObject.GetComponent
                ("BulletBehaviour") as BulletBehaviour;
            health -= bullet.damage;
            Destroy(theCollision.gameObject);
        }
        if (health <= 0)
        {
            // Check if explosion was set
            if (explosion)
            {
                GameObject exploder = ((Transform)Instantiate(explosion, this.
                    transform.position, this.transform.rotation)).gameObject;
                Destroy(exploder, 2.0f);
            }
            Destroy(this.gameObject);
            controller.IncreaseScore(500);
        }
    }

    void ShootLaser()
    {

        // We want to position the laser in relation to
        // our player's location
        Vector3 laserPos = this.transform.position;
        // The angle the laser will move away from the center
        float rotationAngle = transform.localEulerAngles.z - 90;
        // Calculate the position right in front of the ship's
        // position laserDistance units away
        laserPos.x += (Mathf.Cos((rotationAngle) *
            Mathf.Deg2Rad) * -laserDistance);
        laserPos.y += (Mathf.Sin((rotationAngle) *
            Mathf.Deg2Rad) * -laserDistance);
        Instantiate(laser, laserPos, this.transform.rotation);
    }



}
