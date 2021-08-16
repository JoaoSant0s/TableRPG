using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TableRPG;

public class WallCanvasManager : MonoBehaviour
{
    private void Awake()
    {
        Wall.ClickToShowInfo += OpenInfo;
        WorldController.ChangeWorldState += ChangeState;
        WallBuilder.RemoveWallPane += RemoveAllPanels;
    }

    private void OnDestroy()
    {
        Wall.ClickToShowInfo -= OpenInfo;
        WorldController.ChangeWorldState -= ChangeState;
        WallBuilder.RemoveWallPane -= RemoveAllPanels;
    }

    private void ChangeState(WorldState state)
    {
        if (state == WorldState.WALL) return;

        RemoveAllPanels();
    }

    private void RemoveAllPanels()
    {
        PopupManager.Instance.CloseAllWallPopup();
    }    

    private void OpenInfo(Wall wall)
    {
        PopupManager.Instance.ShowWallPopup(wall);
    }
}
