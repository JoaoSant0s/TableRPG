using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuilder : MonoBehaviour
{
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
    private BuilderState state = BuilderState.NONE;    
    [SerializeField]
    private List<Wall> walls;

    private bool movingMouse;
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

    private void BuildWall()
    {
        if (this.state != BuilderState.BUILDING) return;

        if (Input.GetMouseButtonUp((int)MouseButton.RIGHT))
        {
            if (this.currentWall != null)
            {
                Walls.Remove(this.currentWall);
                Destroy(this.currentWall.gameObject);
            }
        }

        if (Input.GetMouseButtonDown((int)MouseButton.LEFT))
        {
            Vector3 worldPoint = MouseWorldPosition();

            Wall wall = Instantiate(this.wallPrefab, worldPoint, Quaternion.identity, this.wallsArea);
            Walls.Add(wall);
            this.currentWall = wall;
        }
        else if (!Input.GetMouseButtonUp((int)MouseButton.LEFT) && this.currentWall != null)
        {
            Vector3 worldPoint = MouseWorldPosition();
            Vector3 direction = (worldPoint - this.currentWall.Position);

            this.currentWall.Rotate(direction);
            this.currentWall.Scale(direction);
        }
    }

    private Vector3 MouseWorldPosition()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = this.wallsArea.position.z;
        return worldPoint;
    }
}

