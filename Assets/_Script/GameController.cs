using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    // PRIVATE INSTANCE VARIABLES ++++++++++++++++++
    private int _livesValue;
    private int _scoreValue;
    private AudioSource _endGameSound;


    // PUBLIC INSTANCE VARIABLES (TESTING) +++++++++


    [Header("UI Objects")]
    public Text LivesLabel;
    public Text ScoreLabel;
    public Text GameOverLabel;
    public Text FinalScoreLabel;
    public Button RestartButton;

    [Header("Game Objects")]
    public GameObject plane;
    public GameObject HomingEnemy;
    public GameObject Disc;
    public GameObject ShootingEnemy;
    public GameObject Coin;



    private float InstantiationTimerD = 5.0f;
    private float InstantiationTimerH = 5.0f;
    private float InstantiationTimerS = 5.0f;
    private float InstantiationTimerC = 20.0f;

    // PUBLIC PROPERTIES +++++++++++++++++++++++++++
    public int LivesValue
    {
        get
        {
            return this._livesValue;
        }

        set
        {
            this._livesValue = value;
            if (this._livesValue <= 0)
            {
                this._endGame();
            }
            else
            {
                this.LivesLabel.text = "Lives: " + this._livesValue;
            }
        }
    }

    public int ScoreValue
    {
        get
        {
            return this._scoreValue;
        }

        set
        {
            this._scoreValue = value;
            this.ScoreLabel.text = "Score: " + this._scoreValue;
        }
    }


    public void IncreaseScore(int increase)
    {
        _scoreValue += increase;
        this.ScoreLabel.text = "Score: " + this._scoreValue;
    }

    // Use this for initialization
    void Start()
    {
        this.LivesValue = 5;
        this.ScoreValue = 0;

        this.GameOverLabel.gameObject.SetActive(false);
        this.FinalScoreLabel.gameObject.SetActive(false);
        this.RestartButton.gameObject.SetActive(false);

        this._endGameSound = this.GetComponent<AudioSource>();


    }



    // Update is called once per frame
    void Update()
    {
        InstantiationTimerD -= Time.deltaTime;
        InstantiationTimerH -= Time.deltaTime;
        InstantiationTimerS -= Time.deltaTime;
        InstantiationTimerC -= Time.deltaTime;
        if (InstantiationTimerD <= 0)
        {
            Instantiate(this.Disc);
            InstantiationTimerD = 6f;
        }
        if (InstantiationTimerH <= 0)
        {
            Instantiate(this.HomingEnemy, new Vector3(680f, Random.Range(-375f, 375f), 0), Quaternion.identity);
            InstantiationTimerH = 5f;
        }

        if (InstantiationTimerS <= 0)
        {
            Instantiate(this.ShootingEnemy, new Vector3(680f, Random.Range(-375f, 375f), 0), Quaternion.identity);
            InstantiationTimerS = 100f;
        }
        if (InstantiationTimerC <= 0)
        {
            Instantiate(this.Coin, new Vector3(680f, Random.Range(-375f, 375f), 0), Quaternion.identity);
            InstantiationTimerC = 50f;
        }
    }


    private void _endGame()
    {
        this.GameOverLabel.gameObject.SetActive(true);
        this.FinalScoreLabel.text = "Final Score: " + this.ScoreValue;
        this.FinalScoreLabel.gameObject.SetActive(true);
        this.RestartButton.gameObject.SetActive(true);
        this.ScoreLabel.gameObject.SetActive(false);
        this.LivesLabel.gameObject.SetActive(false);
        this.plane.SetActive(false);
        this.HomingEnemy.SetActive(false);
        this._endGameSound.Play();
    }

    // PUBLIC METHODS ++++++++++++++++++++++++++++++
    public void RestartButton_Click()
    {
        SceneManager.LoadScene("Game");
    }


}
