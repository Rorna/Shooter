using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.End;
    }
    public override void Clear()
    {
        //destroy objects
        /*GameObject[] GameObjects = FindObjectsOfType<GameObject>() as GameObject[];

        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }*/
    }
}
