using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject PauseUI;

    private bool paused = false;

    void Start()
    {
        PauseUI.SetActive(false);  // when game is run (start function) it Pause UI will be disabled 
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            paused = !paused; 
        }

        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0; //Time scale is setting time to 0 so nothing happens it freezes time this kid can't explain anything lol 
        }

        if (!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1; //Normal Time
        }
    }

    public void Resume()
    {
        paused = false; 
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        paused = !paused;
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit()
    {
        Application.Quit(); 
    }
}

