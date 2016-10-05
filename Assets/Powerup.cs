using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {

    // PRIVATE INSTANCE VARIABLES +++++++++++++++++++++++++++++
    private int _speed = 1;
    private Transform _transform;

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


    // Use this for initialization
    void Start()
    {
        this._transform = this.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        this._move();
    }

    /**
	 * this method moves the game object down the screen by _speed px every frame
	 */
    private void _move()
    {
        Vector2 newPosition = this._transform.position;

        newPosition.x -= this._speed;

        this._transform.position = newPosition;
    }





}
