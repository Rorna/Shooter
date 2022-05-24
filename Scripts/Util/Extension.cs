﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject _go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(_go);
    }

}