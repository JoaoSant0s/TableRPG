using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallUIConfig : MonoBehaviour
{
    public delegate void OnSaveWall();
    public static OnSaveWall SaveWall;

    public delegate void OnLoadWall(WallData data);
    public static OnLoadWall LoadWall;

    private WallData lastWallData;

    private void Awake()
    {
        WallBuilder.SavingWallData += SavingWallData;
    }

    private void OnDestroy()
    {
        WallBuilder.SavingWallData -= SavingWallData;
    }

    private void SavingWallData(WallData wallData)
    {
        this.lastWallData = wallData;
    }

    #region UI
    public void OnSaveCurrentWall()
    {
        if (SaveWall != null)
        {
            SaveWall();
        }
    }

    public void OnLoadLastSavedWall()
    {
        if (LoadWall == null || this.lastWallData == null) return;

        LoadWall(this.lastWallData);
    }
    #endregion UI
}
