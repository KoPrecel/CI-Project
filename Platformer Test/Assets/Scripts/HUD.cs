using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour{


    public Sprite[] HeartSprites; //Added brackets because creating an array

    public Image HeartUI; //Used for actual UI image on the screen 

    private Player player;  // Used to access health of the player 


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    void Update()
    {
        if (player.curHealth > 0 && player.curHealth <= 20)
        {
            HeartUI.sprite = HeartSprites[player.curHealth];    //Acess sprite when player has 5 heatlh gets the number 5 image in the hearts sprite.
        }

    }
}
