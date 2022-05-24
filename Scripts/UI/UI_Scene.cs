using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base
{
    //씬 ui는 우선순위 필요 없음 -> 정렬 false
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }

}
