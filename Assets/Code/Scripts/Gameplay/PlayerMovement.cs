using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement parameters")]
    [SerializeField] private float _forwardSpeed = 10f;
    [SerializeField] private float _backwardSpeed = 5f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float gravitationalPull = 100f;
    [SerializeField] private float turnSensitivity = 10f;
    [SerializeField] private float lookUpDownSensitivity = 0.5f;
    [SerializeField] private float maxLookUpRotation = 90f;
    [SerializeField] private float maxLookDownRotation = 50f;
    [SerializeField] private LayerMask groundMask;
    
    [Header("References")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform upperBodyRotatorTransform;

    private CharacterController characterController;
    private Vector2 horizontalVelocity = Vector2.zero;
    private Vector3 verticalVelocity = Vector3.zero;
    private bool jump, isGrounded;
    private float turnAmount, lookUpDownAmount, lookUpDownRotation = 0f;

    private void OnEnable()
    {
        inputManager.moveEvent += OnMove;
        inputManager.turnEvent += OnTurn;
        inputManager.lookUpDownEvent += OnLookUpDown;
        inputManager.jumpEvent += OnJump;

    }

    private void OnDisable()
    {
        inputManager.moveEvent -= OnMove;
        inputManager.turnEvent -= OnTurn;
        inputManager.lookUpDownEvent -= OnLookUpDown;
        inputManager.jumpEvent -= OnJump;
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnMove(Vector2 _movementInput)
    {
        horizontalVelocity = _movementInput;
    }

    private void OnJump()
    {
        jump = true;
    }

    private void OnTurn(float _turnInputValue)
    {
        turnAmount = _turnInputValue * turnSensitivity;
    }

    private void OnLookUpDown(float _lookUpDownInputValue)
    {
        lookUpDownAmount = _lookUpDownInputValue * lookUpDownSensitivity;
    }

    private void Update()
    {
        //Turn left/right
        transform.Rotate(Vector3.up, turnAmount * Time.deltaTime);

        // Look up/down
        lookUpDownRotation -= lookUpDownAmount;
        lookUpDownRotation = Mathf.Clamp(lookUpDownRotation, -maxLookUpRotation, maxLookDownRotation);
        Vector3 upperBodyRotation = new Vector3(lookUpDownRotation, transform.eulerAngles.y, transform.eulerAngles.z);
        upperBodyRotatorTransform.eulerAngles = upperBodyRotation;

        // Vertical movement
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
        if (isGrounded)
        {
            verticalVelocity.y = 0;
            if (jump)
            {
                verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * -gravitationalPull);
            }
        }
        jump = false;
        verticalVelocity.y -= gravitationalPull * Time.deltaTime;
        characterController.Move(verticalVelocity * Time.deltaTime);

        // Horizontal movement
        float speed = horizontalVelocity.y > 0 ? _forwardSpeed : _backwardSpeed;
        characterController.Move((transform.right * horizontalVelocity.x 
            + transform.forward * horizontalVelocity.y) 
            * speed * Time.deltaTime);  
    }
}
