using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, Input.IPlayerActions
{
    private Input input;
    private Rigidbody2D rb;
    
    [Header("Настройки движения")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float verticalBoundary = 4.5f;
    
    private Vector2 movementInput;
    private bool isInputEnabled = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        InitializeInputSystem();
    }

    private void InitializeInputSystem()
    {
        if (input == null)
        {
            input = new Input();
            input.Player.SetCallbacks(this);
            isInputEnabled = true;
        }
    }

    private void OnEnable()
    {
        if (input != null && isInputEnabled)
        {
            input.Player.Enable();
        }
        else
        {
            InitializeInputSystem();
            input.Player.Enable();
        }
    }

    private void OnDisable()
    {
        if (input != null)
        {
            input.Player.Disable();
        }
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(0, movementInput.y) * moveSpeed;
        rb.linearVelocity = movement;
        
        float clampedY = Mathf.Clamp(transform.position.y, -verticalBoundary, verticalBoundary);
        transform.position = new Vector2(transform.position.x, clampedY);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context) { }
    public void OnFire(InputAction.CallbackContext context) { }
    public void OnInteract(InputAction.CallbackContext context) { }
    public void OnCrouch(InputAction.CallbackContext context) { }
    public void OnJump(InputAction.CallbackContext context) { }
    public void OnPrevious(InputAction.CallbackContext context) { }
    public void OnNext(InputAction.CallbackContext context) { }
    public void OnSprint(InputAction.CallbackContext context) { }
}