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

    [Range(0, 100)]
    public int precision;
}
