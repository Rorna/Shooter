using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables
    [SerializeField]
    Define.CameraMode mode = Define.CameraMode.QuaterView;

    [SerializeField]
    Vector3 delta=new Vector3(0.0f, 6.0f, -5.0f); //카메라와 플레이어간의 거리(방향)

    [SerializeField]
    GameObject player =null;

    //카메라를 플레이어에 세팅
    public void SetPlayer(GameObject _player) { player = _player; }
    #endregion

    private void Awake()
    {
        //camera setting
        if (player == null)
        {
            if(GameObject.FindWithTag("Player") != null)
            {
                player = GameObject.FindWithTag("Player");
            }
        }
    }
    void LateUpdate() //화면 떨림 방지
    {
        //카메라의 좌표 설정
        transform.position = player.transform.position + delta;
    }

    public void SetQuaterView(Vector3 _delta)
    {
        mode = Define.CameraMode.QuaterView;
        _delta = delta;
    }
    
}

