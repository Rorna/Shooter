using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//기능성 함수 넣어줌
public class Util 
{
    //컴포넌트가 있으면 가져오고, 없다면 컴포넌트 붙여줌
    public static T GetOrAddComponent<T>(GameObject _go) where T : UnityEngine.Component
    {
        T component = _go.GetComponent<T>();
        if (component == null)
            component = _go.AddComponent<T>();
        return component;
    }

    //자식 탐색
    public static GameObject FindChild(GameObject _go, string _name = null, bool _recursive = false)
    {
        Transform transform = FindChild<Transform>(_go, _name, _recursive);
        //없으면
        if (transform == null)
            return null;
        //있으면
        return transform.gameObject;
    }

    ///자식 오브젝트를 찾는 함수
    ///recursive : 자식의 자식까지 찾을 것인가
    ///첫째인자: 최상위 부모
    public static T FindChild<T>(GameObject _go, string _name=null, bool _recursive =false) where T : UnityEngine.Object
    {
        if (_go == null) return null; //최상위 객체 없으면 널
        if(_recursive==true) //직속 자식에 대해서만 검색
        {
            ///자식들 하나씩 스캔하면서
            ///컴포넌트를 가지고 있는지 확인후,
            ///있으면 리턴
            for(int i=0; i<_go.transform.childCount; i++)
            {
                Transform transform = _go.transform.GetChild(i);
                //이름이 같다면 가져옴
                if(string.IsNullOrEmpty(_name) || transform.name==_name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null) 
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in _go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(_name) || component.name == _name)
                    return component;
            }
        }
        return null;
    }
}
