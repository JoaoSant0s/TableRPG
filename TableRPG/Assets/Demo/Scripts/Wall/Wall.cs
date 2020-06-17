using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Wall : MonoBehaviour
{
    public delegate void OnClickToShowInfo(Wall wall, Vector2 position);
    public static OnClickToShowInfo ClickToShowInfo;

    [Header("Components")]

    [SerializeField]
    private ShadowCaster2D shadowCaster2D;

    [SerializeField]
    private BoxCollider2D boxCollider;

    [Header("Wall child elements")]

    [SerializeField]
    private Transform wallTransform;

    [SerializeField]
    private SpriteRenderer wallSpriteRender;

    [Header("Clickable elements")]

    [SerializeField]
    private WallInfo wallInfo;

    [SerializeField]
    private WallDragger dragElement;

    [SerializeField]
    private WallRotateScale rotateScaleElement;

    private Vector3 wallOffset;

    private bool editMode;

    public Vector3 Position
    {
        get { return transform.position; }
    }

    public Quaternion Quaternion
    {
        get { return transform.localRotation; }
    }

    public float GetScale
    {
        get { return this.wallTransform.localScale.x; }
    }

    public bool EnableShadowCaster2D
    {
        get { return this.shadowCaster2D.castsShadows; }
        set { this.shadowCaster2D.castsShadows = value; }
    }

    public bool EnableBoxCollider2D
    {
        get { return this.boxCollider.isTrigger; }
        set { this.boxCollider.isTrigger = value; }
    }

    public Color WallColor
    {
        get { return this.wallSpriteRender.color; }
        set { this.wallSpriteRender.color = value; }
    }

    private void Awake()
    {
        RegisterEvent();

        EnableInteractions(false);
    }

    private void OnDestroy()
    {
        UnRegisterEvent();
    }

    private void RegisterEvent()
    {
        WallBuilder.WallInteractions += EnableInteractions;

        this.dragElement.StartChangeWallPosition += SaveWallOffset;
        this.dragElement.ChangeWallPosition += ChangeWallPosition;
        this.wallInfo.ClickWallRight += TryShowWallInfo;

        this.rotateScaleElement.StartChangeWallPosition += SaveWallOffset;
        this.rotateScaleElement.ChangeWallPosition += ChangeWallRotationAndScale;
    }

    private void UnRegisterEvent()
    {
        this.dragElement.StartChangeWallPosition -= SaveWallOffset;
        this.dragElement.ChangeWallPosition -= ChangeWallPosition;
        this.wallInfo.ClickWallRight -= TryShowWallInfo;

        this.rotateScaleElement.StartChangeWallPosition -= SaveWallOffset;
        this.rotateScaleElement.ChangeWallPosition -= ChangeWallRotationAndScale;

        WallBuilder.WallInteractions -= EnableInteractions;
    }

    private void EnableInteractions(bool enable)
    {
        this.editMode = enable;
        this.dragElement.gameObject.SetActive(enable);
        this.rotateScaleElement.gameObject.SetActive(enable);
        this.rotateScaleElement.UpdateElements();
    }

    public void Rotate(Vector3 direction)
    {
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Rotate(Quaternion quaternion)
    {
        transform.localRotation = quaternion;
    }

    public void Scale(float scale)
    {
        ScaleWall(scale);

        UpdateDragElementPosition(scale);
        UpdateRotateScaleElementPosition(scale);
    }

    private void ScaleWall(float scale)
    {
        var localScale = this.wallTransform.localScale;
        localScale.x = scale;
        this.wallTransform.localScale = localScale;
    }

    private void TryShowWallInfo(Vector2 position)
    {
        if (!this.editMode) return;

        if (ClickToShowInfo != null)
        {
            ClickToShowInfo(this, position);
        }
    }

    private void SaveWallOffset(Vector3 position)
    {
        this.wallOffset = position - transform.position;
    }

    private void ChangeWallPosition(Vector3 position)
    {
        transform.position = position - this.wallOffset;
    }

    private void ChangeWallRotationAndScale(Vector3 position)
    {
        var direction = this.wallOffset + (position - this.wallOffset) - Position;

        Rotate(direction);
        Scale(direction.magnitude);
    }

    private void UpdateDragElementPosition(float scale)
    {
        this.dragElement.UpdateLocalPosition(scale);
    }

    private void UpdateRotateScaleElementPosition(float scale)
    {
        this.rotateScaleElement.UpdateLocalPosition(scale);
    }
}
