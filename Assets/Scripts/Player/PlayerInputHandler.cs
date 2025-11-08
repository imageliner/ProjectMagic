using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Player_InputActions controls;
    [SerializeField] private PlayerCharacter playerCharacter;
    public Vector2 lookAxisValue;
    public Vector2 movementAxisValue;

    private void Awake()
    {
        controls = new Player_InputActions();
        playerCharacter = GetComponent<PlayerCharacter>();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Look.performed += OnLookPerformed;
        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;
        controls.Player.Dash.performed += OnDashPerformed;
        controls.Player.Attack.performed += OnAttackPerformed;
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        lookAxisValue = context.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        movementAxisValue = context.ReadValue<Vector2>();
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        movementAxisValue = context.ReadValue<Vector2>();
    }
    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        //playerController.Dash();
    }
    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        playerCharacter.Attack();
    }

    private void Update()
    {
        playerCharacter._mouseTracker.UpdateMousePosition(lookAxisValue);
        playerCharacter.Movement(movementAxisValue);
    }
}
