using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour {

    public GameObject Door;

	// Use this for initialization
	void Start () {

        Door.SetActive(false);
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Door.SetActive(true);
        }
    }
}
