﻿using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // Start the game
    public void StartButton_Click()
    {
        SceneManager.LoadScene("Game");
    }
}