using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float velocity = 10f;
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Move(horizontal, vertical);
    }

    private void Move(float horizontal, float vertical)
    {
        if (horizontal == 0 && vertical == 0) return;

        Vector3 position = new Vector3(horizontal, vertical, 0f);

        position *= (Time.deltaTime * velocity);
        position.z = transform.position.z;

        transform.position += position;
    }


}
