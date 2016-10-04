using UnityEngine;
using System.Collections;

public class HomingEnemy_Behaviour : MonoBehaviour
{
    // How many times should I be hit before I die
    public int health = 2;

    // When the enemy dies, we play an explosion
    public Transform explosion;


    private Transform player;
    public float speed = 2.0f;

    // Use this for initialization
    void Start()
    {
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


    void OnCollisionEnter2D(Collision2D theCollision)
    {
        // Uncomment this line to check for collision
        Debug.Log("Hit"+ theCollision.gameObject.name);
        // this line looks for "laser" in the names of
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
        }
    }
}
