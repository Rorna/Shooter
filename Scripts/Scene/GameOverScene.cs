using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.GameOver;
    }

    public void GoToStart()
    {
        Managers.Scene.LoadScene(Define.Scene.Start);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public override void Clear()
    {
        //destroy object
    }
}
