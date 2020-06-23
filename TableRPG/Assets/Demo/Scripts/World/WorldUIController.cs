using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TableRPG;

[ExecuteAlways]
public class WorldUIController : MonoBehaviour
{
    [Header("World State Color")]
    [SerializeField]
    private WorldStateToColorDictionary worldStateColor;

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
        Color color = worldStateColor.GetColorByWorldState(state);

        ChangeImagesColors(color);
    }

    private void ChangeImagesColors(Color color)
    {
        for (int i = 0; i < this.imagesToCustomize.Length; i++)
        {
            this.imagesToCustomize[i].color = color;
        }

        UpdateDynamicVisuals(color);
    }

    private void UpdateDynamicVisuals(Color color)
    {
        SubActionController subAction = FindObjectOfType<SubActionController>();

        if (subAction != null)
        {
            subAction.gameObject.GetComponent<Image>().color = color;
        }
    }
}
