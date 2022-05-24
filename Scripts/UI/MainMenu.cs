using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToStart()
    {
        Managers.Scene.LoadScene(Define.Scene.Start);
    }
    public void Game()
    {
        Managers.Scene.LoadScene(Define.Scene.InGame);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
