using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCanvasManager : MonoBehaviour
{

    [SerializeField]
    private WallUIConfig wallUIConfig;

    [SerializeField]
    private Transform canvasWorldArea;

    [SerializeField]
    private WallUIInfo wallUIInfoPrefab;

    [SerializeField]
    private Vector2 offset;

    private Dictionary<Wall, WallUIInfo> wallDictionary;

    public Dictionary<Wall, WallUIInfo> WallDictionary
    {
        get
        {
            if (this.wallDictionary == null)
            {
                this.wallDictionary = new Dictionary<Wall, WallUIInfo>();
            }
            return this.wallDictionary;
        }
    }

    private void Awake()
    {
        Wall.ClickToShowInfo += OpenInfo;
        WorldController.ChangeWorldState += ChangeState;
        WallUIInfo.RemoveWall += RemovePanel;
        WallBuilder.RemoveWallPane += RemoveAllPanels;
    }

    private void OnDestroy()
    {
        Wall.ClickToShowInfo -= OpenInfo;
        WorldController.ChangeWorldState -= ChangeState;
        WallUIInfo.RemoveWall -= RemovePanel;
        WallBuilder.RemoveWallPane -= RemoveAllPanels;
    }

    private void ChangeState(WorldState state)
    {
        if (state == WorldState.WALL)
        {
            this.wallUIConfig.gameObject.SetActive(true);
        }
        else
        {
            this.wallUIConfig.gameObject.SetActive(false);
            RemoveAllPanels();
        }
    }

    private void RemoveAllPanels()
    {
        foreach (var item in WallDictionary)
        {
            Destroy(item.Value.gameObject);
        }

        WallDictionary.Clear();
    }

    private void RemovePanel(Wall wall)
    {
        if (!WallDictionary.ContainsKey(wall)) return;

        var panel = WallDictionary[wall];
        WallDictionary.Remove(wall);
        Destroy(panel.gameObject);
    }

    private void OpenInfo(Wall wall, Vector2 position)
    {
        if (WallDictionary.ContainsKey(wall)) return;

        WallUIInfo infoPanel = Instantiate(this.wallUIInfoPrefab, (position + this.offset), Quaternion.identity, this.canvasWorldArea);
        infoPanel.ExtractWallInfo(wall);
        WallDictionary.Add(wall, infoPanel);
    }
}
