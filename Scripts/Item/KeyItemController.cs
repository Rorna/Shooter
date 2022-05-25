using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemController : BaseItem
{
    #region Variables
    public AudioClip getSound;
    public GameObject FloatingText;
    #endregion

    public override void Init()
    {
        ItemType = Define.Item.Key;
    }

    void Update()
    {
        Rotate();
    }

    //열쇠 획득
    protected override void OnTriggerEnter(Collider _collider)
    {
        if (_collider.transform.tag == "Player")
        {
            _collider.transform.GetComponent<PlayerController>().hasKey = true;
            Managers.Sound.Play(getSound);
            ShowFloatingText();

            Destroy(this.gameObject);
        }
    }
    protected override void ShowFloatingText()
    {
        GameObject go = Instantiate(FloatingText, transform.position + Vector3.up * 1.7f, Quaternion.Euler(90f, 0f, 0f));
        go.GetComponent<TextMesh>().color = Color.blue;
        go.GetComponent<TextMesh>().text = "Key";
    }

    
}
