using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TableRPG;

public class WallBuilder : MonoBehaviour
{
    public delegate void OnWallInteractions(bool interaction);
    public static OnWallInteractions WallInteractions;

    public delegate void OnRemoveWallPane();
    public static OnRemoveWallPane RemoveWallPane;

    public enum MouseButton
    {
        LEFT,
        RIGHT,
        MIDDLE
    }

    public enum BuilderState
    {
        NONE,
        WALL,
        DOOR
    }

    [SerializeField]
    private Transform wallsArea;

    [SerializeField]
    private LayerMask layersExcludeInput;

    [Header("Prefabs")]

    [SerializeField]
    private Wall wallPrefab;

    [SerializeField]
    private Wall doorPrefab;

    [Header("Wall manipulation Settings")]

    [SerializeField]
    private float minWallScale;

    [SerializeField]
    private Vector2 wallScaleOffset;

    private List<Wall> walls;
    private BuilderState state = BuilderState.NONE;
    private Wall currentWall;
    private MapController mapController;

    public List<Wall> Walls
    {
        get
        {
            if (this.walls == null)
            {
                this.walls = new List<Wall>();
            }
            return this.walls;
        }
    }

    private void Awake()
    {
        WorldController.ChangeWorldState += ChangeState;

        MapManagerController.UpdateMapContent += LoadMap;
        WallInteractor.EndWallManipulation += UpdateWallData;

        WallSubActionViewer.GenerateWall += SetWallState;
        WallSubActionViewer.GenerateDoor += SetDoorState;
    }

    private void OnDestroy()
    {
        WorldController.ChangeWorldState -= ChangeState;

        MapManagerController.UpdateMapContent -= LoadMap;
        WallInteractor.EndWallManipulation += UpdateWallData;

        WallSubActionViewer.GenerateWall -= SetWallState;
        WallSubActionViewer.GenerateDoor -= SetDoorState;
    }

    void Update()
    {
        if (this.state == BuilderState.NONE) return;
        BuildWall();
    }

    private void SetWallState()
    {
        this.state = BuilderState.WALL;
        EnableWallInteractions(true);
    }

    private void SetDoorState()
    {
        this.state = BuilderState.DOOR;
        EnableWallInteractions(true);
    }

    private void ChangeState(WorldState state)
    {
        if (state == WorldState.WALL) return;

        EnableWallInteractions(false);
        this.state = BuilderState.NONE;
        RemoveCurrentWall();
    }

    private void LoadMap(MapController map)
    {
        this.mapController = map;
        LoadWall(map.WallData);
        EnableWallInteractions(false);
    }

    private void LoadWall(WallData data)
    {
        DeleteCurrentWall();

        GenerateLoadedWall(data);
    }

    private void DeleteCurrentWall()
    {
        EnableWallInteractions(false);

        this.currentWall = null;

        for (int i = 0; i < Walls.Count; i++)
        {
            var wall = Walls[i];
            Destroy(wall.gameObject);
        }
        Walls.Clear();

        if (RemoveWallPane != null)
        {
            RemoveWallPane();
        }
    }

    private void GenerateLoadedWall(WallData data)
    {
        for (int i = 0; i < data.WallDefinitions.Count; i++)
        {
            WallConfig config = data.WallDefinitions[i];
            CreateWallByConfig(config);
        }

        EnableWallInteractions(true);
    }

    private void CreateWallByConfig(WallConfig config)
    {
        Wall wall = Instantiate(UsePrefabMode(config.type), config.position, Quaternion.identity, this.wallsArea);

        wall.Rotate(config.rotation);
        wall.Scale(config.scale);

        Walls.Add(wall);
    }   

    private void EnableWallInteractions(bool active)
    {
        if (WallInteractions != null)
        {
            WallInteractions(active);
        }
    }

    private void BuildWall()
    {
        if (!CanBuildWall()) return;

        if (Input.GetMouseButtonUp((int)MouseButton.RIGHT) && this.currentWall != null)
        {
            EnableWallInteractions(true);
            RemoveCurrentWall();
            UpdateWallData();
        }

        if (Input.GetMouseButtonDown((int)MouseButton.LEFT))
        {
            CreateCurrentWall();
            EnableWallInteractions(false);
        }
        else if (!Input.GetMouseButtonUp((int)MouseButton.LEFT) && this.currentWall != null)
        {
            UpdateCurrentWall();
        }
    }

    private void UpdateWallData()
    {
        this.mapController.UpdateWallData(new WallData(Walls));
    }

    private bool CanBuildWall()
    {
        return this.state != BuilderState.NONE && !UtilWrapper.IsPointOverUIObject() && !this.IsPointOverWallInteractionsArea();
    }

    private bool IsPointOverWallInteractionsArea()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, this.layersExcludeInput);

        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    private void RemoveCurrentWall()
    {
        if (this.currentWall != null)
        {
            Walls.Remove(this.currentWall);
            Destroy(this.currentWall.gameObject);
        }
    }

    private void CreateCurrentWall()
    {
        Vector3 worldPoint = MouseWorldPosition();

        Wall wall = Instantiate(UsePrefabMode(), worldPoint, Quaternion.identity, this.wallsArea);
        Walls.Add(wall);
        this.currentWall = wall;
    }

    private Wall UsePrefabMode()
    {
        switch (this.state)
        {
            case BuilderState.DOOR:
                return this.doorPrefab;
            case BuilderState.WALL:
                return this.wallPrefab;
            default:
                return this.wallPrefab;
        }
    }

    private Wall UsePrefabMode(WallType type)
    {
        switch (type)
        {
            case WallType.DOOR:
                return this.doorPrefab;
            case WallType.WALL:
                return this.wallPrefab;
            default:
                return this.wallPrefab;
        }
    }

    private void UpdateCurrentWall()
    {
        Vector3 worldPoint = MouseWorldPosition();
        Vector3 direction = (worldPoint - this.currentWall.Position);

        this.currentWall.Rotate(direction);

        var scale = ScaleOffset(direction);

        this.currentWall.Scale(scale);
    }

    private float ScaleOffset(Vector3 direction)
    {
        var directionScale = direction.ApplyOffset(this.wallScaleOffset);

        var scale = Mathf.Max(this.minWallScale, directionScale.magnitude);

        return scale;
    }

    private Vector3 MouseWorldPosition()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = this.wallsArea.position.z;
        return worldPoint;
    }
}

