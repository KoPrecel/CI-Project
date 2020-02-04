using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOldHealth : MonoBehaviour {

    public Player player;
    public int holder;

	// Use this for initialization
	void Start ()
    {
        player.curHealth = PlayerPrefs.GetInt("Health");
        holder = PlayerPrefs.GetInt("Health");
        player.magicUnlock = true;
    }
}
