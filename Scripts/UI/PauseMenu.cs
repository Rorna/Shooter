using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : UI_Popup
{
    #region Variables
    public static bool isPaused = false;

    [SerializeField]
    private GameObject pauseMenuUI;
    #endregion
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //freeze
        isPaused = true;
    }

    public void LoadMenu()
    {
        //load scene
        Time.timeScale = 1f;
        Managers.Scene.LoadScene(Define.Scene.Start);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
