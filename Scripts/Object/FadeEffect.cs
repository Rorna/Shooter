using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    #region Variables
    public Animator transition;
    public float transitionTime = 1f;
    #endregion

    public IEnumerator CrossFade()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
    }
}
