using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerF : MonoBehaviour
{
    
  

    [SerializeField]
    private InputActionReference movement, attack, pointerPosition;

    private Vector2 pointerInput, movementInput;

   private WeaponParentF weaponParentF;

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
       weaponParentF.Attack();
    }

    private void Awake()
    {
       
       weaponParentF = GetComponentInChildren<WeaponParentF>();
      
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
       // agentAnimations.RotateToPointer(lookDirection);
    
    }

    private void Update()
    {
        pointerInput = GetPointerInput();
        weaponParentF.PointerPosition = pointerInput;
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