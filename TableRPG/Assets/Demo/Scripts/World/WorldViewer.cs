using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldViewer : MonoBehaviour
{
    [SerializeField]
    private WorldController controller;

    #region  UI

    public void OnActiveWallBuilding()
    {
        ActiveWallBuilding();
    }

    public void OnActiveOtherState()
    {
        ActiveOtherState();
    }

    #endregion UI

    private void Update()
    {
        if (CheckWallHotKeys())
        {
            ActiveWallBuilding();
        }
        else if (CheckOtherStateHotKeys())
        {
            ActiveOtherState();
        }
    }

    private bool CheckWallHotKeys()
    {
        return Input.GetKeyUp(KeyCode.Alpha1);
    }

    private bool CheckOtherStateHotKeys()
    {
        return Input.GetKeyUp(KeyCode.Alpha2);
    }

    private void ActiveWallBuilding()
    {
        this.controller.ActiveWallBuilding();
    }

    private void ActiveOtherState()
    {
        this.controller.ActiveOtherState();
    }
}
