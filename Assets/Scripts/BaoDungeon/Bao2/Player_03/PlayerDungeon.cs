using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDungeon : MonoBehaviour
{



    [SerializeField]
    private InputActionReference movement, attack, pointerPosition;

    private Vector2 pointerInput, movementInput;

    private WeaponParent weaponParent;

    private void OnEnable()
    {
        attack.action.performed += PerformAttack;
    }

    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
    }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        weaponParent.Attack();
    }

    private void Awake()
    {

        weaponParent = GetComponentInChildren<WeaponParent>();

    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        // agentAnimations.RotateToPointer(lookDirection);

    }

    private void Update()
    {
        pointerInput = GetPointerInput();
        weaponParent.PointerPosition = pointerInput;
        movementInput = movement.action.ReadValue<Vector2>().normalized;


        AnimateCharacter();
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

}