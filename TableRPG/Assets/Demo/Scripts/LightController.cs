using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [SerializeField]
    private Light2D globalLight;

    [SerializeField]
    private LayerMask layer;

    private List<Light2D> lights;

    private PlayerController playerRef;
    private void Awake()
    {
        PlayerController.PlayerStart += SavePlayerReference;
        this.lights = new List<Light2D>(FindObjectsOfType<Light2D>());
        this.lights.Remove(this.globalLight);
    }

    private void OnDestroy()
    {
        PlayerController.PlayerStart -= SavePlayerReference;
    }

    private void OnDrawGizmos()
    {
        if (this.playerRef == null) return;

        foreach (var item in this.lights)
        {
            if (playerRef.Light == item) continue;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.playerRef.transform.position, item.transform.position);
            Gizmos.color = Color.white;
        }
    }

    private void LateUpdate()
    {
        if (this.playerRef == null) return;        
        DisableRayCastedLights();
    }

    private void SavePlayerReference(PlayerController player)
    {
        this.playerRef = player;
        DisableRayCastedLights();
    }

    private void DisableRayCastedLights()
    {
        foreach (var item in this.lights)
        {
            if (playerRef.Light == item) continue;                        

            var direction = (item.transform.position - this.playerRef.transform.position);

            RaycastHit2D value = Physics2D.Raycast(this.playerRef.transform.position, direction, direction.magnitude, this.layer);

            var active = false;

            if (value.collider == null)
            {
                active = true;
            }

            item.gameObject.SetActive(active);

        }
    }
}
