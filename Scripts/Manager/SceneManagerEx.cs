using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx 
{
    //현재 씬 반환
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.Scene _type)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(_type)); //로드
    }

    //씬 이름 획득
    string GetSceneName(Define.Scene _type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), _type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear(); //현재씬 데이터 삭제
    }
}
