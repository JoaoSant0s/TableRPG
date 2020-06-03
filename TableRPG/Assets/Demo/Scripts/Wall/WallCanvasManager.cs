using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCanvasManager : MonoBehaviour
{
    [SerializeField]
    private Transform canvasWorldArea;

    [SerializeField]
    private WallUIInfo wallUIInfoPrefab;

    [SerializeField]
    private Vector2 offset;

    private List<WallUIInfo> wallUIlist;

    public List<WallUIInfo> WallUIlist
    {
        get
        {
            if (this.wallUIlist == null)
            {
                this.wallUIlist = new List<WallUIInfo>();
            }
            return this.wallUIlist;
        }
    }

    private void Awake()
    {
        Wall.ClickToShowInfo += OpenInfo;
        WorldController.ChangeWorldState += ChangeState;
    }

    private void OnDestroy()
    {
        Wall.ClickToShowInfo -= OpenInfo;
        WorldController.ChangeWorldState -= ChangeState;
    }

    private void ChangeState(WorldState state)
    {
        if (state == WorldState.WALL) return;

        for (int i = 0; i < WallUIlist.Count; i++)
        {
            Destroy(WallUIlist[i].gameObject);
        }
        WallUIlist.Clear();
    }

    private void OpenInfo(Wall wall, Vector2 position)
    {
        WallUIInfo infoPanel = Instantiate(this.wallUIInfoPrefab, (position + this.offset), Quaternion.identity, this.canvasWorldArea);
        infoPanel.ExtractWallInfo(wall);
        this.wallUIlist.Add(infoPanel);
    }
}
