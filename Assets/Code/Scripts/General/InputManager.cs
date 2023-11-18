using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class InputManager : ScriptableObject
{
    public event UnityAction jumpEvent;
    public event UnityAction attackReleaseEvent;
    public event UnityAction attackChargeEvent;
    public event UnityAction<Vector2> moveEvent;
    public event UnityAction<float> turnEvent;
    public event UnityAction<float> lookUpDownEvent;
    public event UnityAction<float> changeWeaponEvent;

    public void OnAttackRelease(InputAction.CallbackContext _context)
    {
        if(attackReleaseEvent != null 
            && _context.phase == InputActionPhase.Performed)
        {
            attackReleaseEvent.Invoke();
        }
    }

    public void OnAttackCharge(InputAction.CallbackContext _context)
    {
        if (attackChargeEvent != null
            && _context.phase == InputActionPhase.Performed)
        {
            attackChargeEvent.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        if(moveEvent != null)
        {
            moveEvent.Invoke(_context.ReadValue<Vector2>());
        }
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        if (jumpEvent != null
            && _context.phase == InputActionPhase.Performed)
        {
            jumpEvent.Invoke();
        }
    }

    public void OnTurn(InputAction.CallbackContext _context)
    {
        if (turnEvent != null)
        {
            turnEvent.Invoke(_context.ReadValue<float>());
        }
    }

    public void OnLookUpDown(InputAction.CallbackContext _context)
    {
        if (lookUpDownEvent != null)
        {
            lookUpDownEvent.Invoke(_context.ReadValue<float>());
        }
    }

    public void OnChangeWeapon(InputAction.CallbackContext _context)
    {
        if (changeWeaponEvent != null)
        {
            changeWeaponEvent.Invoke(_context.ReadValue<float>());
        }
    }
}
