using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;


public class crosshairFunctions : MonoBehaviour
{
    public CinemachineCamera playerCam;

    [SerializeField] private CrosshairBase activeCrosshair;

    public playerManager playerManagerScript;
    public PlayerControls controls;

    [SerializeField] private float decaySpeed = 2f; // velocidad para volver a 0
    public float defaultCameraDistance = 1.5f;

    [Header("variables del arma")]
    private GameObject currentCrosshairObj;

    private float weaponCameraDistance;


    private void Awake()
    {
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


    public void EquipWeapon(WeaponInstance weapon)
    {
        // Destruir mira anterior
        if (currentCrosshairObj != null)
            Destroy(currentCrosshairObj);

        // Instanciar la nueva
        currentCrosshairObj = Instantiate(weapon.weaponData.crosshairPrefab, transform);
        activeCrosshair = currentCrosshairObj.GetComponent<CrosshairBase>();

        // Ajustar zoom único de esa arma
        // cuando las mejoras del arma influyan en el zoom, regresaremos por aqui.
        weaponCameraDistance = activeCrosshair.ZoomCameraDistance;
    }


    private void startAiming()
    {
        CinemachineThirdPersonFollow thirdPersonFollow = playerCam.GetComponent<CinemachineThirdPersonFollow>();

        activeCrosshair.gameObject.SetActive(true);

        playerManagerScript.aiming = true;

        // Ajustar zoom único de esa arma
        thirdPersonFollow.CameraDistance = weaponCameraDistance;

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

    }


}
