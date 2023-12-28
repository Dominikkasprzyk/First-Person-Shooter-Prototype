using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationManager : MonoBehaviour
{
    [Min(0)] [SerializeField] private float _fullBlendTime = 0f;
    
    [Header("References")]
    [SerializeField] private InputManager _inputManager;

    private Animator _animator;
    private Coroutine _verticalCoroutine, _horizontalCoroutine;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _inputManager.moveEvent += OnMove;
        _inputManager.jumpEvent += OnJump;
    }

    private void OnDisable()
    {
        _inputManager.moveEvent -= OnMove;
        _inputManager.jumpEvent -= OnJump;
    }

    private void OnMove(Vector2 _movementInput)
    {
        _movementInput = new Vector2((float)Math.Round(_movementInput.x), (float)Math.Round(_movementInput.y));
        if (_verticalCoroutine != null)
        {
            StopCoroutine(_verticalCoroutine);
        }
        _verticalCoroutine = StartCoroutine(SmoothAnimationTransitionRoutine("Vertical", _movementInput.y));

        if (_movementInput.y >= 0)
        {
            if (_horizontalCoroutine != null)
            {
                StopCoroutine(_horizontalCoroutine);
            }
            _horizontalCoroutine = StartCoroutine(SmoothAnimationTransitionRoutine("Horizontal", _movementInput.x));
        }
        else
        {
            if (_horizontalCoroutine != null)
            {
                StopCoroutine(_horizontalCoroutine);
            }
            _horizontalCoroutine = StartCoroutine(SmoothAnimationTransitionRoutine("Horizontal", 0));
        }
    }

    private void OnJump()
    {
        _animator.SetBool("isJumping", true);
        StartCoroutine(WaitForJumpEnd());
    }

    private IEnumerator WaitForJumpEnd()
    {
        float previousY;
        float currentY;
        do
        {
            previousY = transform.position.y;
            yield return new WaitForEndOfFrame();
            currentY = transform.position.y;

        } while (previousY != currentY);
        _animator.SetBool("isJumping", false);
    }

    private IEnumerator SmoothAnimationTransitionRoutine(string variableName, float finalValue)
    {
        float startingValue = _animator.GetFloat(variableName);
        float currentValue = startingValue;
        float timeBetweenUpdates = .01f;
        float currentBlendTime = _fullBlendTime * Math.Abs(finalValue - startingValue);
        float change = currentBlendTime > timeBetweenUpdates ? ((finalValue - startingValue) * timeBetweenUpdates) / currentBlendTime : (finalValue - startingValue);

        while ((change > 0 && currentValue < finalValue) || (change < 0 && currentValue > finalValue))
        {
            yield return new WaitForSeconds(timeBetweenUpdates);
            currentValue += change;
            if(change > 0)
            {
                currentValue = currentValue > finalValue ? finalValue : currentValue;  
            } else
            {
                currentValue = currentValue < finalValue ? finalValue : currentValue;
            }
            _animator.SetFloat(variableName, currentValue, .1f, .1f);
        }
    }
}
