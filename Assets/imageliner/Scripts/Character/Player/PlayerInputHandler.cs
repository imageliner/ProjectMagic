using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Player_InputActions controls;
    [SerializeField] private PlayerCharacter playerCharacter;
    public Vector2 lookAxisValue;
    public Vector2 movementAxisValue;

    public List<Interactable> interactableList = new List<Interactable>();

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
        controls.Player.Attack.performed += OnAttackPerformed;

        controls.Player.OpenMenu.performed += OnMenuPerformed;

        controls.Player.Ability0.performed += OnAbility0Performed;
        controls.Player.Ability1.performed += OnAbility1Performed;
        controls.Player.Ability2.performed += OnAbility2Performed;

        controls.Player.Item1.performed += OnItem1Performed;
        controls.Player.Item2.performed += OnItem2Performed;

        controls.Player.Interact.performed += OnInteractPerformed;

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
        if (!GameManager.singleton.inMenu)
            playerCharacter.Attack();
    }

    private void OnAbility0Performed(InputAction.CallbackContext context)
    {
        playerCharacter.AbilityInput(0);
    }

    private void OnAbility1Performed(InputAction.CallbackContext context)
    {
        playerCharacter.AbilityInput(1);
    }

    private void OnAbility2Performed(InputAction.CallbackContext context)
    {
        playerCharacter.AbilityInput(2);

    }

    private void OnItem1Performed(InputAction.CallbackContext context)
    {
        FindAnyObjectByType<Inventory>().GetFirstHealthPotion();
    }

    private void OnItem2Performed(InputAction.CallbackContext context)
    {
        FindAnyObjectByType<Inventory>().GetFirstManaPotion();
    }

    private void OnMenuPerformed(InputAction.CallbackContext context)
    {
        GameManager.singleton.toggleMenu?.Invoke();   
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (interactableList.Count == 0)
            return;

        interactableList[interactableList.Count - 1].OnInteract();
    }

    private void Update()
    {
        playerCharacter._mouseTracker.UpdateMousePosition(lookAxisValue);
        playerCharacter.Movement(movementAxisValue);
    }
}
