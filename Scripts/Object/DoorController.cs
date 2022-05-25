using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///자동문 하고자하는 문에 애니메이션 만든 후에
///그 문을 넣음 됨
public class DoorController : MonoBehaviour
{
    #region Variables
    public AudioClip beepSound;
    public AudioClip doorSound;

    public GameObject FloatingText;

    private Animator anim;

    //움직이고자 하는 문 넣기
    public GameObject Door;
    #endregion

    void Start()
    {
        anim = Door.transform.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider _collider)
    {
        if (_collider.transform.tag == "Player")
        {
            StopCoroutine(DoorSystem(_collider));
            StartCoroutine(DoorSystem(_collider));
        }
    }

    void ShowFloatingText()
    {
        GameObject go = Instantiate(FloatingText, transform.position + Vector3.up * 1.7f, Quaternion.Euler(90f, 0f, 0f));
        go.GetComponent<TextMesh>().color = Color.red;
        go.GetComponent<TextMesh>().text = "Can't Open!";
    }

    IEnumerator DoorSystem(Collider _collider)
    {
        PlayerController charCon = _collider.transform.GetComponent<PlayerController>();
        if (charCon.hasKey && charCon.isBossDead) //키를 가지고 있다면, 보스 죽였으면
        {
            anim.SetBool("canOpen", true);
            Managers.Sound.Play(doorSound);

            //다시 닫힘
            yield return new WaitForSeconds(3.0f);
            anim.SetBool("canOpen", false);
            Managers.Sound.Play(doorSound);

        }
        else
        {
            Managers.Sound.Play(beepSound);
            ShowFloatingText();
        }

    }
}
