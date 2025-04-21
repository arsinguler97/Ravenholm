using UnityEngine;

public class Controller : MonoBehaviour
{
    private CharacterController _controller;
    private Vector3 _velocity;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float mouseSensitivity = 100f;

    private float _xRotation;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        _xRotation = 0f;
        cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad < 0.1f) return;

        bool isGrounded = _controller.isGrounded;
        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        _controller.Move(move * (speed * Time.deltaTime));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}