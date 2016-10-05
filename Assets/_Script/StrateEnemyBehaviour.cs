using UnityEngine;
using System.Collections;

public class StrateEnemyBehaviour : MonoBehaviour
{
    [Header("Sounds")]
    public AudioSource ExplosionSound;
    public AudioSource Bullethit;
    [Header("Attribute")]
    // Destroy object after lifetime of time
    public float lifetime = 200.0f;
    // How many times should I be hit before I die
    public int health = 6;
    [Header("GameObject")]
    public Transform firepoint;
    public Transform explosion;
    public GameObject laser;

    private int _speed = 1;
    private float _drift;
    private Transform _transform;
    private GameController controller;
    private float InstantiationTimer = 4.0f;


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
    public float Drift
    {
        get
        {
            return this._drift;
        }
        set
        {
            this._drift = value;
        }
    }
    /* 
     * this method move the gameobject towards right with a suttle drift
     */
    private void _move()
    {
        Vector2 newPosition = this._transform.position;

        newPosition.x -= this.Speed;
        newPosition.y += this.Drift;

        this._transform.position = newPosition;
    }

    // Use this for initialization
    void Start ()
    {
        // Access GameController Script
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        this._transform = this.GetComponent<Transform>();
        // randomize Drift Value
        this.Drift = Random.Range(-0.2f, 0.2f);
        Destroy(gameObject, lifetime);
    }
	
	// Update is called once per frame
	void Update ()
    {
        _move();

        // Instantiate with a set timer
        InstantiationTimer -= Time.deltaTime;
        if (InstantiationTimer <= 0)
        {
            ShootLaser();
            InstantiationTimer = 2f;
        }
    }
    // Check Collosion
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("bullet"))
        {
            this.Bullethit.Play();
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
                this.ExplosionSound.Play();
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
