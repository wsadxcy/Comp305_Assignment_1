﻿using UnityEngine;
using System.Collections;

public class Strate_Behaviour : MonoBehaviour {
    [Header("Attribute")]
    // How long the laser will live
    public float lifetime = 6.0f;
    // How fast will the laser move
    public float speed = 450.0f;
    [Header("GameObject")]
    // When the enemy dies, we play an explosion
    public Transform explosion;
    // Use this for initialization
    void Start()
    {
        // The game object that contains this component will be
        // destroyed after lifetime seconds have passed
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
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
}
