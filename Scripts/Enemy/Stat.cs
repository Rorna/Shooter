using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    #region Variables
    [SerializeField]
    protected int hp;
    [SerializeField]
    protected int maxHp;
    [SerializeField]
    protected int attack;
    [SerializeField]
    protected float moveSpeed;

    public int Hp { get { return hp; } set { hp = value; } }
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }
    public int Attack { get { return attack; } set { attack = value; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    #endregion
    ///피격 처리 함수
    public virtual int OnAttacked(Stat _attacker)
    {
        int damage = _attacker.Attack;

        int rand = Random.RandomRange(1, 7);
        damage -= rand;

        //타겟 체력--
        Hp -= damage; 

        return damage;
    }
}
