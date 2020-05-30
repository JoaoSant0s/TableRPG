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
    private Light2D lightReference;

    private bool moving;

    private void Start()
    {
        if (PlayerStart != null) PlayerStart(this);
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        this.moving = Move(horizontal, vertical);
    }

    public bool Moving
    {
        get { return this.moving; }
    }

    public Light2D LightReference
    {
        get { return this.lightReference; }
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
