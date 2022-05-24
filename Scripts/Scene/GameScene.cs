using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    #region position
    [SerializeField]
    public float setx1;
    [SerializeField]
    public float setx2;
    [SerializeField]
    public float sety1;
    [SerializeField]
    public float sety2;
    #endregion

    protected override void Init()
    {
        base.Init();
        Managers.UI.CloseAllPopupUI();

        //씬타입 설정
        SceneType = Define.Scene.InGame;

        //popup ui, pause menu
        Managers.UI.ShowPopupUI<PauseMenu>("Pause");

        //scene ui
        Managers.UI.ShowSceneUI<UI_Ammo>();

        //플레이어 생성
        GameObject player = Managers.Game.Spawn(Define.ObjectType.Player, "SDPlayer"); 

        //카메라 매핑
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        //아이템 생성
        GameObject go = new GameObject { name = "ItemGenerator" };
        ObjectGenerator generator = go.GetOrAddComponent<ObjectGenerator>();
        SetPosition(generator);

        generator.SetKeepObjectCount(2);
    }

    public void SetPosition(ObjectGenerator _generator)
    {
        _generator.x1 = setx1;
        _generator.x2 = setx2;
        _generator.y1 = sety1;
        _generator.y2 = sety2;
    }

    public override void Clear()
    {
        Managers.UI.CloseAllPopupUI();
    }
}
