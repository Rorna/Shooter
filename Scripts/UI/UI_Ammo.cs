using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Ammo : UI_Scene
{
    private string str;
    public Transform player;

    enum GameObjects
    {
        Ammo,
        Frame,
        Sprite,
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        SetPlayer();
    }
    private void Update()
    {
        Weapons();
    }

    void SetPlayer()
    {
        if (player == null)
        {
            if (GameObject.FindWithTag("Player") != null)
            {
                player = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }
    }

    //현재 무기 탄창 수 취득
    public void AmmoCounter(Gun _gun)
    {
        GetObject((int)GameObjects.Ammo).GetComponent<Text>().text = (str + "\n" + _gun.current_ammo.ToString() + " /" + _gun.max_ammo);
        GetObject((int)GameObjects.Sprite).GetComponent<Image>().sprite = _gun.sprite;
    }

    public void Weapons()
    {
        ///로직
        ///플레이어 하위 자식인 weapons 오브젝트에서 무기 정보 취득
        ///각 무기별로 gun 스크립트 가지고 있는데, 현재 선택된 무기만 active 되어있으므로
        ///현재의 무기 정보(탄창수) 취득 가능
        Transform weapons = player.transform.GetChild(1);
        WeaponChange wc = weapons.GetComponent<WeaponChange>();

        switch (wc.selectedWeapon)
        {
            case (int)Define.Weapons.normalGun:
                {
                    Gun gun = weapons.transform.GetChild(wc.selectedWeapon).GetComponent<Gun>();
                    str = weapons.transform.GetChild(wc.selectedWeapon).name; //무기이름
                    AmmoCounter(gun);
                }
                break;
            case (int)Define.Weapons.powerGun:
                {
                    Gun gun = weapons.transform.GetChild(wc.selectedWeapon).GetComponent<Gun>();
                    str = weapons.transform.GetChild(wc.selectedWeapon).name;
                    AmmoCounter(gun);
                }
                break;
        }


    }
}
