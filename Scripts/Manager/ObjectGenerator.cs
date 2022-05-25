using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    #region Variables
    [SerializeField]
    int objectCount = 0; //현재 오브젝트 수
    int reserveCount = 0;
    [SerializeField]
    int keepCount = 0; //유지시켜야하는 오브젝트 수
    [SerializeField]
    float spawnTime = 3.0f; //0~5초 사이

    //border position
    public float x1;
    public float x2;
    public float y1;
    public float y2;
    #endregion

    //오브젝트 카운트 추가
    public void AddObjectCount(int _value) { objectCount += _value; }
    //유지시켜야 하는 오브젝트 수 세팅
    public void SetKeepObjectCount(int _count) { keepCount = _count; }
    void Start()
    {
        //이벤트 생성
        Managers.Game.OnSpwanEvent -= AddObjectCount;
        Managers.Game.OnSpwanEvent += AddObjectCount;
    }

    void Update()
    {
        //내가 예약한 오브젝트 수와 현재 오브젝트 수의 합이 전체보다 부족하면 생성
        while (reserveCount + objectCount < keepCount)
        {
            StartCoroutine(ReserveSpawn());
        }
    }

    IEnumerator ReserveSpawn() //생성
    {
        reserveCount++; //예약

        //지정값 사이에서 랜덤하게 스폰
        yield return new WaitForSeconds(Random.Range(0, spawnTime));
        GameObject obj = Managers.Game.Spawn(Define.ObjectType.Item, "HP_Item_Pool");

        //랜덤 위치
        Vector3 randomPos = new Vector3(Random.RandomRange(x1, x2), 1.4f, Random.RandomRange(y1, y2));

        obj.transform.position = randomPos; 

        reserveCount--;
    }
}
