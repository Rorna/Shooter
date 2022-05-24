using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public GameObject Instantiate(string _path, Transform _parent=null)
    {
        //기본 경로 프리팹 폴더
        GameObject original = Resources.Load<GameObject>($"Prefabs/{_path}");
        if(original == null)
        {
            Debug.Log($"Failed to Load prefab : {_path}");
            return null;
        }

        GameObject go = Object.Instantiate(original, _parent);
        return go;
    }

    public GameObject Instantiate(string _path, Vector3 _position, Quaternion _rotation)
    {
        GameObject original = Resources.Load<GameObject>($"Prefabs/{_path}");

        if (original == null)
        {
            Debug.Log($"Failed to Load prefab : {_path}");
            return null;
        } 

        GameObject go = Object.Instantiate(original, _position, _rotation);
        return go;
    }

    public void Destroy(GameObject _go)
    {
        if (_go == null) return;
        Object.Destroy(_go);
    }
}
