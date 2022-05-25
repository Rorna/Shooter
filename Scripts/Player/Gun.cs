using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    #region Variables
    public int damage = 10;
    public float range = 100f;
    public int current_ammo;
    public int max_ammo;

    public float reloadTime = 1f;
    private bool isReloading = false;

    public float shotDelay; //연사 가능 방지
    private float current_Delay;

    public LineRenderer bulletTrail;
    public ParticleSystem muzzleFlash; //총구 번쩍임

    public GameObject shotEffect; //effect

    private Transform WeaponTransform;
    private Animator aimAnimator;

    public PlayerStat _stat;

    public GameObject FloatingDamage;

    public float knockbackForce = 100f;

    public AudioClip gunsound;
    public AudioClip reloadSound;

    public GameObject FloatingText;

    public Sprite sprite;
    #endregion

    private void Start()
    {      
        current_ammo = max_ammo;
        _stat = transform.GetComponentInParent<PlayerStat>();
        current_Delay = shotDelay; //바로 대입
    }
    void Update()
    {
        current_Delay -= Time.deltaTime;
        
        if (isReloading) //과도한 연산 방지
            return;

        if (current_ammo <= 0)
        {
            StartCoroutine(Reload());
            return; //다음 단계로 안가기 위해
        }
        if(Input.GetKeyDown(KeyCode.R) && current_ammo!=max_ammo)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (current_Delay <= 0)
            {
                current_Delay = shotDelay;
                Shoot();
            }
        }
    }

    //사격
    void Shoot()
    {
        current_ammo--;
        muzzleFlash.Play();
        RaycastHit hit;

        //레이캐스트 맞으면
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            SpwanBulletTrail(hit.point);
            HitEffect(shotEffect, hit);

            Managers.Sound.Play(gunsound, Define.Sound.Effect);

            //camerashake
            WeaponChange wc = transform.parent.GetComponent<WeaponChange>();
            if(wc.selectedWeapon ==(int)Define.Weapons.powerGun) //파워건만 흔들흔들
                ShakeCamera.Instance.OnShakeCamera();

            if (hit.collider.tag == "monster" || hit.collider.tag=="turret")
            {
                int realDamage = damage + _stat.Attack - (Random.RandomRange(1, 5));
                if (FloatingDamage)
                    ShowFloatingDamage(hit.transform.gameObject, realDamage);

                //넉백
                if(hit.collider.tag == "monster")
                    hit.transform.position += transform.forward * Time.deltaTime * knockbackForce;

                hit.transform.gameObject.GetComponent<Stat>().Hp -= realDamage;
            }
        }
    }

    //데미지 표시
    void ShowFloatingDamage(GameObject _hit, int _damage)
    {
        GameObject go = Instantiate(FloatingDamage, _hit.transform.position + Vector3.up * 1.7f, Quaternion.Euler(90f, 0f, 0f));
        go.GetComponent<TextMesh>().text = _damage.ToString();
    }

    //재장전
    IEnumerator Reload()
    {
        isReloading = true;

        //effect
        StartCoroutine( ShowFloatingText());

        //sound
        Managers.Sound.Play(reloadSound);

        yield return new WaitForSeconds(reloadTime);

        current_ammo = max_ammo;
        isReloading = false;
    }

    //사격 궤적 생성
    private void SpwanBulletTrail(Vector3 _hitPoint)
    {
        GameObject bulletTrailEffetct = Instantiate(bulletTrail.gameObject, transform.position, Quaternion.identity);
        LineRenderer lineR = bulletTrailEffetct.GetComponent<LineRenderer>();

        lineR.SetPosition(0, transform.position);
        lineR.SetPosition(1, _hitPoint);

        Destroy(bulletTrailEffetct, 1f);
    }

    //타격 효과(상대편)
    private void HitEffect(GameObject _go, RaycastHit _hit)
    {
        _go.transform.position = _hit.point;
        GameObject effect = Instantiate(_go, _go.transform.position, Quaternion.identity);
        Destroy(effect, 0.1f);
    }

    //리로드 텍스트
    IEnumerator ShowFloatingText()
    {
        GameObject go = Instantiate(FloatingText, transform.parent.position + Vector3.up * 1.7f, Quaternion.Euler(90f, 0f, 0f));

        go.GetComponent<TextMesh>().color = Color.white;
        go.GetComponent<TextMesh>().text = "Reload...";

        yield return new WaitForSeconds(1.0f); 
    }
}
