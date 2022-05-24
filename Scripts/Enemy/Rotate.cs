using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    //회전 스크립트 빙글빙글
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected Vector3 rotation;

    void Update()
    {
        RotateObject();
    }

    void RotateObject()
    {
        transform.Rotate(rotation * Time.deltaTime * speed);
    }
}
