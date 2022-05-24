using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{
    GameObject player;
    HashSet<GameObject> monsters = new HashSet<GameObject>(); 

    public Action<int> OnSpwanEvent;
    public GameObject GetPlayer() { return player; } //player 반환

    //생성 함수
    public GameObject Spawn(Define.ObjectType _type, string _path, Transform _parent = null)
    {
        //생성
        GameObject go = Managers.Resource.Instantiate(_path, _parent);
        //타입 체크
        switch (_type)
        {
            //해시에 삽입
            case Define.ObjectType.Enemy:
                {
                    monsters.Add(go);

                    //정보 전달
                    if (OnSpwanEvent != null)
                        OnSpwanEvent.Invoke(1);
                }
                break;
            case Define.ObjectType.Player:
                player = go;
                break;
            case Define.ObjectType.Item:
                {
                    monsters.Add(go);

                    if (OnSpwanEvent != null)
                        OnSpwanEvent.Invoke(1);
                }
                break;
        }
        return go;
    }

    //타입 반환 함수
    public Define.ObjectType GetWorldObjectType(GameObject _go)
    {
        ObjectType objectType = _go.GetComponent<ObjectType>();
        if (objectType == null)
            return Define.ObjectType.Unknown;
        return objectType.ot;
    }

    //디스폰
    public void Despawn(GameObject _go)
    {
        //타입 갖고옴
        Define.ObjectType type = GetWorldObjectType(_go);

        switch (type)
        {
            case Define.ObjectType.Enemy:
                {
                    if (monsters.Contains(_go)) //있으면
                    {
                        monsters.Remove(_go); //삭제

                        if (OnSpwanEvent != null)
                            OnSpwanEvent.Invoke(-1);
                    }
                }
                break;
            case Define.ObjectType.Player:
                {
                    if (player == _go)
                        player = null;
                }
                break;
            case Define.ObjectType.Item:
                {
                    if (monsters.Contains(_go))
                    {
                        monsters.Remove(_go);

                        if (OnSpwanEvent != null)
                            OnSpwanEvent.Invoke(-1);
                    }
                }
                break;
        }
        Managers.Resource.Destroy(_go);
    }
}
