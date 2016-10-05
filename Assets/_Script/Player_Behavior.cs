using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Behavior : MonoBehaviour
{

    [Header("Sounds")]
    public AudioSource Explosion;
    public AudioSource Lasershoot;
    public AudioSource Coinget;
    public AudioSource poweredup;

    [Header("GameObject")]
    // The laser we will be shooting
    public Transform laser;
    public GameController gameController;
    [Header("Attribute")]
    public float Speed;
    public float xMin, xMax, yMin, yMax;
    public bool isMoving = false;
    // How far from the center of the ship should the laser be
    public float laserDistance = 45f;
    // The buttons that we can use to shoot lasers
    public List<KeyCode> shootButton;
    // How much time (in seconds) we should wait before
    // we can fire again
    public float timeBetweenFires = 0.4f;
    // Movement modifier applied to directional movement.
    public float playerSpeed = 4.0f;
    // timer for power up
    public float timeLeft = 10.0f;


    private Rigidbody2D rBody;
    // If value is less than or equal 0, we can fire
    private float timeTilNextFire = 0.0f;
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
        this.Lasershoot.Play();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Coin"))
        {
            this.gameController.ScoreValue += 100;
            this.Coinget.Play();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            this.gameController.LivesValue -= 1;
            this.Explosion.Play();
        }
        if(other.gameObject.CompareTag("PowerUp"))
        {
            StartCoroutine(RapidFire());
            Destroy(other.gameObject);
            this.poweredup.Play();
        }
    }

    /*
     * This Method set a timer for power up pick up
     * it sets the fire rate to 0
     */
    public IEnumerator RapidFire()
    {
        this.timeBetweenFires = 0;
        // Give the player time before we start the game
        yield return new WaitForSeconds(3);
        this.timeBetweenFires = 0.4f;

    }
}
