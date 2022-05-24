using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    //컴포넌트와 오브젝트 저장용
    Dictionary<Type, UnityEngine.Object[]> objects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    private void Start() 
    {
        Init();
    }

    //reflection 사용
    protected void Bind<T>(Type _type) where T : UnityEngine.Object
    {
        //저장할 공간 생성
        string[] names = Enum.GetNames(_type); 
        //object 생성
        UnityEngine.Object[] arr_objects = new UnityEngine.Object[names.Length];

        objects.Add(typeof(T), arr_objects); 

        //순회하며 하나씩 매핑
        for (int i = 0; i < names.Length; i++)
        {
            //게임 오브젝트일경우
            if (typeof(T) == typeof(GameObject))
                arr_objects[i] = Util.FindChild(gameObject, names[i], true);

            //자식의 자식 찾아야하므로 true
            else //컴포넌트일경우
                arr_objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (arr_objects[i] == null) //못찾을 경우
                Debug.Log($"Failed to Bind : {names[i]}");
        }
    }

    //컴포넌트 get
    protected T Get<T>(int _idx) where T : UnityEngine.Object
    {
        //값 추출
        UnityEngine.Object[] arr_objects = null;
        if (objects.TryGetValue(typeof(T), out arr_objects) == false)
            return null; //없으면 널
        return arr_objects[_idx] as T; //T로 형변환
    }
    protected GameObject GetObject(int _idx)
    {
        return Get<GameObject>(_idx);
    }

}
