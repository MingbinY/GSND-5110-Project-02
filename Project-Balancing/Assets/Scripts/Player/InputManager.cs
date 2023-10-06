using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFootActions;

    private PlayerLocomotion locomotion;
    private PlayerLook look;
    private PlayerWeaponSlot weaponSlot;

    private void OnEnable()
    {
        onFootActions.Enable();
    }

    private void OnDisable()
    {
        onFootActions.Disable();
    }

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFootActions = playerInput.OnFoot;

        locomotion = GetComponent<PlayerLocomotion>();
        look = GetComponent<PlayerLook>();
        weaponSlot = GetComponent<PlayerWeaponSlot>();

        onFootActions.Jump.performed += i => locomotion.HandleJump();
    }

    private void FixedUpdate()
    {
        if (onFootActions.Fire.IsPressed())
        {
            weaponSlot.FireWeapon();
        }
        locomotion.HandleMove(onFootActions.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.HandleLook(onFootActions.Look.ReadValue<Vector2>());
    }

}
