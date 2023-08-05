using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchVCam : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    private CinemachineVirtualCamera _virtualCamera;
    private InputAction _aimAction;
    [SerializeField] private int _priorityBoostAmount = 10;
    [SerializeField] private Canvas _thirdPersonCanvas;
    [SerializeField] private Canvas _aimCanvas;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _aimAction = _playerInput.actions["Aim"];
    }

    private void OnEnable()
    {
        _aimAction.performed += _ => StartAim();
        _aimAction.canceled += _ => CancelAim();
    }

    private void OnDisable()
    {
        _aimAction.performed -= _ => StartAim();
        _aimAction.canceled -= _ => CancelAim();
    }

    private void StartAim()
    {
        _virtualCamera.Priority += _priorityBoostAmount;
        _aimCanvas.enabled = true;
        _thirdPersonCanvas.enabled = false;
    }

    private void CancelAim()
    {
        _virtualCamera.Priority -= _priorityBoostAmount;
        _aimCanvas.enabled = false;
        _thirdPersonCanvas.enabled = true;
    }
}
