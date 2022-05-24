using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : BaseEnemy
{
    public Transform ShootPoint; 
    public GameObject bulletPrefab; 
    public float bulletForce; 

    public override void Init()
    {
        stat = GetComponent<Stat>();
        InitStat();
    }

    protected override void InitStat()
    {
        stat.MaxHp = 300;
        stat.Hp = 300;
        stat.Attack = 60;
    }

    protected override void Idle(float _distance)
    {
        if (_distance < minDist) //사정거리 내에 들어오면
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
            LookAt(); //타겟 방향 바라봄
            if (Time.time > nextTimeToFire) //설정값에 따라 발사
            {
                nextTimeToFire = Time.time + 1 / FireRate;

                Managers.Sound.Play(attackSound, Define.Sound.Effect);
                GameObject BulletIns = Instantiate(bulletPrefab, ShootPoint.position, transform.rotation);
                Rigidbody rb = BulletIns.GetComponent<Rigidbody>();
                Projectile pj = BulletIns.GetComponent<Projectile>();


                pj.attack = stat.Attack; //데미지
                rb.AddForce(ShootPoint.forward * bulletForce); //속도
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
            if(isDead)
                Explode();

            Destroy(this.gameObject, 1.0f);
        }
    }
}
