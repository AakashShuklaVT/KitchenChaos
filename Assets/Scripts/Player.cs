using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    // Singleton pattern to ensure only one player instance
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 7f; // Movement speed
    [SerializeField] GameInput gameInput; // Input handler reference
    [SerializeField] LayerMask countersLayerMask; // Layer for interactable counters
    [SerializeField] Transform kitchenObjectHoldPoint;

    private KitchenObject kitchenObject;
    private Vector3 lastInteractionDirection; // Last movement direction for interactions
    private BaseCounter selectedCounter; // Currently focused counter
    private bool isWalking; // Movement state flag

    // Event for UI/visuals when selected counter changes
    public event EventHandler<OnSelectedCounterChangedArgs> OnSelectedCounterChanged;
    public event EventHandler OnPickedSomething;
    public class OnSelectedCounterChangedArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    private void Awake()
    {
        // Singleton initialization
        if (Instance != null)
            Debug.LogError("Multiple Player instances detected!");
        Instance = this;
    }

    private void Start()
    {
        // Subscribe to interact input event
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    // Trigger interaction with selected counter
    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;

        // Collision check parameters
        float playerHeight = 2f;
        float playerRadius = 0.7f;

        // Check if movement is possible
        bool canMove = !Physics.CapsuleCast(
            transform.position, 
            transform.position + Vector3.up * playerHeight, 
            playerRadius, 
            moveDirection, 
            moveDistance
        );

        // Handle diagonal collision sliding
        if (!canMove)
        {
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance); // Simplified for brevity

            if (canMove) moveDirection = moveDirectionX;
            else
            {
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance); // Simplified
                if (canMove) moveDirection = moveDirectionZ;
            }
        }

        // Apply movement
        if (canMove)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

        // Update state and rotation
        isWalking = moveDirection != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, 10f * Time.deltaTime);
    }

    private void HandleInteraction()
    {
        float interactDistance = 2f;
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        // Update interaction direction
        if (moveDirection != Vector3.zero)
        {
            lastInteractionDirection = moveDirection;
        }

        // Raycast to detect counters
        if (Physics.Raycast(
            transform.position, 
            lastInteractionDirection, 
            out RaycastHit hit, 
            interactDistance, 
            countersLayerMask))
        {
            if (hit.transform.TryGetComponent(out BaseCounter counter))
            {
                if (counter != selectedCounter)
                {
                    SetSelectedCounter(counter);
                }
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    // Update selected counter and fire event
    private void SetSelectedCounter(BaseCounter counter)
    {
        selectedCounter = counter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public bool IsWalking() => isWalking;

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}