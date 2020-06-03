using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WallInfo : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnClickWallRight(Vector2 position);
    public OnClickWallRight ClickWallRight;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!UtilWrapper.CheckRightButton(eventData.button)) return;

        var worlPosition = eventData.position.ScreenToWorldPoint();

        if (ClickWallRight != null) ClickWallRight(worlPosition);
    }   
}
