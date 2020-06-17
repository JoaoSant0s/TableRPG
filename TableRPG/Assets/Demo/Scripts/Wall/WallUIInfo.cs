using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WallUIInfo : MonoBehaviour
{
    public delegate void OnRemoveWall(Wall wall);
    public static OnRemoveWall RemoveWall;

    [SerializeField]
    private WallUIInfoDrag dragElement;

    [SerializeField]
    private WallUIInfoButtons buttonsUI;

    private Wall linkedWall;

    private Vector3 offset;

    private void Awake()
    {
        this.dragElement.PrepareToDragElement += SaveDragOffset;
        this.dragElement.DragElement += ChangeInfoPosition;
    }

    private void OnDestroy()
    {
        this.dragElement.PrepareToDragElement -= SaveDragOffset;
        this.dragElement.DragElement -= ChangeInfoPosition;
    }

    private void UpdateButtonsVisuals()
    {
        this.buttonsUI.EnableBlockLightButton(this.linkedWall.EnableShadowCaster2D);
        this.buttonsUI.EnableColliderButton(this.linkedWall.EnableBoxCollider2D);
        this.buttonsUI.SetWallColorButton(this.linkedWall.WallColor);
    }

    private void SaveDragOffset(Vector3 position)
    {
        this.offset = position - transform.position;
    }

    private void ChangeInfoPosition(Vector3 position)
    {
        transform.position = position - this.offset;
    }

    public void ExtractWallInfo(Wall wall)
    {
        this.linkedWall = wall;
        UpdateButtonsVisuals();
    }

    #region UI

    public void OnClosePanel()
    {
        if (RemoveWall != null)
        {
            RemoveWall(this.linkedWall);
        }
    }

    public void OnBlockLight()
    {
        if (this.linkedWall == null) return;

        this.linkedWall.EnableShadowCaster2D = !this.linkedWall.EnableShadowCaster2D;
        UpdateButtonsVisuals();
    }

    public void OnEnableCollider()
    {
        if (this.linkedWall == null) return;

        this.linkedWall.EnableBoxCollider2D = !this.linkedWall.EnableBoxCollider2D;
        UpdateButtonsVisuals();
    }

    public void OnWallVisible()
    {

    }

    public void OnColor()
    {
        if (this.linkedWall == null) return;
    }

    #endregion UI
}

[System.Serializable]
public class WallUIInfoButtons
{
    public Color disabledColor;

    public Button blockLightButton;
    public Button enableColliderButton;
    public Button enableButton;
    public Image colorImage;

    public void EnableBlockLightButton(bool value)
    {        
        this.blockLightButton.image.color = value ? Color.white : this.disabledColor;
    }

    public void EnableColliderButton(bool value)
    {
        this.enableColliderButton.image.color = !value ? Color.white : this.disabledColor;
    }

    public void SetWallColorButton(Color color)
    {
        this.colorImage.color = color;
    }
}