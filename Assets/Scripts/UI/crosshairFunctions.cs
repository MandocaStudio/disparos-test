using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;


public class crosshairFunctions : MonoBehaviour
{
    public CinemachineCamera playerCam;

    [SerializeField] private crosshairRifle RifleCrosshair;

    // [SerializeField] private crosshairPistol PistolCrosshair;

    // [SerializeField] private crosshairShotgun ShotgunCrosshair;

    [SerializeField] private CrosshairBase activeCrosshair;

    public playerManager playerManagerScript;
    public PlayerControls controls;

    [SerializeField] private float decaySpeed = 2f; // velocidad para volver a 0


    public float defaultCameraDistance = 1.5f;
    private void Awake()
    {

        RifleCrosshair.gameObject.SetActive(false);
        // crosshairPistol.gameObject.SetActive(false);
        // crosshairShotgun.gameObject.SetActive(false);


        activeCrosshair = RifleCrosshair;

        playerCam = playerManagerScript.playerCam;

        controls = new PlayerControls();

        controls.Player.aimButton.performed += ctx => startAiming();
        controls.Player.aimButton.canceled += ctx => stopAiming();
    }

    private void Update()
    {
        float targetPrecision = 0f;

        if (playerManagerScript.playerMoving) targetPrecision += 0.3f;
        //if (player.isShooting) targetPrecision += 0.2f;

        targetPrecision = Mathf.Clamp01(targetPrecision);

        playerManagerScript.precision = Mathf.MoveTowards(playerManagerScript.precision, targetPrecision, Time.deltaTime * decaySpeed);

        if (activeCrosshair != null)
        {
            activeCrosshair.SetPrecision(playerManagerScript.precision);
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }



    private void startAiming()
    {
        CinemachineThirdPersonFollow thirdPersonFollow = playerCam.GetComponent<CinemachineThirdPersonFollow>();

        activeCrosshair.gameObject.SetActive(true);

        if (activeCrosshair is crosshairRifle crosshairRifle)
        {
            thirdPersonFollow.CameraDistance = crosshairRifle.zoomCameraDistance;
        }

        playerManagerScript.aiming = true;

        playerManagerScript.playerAnimator.SetBool("aiming", true);
        playerManagerScript.playerAnimator.SetTrigger("toAim");



    }

    private void stopAiming()
    {
        CinemachineThirdPersonFollow thirdPersonFollow = playerCam.GetComponent<CinemachineThirdPersonFollow>();

        activeCrosshair.gameObject.SetActive(false);

        playerManagerScript.aiming = false;


        playerManagerScript.playerAnimator.SetBool("aiming", false);

        thirdPersonFollow.CameraDistance = defaultCameraDistance;



        ;


    }

    private void switchCrosshair(CrosshairBase newCrossHair)
    {
        activeCrosshair.gameObject.SetActive(false);

        activeCrosshair = newCrossHair;
    }
}
