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

    public float sensitivity;

    private float pitch = 0f; // ángulo vertical acumulado
    private float yaw = 0f;   // ángulo horizontal acumulado (opcional)

    public GameObject cameraTarget;
    public Rigidbody rbPlayer;

    public bool canMove;

    public bool isGrounded;

    public float changeSideDuration;

    public float currentSide;

    public Animator animator;

    [Header("Rotación del personaje")]
    public float rotationSmoothness = 0.15f;
    public float angleThreshold = 5f; // umbral angular en grados

    [SerializeField] private Transform playerBody; // referencia al jugador
    [SerializeField] private Vector3 cameraOffset;
    private void Awake()
    {

        playerBody = transform;

        cameraOffset = new Vector3(0, 1.5f, 0);
        playerManagerScript = GetComponent<playerManager>();
        rbPlayer = GetComponent<Rigidbody>();

        // Instancia del Input System generado
        controls = new PlayerControls();

        currentSide = playerCam.GetComponent<CinemachineThirdPersonFollow>().CameraSide;


        // Suscripción a eventos (solo una vez en Awake)
        controls.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.MoveCam.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.MoveCam.canceled += ctx => lookInput = Vector2.zero;

        controls.Player.cameraChange.performed += cameraLocationCHange;
        //playerManagerScript.controls.Player.Jump.performed += ctx => jumpPressed = true;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }


    // altura típica a la cabeza

    private void LateUpdate()
    {
        float mouseX = lookInput.x * sensitivity;
        float mouseY = lookInput.y * sensitivity;

        // Acumular rotaciones
        yaw += mouseX;
        pitch -= mouseY;

        // Limitar pitch
        pitch = Mathf.Clamp(pitch, -45f, 45f);

        // Actualizar la posición del cameraTarget para que siga al jugador
        cameraTarget.transform.position = playerBody.position + cameraOffset;

        // Aplicar rotación del input SOLO al cameraTarget
        cameraTarget.transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
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
        StopAllCoroutines(); // detener fade anterior
        StartCoroutine(FadeCameraSide(targetSide));
    }

    [SerializeField] private float rotationSpeedMoving = 5f;   // más lento, mientras camina
    [SerializeField] private float rotationSpeedIdle = 12f;    // más rápido, al soltar y volver a mover

    private void FixedUpdate()
    {
        // --- Movimiento relativo a la cámara ---
        Vector3 forward = cameraTarget.transform.forward;
        Vector3 right = cameraTarget.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = forward * moveInput.y + right * moveInput.x;
        Vector3 velocity = desiredMoveDirection.normalized * speed;
        velocity.y = rbPlayer.linearVelocity.y;
        rbPlayer.linearVelocity = velocity;

        // --- Rotación del jugador sincronizada con cámara ---
        if (moveInput.magnitude > 0.1f) // SOLO si se mueve
        {
            float cameraYaw = cameraTarget.transform.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0, cameraYaw, 0);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeedMoving * Time.deltaTime
            );
        }

        // Si está quieto, no forzamos la rotación del personaje

        // --- Animator ---
        animator.SetFloat("moveX", moveInput.x);
        animator.SetFloat("moveY", moveInput.y);
        animator.SetFloat("speed", moveInput.magnitude);

        if (moveInput.magnitude > 0.1f)
        {
            animator.SetFloat("lastMoveX", moveInput.x);
            animator.SetFloat("lastMoveY", moveInput.y);
        }
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

        // asegurar que llega exactamente al objetivo
        thirdPersonFollow.CameraSide = target;
        currentSide = target;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts.Length > 0)
        {
            // Verifica si el contacto es desde abajo
            if (collision.contacts[0].normal.y > 0.5f)
            {
                isGrounded = true;
            }
        }
    }


}
