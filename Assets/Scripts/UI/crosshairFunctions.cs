using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;


public class crosshairFunctions : MonoBehaviour
{
    public CinemachineCamera playerCam;

    [Header("para rifle")]
    [SerializeField] private RectTransform topRifle;
    [SerializeField] private RectTransform bottomRifle;
    [SerializeField] private RectTransform leftRifle;
    [SerializeField] private RectTransform rightRifle;

    [SerializeField] private Image centerRifle;
    [SerializeField] private CanvasGroup crossHairRifleCG;



    [Header("Ajustes rifle")]
    [SerializeField] private float baseSize = 20f; // distancia mínima
    [SerializeField] private float maxExpand = 60f; // expansión máxima
    [SerializeField] private float expandSpeed = 5f; // velocidad de cambio

    public playerManager playerManagerScript;
    public PlayerControls controls;

    private void Awake()
    {
        crossHairRifleCG = GetComponent<CanvasGroup>();
        crossHairRifleCG.alpha = 0;

        playerCam = playerManagerScript.playerCam;

        controls = new PlayerControls();

        controls.Player.aimButton.performed += ctx => startAiming();
        controls.Player.aimButton.canceled += ctx => stopAiming();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        //funcionalidad solo para el rifle (las miras seran diferentes dependiendo el arma)



    }

    private void startAiming()
    {
        CinemachineThirdPersonFollow thirdPersonFollow = playerCam.GetComponent<CinemachineThirdPersonFollow>();
        thirdPersonFollow.CameraDistance = 0.9f;
        crossHairRifleCG.alpha = 1;
        playerManagerScript.playerAnimator.SetBool("aiming", true);
        playerManagerScript.playerAnimator.SetTrigger("toAim");



    }

    private void stopAiming()
    {
        CinemachineThirdPersonFollow thirdPersonFollow = playerCam.GetComponent<CinemachineThirdPersonFollow>();

        crossHairRifleCG.alpha = 0;
        playerManagerScript.playerAnimator.SetBool("aiming", false);

        thirdPersonFollow.CameraDistance = 1.5f;


    }
}
