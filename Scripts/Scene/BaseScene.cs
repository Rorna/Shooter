using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;
    void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {
        //이벤트 시스템이 있는지 검색
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        //없으면 생성, 이름도 변경
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }

    public abstract void Clear(); //추상
}
