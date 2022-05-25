using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    #region Variables
    public Define.ObjectType objectType { get; protected set; } = Define.ObjectType.Enemy;

    protected Define.State state = Define.State.Idle;

    protected Stat stat;

    //target
    public float minDist = 1f;
    public Transform target;

    //time
    public float FireRate; //5면 1초에 5번
    public float nextTimeToFire;

    public float distance;

    public GameObject UIHPBar;

    //Explode 함수 여러번 실행 방지
    protected bool isDead = false;

    public GameObject ExplodeEffect;
    public AudioClip deadSound;
    public AudioClip attackSound;
    #endregion

    private void Start()
    {
        if (target == null)
        {
            if (GameObject.FindWithTag("Player") != null)
            {
                target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }

        Init();
    }

    //상태머신
    private void Update()
    {
        if (target == null)
            return;

        distance = Vector3.Distance(transform.position, target.position);

        switch (state)
        {
            case Define.State.Idle:
                Idle(distance);
                break;
            case Define.State.Move:
                Move(distance);
                break;
            case Define.State.Attack:
                Attack(distance);
                break;
            case Define.State.Die:
                Die();
                break;
        }
    }

    public abstract void Init();
    protected virtual void InitStat() { }
    protected virtual void Idle(float _distance) { }
    protected virtual void Move(float _distance) { }
    protected virtual void Attack(float _distance) { }
    protected virtual void Die() { }

    //타겟 바라봄
    protected void LookAt()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion quat = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, quat, 10 * Time.deltaTime);
    }
    //사망 이펙트
    protected void Explode()
    {
        GameObject effect = Instantiate(ExplodeEffect, transform.position, Quaternion.identity);
        Managers.Sound.Play(deadSound);
        Destroy(effect, 1.0f);
        isDead = false;
    }
}

