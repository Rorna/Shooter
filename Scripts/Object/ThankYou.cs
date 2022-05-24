using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThankYou : MonoBehaviour
{
    private void OnTriggerEnter(Collider _collider)
    {
        if (_collider.transform.tag == "Player")
        {
            Managers.Scene.LoadScene(Define.Scene.End);
        }
    }
}
