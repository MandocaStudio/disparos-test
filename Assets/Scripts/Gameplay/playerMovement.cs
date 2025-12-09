using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;
using System.Collections;

public class playerMovement : MonoBehaviour
{
    public playerManager playerManagerScript;
    public PlayerControls controls;

    public CinemachineCamera playerCam;

    public float speed;

    private Vector2 moveInput;
    private Vector2 lookInput;

    [Header("Sensibilidad")]
    public float mouseSensitivity = 0.8f;
    public float gamepadSensitivity = 2f;

    private float pitch = 0f;
    private float yaw = 0f;

    public GameObject cameraTarget;
    public Rigidbody rbPlayer;

    public bool canMove = true;
    public bool isGrounded;

    public float changeSideDuration;
    public float currentSide;

    public Animator animator;

    public float rotationSpeedMoving = 5f;
    public float rotationSpeedIdle = 12f;

    private bool wasIdle = true;

    [Header("Límites de cámara")]
    public float pitchMin = -30f;
    public float pitchMax = 70f;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerManagerScript = GetComponent<playerManager>();
        rbPlayer = GetComponent<Rigidbody>();

        controls = new PlayerControls();

        currentSide = playerCam.GetComponent<CinemachineThirdPersonFollow>().CameraSide;

        controls.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.MoveCam.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.MoveCam.canceled += ctx => lookInput = Vector2.zero;

        controls.Player.cameraChange.performed += cameraLocationCHange;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    // 🔵 === ROTACIÓN DE LA CÁMARA === (SIEMPRE EN LATEUPDATE)
    private void LateUpdate()
    {
        float sens = (controls.Player.MoveCam.activeControl?.device is Gamepad)
            ? gamepadSensitivity
            : mouseSensitivity;

        yaw += lookInput.x * sens;
        pitch -= lookInput.y * sens;

        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        // SOLO cameraTarget rota (Cinemachine sigue este target)
        cameraTarget.transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    // 🔴 === MOVIMIENTO DEL JUGADOR === (SIEMPRE EN FIXEDUPDATE)
    private void FixedUpdate()
    {
        if (!canMove)
            return;

        Vector3 forward = cameraTarget.transform.forward;
        Vector3 right = cameraTarget.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 dir = forward * moveInput.y + right * moveInput.x;

        Vector3 velocity = dir * speed;
        velocity.y = rbPlayer.linearVelocity.y;

        rbPlayer.linearVelocity = velocity;

        // 🟢 Rotar el jugador hacia la dirección de la cámara SOLO cuando se mueve
        if (dir.magnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.Euler(0f, cameraTarget.transform.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                rotationSpeedMoving * Time.fixedDeltaTime
            );
        }

        // Actualizar animaciones
        if (animator)
        {
            animator.SetFloat("speed", dir.magnitude);
            animator.SetFloat("moveX", moveInput.x);
            animator.SetFloat("moveY", moveInput.y);
        }
    }

    private void cameraLocationCHange(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        float targetSide = currentSide;

        if (value == 1)
        {
            targetSide = 1;
        }
        else if (value == -1)
        {
            targetSide = 0;
        }

        StopAllCoroutines();
        StartCoroutine(FadeCameraSide(targetSide));
    }

    private IEnumerator FadeCameraSide(float target)
    {
        CinemachineThirdPersonFollow thirdPersonFollow = playerCam.GetComponent<CinemachineThirdPersonFollow>();
        float start = thirdPersonFollow.CameraSide;
        float elapsed = 0f;

        while (elapsed < changeSideDuration)
        {
            elapsed += Time.deltaTime;
            currentSide = Mathf.Lerp(start, target, elapsed / changeSideDuration);
            thirdPersonFollow.CameraSide = currentSide;
            yield return null;
        }

        thirdPersonFollow.CameraSide = target;
        currentSide = target;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts.Length > 0)
        {
            if (collision.contacts[0].normal.y > 0.5f)
            {
                isGrounded = true;
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.contacts.Length > 0)
        {
            if (collision.contacts[0].normal.y > 0.5f)
            {
                isGrounded = true;
            }
        }
    }
}
