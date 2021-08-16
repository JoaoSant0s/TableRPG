
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public static class UtilWrapper
{
    public static bool IsPointOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }

    public static bool IsPointOverScrollUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        RaycastResult result = results.Find(context => CheckScrollUI(context.gameObject));

        return result.gameObject != null;
    }

    private static bool CheckScrollUI(GameObject obj)
    {
        if (obj == null) return false;
        if (obj.GetComponent<ScrollRect>() == null) return false;

        return true;
    }

    public static bool CheckLeftButton(PointerEventData.InputButton input)
    {
        return input == PointerEventData.InputButton.Left;
    }
    public static bool CheckRightButton(PointerEventData.InputButton input)
    {
        return input == PointerEventData.InputButton.Right;
    }
}
