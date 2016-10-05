using UnityEngine;
using System.Collections;

public class Disc_Behaviour : MonoBehaviour {


    // How many times should I be hit before I die
    public int health = 2;

    // When the enemy dies, we play an explosion
    public Transform explosion;
    private Random random = new Random();

    // PRIVATE INSTANCE VARIABLES +++++++++++++++++++++++++++++
    private int _speed;
    private int _drift;
    private Transform _transform;
    private GameController controller;
    // PUBLIC PROPERTIES
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

    public int Drift
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


    // Use this for initialization
    void Start()
    {

        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        this._transform = this.GetComponent<Transform>();
        this._reset();
    }

    // Update is called once per frame
    void Update()
    {
        this._move();
        this._checkBounds();
        this._rotate();
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
            controller.IncreaseScore(100);
        }
    }


    /**
     * this method rotate the gameobject each frame
     */
    private void _rotate()
    {
        transform.Rotate(0, 0, 400 * Time.deltaTime);
    }


    /**
	 * this method moves the game object down the screen by _speed px every frame
	 */
    private void _move()
    {
        Vector2 newPosition = this._transform.position;

        newPosition.x -= this.Speed;
        newPosition.y += this.Drift;

        this._transform.position = newPosition;
    }

    /**
	 * this method checks to see if the game object meets the top-border of the screen
	 */
    private void _checkBounds()
    {
        if (this._transform.position.x <= -680f)
        {
            this._reset();
        }
    }

    /**
	 * this method resets the game object to the original position
	 */
    private void _reset()
    {
        this.Speed = Random.Range(5, 10);
        this.Drift = Random.Range(-2, 2);
        this._transform.position = new Vector2(680f, Random.Range(-375f, 375f));
    }

}
