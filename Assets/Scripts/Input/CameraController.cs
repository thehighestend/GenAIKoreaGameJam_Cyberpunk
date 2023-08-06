using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float speedYaw = 2f;
    [SerializeField] float speedPitch = 2f;
    [SerializeField] Transform characterTransform;

    private float yaw = 0f, pitch = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        yaw += speedYaw * Input.GetAxis("Mouse X");
        pitch -= speedPitch * Input.GetAxis("Mouse Y");

        //the rotation range
        /*yaw = Mathf.Clamp(yaw, -90f, 90f);*/
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        var newAngle = new Vector3(pitch, yaw, 0.0f);
        transform.eulerAngles = newAngle;
        var cameraFront = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        /*characterTransform.forward = cameraFront;*/
    }
}
