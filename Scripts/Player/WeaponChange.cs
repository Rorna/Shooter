using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChange : MonoBehaviour
{
    #region Variables
    public bool hasPowerGun = false; //아이템 습득시 true로 변경
    public int selectedWeapon = (int)Define.Weapons.normalGun;

    public AudioClip changeWeaponSound;
    public AudioClip beep;
    public GameObject FloatingText;
    #endregion

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = (int)Define.Weapons.normalGun;
            Managers.Sound.Play(changeWeaponSound);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 
            && hasPowerGun==true) //무기 두개 이상
        {
            selectedWeapon = (int)Define.Weapons.powerGun;
            Managers.Sound.Play(changeWeaponSound);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2
            && hasPowerGun == false) 
        {
            Managers.Sound.Play(beep);
            StartCoroutine(ShowFloatingText());
        }
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform) //자식 가져오기
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }      
            i++;
        }
    }

    IEnumerator ShowFloatingText()
    {

        GameObject go = Instantiate(FloatingText, transform.parent.position + Vector3.up * 1.7f, Quaternion.Euler(90f, 0f, 0f));

        go.GetComponent<TextMesh>().color = Color.white;
        go.GetComponent<TextMesh>().text = "Can't Change Weapon";

        yield return new WaitForSeconds(1.0f);
    }

}
