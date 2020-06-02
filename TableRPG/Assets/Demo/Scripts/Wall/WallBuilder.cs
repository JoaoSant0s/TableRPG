using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    public delegate void OnWallInteractions(bool interaction);
    public static OnWallInteractions WallInteractions;

    public enum MouseButton
    {
        LEFT,
        RIGHT,
        MIDDLE
    }

    public enum BuilderState
    {
        NONE,
        BUILDING
    }

    [SerializeField]
    private Transform wallsArea;

    [SerializeField]
    private Wall wallPrefab;

    [SerializeField]
    private float minWallScale;

    [SerializeField]
    private Vector2 wallScaleOffset;

    [SerializeField]
    private LayerMask layersExcludeInput;

    private List<Wall> walls;

    private BuilderState state = BuilderState.NONE;
    private Wall currentWall;

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

    void Update()
    {
        if (this.state == BuilderState.NONE) return;
        BuildWall();
    }

    private void Awake()
    {
        WorldController.ChangeWorldState += ChangeState;
    }

    private void OnDestroy()
    {
        WorldController.ChangeWorldState -= ChangeState;
    }

    private void ChangeState(WorldState state)
    {
        if (state == WorldState.WALL)
        {
            this.state = BuilderState.BUILDING;
            EnableWallInteractions(true);
        }
        else
        {
            EnableWallInteractions(false);
            this.state = BuilderState.NONE;
            RemoveCurrentWall();
        }
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

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyUp(KeyCode.Z))
        {
            UndoLastWall();
        }
        else
        {
            if (Input.GetMouseButtonUp((int)MouseButton.RIGHT))
            {
                EnableWallInteractions(true);
                RemoveCurrentWall();
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
    }

    private bool CanBuildWall()
    {
        return this.state == BuilderState.BUILDING && !UtilWrapper.IsPointOverUIObject() && !this.IsPointOverWallInteractionsArea();
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

    private void UndoLastWall()
    {
        if (Walls.Count == 0) return;
        this.currentWall = null;
        var wall = Walls[Walls.Count - 1];

        Walls.Remove(wall);
        Destroy(wall.gameObject);
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

        Wall wall = Instantiate(this.wallPrefab, worldPoint, Quaternion.identity, this.wallsArea);
        Walls.Add(wall);
        this.currentWall = wall;
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

