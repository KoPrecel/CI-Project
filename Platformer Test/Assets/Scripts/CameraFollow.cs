using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;

    public GameObject player; //player which we are referencing to

    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;
	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player"); //setting player to game object with the tag player aka our player (Tag is in inspector)
		
	}

    private void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX); //for x position of camera 
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY); //for y position of camera

        transform.position = new Vector3(posX, posY, transform.position.z);  // Doesnt affect z position of our camera

        if (bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z)); //Clamp so it doesn't exceed a value
        }
    }

    public void SetMinCamPosition()
    {

        minCameraPos = gameObject.transform.position; 

    }

    public void SetMaxCamPosition()
    {

        maxCameraPos = gameObject.transform.position; 

    }

}
