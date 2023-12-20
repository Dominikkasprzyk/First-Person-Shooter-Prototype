using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputManager _inputManager;
    private Animator _animator;

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
        _animator.SetFloat("Vertical", Mathf.Round(_movementInput.y));
        if (_movementInput.y >= 0)
        {
            _animator.SetFloat("Horizontal", Mathf.Round(_movementInput.x));
        }
    }

    private void OnJump()
    {
        //_animator.SetBool("isJumping", true);
    }
}
