using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class ContextBehaviour<T> : MonoBehaviour where T : ContextBehaviour<T>
    {
        protected static T instance = null;
        protected virtual void Awake()
        {
            instance = GetComponent<T>();
        }

        public static T Instance
        {
            get { return instance; }
        }
    }
}