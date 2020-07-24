using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTargetZoom : MonoBehaviour
{
    [Header("Zoom control")]

    [SerializeField]
    private float minOrthographicSize = 2f;
    [SerializeField]
    private float maxOrthographicSize = 10f;
    [SerializeField]
    private float divisionFactor = 175f;
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
    private InputCamera controls;
    private void Awake()
    {
        this.controls = new InputCamera();
        this.controls.Camera.Zoom.performed += context => Zoom(context.ReadValue<Vector2>());
    }

    private void OnDestroy()
    {
        this.controls.Camera.Zoom.performed -= context => Zoom(context.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        this.controls.Enable();
    }

    private void OnDisable()
    {
        this.controls.Disable();
    }

    public void Zoom(Vector2 scroll)
    {
        if (UtilWrapper.IsPointOverScrollUIObject()) return;

        var size = this.virtualCamera.m_Lens.OrthographicSize + (-scroll.y / this.divisionFactor);

        var sizeCorrection = Mathf.Clamp(size, this.minOrthographicSize, this.maxOrthographicSize);

        this.virtualCamera.m_Lens.OrthographicSize = sizeCorrection;
    }
}
