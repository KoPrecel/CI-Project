using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour {

    public string levelName;
    public Player player;

    private void Start()
    {
        //player.curHealth = 20;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerPrefs.SetInt("Health", player.curHealth);
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(levelName);
        }
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene(2);
    }
    public void Quit()
    {
        Application.Quit();
    }
}

