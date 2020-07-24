using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WallInfo : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnClickWallRight();
    public OnClickWallRight ClickWallRight;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!UtilWrapper.CheckRightButton(eventData.button)) return;
        
        if (ClickWallRight != null) ClickWallRight();
    }   
}
