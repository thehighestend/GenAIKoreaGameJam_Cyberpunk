using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float runSpeed = 1f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private bool groundedPlayer;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;

    // Start is called before the first frame update
    void Start()
    {
        if(controller == null)
            controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;

        // 이동
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 frontDirection = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized;
        Vector3 rightDirection = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z).normalized;
        Vector3 moveDirection = frontDirection * moveInput.y + rightDirection * moveInput.x;
        transform.forward = frontDirection;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        runSpeed = (Input.GetKey(KeyCode.LeftShift)) ? 2f : 1f;
        controller.Move(moveDirection * Time.deltaTime * playerSpeed * runSpeed);



        // 점프
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
