using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseEnemy
{
    #region Variables
    private Animator anim;

    public float attackRange = 2f;
    public int damage = 5;
    public float speed;

    public GameObject FloatingDamage;

    public override void Init()
    {
        stat = GetComponent<Stat>();
        anim = GetComponent<Animator>();
        InitStat();
    }
    #endregion
    protected override void InitStat()
    {
        stat.MaxHp = 200;
        stat.Hp = 200;
        stat.Attack = 50;
        stat.MoveSpeed = 5.0f;
    }

    protected override void Idle(float _distance)
    {
        anim.Play("IDLE");

        if (_distance < minDist)
        {
            LookAt();
            state = Define.State.Move;
        }
        if(stat.Hp <= 0)
        {
            state = Define.State.Die;
            isDead = true;
        }
    }
    protected override void Move(float _distance)
    {
        anim.Play("MOVE");

        if (_distance < minDist) //탐색 거리 안에 플레이어 있다면
        {
            LookAt(); //타겟 바라봄
            transform.position += transform.forward * speed * Time.deltaTime; //이동

            if (_distance < attackRange) //공격범위 안에 들어가면
            {
                state = Define.State.Attack;
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
    protected override void Attack(float _distance)
    {
        if (_distance < attackRange)
        {
            if (Time.time > nextTimeToFire) //공격횟수
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                anim.CrossFade("ATTACK", 0.1f, -1, 0);
                Managers.Sound.Play(attackSound);

                //공격
                Stat targetStat = target.GetComponent<Stat>();
                int damage = targetStat.OnAttacked(stat); //본인의 스탯

                ShowFloatingDamage(target.gameObject, damage);
            }
        }
        else
        {
            anim.Play("MOVE");
            state = Define.State.Move;
        }

        if (stat.Hp <= 0)
        {
            state = Define.State.Die;
            isDead = true;
        }
    }

    void ShowFloatingDamage(GameObject _hit, int _damage)
    {
        GameObject go = Instantiate(FloatingDamage, _hit.transform.position + Vector3.up * 5.5f, Quaternion.Euler(90f, 0f, 0f));
        go.GetComponent<TextMesh>().text = _damage.ToString();
    }

    protected override void Die()
    {
        if (stat.Hp <= 0)
        {
            //effect
            UIHPBar.SetActive(false);
            
            anim.Play("DIE");

            //effect
            if(isDead)
                Explode();

            Destroy(this.gameObject, 1.0f);
        }
    }

}
