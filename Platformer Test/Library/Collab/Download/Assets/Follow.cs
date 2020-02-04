using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    private Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;

    public GameObject player; //player which we are referencing to

    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;
    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player"); //setting player to game object with the tag player aka our player (Tag is in inspector)

    }

    private void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX); //for x position of camera 
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY) + 5; //for y position of camera

        transform.position = new Vector3(posX, posY, transform.position.z);  // Doesnt affect z position of our camera

    }



}
