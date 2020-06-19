using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class WorldUIController : MonoBehaviour
{
    [Header("Walls")]
    [SerializeField]
    private Color wallColor;

    [Header("None")]
    [SerializeField]
    private Color noneColor;

    [Header("Images references")]

    [SerializeField]
    private Image[] imagesToCustomize;

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
            ChangeImagesColors(this.wallColor);
        }
        else
        {
            ChangeImagesColors(this.noneColor);
        }        
    }    

    private void ChangeImagesColors(Color color)
    {
        for (int i = 0; i < this.imagesToCustomize.Length; i++)
        {
            this.imagesToCustomize[i].color = color;
        }
    }
}
