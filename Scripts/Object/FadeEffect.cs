using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public IEnumerator CrossFade()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
    }
}
