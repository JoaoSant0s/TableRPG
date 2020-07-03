using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    protected static T instance = null;
    protected static Transform _transform = null;
    protected virtual void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(GetComponent<T>());
        }
        else
        {
            _transform = transform;
            instance = GetComponent<T>();
        }
    }

    public static T Instance
    {
        get { return instance; }
    }

    public static Transform Transform
    {
        get { return _transform; }
    }
}
