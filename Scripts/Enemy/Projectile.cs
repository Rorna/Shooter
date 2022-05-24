using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject FloatingDamage;
    public GameObject shootEffect;
    public float destroyTime;
    public int attack;

    private void Update()
    {
        GameObject effect = Instantiate(shootEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.1f);
        DestroyThatTime();
    }

    //피격
    private void OnCollisionEnter(Collision _collision)
    {
        if(_collision.gameObject.tag=="Player")
        {
            int damage= Random.RandomRange(1, 10); 
            ShowFloatingDamage(_collision.gameObject, attack+damage);
            _collision.gameObject.GetComponent<PlayerStat>().Hp -= (attack+damage); //데미지
        }
            
        Destroy(this.gameObject);
    }

    void ShowFloatingDamage(GameObject _hit, int _damage)
    {
        GameObject go = Instantiate(FloatingDamage, _hit.transform.position + Vector3.up * 3.5f, Quaternion.Euler(90f, 0f, 0f));
        go.GetComponent<TextMesh>().text = _damage.ToString();
    }

    void DestroyThatTime() //자동파괴
    {
        destroyTime -= Time.deltaTime;
        if(destroyTime<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
