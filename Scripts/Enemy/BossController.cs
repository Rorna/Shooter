using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BaseEnemy
{
    #region Variables
    public Transform ShootPoint;
    public GameObject bulletPrefab;
    public float bulletForce;
    #endregion
    public override void Init()
    {
        InitStat();
    }

    protected override void InitStat()
    {
        stat = GetComponent<Stat>();

        stat.MaxHp = 500;
        stat.Hp = 500;
        stat.Attack = 70;
    }

    protected override void Idle(float _distance)
    {

        if (_distance < minDist) 
        {
            state = Define.State.Attack;
        }

        if (stat.Hp <= 0)
        {
            state = Define.State.Die;
            isDead = true;
        }
    }
    protected override void Attack(float _distance)
    {
        if (_distance < minDist)
        {
            if (Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / FireRate;

                Managers.Sound.Play(attackSound, Define.Sound.Effect);
                GameObject BulletIns = Instantiate(bulletPrefab, ShootPoint.position, ShootPoint.rotation);
                Rigidbody rb = BulletIns.GetComponent<Rigidbody>();
                Projectile pj = BulletIns.GetComponent<Projectile>();
                pj.attack = stat.Attack;
                rb.AddForce(ShootPoint.forward * bulletForce); 

            }
        }
        else
        {
            state = Define.State.Idle;
        }

        if (stat.Hp <= 0)
        {
            state = Define.State.Die;
            isDead = true;
        }
    }
    protected override void Die()
    {
        if (stat.Hp <= 0)
        {
            UIHPBar.SetActive(false);

            //effect
            if (isDead)
                Explode();

            PlayerController cc = target.GetComponent<PlayerController>();
            cc.isBossDead = true;

            Destroy(this.gameObject, 1.0f);
        }
    }
}
