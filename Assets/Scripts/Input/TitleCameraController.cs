using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        rotationSpeed = (Input.GetKey(KeyCode.LeftShift))?2f:1f;

        transform.Rotate(0f, Input.GetAxis("Horizontal") * 0.1f * rotationSpeed, 0f);
    }
}
