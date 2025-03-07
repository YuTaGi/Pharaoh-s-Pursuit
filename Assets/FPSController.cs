using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public static FPSController instance; // ��������� singleton
    public Camera playerCamera;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float gravity = -9.81f;

    public Transform groundCheck;  // �ش��Ǩ�ͺ���
    public LayerMask groundMask;   // ��˹�����Ǩ�Ѻ੾�о��

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ��Ǩ�ͺ��ҵԴ������������� Raycast
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // �Ѻ��ҡ����ع���ͧ
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);
        playerCamera.transform.Rotate(Vector3.left * mouseY);

        // �Ѻ��ҡ������͹���
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // ���ⴴ
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);

        // �ç�����ǧ
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void Awake()
    {
        instance = this; 
    }
}
