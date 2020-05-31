using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public delegate void OnPlayerStart(PlayerController player);
    public static OnPlayerStart PlayerStart;

    [SerializeField]
    private float velocity = 10f;

    [SerializeField]
    private Light2DAdaptative currentLight;    

    private void Start()
    {
        if (PlayerStart != null) PlayerStart(this);
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Move(horizontal, vertical);
    }

    public Light2D Light
    {
        get { return this.currentLight.Light; }
    }

    private bool Move(float horizontal, float vertical)
    {
        if (horizontal == 0 && vertical == 0) return false;

        Vector3 position = new Vector3(horizontal, vertical, 0f);

        position *= (Time.deltaTime * velocity);
        position.z = transform.position.z;

        transform.position += position;
        return true;
    }
}
