﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 2f;

    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }

}
