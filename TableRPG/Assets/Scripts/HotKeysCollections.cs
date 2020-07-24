using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotKeysCollections
{
    public static bool ButtonEsc
    {
        get { return Input.GetKeyUp(KeyCode.Escape); }
    }

    public static bool ButtonAlpha1
    {
        get { return Input.GetKeyUp(KeyCode.Alpha1); }
    }

    public static bool ButtonAlpha2
    {
        get { return Input.GetKeyUp(KeyCode.Alpha2); }
    }

    public static bool ButtonAlpha3
    {
        get { return Input.GetKeyUp(KeyCode.Alpha3); }
    }

    public static bool ButtonSpace
    {
        get { return Input.GetKeyUp(KeyCode.Space); }
    }
}
