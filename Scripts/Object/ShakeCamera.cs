using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    #region Variables
    private static ShakeCamera instance; //어디서든 가져다 쓸수있게 싱글턴

    public static ShakeCamera Instance { get { return instance; } }

    [SerializeField]
    private float shakeTime;

    [SerializeField]
    private float shakeIntensity;
    #endregion

    public ShakeCamera()
    {
        instance = this;
    }

    public void OnShakeCamera(float _shakeTime=1.0f, float _shakeIntensity=0.1f)
    {
        this.shakeTime = _shakeTime;
        this.shakeIntensity = _shakeIntensity;

        StopCoroutine(CameraShake());
        StartCoroutine(CameraShake());
    }

    IEnumerator CameraShake()
    {
        Vector3 startRotation = transform.eulerAngles;

        float power = 10f;

        while (shakeTime > 0.0f)
        {
            float x = 0;
            float y = 0;
            float z = Random.Range(-1f, 1f);
            transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * shakeIntensity * power);

            shakeTime -= Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.Euler(startRotation);

        yield return null;
    }
}
