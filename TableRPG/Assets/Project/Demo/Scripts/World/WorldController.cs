using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorldState
{
    NONE,
    WALL,
    TEST_1,
    TEST_2
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

    private WorldState State
    {
        get { return this.state; }
        set
        {
            this.state = value;
            if (ChangeWorldState != null) ChangeWorldState(this.state);
        }
    }    

    private void Start()
    {
        this.State = WorldState.NONE;
        if (ChangeWorldState != null)
        {
            ChangeWorldState(this.State);
        }
    }

    public void UpdateToState(WorldState state)
    {
        this.State = state;
    }

}
