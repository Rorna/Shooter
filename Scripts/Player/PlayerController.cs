using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private enum State
    {
        Move, //이동
        Roll, //회피
        Die
    }

    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private GameObject HPBar;

    public Camera cam;
    private Rigidbody rigidbody;
    private Vector3 moveDir; //방향
    private Vector3 rollDir;

    private float rollSpeed; //회피 속도

    private State state;

    public PlayerStat stat;

    private Animator anim;

    private bool isDead = false;

    public AudioClip avoidSound;
    public AudioClip deadSound;

    public AudioClip BGM;

    //열쇠 변수 추가
    public bool hasKey = false;
    public bool isBossDead = false;

    void Awake()
    {
        cam =  Camera.main;
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        state = State.Move;
        stat = GetComponent<PlayerStat>();

        Managers.Sound.Play(BGM, Define.Sound.Bgm);
    }

    void Update()
    {
        PlayerAim();
        
        switch (state)
        {
            case State.Move:
                Moving();
                break;
            case State.Roll:
                Rolling();
                break;
            case State.Die:
                Die();
                break;
        }   
    }

    //마우스 위치에 맞게 조준
    void PlayerAim()
    {
        if(stat.Hp>0 && !PauseMenu.isPaused)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float raylength;
            if (groundPlane.Raycast(ray, out raylength))
            {
                Vector3 pointToLook = ray.GetPoint(raylength);
                Debug.DrawLine(ray.origin, pointToLook, Color.blue);
                transform.LookAt(pointToLook);
            }
        }
    }

    void Moving()
    {
        float moveX = 0f;
        float moveZ = 0f;

        //애니메이션
        if (Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.S) == false &&
            Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == false)
        {
            anim.SetBool("isRun", false);
            if (anim.GetBool("isJump") == true)
                anim.SetBool("isJump", false);
        }
        else //이동
        {
            anim.SetBool("isRun", true);
            anim.SetBool("isJump", false);
            if (Input.GetKey(KeyCode.W)) moveZ = +1f;
            if (Input.GetKey(KeyCode.S)) moveZ = -1f;
            if (Input.GetKey(KeyCode.A)) moveX = -1f;
            if (Input.GetKey(KeyCode.D)) moveX = +1f;

        }
        moveDir = new Vector3(moveX, 0, moveZ).normalized;

        rigidbody.velocity = moveDir * stat.MoveSpeed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rollDir = moveDir;
            rollSpeed = 10f;
            state = State.Roll;
            if (anim.GetBool("isJump") == false)
                anim.SetBool("isJump", true);

            Managers.Sound.Play(avoidSound);

        }

        if (stat.Hp <= 0) //체력 0인지 항상 검사
        {
            state = State.Die;
            isDead = true;
        }
    }
 
    void Rolling()
    {
        rigidbody.velocity = rollDir * rollSpeed;

        float rollSpeedDropMultiplier = 2f; //감소수
        rollSpeed -= rollSpeed * rollSpeedDropMultiplier * Time.deltaTime;
        float rollSpeedMinimum = 3f; //최저 구르기 속도
        if (rollSpeed < rollSpeedMinimum)
            state = State.Move;

        if (stat.Hp <= 0)
        {
            state = State.Die;
            isDead = true;
        }
    }

    void Die()
    {
        if(stat.Hp<=0)
        {

            if (isDead)
            {
                stat.Hp = 0;

                anim.Play("DIE");

                Managers.Sound.Play(deadSound);

                weapon.SetActive(false);
                HPBar.SetActive(false);

                isDead = false; //반복실행 방지

                GameOver();
            }
        }
    }
    void GameOver()
    {
        Managers.Scene.LoadScene(Define.Scene.GameOver);
    }
}
