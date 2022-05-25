using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : BaseItem
{
    #region Variables
    public AudioClip getSound;
    public GameObject FloatingText;
    #endregion

    public override void Init()
    {
        ItemType = Define.Item.Weapon;
    }

    void Update()
    {
        Rotate();
    }

    //무기 획득
    protected override void OnTriggerEnter(Collider _collider)
    {
        if (_collider.transform.tag == "Player")
        {
            _collider.transform.GetComponentInChildren<WeaponChange>().hasPowerGun=true;
            Managers.Sound.Play(getSound);
            ShowFloatingText();

            Destroy(this.gameObject);
        }
    }

    //텍스트 출력
    protected override void ShowFloatingText()
    {
        GameObject go = Instantiate(FloatingText, transform.position + Vector3.up * 1.7f, Quaternion.Euler(90f, 0f, 0f));
        go.GetComponent<TextMesh>().text = "Power Gun";
    }
}
