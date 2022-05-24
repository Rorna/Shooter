using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    enum GameObjects
    {
        HPBar,
    }

    Stat stat; //체력 정보 추출용

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        stat = transform.parent.GetComponent<Stat>(); //부모객체의 Stat 뽑아옴
    }
    private void LateUpdate()
    {

        Transform parent = transform.parent;

        //위치 설정
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);

        //체력바가 바라보고 있는 방향을 카메라의 방향과 일치시킴
        transform.rotation = Camera.main.transform.rotation;

        ///체력 비율
        float ratio = stat.Hp / (float)stat.MaxHp;
        //체력 설정
        SetHpRatio(ratio);
        
    }

    ///체력바 수치 조절 함수
    ///ratio: 비율
    public void SetHpRatio(float _ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = _ratio;
    }
}
