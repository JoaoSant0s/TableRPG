using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

# if UNITY_EDITOR
using UnityEditorInternal;
# endif

[ExecuteAlways]
[RequireComponent(typeof(Light2D))]
public class Light2DAdaptative : MonoBehaviour
{
    [Header("Custom Attributes")]
    [SerializeField]
    [Min(3)]
    private int sides = 4;

    [SerializeField]
    [Min(0.01f)]
    private float radius = 1f;

    [SerializeField]
    [Range(0, 359)]
    private int angle = 45;

    [SerializeField]
    private LayerMask layersFilter;

    [Header("Base Attributes")]

    [SerializeField]
    [Min(0)]
    private int lightOrder = 0;

    [SerializeField]
    [ColorUsage(false)]
    private Color color = Color.white;

    [SerializeField]
    [Range(0, 1f)]
    private float shadowIntensity = 0f;

    private Light2D currentLight;

    public Light2D Light
    {
        get
        {
            if (this.currentLight == null)
            {
                this.currentLight = GetComponent<Light2D>();
                this.currentLight.lightType = Light2D.LightType.Freeform;
            }
            return this.currentLight;
        }
    }

    private void OnValidate()
    {
        ChangeProperties();
        TryResizeLight();
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        ComponentUtility.MoveComponentUp(this);
#endif
        TryResizeLight();
    }

    private void Update()
    {
        TryResizeLight();
    }

    private void ChangeProperties()
    {
        Light.lightOrder = this.lightOrder;
        Light.color = this.color;
        Light.shadowIntensity = this.shadowIntensity;
    }

    private void TryResizeLight()
    {
        var lightNaturalPoints = this.Light.shapePath.Length;

        if (this.sides != lightNaturalPoints)
        {
            this.sides = lightNaturalPoints;
            Debugs.Log("Create or reduce more points in Freeform light 2d component to see the correct draw form");
        }

        ResizeLight();        
    }

    private void ResizeLight()
    {
        var localRadius = this.radius;

        var sides = this.sides;
        var baseAngle = Mathf.PI * 2 / sides;

        var offsetAngle = Mathf.Deg2Rad * this.angle;

        for (int i = 0; i < sides; i++)
        {
            var localAngle = baseAngle * i;
            var position = new Vector3(Mathf.Cos(localAngle + offsetAngle), Mathf.Sin(localAngle + offsetAngle), 0f) * localRadius;

            RaycastHit2D value = Physics2D.Raycast(transform.position, position, localRadius, this.layersFilter);

            var relativePosition = transform.localPosition;
            if (value.collider != null)
            {
                Vector3 transportPoint = transform.InverseTransformPoint(value.point);
                relativePosition += transportPoint;
            }
            else
            {
                relativePosition += position;
            }

            this.Light.shapePath[i] = relativePosition;
        }

    }

}
