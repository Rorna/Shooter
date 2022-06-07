using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{

    private void Start()
    {
        SetStat(); //스탯 반영
    }

    //스탯 설정
    public void SetStat()
    {
        Hp = 500;
        MaxHp = 500;
        Attack = 30;
        MoveSpeed = 9;

    }
}
