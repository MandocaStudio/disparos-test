using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
{

    public CinemachineCamera playerCam;

    public PlayerControls controls;
    public Rigidbody rbPlayer;

    public Animator playerAnimator;

    public bool aiming;


    [Range(0, 1)]
    public float precision;

    public bool playerMoving;

}
