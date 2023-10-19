using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement parameters")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float gravitationalPull = 100f;
    [SerializeField] private float turnSensitivity = 10f;
    [SerializeField] private float lookUpDownSensitivity = 0.5f;
    [SerializeField] private float maxLookUpDownRotation = 85f;
    [SerializeField] private LayerMask groundMask;
    
    [Header("References")]
    [SerializeField] private InputManager inputManager;

    private Transform playerCameraTransform;
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
        playerCameraTransform = Camera.main.transform;
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
        lookUpDownRotation = Mathf.Clamp(lookUpDownRotation, -maxLookUpDownRotation, maxLookUpDownRotation);
        Vector3 cameraRotation = new Vector3(lookUpDownRotation, transform.eulerAngles.y, transform.eulerAngles.z);
        playerCameraTransform.eulerAngles = cameraRotation;

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
        characterController.Move((transform.right * horizontalVelocity.x 
            + transform.forward * horizontalVelocity.y) 
            * speed * Time.deltaTime);  
    }
}
