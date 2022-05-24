using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Start;
    }

    public void Game()
    {
        Managers.Scene.LoadScene(Define.Scene.InGame);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public override void Clear()
    {
        //destroy objects
    }
}
