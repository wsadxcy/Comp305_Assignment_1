using UnityEngine;
using System.Collections;

public class StrateEnemyBehaviour : MonoBehaviour
{

    private int _speed = 1;
    private Transform _transform;
    public Transform firepoint;
    public Transform explosion;
    public GameObject laser;
    private GameController controller;
    private float InstantiationTimer = 4.0f;
    // How many times should I be hit before I die
    public int health = 6;

    public int Speed
    {
        get
        {
            return this._speed;
        }
        set
        {
            this._speed = value;
        }
    }

    private void _move()
    {
        Vector2 newPosition = this._transform.position;

        newPosition.x -= this._speed;

        this._transform.position = newPosition;
    }

    // Use this for initialization
    void Start ()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        this._transform = this.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        _move();
        InstantiationTimer -= Time.deltaTime;
        if (InstantiationTimer <= 0)
        {
            ShootLaser();
            InstantiationTimer = 2f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("bullet"))
        {
            BulletBehaviour bullet =
                other.gameObject.GetComponent
                ("BulletBehaviour") as BulletBehaviour;
            health -= bullet.damage;
            Destroy(other.gameObject);
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
            controller.IncreaseScore(200);
        }
    }



    void ShootLaser()
    {
        // Calculate the position right in front of the ship's
        // position laserDistance units away
        Instantiate(laser, firepoint.position, firepoint.rotation);
    }


}
