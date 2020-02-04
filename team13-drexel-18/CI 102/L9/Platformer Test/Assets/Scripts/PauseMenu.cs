using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject PauseUI;

    private bool paused = false;

    void Start()
    {

        PauseUI.SetActive(false);  // when game is run (start function) it Pause UI will be disabled 
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
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
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        EditorSceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit(); 
    }
}

