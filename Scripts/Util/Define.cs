using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum ObjectType
    {
        Unknown,
        Player,
        Enemy,
        Item,
    }
    public enum State
    {
        Idle,
        Move,
        Attack,
        Die,
        Dash,
    }
    public enum Scene
    {
        Unknown,
        Start,
        InGame,
        End,
        GameOver,
    }
    public enum Item
    {
        Unknown,
        Key,
        HPRecover,
        Weapon,

    }
    public enum Weapons
    {
        normalGun,
        powerGun,
    }
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
    public enum CameraMode
    {
        QuaterView,
    }

}
