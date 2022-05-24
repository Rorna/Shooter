using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItem : BaseItem
{
    [SerializeField]
    private int recoverHP;
    public AudioClip recoverSound;
    public GameObject FloatingText;

    void Update()
    {
        Rotate();
    }

    public override void Init()
    {
        ItemType = Define.Item.HPRecover;
    }

    //회복
    protected override void OnTriggerEnter(Collider _collider)
    {
        if (_collider.transform.tag == "Player")
        {
            PlayerStat _stat = _collider.transform.GetComponent<PlayerStat>();
            if (_stat.Hp + recoverHP >= _stat.MaxHp)
            {
                _stat.Hp = _stat.MaxHp;
            }
            else
                _stat.Hp += recoverHP;

            Managers.Sound.Play(recoverSound);
            ShowFloatingText();

            Managers.Game.Despawn(gameObject);
        }
    }

    protected override void ShowFloatingText()
    {
        GameObject go = Instantiate(FloatingText, transform.position + Vector3.up * 1.7f, Quaternion.Euler(90f, 0f, 0f));
        go.GetComponent<TextMesh>().color = Color.green;
        go.GetComponent<TextMesh>().text = "Recover HP";
    }
}
