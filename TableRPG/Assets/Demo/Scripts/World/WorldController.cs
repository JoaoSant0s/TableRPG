using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorldState
{
    NONE,
    WALL
}

[ExecuteAlways]
public class WorldController : MonoBehaviour
{
    public delegate void OnChangeWorldState(WorldState state);
    public static OnChangeWorldState ChangeWorldState;

    [SerializeField]
    private WallBuilder wallBuilder;

    [SerializeField]
    private WorldState state;

    public WorldState State
    {
        get { return this.state; }
        set
        {
            this.state = value;
            if (ChangeWorldState != null) ChangeWorldState(this.state);
        }
    }

    private void OnValidate()
    {
        if (ChangeWorldState != null) ChangeWorldState(this.state);
    }

    private void Start()
    {
        this.State = WorldState.NONE;
        if (ChangeWorldState != null)
        {
            ChangeWorldState(this.State);
        }
    }

    public void ActiveWallBuilding()
    {
        this.State = WorldState.WALL;
    }

    public void ActiveOtherState()
    {
        this.State = WorldState.NONE;
    }
}
