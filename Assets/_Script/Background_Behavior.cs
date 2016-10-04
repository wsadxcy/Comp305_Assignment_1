using UnityEngine;
using System.Collections;

public class Background_Behavior : MonoBehaviour
{
    // PRIVATE INSTANCE VARIABLES +++++++++++++++++++++++++++++
    public float Speed = 0.1f;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(Time.time * Speed, 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }

 
}
