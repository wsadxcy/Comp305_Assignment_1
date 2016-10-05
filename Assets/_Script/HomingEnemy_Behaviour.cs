using UnityEngine;
using System.Collections;

public class HomingEnemy_Behaviour : MonoBehaviour
{
    // How many times should I be hit before I die
    public int health = 1;

    // When the enemy dies, we play an explosion
    public Transform explosion;


    private Transform player;
    public float speed = 700.0f;

    private GameController controller;

    // Use this for initialization
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = GameObject.Find("Player_ship").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = player.position - transform.position;
        delta.Normalize();
        float moveSpeed = speed * Time.deltaTime;
        transform.position = transform.position + (delta * moveSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Check if explosion was set
            if (explosion)
            {
                GameObject exploder = ((Transform)Instantiate(explosion, this.
                    transform.position, this.transform.rotation)).gameObject;
                Destroy(exploder, 2.0f);
            }
            Destroy(this.gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D theCollision)
    {
        // Uncomment this line to check for collision
        Debug.Log("Hit"+ theCollision.gameObject.name);
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
            controller.IncreaseScore(50);
        }
    }

    
}
