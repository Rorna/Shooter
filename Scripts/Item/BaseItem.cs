using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected Vector3 rotation;
    [SerializeField]
    public Define.Item ItemType { get; protected set; } = Define.Item.Unknown;
    public Define.ObjectType objectType { get; protected set; } = Define.ObjectType.Item;

    private void Start()
    {
        Init();
    }
    protected void Rotate() //아이템 회전
    {
        transform.Rotate(rotation * Time.deltaTime * speed);
    }

    public abstract void Init();
    protected virtual void ShowFloatingText() { }
    protected virtual void OnTriggerEnter(Collider _collider) { }
}
