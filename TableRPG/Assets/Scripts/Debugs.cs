using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugs : MonoBehaviour
{
    public static void Log(object parameter, params object[] list)
    {
        string debubLog = BuildFormat(parameter, list);
        Debug.Log(debubLog);
    }

    public static void Error(object parameter, params object[] list)
    {
        string debubLog = BuildFormat(parameter, list);
        Debug.LogError(debubLog);
    }

    private static string BuildFormat(object parameter, params object[] list)
    {
        string debubLog = parameter.ToString();

        foreach (var item in list)
        {
            debubLog += string.Format(", {0}", item);
        }

        return debubLog;
    }
}
